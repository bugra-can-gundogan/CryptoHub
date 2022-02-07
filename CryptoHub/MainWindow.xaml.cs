using CryptoHub.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;

namespace CryptoHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //This class is the mainwindow that has all the other observable views.
        //This class also handles the cryptobot python files. Gets the output from them and sends them to cryptobotview.xaml to show the user.

        //collection binded to datagrid of cryptoview.xaml
        public ObservableCollection<CryptoViewDataGridRowClass> datagridBindedCollection;
        //dispatcher timer that will handle the starting and stopping processes of cryptobot python files.
        DispatcherTimer timer = new DispatcherTimer();
        //parity that will be traded in cryptobot.
        public string parity = "";
        //api_key and secret that will be sent to python files.
        public string api_key = CryptoBotController.original_Api_Key;
        public string api_secret = CryptoBotController.original_Api_Secret;
        //quantity, prices that the bot will trade with
        public decimal quantity = 0;
        public decimal buyprice = 0;
        //if open_position we run selling.py file if not we run buying.py file
        public bool open_pos = false;
        //class that handles all the relationship with binance.com
        public BinanceRelation binanceRelation;

        public MainWindow()
        {
            InitializeComponent();
            //initiliazing the sqlite class to create a table if it doesn't exist already
            SQLiteRelation sqlRel = new SQLiteRelation();
            sqlRel.CreateTable();
            //creating an instance of BinanceRelation class
            binanceRelation = new BinanceRelation(api_key, api_secret);
            //getting all the coins in the market and saving them to the public static class cryptobotcontroller.cs
            var callforAllCoinsInTheMarket = binanceRelation.BuildDictionaryTask().GetAwaiter();
            callforAllCoinsInTheMarket.OnCompleted(() =>
            {
                CryptoBotController.coinsInTheMarket = callforAllCoinsInTheMarket.GetResult();
            });

            //getting all the coins the user owns to the public static class cryptobotcontroller.cs
            var callForCoinsOwnedByUser = binanceRelation.getWalletDataTask().GetAwaiter();
            callForCoinsOwnedByUser.OnCompleted(() =>
            {
                CryptoBotController.ownedCoinsByUser = callForCoinsOwnedByUser.GetResult();
            });
            //setting the timer interval and timer function that will be called in that interval
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += timer_Tick;
        }

        //function that starts the timer, this will be reached from cryptobotview.xaml button
        public void timerstart(ObservableCollection<CryptoViewDataGridRowClass> dglist)
        {
            CryptoBotController.botIsRunning = true;
            datagridBindedCollection = dglist;
            timer.Start();
        }
        //function that stops the timer, this will be reached from cryptobotview.xaml button
        public void timerstop()
        {
            CryptoBotController.botIsRunning = false;
            timer.Stop();
        }
        //timer_tick function calls RunPythonBot task, result of the task is sent to the cryptoviewxaml.cs' datagrid
        private void timer_Tick(object? sender, EventArgs e)
        {
            var x = RunPythonBot().GetAwaiter();
            x.OnCompleted(() =>
            {
                datagridBindedCollection.Insert(0, new CryptoViewDataGridRowClass() { Date = DateTime.Now, Messagge = x.GetResult() }
                    );
                /*datagridBindedCollection.Add(
                new CryptoViewDataGridRowClass() { Date = DateTime.Now, Messagge = x.GetResult() });*/
            });
        }

        //Task that runs buying.py and selling.py files and returns the console output from them.
        private async Task<string> RunPythonBot()
        {
            string output = "None";
            string result = string.Empty;
            //arguements that will be sent to python files
            string args = api_key + " " + api_secret + " " + quantity + " " + parity + " " + buyprice.ToString();
           

            try
            {
                //if open_pos then we run selling.py file
                //for both open_pos == true and open_pos == false we do the following:
                /*Create a new process, start pythonw.exe with the filename and arguements above
                 * get the output using process.standardoutput.readtoend() method
                 */
                if (open_pos)
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = CryptoBotController.pythonPath;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.Arguments = string.Format("{0} {1}", "Selling.Py", "") + args;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.Start();

                        await process.WaitForExitAsync();
                        if (process.ExitCode == 0)
                        {
                            result = process.StandardOutput.ReadToEnd();
                            string[] output_all = result.Split("\n");
                            foreach (var line in output_all)
                            {
                                if (line.Contains("ORDER"))
                                {
                                    open_pos = false;
                                }
                            }
                        }

                        return result;
                    }
                }
                else
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = CryptoBotController.pythonPath;
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.Arguments = string.Format("{0} {1}", "Buying.Py", "") + args;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.Start();

                        await process.WaitForExitAsync();
                        if (process.ExitCode == 0)
                        {
                            result = process.StandardOutput.ReadToEnd();
                            string[] output_all = result.Split("\n");
                            foreach (var line in output_all)
                            {
                                if (line.Contains("ORDER"))
                                {
                                    open_pos = true;
                                }
                            }
                        }
                        //since here we buy something we change open_pos to true
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                //returning the error message so that user can see it inside the datagrid
                return e.Message;
            }
        }

        public object DisplayArea { get; private set; }

        //function that allows user to move the window
        private void Mover_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        //changing the cursor
        private void Mover_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void Mover_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            
        }

        //closing the application
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
