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
            ownedCoins = _bRelation.getWalletData();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();
            buildBaseChart();
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            if (selectedCoin != "")
            {
                List<decimal> coinInfos = _bRelation.UpdatePriceOfSymbol(selectedCoin);
                CurrentPriceLabel.Content = selectedCoin + "(" + coinInfos[1] + ")" + ": " + coinInfos[0];
                dHighLabel.Content = "Daily High: $" + coinInfos[4];
                dLowLabel.Content = "Daily Low: $" + coinInfos[3];
                PriceTextBoxMarket.Text = coinInfos[0].ToString();
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void buildBaseChart()
        {
            SeriesCollection = new SeriesCollection { };

            var dates = new List<String>();

            for (var dt = DateTime.Now.AddMonths(-1); dt <= DateTime.Now; dt = dt.AddDays(1))
            {
                dates.Add(dt.ToShortDateString());
            }

            Labels = dates.ToArray();

            YFormatter = value => value.ToString("C5");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Title = "Parity",
                Values = new ChartValues<double> { },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry = DefaultGeometries.Diamond,
                PointGeometrySize = 15,
                PointForeground = Brushes.Gray
            });

            //modifying any series values will also animate and update the chart
            //SeriesCollection[3].Values.Add(5d);

            DataContext = this;
        }

        private void updateChartForSelectedCoin(string symbol)
        {
            _coinsDictionary<string,decimal> callforPriceHistory = _bRelation.PriceHistoryOfParity(symbol);
            SeriesCollection[0].Values.Clear();
            foreach (var price in coinPriceHistory.Values)
            {
                SeriesCollection[0].Values.Add(Convert.ToDouble(price));
            };
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

        public SeriesCollection WalletSeriesCollection { get; set; }
        private void BuildWalletChart()
        {
            WalletSeriesCollection = new SeriesCollection { };

            foreach (var coin in ownedCoins)
            {
                if (marketCoins.ContainsKey(coin.Key + "USDT"))
                {
                    double totalValOfCoin = Convert.ToDouble(marketCoins[coin.Key + "USDT"] * coin.Value);
                    if (totalValOfCoin > 10)
                    {
                        WalletSeriesCollection.Add(new PieSeries
                        {
                            Title = coin.Key,
                            Values = new ChartValues<ObservableValue> { new ObservableValue(totalValOfCoin) },
                            DataLabels = true
                        });
                    }
                }
            }
            DataContext = this;
        }
        public class Coin
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }
            public decimal Total { get; set; }

        }
    }
}
