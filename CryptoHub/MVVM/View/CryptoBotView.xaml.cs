using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                datagridBindedCollection = ((MainWindow)Application.Current.MainWindow).datagridBindedCollection;
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

        private void Button_Click(object sender, RoutedEventArgs e) // Run the crypto bot
        {
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

            string api_key = "";
            string api_secret = "";

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

            ((MainWindow)Application.Current.MainWindow).parity = parityStr;
            ((MainWindow)Application.Current.MainWindow).api_key = api_key;
            ((MainWindow)Application.Current.MainWindow).api_secret = api_secret;
            ((MainWindow)Application.Current.MainWindow).quantity = quantityWeAllow;
            ((MainWindow)Application.Current.MainWindow).timerstart(datagridBindedCollection);
            runBotBtn.IsEnabled = false;
            stopBotBtn.IsEnabled = true;
        }

        private void stopBotBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).timerstop();
            runBotBtn.IsEnabled = false;
            stopBotBtn.IsEnabled = true;
        }
    }
}
