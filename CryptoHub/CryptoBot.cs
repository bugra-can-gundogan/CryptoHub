using Binance.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CryptoHub
{
    public static class CryptoBot
    {
        public static string api_key = "";
        public static string api_secret = "";
        public static DispatcherTimer timerPublic = new DispatcherTimer();
        
        public static void buildTimer()
        {
            timerPublic.Interval = TimeSpan.FromSeconds(2);
            timerPublic.Tick += timer_Tick;
        }
        private static void timer_Tick(object? sender, EventArgs e)
        {
            Console.WriteLine("LÖLÖLÖLÖL");
        }

        public static void startTimer()
        {
            timerPublic.Start();
        }

        public static void stopTimer()
        {
            timerPublic.Stop();
        }
        public static string runPythonBot(string parity, decimal quantity, bool open_position = false, decimal buyprice = 0)
        {
            string result = string.Empty;
            api_key = CryptoBotController.original_Api_Key;
            api_secret = CryptoBotController.original_Api_Secret;
            string args = api_key + " " + api_secret;
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = @"C:\Frameworks\Python\Python310\python.exe";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.Arguments = string.Format("{0} {1}", "Botilito.Py", "") + args;
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
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
