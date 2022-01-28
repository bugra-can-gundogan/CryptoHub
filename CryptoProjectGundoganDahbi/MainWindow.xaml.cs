using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace CryptoProjectGundoganDahbi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String selectedCoin = "";
        BinanceRelation _bRelation;
        Dictionary<String, decimal> _coinsDictionary = new Dictionary<String, decimal>();
        public MainWindow()
        {
            InitializeComponent();
            CultureInfo USCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = USCulture;

            string api_key = "";
            string api_secret = "";

            _bRelation = new BinanceRelation(api_key, api_secret);
            _coinsDictionary = _bRelation.BuildDictionary();
        }

        private void m_Search_Box_GotFocus(object sender, RoutedEventArgs e)
        {
            m_Search_Box.Text = "";
        }

        private void m_Search_Box_KeyDown(object sender, KeyEventArgs e)
        {
            TotalTextBoxMarket.Text = "";
            AmountTextBoxMarket.Text = "";
            PriceTextBoxMarket.Text = "";
            if (e.Key == Key.Enter)
            {
                if (_coinsDictionary.ContainsKey(m_Search_Box.Text))
                {
                    updateUILabelsForSelectedCoin(m_Search_Box.Text);
                    selectedCoin = m_Search_Box.Text;
                }
                else
                {
                    MessageBox.Show("We could not find this parity... " + m_Search_Box.Text);
                }
            }
        }

        private void m_Search_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            m_Search_Box.Text = "Search for parity...";
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9\.]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TotalTextBoxMarket_TextChanged(object sender, TextChangedEventArgs e)
        {
            string totalStr = TotalTextBoxMarket.Text;
            decimal total = 0;
            decimal amount = 0;
            bool isNumeric = decimal.TryParse(totalStr, out total);
            if (isNumeric && selectedCoin != "")
            {
                decimal price;
                bool isPriceDecimal = decimal.TryParse(PriceTextBoxMarket.Text, out price);
                if (isPriceDecimal)
                {
                    amount = total / price;
                    AmountTextBoxMarket.Text = amount.ToString();
                }
            }
        }

        private void MarketBUYBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCoin == "")
            {
                MessageBox.Show("Please select a coin first.");
                return;
            }
            decimal num = 0;
            if (!decimal.TryParse((TotalTextBoxMarket.Text), out num))
            {
                MessageBox.Show("Please enter a Total bigger than 15$.");
                return;
            }

            if (num < 15)
            {
                MessageBox.Show("Please enter a Total bigger than 15$.");
                return;
            }

            decimal amount = 0;
            string symbol = "";

            symbol = selectedCoin;
            amount = Convert.ToDecimal(AmountTextBoxMarket.Text);

            string z = _bRelation.BuyOrSellWithMarketOrLimit(symbol, Binance.Net.Enums.OrderSide.Buy, amount);
            MessageBox.Show(z);
        }

        private void MarketSELLBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCoin == "")
            {
                MessageBox.Show("Please select a coin first.");
                return;
            }
            decimal num = 0;
            if (!decimal.TryParse((TotalTextBoxMarket.Text), out num))
            {
                MessageBox.Show("Please enter a Total bigger than 15$.");
                return;
            }

            if (num < 15)
            {
                MessageBox.Show("Please enter a Total bigger than 15$.");
                return;
            }

            decimal amount = 0;
            string symbol = "";

            symbol = selectedCoin;
            amount = Convert.ToDecimal(AmountTextBoxMarket.Text);


            string z = _bRelation.BuyOrSellWithMarketOrLimit(symbol, Binance.Net.Enums.OrderSide.Sell, amount);
            MessageBox.Show(z.ToString());
        }

        private void updateUILabelsForSelectedCoin(string symbol)
        {
            List<decimal> coinInfos = _bRelation.UpdatePriceOfSymbol(symbol);
            CurrentPriceLabel.Content = symbol + "(%" + coinInfos[1] + ")" + ": " + coinInfos[0];
            dHighLabel.Content = "Daily High: $" + coinInfos[4];
            dLowLabel.Content = "Daily Low: $" + coinInfos[3];
        }
    }
}
