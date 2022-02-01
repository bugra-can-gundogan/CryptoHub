using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CryptoHub.MVVM.View
{
    /// <summary>
    /// Interaction logic for CryptoBotView.xaml
    /// </summary>
    public partial class CryptoBotView : UserControl
    {
        public ObservableCollection<CryptoViewDataGridRowClass> datagridBindedCollection;

        public CryptoBotView()
        {
            InitializeComponent();
            if (CryptoBotController.botIsRunning)
            {
                datagridBindedCollection = ((MainWindow)Application.Current.Windows[0]).datagridBindedCollection;
                ValueAllowedByUserLBL.Text = CryptoBotController.valueLabel;
                ParityWantedToTradeLBL.Text = CryptoBotController.parityLabel;
                runBotBtn.IsEnabled = false;
                stopBotBtn.IsEnabled = true;
            }
            else
            {
                datagridBindedCollection = new ObservableCollection<CryptoViewDataGridRowClass>();
            }
            ActivityDataGrid.ItemsSource = datagridBindedCollection;
           
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9\.]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private string getPythonPath()
        {
            var path = Environment.GetEnvironmentVariable("PATH");
            string pythonPath = "";
            foreach (var p in path.Split(new char[] { ';' }))
            {
                var fullPath = System.IO.Path.Combine(p, "pythonw.exe");
                if (File.Exists(fullPath))
                {
                    pythonPath = fullPath;
                    break;
                }
            }
            return pythonPath;
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Run the crypto bot
        {
            string getMePythonPath = getPythonPath();
            if (getMePythonPath == "")
            {
                MessageBox.Show("To run the crypto bot you should first have PYTHON installed in your computer. Please install python and try again.");
                return;
            }
            else
            {
                CryptoBotController.pythonPath = getMePythonPath;
            }


            string valueStr = ValueAllowedByUserLBL.Text;
            string parityStr = ParityWantedToTradeLBL.Text;

            decimal valueInUsd;

            var parsable = decimal.TryParse(valueStr, out valueInUsd);

            if (!parsable)
            {
                MessageBox.Show("Please enter a correct value for USDT to trade with.");
                return;
            }

            if(valueInUsd < 15)
            {
                MessageBox.Show("Please enter a bigger number than 15$.");
                return;
            }

            string api_key = CryptoBotController.original_Api_Key;
            string api_secret = CryptoBotController.original_Api_Secret;

            BinanceRelation binanceRelation = new BinanceRelation(api_key, api_secret);
            Dictionary<string,decimal> coinsInMarket = binanceRelation.BuildDictionary();

            if (!coinsInMarket.ContainsKey(parityStr))
            {
                MessageBox.Show("This parity doesn't exist in Binance.");
                return;
            }
            decimal priceInMarket = coinsInMarket[parityStr];

            decimal quantityWeAllow = valueInUsd / priceInMarket;
            quantityWeAllow = quantityWeAllow * (decimal)0.90;

            CryptoBotController.parityLabel = ParityWantedToTradeLBL.Text;
            CryptoBotController.valueLabel = ValueAllowedByUserLBL.Text;

            ((MainWindow)Application.Current.Windows[0]).parity = parityStr;
            ((MainWindow)Application.Current.Windows[0]).api_key = api_key;
            ((MainWindow)Application.Current.Windows[0]).api_secret = api_secret;
            ((MainWindow)Application.Current.Windows[0]).quantity = quantityWeAllow;
            ((MainWindow)Application.Current.Windows[0]).timerstart(datagridBindedCollection);

            //((MainWindow)Application.Current.MainWindow).parity = parityStr;
            //((MainWindow)Application.Current.MainWindow).api_key = api_key;
            //((MainWindow)Application.Current.MainWindow).api_secret = api_secret;
            //((MainWindow)Application.Current.MainWindow).quantity = quantityWeAllow;
            //((MainWindow)Application.Current.MainWindow).timerstart(datagridBindedCollection);
            runBotBtn.IsEnabled = false;
            stopBotBtn.IsEnabled = true;
        }

        private void stopBotBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.Windows[0]).timerstop();
            runBotBtn.IsEnabled = true;
            stopBotBtn.IsEnabled = false;
        }
    }
}
