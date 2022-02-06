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
        //collection that is binded to the datagrid
        public ObservableCollection<CryptoViewDataGridRowClass> datagridBindedCollection;

        public CryptoBotView()
        {
            InitializeComponent();
            //boolean that checks if bot was running, if it is running we load the labels and datagrid list from static class cryptobotcontroller.cs
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
            //binding the datagrid
            ActivityDataGrid.ItemsSource = datagridBindedCollection;
           
        }

        //regex to check user's input on total value textbox
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9\.]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }
        //custom function that detects the path of 'pythonw.exe' in the user's computer
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

        //Event of cryptobot run button click
        private void Button_Click(object sender, RoutedEventArgs e) // Run the crypto bot
        {
            //checking if python is installed in user's computer
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
            //checking user input
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

            Dictionary<string,decimal> coinsInMarket = CryptoBotController.coinsInTheMarket;
            //checking if the user's wanted parity is in Binance
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
            //setting the values for mainwindow class which handles the execution of python files
            //we also access the dispatchertimer of mainwindow from here and start the execution
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
        //function that is called when stop button is clicked.
        //once this is called, we access dispatchertimer of mainwindow and stop it
        private void stopBotBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.Windows[0]).timerstop();
            runBotBtn.IsEnabled = true;
            stopBotBtn.IsEnabled = false;
        }
    }
}
