using System;
using System.Threading;
using System.Threading.Tasks;
using GRLC_Wallet_Wrapper;

namespace Demo_App
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cli_Manager.Start();

            new Thread(AppThread).Start();

            while (true) { }
        }

        static async void AppThread()
        {
            Console.WriteLine(await Cli_Actions.IsNetworkRunning());
            Console.WriteLine((await Cli_Actions.GetTransaction("d44bf842d47be8b49827d34da5878720f1780eff6b25d7a21bd392fea6e49648")).amount);
        }
    }
}
