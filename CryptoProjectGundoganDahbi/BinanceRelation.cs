using Binance.Net;
using Binance.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoProjectGundoganDahbi
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
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(api_key, api_secret)
            });
            _client = client;
        }

        public Dictionary<String, decimal> BuildDictionary()
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
        public List<decimal> UpdatePriceOfSymbol(string symbol)
        {
            var callforDailyHigh = _client.Spot.Market.GetTickerAsync(symbol, default);
            if (callforDailyHigh.Result.Success)
            {
                var dataDailyHigh = callforDailyHigh.Result.Data;

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

            var result = _client.Spot.Order.PlaceTestOrderAsync(symbol, buyOrSell, Binance.Net.Enums.OrderType.Market, quantity);
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
    }
}
