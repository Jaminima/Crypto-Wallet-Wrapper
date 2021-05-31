using GRLC_Wallet_Wrapper;
using System;
using System.Threading;

namespace Demo_App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Cli_Manager.Start();

            new Thread(AppThread).Start();

            while (true) { }
        }

        private static async void AppThread()
        {
            Console.WriteLine(await Cli_Actions.IsNetworkRunning() ? "Network Running" : "You Must Start the Network");
            Console.WriteLine((await Cli_Actions.GetTransaction("d44bf842d47be8b49827d34da5878720f1780eff6b25d7a21bd392fea6e49648")).amount);
        }
    }
}