using Binance.Net;
using Binance.Net.Objects;
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
using System.Windows.Shapes;

namespace CryptoHub
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        //Login button BORDER background colour change.
        private void borderOfLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            borderOfLogin.Background = new BrushConverter().ConvertFromString("#0000ffff") as Brush;
        }

        //Login button BORDER background colour change.
        private void borderOfLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            borderOfLogin.Background= new BrushConverter().ConvertFromString("#FFFFFF") as Brush;
        }

        //Login button background colour change.
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            loginButton.Background = new BrushConverter().ConvertFromString("#0000ffff") as Brush;
        }

        //Login button click event.
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            //UI adjustments, so that login isn't clicked twice.
            loginButton.IsEnabled = false;
            api_key_pb.IsEnabled = false;
            api_scret_pb.IsEnabled = false;

            //getting the api_key and secret
            string api_key_entered = api_key_pb.Password;
            string api_secret_entered = api_scret_pb.Password;

            //check if any of them are empty or shorter than 10 chars.
            if(api_key_entered.Length < 10 || api_secret_entered.Length < 10)
            {
                MessageBox.Show("Please enter your API information before continuing.");
                loginButton.IsEnabled = true;
                api_key_pb.IsEnabled = true;
                api_scret_pb.IsEnabled = true;
                return;
            }

            //call for task which controls if the api is correct in the background
            var checkIfEligible = IsApiCorrect(api_key_entered, api_secret_entered).GetAwaiter();
            checkIfEligible.OnCompleted(() =>
            {
                //when task is completed, we open mainwindow if true returned, warn the user if false returned.
                bool res = checkIfEligible.GetResult();
                if (res)
                {
                    CryptoBotController.original_Api_Key = api_key_entered;
                    CryptoBotController.original_Api_Secret = api_secret_entered;
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The api key and secret you entered is not correct.");
                    loginButton.IsEnabled = true;
                    api_key_pb.IsEnabled = true;
                    api_scret_pb.IsEnabled = true;
                    return;
                }
            });

        }

        //Login button sending a random call to the Binance
        //To see if the api_key and api_secret actually working.
        private async Task<bool> IsApiCorrect(string api_key, string api_secret)
        {
            bool correct = false;

            //Building the client
            BinanceClient client = new BinanceClient(new BinanceClientOptions()
            {
                // Specify options for the client
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(api_key, api_secret)
            });

            //TestCall to get the accountstatus
            var testcall = await client.General.GetAccountStatusAsync();

            if (testcall.Success)
            {
                //If it is true then the task will return true
                string x = testcall.ResponseStatusCode.ToString();
                correct = true;
                return correct;
            }
            else
            {
                //If it is false then the task will return false
                string x = testcall.ResponseStatusCode.ToString();
                return false;
            }

            return true;
        }

        //Quiting the application.
        private void CloseTheApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
