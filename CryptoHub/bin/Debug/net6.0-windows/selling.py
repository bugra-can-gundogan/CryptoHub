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
qunatityAllowed = float(round(qunatityAllowed,8))
symbolSpecified = sys.argv[4]
buy_price = float(sys.argv[5])
#building the client
client = Client(api_key,api_secret)

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
    df = getminutedata(pair, '1m', '2')
    print(f'INFO:current Close' + str(df.Close.iloc[-1]))
    print(f'INFO:current Target' + str(buy_price * 1.005))
    print(f'INFO:current Stop is' + str(buy_price * 0.995))
    if df.Close[-1] <= buy_price * 0.995 or df.Close[-1] >= 1.005 * buy_price:
        order = client.create_order(symbol=pair,
                                    side='SELL',
                                    type='MARKET',
                                    quantity=qty)
        print("FULLORDER:" + order)

#running the strategy function and outputting the result or the error
try:
    strategy(symbolSpecified, qunatityAllowed)
except Exception as e:
    print("ERROR:" + str(e))
