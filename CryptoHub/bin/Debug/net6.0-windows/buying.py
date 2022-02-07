from binance import Client
import pandas as pd
import ta
import numpy as np
import time
import sys

# getting the arguments from c# file
api_key = sys.argv[1]
api_secret = sys.argv[2]
qunatityAllowed = float(sys.argv[3])
symbolSpecified = sys.argv[4]
buy_price = float(sys.argv[5])

#building the client
client = Client(api_key,api_secret)

#change for lot_size
info = client.get_symbol_info(symbolSpecified)
step_size_allowed_by_binance = info['filters'][2]['stepSize']
howManyDecimals = 0
after_dp = step_size_allowed_by_binance.split('.')[1]
howManyDecimals = len(after_dp)

qunatityAllowed = round(qunatityAllowed,howManyDecimals)

#function that fetches prices throughout the last minute
def getminutedata(symbol, interval, lookback):
    frame = pd.DataFrame(client.get_historical_klines(symbol,
                                                      interval,
                                                      lookback + ' min ago UTC'))
    frame = frame.iloc[:,:6]
    frame.columns = ['Time', 'Open', 'High', 'Low', 'Close', 'Volume']
    frame = frame.set_index('Time')
    frame.index = pd.to_datetime(frame.index, unit='ms')
    frame = frame.astype(float)
    return frame

#function that applies analysis using ta library
def applytechnicals(df):
    df['%K'] = ta.momentum.stoch(df.High, df.Low, df.Close, window = 14,
                                 smooth_window=3)
    df['%D'] = df['%K'].rolling(3).mean()
    df['rsi'] = ta.momentum.rsi(df.Close, window = 14)
    df['macd'] = ta.trend.macd_diff(df.Close)
    df.dropna(inplace=True)

# class that decides whether we should by or not
class Signals:

    def __init__(self, df, lags):
        self.df = df
        self.lags = lags

    def gettrigger(self):
        dfx = pd.DataFrame()
        for i in range(self.lags + 1):
            mask = (self.df['%K'].shift(i)< 20) & (self.df['%D'].shift(i)< 20)
            dfx = dfx.append(mask, ignore_index = True)
        return dfx.sum(axis=0)

    def decide(self):
        self.df['trigger'] = np.where(self.gettrigger(), 1, 0)
        self.df['Buy'] = np.where((self.df.trigger) &
    (self.df['%K'].between(20,80)) & (self.df['%D'].between(20,80))
                                  & (self.df.rsi>50) & (self.df.macd>0), 1, 0)

#main function that uses all above functions and signals class
#and outputs to the console the activity
#this output is read by the wpf program and printed for the user
def strategy(pair, qty):
    df = getminutedata(pair, '1m', '100')
    applytechnicals(df)
    inst = Signals(df,5)
    inst.decide()
    print(f'INFO:current Close is ' + str(df.Close.iloc[-1]))
    if df.Buy.iloc[-1]:
        order = client.create_order(symbol = pair,
                                    side = 'BUY',
                                    type = 'MARKET',
                                    quantity = qty)
        print("FULLORDER:"+order)
        buyprice = float(order['fills'][0]['price'])
        print(str(buyprice))

#running the strategy function and outputting the result or the error
try:
    strategy(symbolSpecified, qunatityAllowed)
except Exception as e:
    print("ERROR:" + str(e))