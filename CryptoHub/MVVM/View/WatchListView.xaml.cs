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
        public DatabaseRelationship dr;
        public List<Tuple<int, string, DateTime, decimal>> watchListCoinsList = new List<Tuple<int, string, DateTime, decimal>>();
        public WatchListView()
        {
            InitializeComponent();
            dr = new DatabaseRelationship();
            watchListCoinsList = dr.ReadWatchListTable();
            McDataGrid.ItemsSource = LoadCollectionData();
        }

        public class WatchedCoin
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime DateOfAddition { get; set; }
            public decimal PriceAtTimeofAddition { get; set; }
        }
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

        private void WatchListRefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshWtcDg();
        }

        private void RefreshWtcDg()
        {
            McDataGrid.ItemsSource = null;
            watchListCoinsList = dr.ReadWatchListTable();
            McDataGrid.ItemsSource = LoadCollectionData();
        }
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
