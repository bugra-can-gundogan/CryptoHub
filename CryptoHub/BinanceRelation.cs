using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Objects;

namespace CryptoHub
{
    public class BinanceRelation
    {
        public static string api_key = "";
        public static string api_secret = "";
        BinanceClient _client;

        public BinanceRelation(string apikey, string apisecret)
        {
            api_key = apikey;
            api_secret = apisecret;

            BinanceClient client = new BinanceClient(new BinanceClientOptions()
            {
                // Specify options for the client
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(api_key, api_secret),
                TradeRulesBehaviour = Binance.Net.Enums.TradeRulesBehaviour.AutoComply
            });
            _client = client;
        }

        public Dictionary<String,decimal> BuildDictionary()
        {
            var callresult = _client.Spot.Market.GetPricesAsync();
            Dictionary<String, decimal> coinDictionary = new Dictionary<String, decimal>();

            if (!callresult.Result.Success)
            {
                coinDictionary.Add("Unsuccesful Connection.", 0);
                coinDictionary.Add(callresult.Result.Error.Message, 0);
                return coinDictionary;
            }
            var rawdata = callresult.Result.Data;
            

            foreach (var symbol in rawdata)
            {
                if (symbol.Symbol.Contains("USDT"))
                {
                    coinDictionary.Add(symbol.Symbol, symbol.Price);
                }
            }
            return coinDictionary;
        }

        public async Task<Dictionary<String, decimal>> BuildDictionaryTask()
        {
            var callresult = await _client.Spot.Market.GetPricesAsync();
            Dictionary<String, decimal> coinDictionary = new Dictionary<String, decimal>();

            if (!callresult.Success)
            {
                coinDictionary.Add("Unsuccesful Connection.", 0);
                coinDictionary.Add(callresult.Error.Message, 0);
                return coinDictionary;
            }
            var rawdata = callresult.Data;

            foreach (var symbol in rawdata)
            {
                if (symbol.Symbol.Contains("USDT"))
                {
                    coinDictionary.Add(symbol.Symbol, symbol.Price);
                }
            }
            return coinDictionary;
        }

        public async Task<Dictionary<String, decimal>> PriceHistoryOfParity(string symbol)
        {
            DateTime today = DateTime.Now;
            DateTime lastmonth = DateTime.Today.AddMonths(-1);
            Dictionary<String, decimal> priceHistory = new Dictionary<String, decimal>();

            var call = await _client.Spot.Market.GetKlinesAsync(symbol, Binance.Net.Enums.KlineInterval.OneDay, lastmonth, today);
            if (call.Success)
            {
                var rawdata = call.Data;
                foreach (var coin in rawdata)
                {
                    priceHistory.Add(coin.CloseTime.ToShortDateString(), coin.Close);
                }
                return priceHistory;
            }
            else
            {
                priceHistory.Add("Error Getting the Price History", Convert.ToDecimal("0"));
            }

            return priceHistory;
        }

        public async Task<List<decimal>> UpdatePriceOfSymbol(string symbol)
        {
            var callforDailyHigh = await _client.Spot.Market.GetTickerAsync(symbol, default);
            if (callforDailyHigh.Success)
            {
                var dataDailyHigh = callforDailyHigh.Data;

                decimal dataPrice = dataDailyHigh.LastPrice;
                decimal percentChange = dataDailyHigh.PriceChangePercent;
                decimal priceChange = dataDailyHigh.PriceChange;
                decimal dlow = dataDailyHigh.LowPrice;
                decimal dhigh = dataDailyHigh.HighPrice;
                return new List<decimal> { dataPrice, percentChange, priceChange, dlow, dhigh };
            }
            else
            {
                return new List<decimal> { 0, 0, 0, 0, 0 };
            }          
        }
        
        public string BuyOrSellWithMarketOrLimit(string symbol, Binance.Net.Enums.OrderSide buyOrSell, decimal quantity)
        {

            var result = _client.Spot.Order.PlaceOrderAsync(symbol, buyOrSell, Binance.Net.Enums.OrderType.Market, quantity);
            bool x = result.Result.Success;
            if (x)
            {
                return "Your trade is completed.";
            }
            else
            {
                string errormessage = "Your trade DID NOT get completed. Binance did not accept this trade because: \n" + result.Result.Error.ToString();
                return errormessage;
            }
        }

        public async Task<Dictionary<string,decimal>> getWalletDataTask()
        {
            var callforWalletInfo = await _client.General.GetAccountInfoAsync();
            Dictionary<string, decimal> coinsOwnedDictionary = new Dictionary<string, decimal>();
            if (!callforWalletInfo.Success)
            {
                string error = callforWalletInfo.Error.Message;
                Console.WriteLine(error);
            }
            foreach(var item in callforWalletInfo.Data.Balances)
            {
                if (item.Free > 0)
                {
                    coinsOwnedDictionary.Add(item.Asset, item.Free);
                }
            }

            return coinsOwnedDictionary;
        }

        public async Task<Dictionary<string,Tuple<double,decimal,decimal>>> updateOwnedCoinsTask()
        {
            Dictionary<string, Tuple<double, decimal,decimal>> coinsWithNewPrices = new Dictionary<string, Tuple<double, decimal,decimal>>();
            var call = await _client.General.GetUserCoinsAsync();
            var data = call.Data.ToList();
            Dictionary<string, decimal> coinsOwnedDictionary = new Dictionary<string, decimal>();
            foreach (var item in data)
            {
                if (item.Free > 0)
                {
                    coinsOwnedDictionary.Add(item.Coin, item.Free);
                }
            }
            var callforprices = await _client.Spot.Market.GetPricesAsync();
            var prices = callforprices.Data.ToList();
            foreach (var price in prices)
            {
                if (price.Symbol.Contains("USDT"))
                {
                    string strUsdtRemoved = price.Symbol.Replace("USDT", "");
                    if (coinsOwnedDictionary.ContainsKey(strUsdtRemoved))
                    {
                        Tuple<double, decimal,decimal> coinNumericInfo = new Tuple<double,decimal,decimal>(Convert.ToDouble(price.Price*coinsOwnedDictionary[strUsdtRemoved]), coinsOwnedDictionary[strUsdtRemoved],price.Price);
                        coinsWithNewPrices.Add(strUsdtRemoved, coinNumericInfo);
                    }
                }
            }

            return coinsWithNewPrices;
        }
    }
}
