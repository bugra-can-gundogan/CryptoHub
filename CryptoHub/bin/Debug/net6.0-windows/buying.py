from binance import Client
import pandas as pd
import ta
import numpy as np
import time
import sys

api_key = sys.argv[1]
api_secret = sys.argv[2]
qunatityAllowed = float(sys.argv[3])
qunatityAllowed = float(round(qunatityAllowed,8))
symbolSpecified = sys.argv[4]
buy_price = float(sys.argv[5])

client = Client(api_key,api_secret)

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


def applytechnicals(df):
    df['%K'] = ta.momentum.stoch(df.High, df.Low, df.Close, window = 14,
                                 smooth_window=3)
    df['%D'] = df['%K'].rolling(3).mean()
    df['rsi'] = ta.momentum.rsi(df.Close, window = 14)
    df['macd'] = ta.trend.macd_diff(df.Close)
    df.dropna(inplace=True)

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

def strategy(pair, qty):
    df = getminutedata(pair, '1m', '100')
    applytechnicals(df)
    inst = Signals(df,5)
    inst.decide()
    print(f'INFO:current Close is ' + str(df.Close.iloc[-1]))
    if df.Buy.iloc[-1]:
        #qty for both
        # # #
        order = client.create_order(symbol = pair,
                                    side = 'BUY',
                                    type = 'MARKET',
                                    quantity = qty)
        print("FULLORDER:"+order)
        buyprice = float(order['fills'][0]['price'])
        print(str(buyprice))

try:
    strategy(symbolSpecified, qunatityAllowed)
except Exception as e:
    print("ERROR:" + str(e))