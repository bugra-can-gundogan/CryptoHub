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
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;

namespace CryptoHub.MVVM.View
{
    /// <summary>
    /// Interaction logic for MarketView.xaml
    /// </summary>
    public partial class MarketView : UserControl
    {

        BinanceRelation _bRelation; //BinanceRelation class
        SQLiteRelation _dRelation; //SQLiteRelation class
        String selectedCoin = ""; //string representing the coin selected by the user.

        //Three variables that are used by the graph.
        //SeriesCollection is the collection that has all the values
        //Labels is a string array that holds every label
        //YFormatter adjusts the measure of the graph.
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public MarketView()
        {
            InitializeComponent();

            //Setting the culture as US so that graph will show $ instead of TRY
            CultureInfo USCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = USCulture;
            
            //getting the api credentials from CryptoBotController class
            string api_key = CryptoBotController.original_Api_Key;
            string api_secret = CryptoBotController.original_Api_Secret;

            //creating an instance of both Binance Relation class and Database Relation class.
            _bRelation = new BinanceRelation(api_key, api_secret);
            _dRelation = new SQLiteRelation();

            //creating a dispatchertimer that will run every three seconds
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();

            //calling the function that builds the base chart.
            buildBaseChart();
        }

        //Function that is activated every three seconds.
        private void timer_Tick(object? sender, EventArgs e)
        {
            //if a coin is selected we update the labels, textboxes and graphs according to the...
            //...data we get from Binance.COM
            if(selectedCoin != "")
            {
                //Running the task that returns the current price of the coin
                var callforinfos = _bRelation.UpdatePriceOfSymbol(selectedCoin).GetAwaiter();
                callforinfos.OnCompleted(() =>
                {
                    //once the task is completed the result is held in a list than different labels are changed according to the result.
                    List<decimal> coinInfos = callforinfos.GetResult();
                    CurrentPriceLabel.Content = selectedCoin + "(" + coinInfos[1] + ")" + ": " + coinInfos[0];
                    dHighLabel.Content = "Daily High: $" + coinInfos[4];
                    dLowLabel.Content = "Daily Low: $" + coinInfos[3];
                    PriceTextBoxMarket.Text = coinInfos[0].ToString();

                    //setting the measure of graph according to the price.
                    if (coinInfos[0] > 1000)
                    {
                        YFormatter = value => value.ToString("C");
                    }
                    else if (coinInfos[0] > 10)
                    {
                        YFormatter = value => value.ToString("C2");
                    }
                    else
                    {
                        YFormatter = value => value.ToString("C6");
                    }

                });                
          }
        }


        private void m_Search_Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        //method that builds the base chart
        private void buildBaseChart()
        {
            //assigning the series collection to an empty one
            SeriesCollection = new SeriesCollection{};

            //creating an empty list to hold dates
            var dates = new List<String>();

            //we add each date to the date list
            for (var dt = DateTime.Now.AddMonths(-1); dt <= DateTime.Now; dt = dt.AddDays(1))
            {
                dates.Add(dt.ToShortDateString());
            }

            //assigning the labels variable to the dates list we created.
            //since labels is an array we have to convert the list to an array as well
            Labels = dates.ToArray();

            //setting the yformatter
            YFormatter = value => value.ToString("C5");

            //modifying the series collection will animate and update the chart
            //we add a new LineSeries to the SeriesCollection which will be the prices
            SeriesCollection.Add(new LineSeries
            {
                Title = "Parity",
                Values = new ChartValues<double> {},
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry = DefaultGeometries.Diamond,
                PointGeometrySize = 15,
                PointForeground = Brushes.Gray
            });

            //modifying any series values will also animate and update the chart
            //SeriesCollection[3].Values.Add(5d);

            DataContext = this;
        }

        //method that updates the graph for the selected coin
        private void updateChartForSelectedCoin(string symbol) 
        {
            //calling the task that returns the price history of a given coin
            var callforPriceHistory = _bRelation.PriceHistoryOfParity(symbol).GetAwaiter();
            callforPriceHistory.OnCompleted(() =>
            {
                //once it is completed we get its result and hold it in a dictionary
                Dictionary<String, decimal> coinPriceHistory = callforPriceHistory.GetResult();
                //Cleaning the LineSeries's Values(aka prices)
                SeriesCollection[0].Values.Clear();
                //We add each price to the LineSeries
                foreach (var price in coinPriceHistory.Values)
                {
                    SeriesCollection[0].Values.Add(Convert.ToDouble(price));
                };
            });   
        }

        //method that updates UI labels when a new coin is selected.
        private void updateUILabelsForSelectedCoin(string symbol)
        {
            //calling the task that returns information about daily and current price of a coin
            var callforInfos = _bRelation.UpdatePriceOfSymbol(symbol).GetAwaiter();
            callforInfos.OnCompleted(() =>
            {
                //Once completed we get the result, set the labels according to the result
                List<decimal> coinInfos = callforInfos.GetResult();
                CurrentPriceLabel.Content = symbol + "(%" + coinInfos[1] + ")" + ": " + coinInfos[0];
                dHighLabel.Content = "Daily High: $" + coinInfos[4];
                dLowLabel.Content = "Daily Low: $" + coinInfos[3];

                //set the YFormatter to change the measure of the graph according to the price
                if (coinInfos[0] > 1000)
                {
                    YFormatter = value => value.ToString("C");
                }
                else if (coinInfos[0] > 10)
                {
                    YFormatter = value => value.ToString("C2");
                }
                else
                {
                    YFormatter = value => value.ToString("C6");
                }
            });
        }

        //Whenever user presses any key while writing to the searchbox this event is called
        private void m_Search_Box_KeyDown(object sender, KeyEventArgs e)
        {
            // resetting textboxes
            TotalTextBoxMarket.Text = "";
            AmountTextBoxMarket.Text = "";
            PriceTextBoxMarket.Text = "";

            //Search event, called when user presses enter when they are writing on the search box
            if (e.Key == Key.Enter)
            {
                //check if the coin is inside the market
                if (CryptoBotController.coinsInTheMarket.ContainsKey(m_Search_Box.Text))
                {
                    //call the method to update the chart for the new coin
                    updateChartForSelectedCoin(m_Search_Box.Text);
                    //call the method to update labels for the new coin
                    updateUILabelsForSelectedCoin(m_Search_Box.Text);
                    //set the selectedCoin as the new coin
                    selectedCoin = m_Search_Box.Text;
                }
                else
                {
                    //inform user that this coin doesn't exist in Binance.COM
                    MessageBox.Show("We could not find this parity... " + m_Search_Box.Text);
                }
            }
        }
        
        //getting rid of the placeholder once user starts writing to the searchbox
        private void m_Search_Box_GotFocus(object sender, RoutedEventArgs e)
        {
            m_Search_Box.Text = "";
        }

        //resetting the search box to default state
        private void m_Search_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            m_Search_Box.Text = "Search for parity...";
        }

        //checking the input inside the TotalTextBox, so that user can only enter decimals (10, 10.3, 1.234)
        //values that can actually be paid instead of some random text like ("adsfasdf", or (1.234.432423.4324)
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9\.]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        //When the total text box value is changed this event is called
        //This event sets the amount textbox according to the TOTAL entered by the user
        private void TotalTextBoxMarket_TextChanged(object sender, TextChangedEventArgs e)
        {
            string totalStr = TotalTextBoxMarket.Text;
            decimal total = 0;
            decimal amount = 0;
            bool isNumeric = decimal.TryParse(totalStr, out total);
            if (isNumeric && selectedCoin != "")
            {
                decimal price; 
                bool isPriceDecimal = decimal.TryParse(PriceTextBoxMarket.Text,out price);
                if (isPriceDecimal)
                {
                    amount = total / price;
                    AmountTextBoxMarket.Text = amount.ToString();
                }
            }
        }

        //BUY event
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

            if(PriceTextBoxMarket.Text == "")
            {
                MessageBox.Show("Please wait while price is being fetched.");
                return;
            }

            decimal amount = 0;
            string symbol = "";

            symbol = selectedCoin;
            amount = Convert.ToDecimal(AmountTextBoxMarket.Text);
            amount = Math.Round(amount, 8);

            string z = _bRelation.BuyOrSellWithMarketOrLimit(symbol, Binance.Net.Enums.OrderSide.Buy, amount);
            MessageBox.Show(z);
            var callForCoinsOwnedByUser = _bRelation.getWalletDataTask().GetAwaiter();
            callForCoinsOwnedByUser.OnCompleted(() =>
            {
                CryptoBotController.ownedCoinsByUser = callForCoinsOwnedByUser.GetResult();
            });

        }

        //SELL event
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

            if (PriceTextBoxMarket.Text == "")
            {
                MessageBox.Show("Please wait while price is being fetched.");
                return;
            }

            decimal amount = 0;
            string symbol = "";

            symbol = selectedCoin;
            amount = Convert.ToDecimal(AmountTextBoxMarket.Text);
            amount = Math.Round(amount, 8);

            string z = _bRelation.BuyOrSellWithMarketOrLimit(symbol, Binance.Net.Enums.OrderSide.Sell, amount);
            MessageBox.Show(z.ToString());
            var callForCoinsOwnedByUser = _bRelation.getWalletDataTask().GetAwaiter();
            callForCoinsOwnedByUser.OnCompleted(() =>
            {
                CryptoBotController.ownedCoinsByUser = callForCoinsOwnedByUser.GetResult();
            });
        }

        //Event where add to watch list button is clicked
        private void Button_Click(object sender, RoutedEventArgs e) // Add to WatchList
        {
            //checking if user selected a coin first
            if(selectedCoin != "")
            {
                //creating variables to hold the values from the textboxes
                decimal currentPrice;
                DateTime now = DateTime.Now;
                string coinName;

                //assigning values to variables that will be sent to database class to add to the database
                currentPrice = Convert.ToDecimal(PriceTextBoxMarket.Text);
                coinName = selectedCoin;
                coinName = coinName.Replace("USDT", "");

                //calling the method from the database class to add this coin to the DB.
                _dRelation.AddToWatchListTable(coinName, now, currentPrice);
                MessageBox.Show(coinName + " is added to the Watch List succesfully.");
            }
            else
            {
                //Warning the user that they need to select a coin first.
                MessageBox.Show("Please select a coin first.");
            }
        }
    }
}
