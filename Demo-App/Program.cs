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

            //string txid = await Cli_Payments.PayOut("MPDfUYATrVaNG9pX3Vg76QDtyrwmkzbeWa", 0.5f);

            //Console.WriteLine(txid);

            await Cli_Payments.ConfirmPayment("MPDfUYATrVaNG9pX3Vg76QDtyrwmkzbeWa", "e1ce2e0a6adc4ef95667c4ceedfb14ea01f612379e2d19ca26af1957030c9b4f");

            //Console.WriteLine((await Cli_Gets.GetTransaction("ecb25199f01d40a942cf35ddf649289a9dee468c76533d789729f7acd1946511")).amount);
        }
    }
}