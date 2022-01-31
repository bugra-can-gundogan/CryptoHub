using CryptoHub.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public ObservableCollection<CryptoViewDataGridRowClass> datagridBindedCollection;
        DispatcherTimer timer = new DispatcherTimer();
        public string parity = "";
        public string api_key = CryptoBotController.original_Api_Key;
        public string api_secret = CryptoBotController.original_Api_Secret;
        public decimal quantity = 0;
        public decimal buyprice = 0;
        public bool open_pos = false;
        public BinanceRelation binanceRelation;

        public MainWindow()
        {
            InitializeComponent();
            SQLiteRelation sqlRel = new SQLiteRelation();
            sqlRel.CreateTable();
            binanceRelation = new BinanceRelation(api_key, api_secret);
            var callforAllCoinsInTheMarket = binanceRelation.BuildDictionaryTask().GetAwaiter();
            callforAllCoinsInTheMarket.OnCompleted(() =>
            {
                CryptoBotController.coinsInTheMarket = callforAllCoinsInTheMarket.GetResult();
            });

            var callForCoinsOwnedByUser = binanceRelation.getWalletDataTask().GetAwaiter();
            callForCoinsOwnedByUser.OnCompleted(() =>
            {
                CryptoBotController.ownedCoinsByUser = callForCoinsOwnedByUser.GetResult();
            });
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += timer_Tick;
        }

        public void timerstart(ObservableCollection<CryptoViewDataGridRowClass> dglist)
        {
            CryptoBotController.botIsRunning = true;
            datagridBindedCollection = dglist;
            timer.Start();
        }
        public void timerstop()
        {
            CryptoBotController.botIsRunning = false;
            timer.Stop();
        }
        private void timer_Tick(object? sender, EventArgs e)
        {
            string x = RunPythonBot();
            datagridBindedCollection.Add(
                new CryptoViewDataGridRowClass() { Date=DateTime.Now, Messagge= x});
            Console.WriteLine(x);
            //((MainWindow)Application.Current.MainWindow)

        }

        private string RunPythonBot()
        {
            string output = "None";
            string result = string.Empty;
            string args = api_key + " " + api_secret + " " + quantity + " " + parity + " " + buyprice.ToString();

            try
            {
                if (open_pos)
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = @"C:\Frameworks\Python\Python310\python.exe";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.Arguments = string.Format("{0} {1}", "Selling.Py", "") + args;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.Start();

                        process.WaitForExit();
                        if (process.ExitCode == 0)
                        {
                            result = process.StandardOutput.ReadToEnd();
                        }

                        return result;
                    }
                }
                else
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = @"C:\Frameworks\Python\Python310\python.exe";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.Arguments = string.Format("{0} {1}", "Buying.Py", "") + args;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.Start();

                        process.WaitForExit();
                        if (process.ExitCode == 0)
                        {
                            result = process.StandardOutput.ReadToEnd();
                        }

                        open_pos = true;
                        return result;
                    }
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public object DisplayArea { get; private set; }

        private void Mover_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Mover_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void Mover_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            
        }
    }
}
