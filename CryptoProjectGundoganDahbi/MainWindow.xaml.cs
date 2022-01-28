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

namespace CryptoProjectGundoganDahbi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String selectedCoin = "";
        public MainWindow()
        {
            InitializeComponent();
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
                //NOT IMPLEMENTED - Check if coin exists in the market
                //NOT IMPLEMENTED - UpdateTheChartForSelectedCoin
                //NOT IMPLEMENTED - UpdateTheLabelsForSelectedCoin
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

            //NOT IMPLEMENTED buy with binance class and get the output as a string
            //NOT IMPLEMENTED show the output to the user
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


            //NOT IMPLEMENTED SELL with binance class and get the output as a string
            //NOT IMPLEMENTED show the output to the user
        }
    }
}
