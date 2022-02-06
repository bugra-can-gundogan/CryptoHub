using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoHub
{
    //public static class to hold global variables
    //some labels in the cryptobotview.xaml are saved with this class.
    //coins in the market and ownedcoins dictionaries are saved here.
    //user credentials are saved here.
    //python path is saved here so the application doesn't have to find it everytime bot starts running.
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
