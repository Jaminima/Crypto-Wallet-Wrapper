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
            bool running = await Cli_Gets.IsNetworkRunning();
            Console.WriteLine(running ? "Network Running" : "You Must Start the Network");

            if (!running) return;

            string txid = await Cli_Payments.PayOut("MPDfUYATrVaNG9pX3Vg76QDtyrwmkzbeWa", 0.5f);

            Console.WriteLine(txid);

            //Console.WriteLine((await Cli_Gets.GetTransaction("ecb25199f01d40a942cf35ddf649289a9dee468c76533d789729f7acd1946511")).amount);
        }
    }
}