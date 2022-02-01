using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoHub
{
    public static class CryptoBotController
    {
        public static bool botIsRunning = false;
        public static string valueLabel = "";
        public static string parityLabel = "";
        public static Dictionary<string,decimal> coinsInTheMarket = new Dictionary<string,decimal>();
        public static Dictionary<string, decimal> ownedCoinsByUser = new Dictionary<string, decimal>();
        public static string original_Api_Key = "";
        public static string original_Api_Secret = "";
        public static string pythonPath = "";
    }
}
