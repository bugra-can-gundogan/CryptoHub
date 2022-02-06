using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace CryptoHub.MVVM.View
{
    /// <summary>
    /// Interaction logic for WatchListView.xaml
    /// </summary>
    public partial class WatchListView : UserControl
    {
        //class that holds all database related functions
        public SQLiteRelation dr;
        //list that holds all the watchlistcoins initiliazed here
        public List<Tuple<int, string, DateTime, decimal>> watchListCoinsList = new List<Tuple<int, string, DateTime, decimal>>();
        public WatchListView()
        {
            InitializeComponent();
            //creating an instance of SQLiteRelation class
            dr = new SQLiteRelation();
            //reading the table, we already check if table exists in mainwindow.xaml class using createtable method in sqliterelation class
            //so this won't return an exception or crash the application
            watchListCoinsList = dr.ReadWatchListTable();
            //binding the datagrid to the method that returns list of all held coins
            McDataGrid.ItemsSource = LoadCollectionData();
        }

        //class of watched coins
        public class WatchedCoin
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime DateOfAddition { get; set; }
            public decimal PriceAtTimeofAddition { get; set; }
        }
        //method that returns watched coins list
        private List<WatchedCoin> LoadCollectionData()
        {
            List<WatchedCoin> watchedCoins = new List<WatchedCoin>();
            foreach (var item in watchListCoinsList)
            {
                watchedCoins.Add(new WatchedCoin()
                {
                    ID = item.Item1,
                    Name = item.Item2,
                    DateOfAddition = item.Item3,
                    PriceAtTimeofAddition = item.Item4
                });
            }           
            return watchedCoins;
        }
        //refresh button click event
        private void WatchListRefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshWtcDg();
        }

        //button that refreshes datagrid
        private void RefreshWtcDg()
        {
            McDataGrid.ItemsSource = null;
            watchListCoinsList = dr.ReadWatchListTable();
            McDataGrid.ItemsSource = LoadCollectionData();
        }
        //removing from the watchlist
        private void WatchlistRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(McDataGrid.SelectedItem != null)
            {
                WatchedCoin wc = (WatchedCoin)McDataGrid.SelectedItem;
                int id = wc.ID;
                dr.RemoveFromWatchListTable(id);
                RefreshWtcDg();
            }
            
        }
    }
}
