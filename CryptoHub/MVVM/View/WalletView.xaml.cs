using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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

namespace CryptoHub.MVVM.View
{
    /// <summary>
    /// Interaction logic for WalletView.xaml
    /// </summary>
    /// //This view is used to show the user their owned coins
    /// //logic is very similar with marketview.xaml
    /// //piechart only shows the coins that have more value in the wallet than 15$
    /// //datagrid shows every coin
    public partial class WalletView : UserControl
    {
        BinanceRelation _bRelation;
        Dictionary<string, decimal> ownedCoins;
        Dictionary<string, decimal> marketCoins;
        public WalletView()
        {
            InitializeComponent();
            //setting the culture to US, so that it shows $ instead of try
            CultureInfo USCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = USCulture;
            //getting the api_key and secret
            string api_key = CryptoBotController.original_Api_Key;
            string api_secret = CryptoBotController.original_Api_Secret;
            //creating an instance of BinanceRelation class
            _bRelation = new BinanceRelation(api_key, api_secret);
            //getting the dictionaries that hold ownedcoins and marketcoins
            ownedCoins = CryptoBotController.ownedCoinsByUser;
            marketCoins = CryptoBotController.coinsInTheMarket;
            //updating the coins values and information etc.
            //and then we bind the datagrid to the coins list
            var taskForCoinsOwned = _bRelation.getWalletDataTask().GetAwaiter();
            taskForCoinsOwned.OnCompleted(() =>
            {
                List<Coin> coins = new List<Coin>();
                Dictionary<string, decimal> ownedCoins = taskForCoinsOwned.GetResult();
                foreach (var item in ownedCoins)
                {
                    if (marketCoins.ContainsKey(item.Key + "USDT"))
                    {
                        coins.Add(new Coin()
                        {
                            Name = item.Key,
                            Price = marketCoins[item.Key + "USDT"],
                            Quantity = item.Value,
                            Total = item.Value * marketCoins[item.Key + "USDT"]
                        });
                    }
                }
                AllCoinsDataGrid.ItemsSource = null;
                AllCoinsDataGrid.ItemsSource = coins;
            });
            //function that builds the base of pie chart
            BuildWalletChart();
            //dispatcher timer that has a function that will be called every 5 seconds to update the graph and datagrid
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        //function that updates datagrid and piechart every 5 seconds
        private void timer_Tick(object? sender, EventArgs e)
        {
            double Total = 0;
            var callforUpdatedCoins = _bRelation.updateOwnedCoinsTask().GetAwaiter();
            callforUpdatedCoins.OnCompleted(() =>
            {
                List<Coin> coins = new List<Coin>();
                Dictionary<string, Tuple<double,decimal,decimal>> updatedCoins = callforUpdatedCoins.GetResult();
                //updating the datagrid
                foreach (var item in updatedCoins)
                {
                    coins.Add(new Coin()
                    {
                        Name = item.Key,
                        Price = item.Value.Item3,
                        Quantity = item.Value.Item2,
                        Total = Convert.ToDecimal(item.Value.Item1)
                    });
                }
                AllCoinsDataGrid.ItemsSource = null;
                AllCoinsDataGrid.ItemsSource = coins;

                //updating the graph
                foreach (var series in WalletSeriesCollection)
                {
                    int X = series.Values.Cast<ObservableValue>().Count();
                    string tit = series.Title;
                    foreach (var observable in series.Values.Cast<ObservableValue>())
                    {
                        observable.Value = updatedCoins[series.Title].Item1;
                    }
                    Total += updatedCoins[series.Title].Item1;
                }

                walletTotalValLbl.Content = "Total Value($): " + Total.ToString();
            });
        }
        //series collection that is binded to the piechart
        public SeriesCollection WalletSeriesCollection { get; set; }

        //function that builds the base of pie chart
        private void BuildWalletChart()
        {
            WalletSeriesCollection = new SeriesCollection { };

            foreach (var coin in ownedCoins)
            {
                if (marketCoins.ContainsKey(coin.Key + "USDT"))
                {
                    double totalValOfCoin = Convert.ToDouble(marketCoins[coin.Key + "USDT"] * coin.Value);
                    totalValOfCoin = Math.Round(totalValOfCoin, 2);
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

        //coin class that is used in datagrid
        public class Coin
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }
            public decimal Total { get; set; }

        }
    }
}
