using System;
using System.Threading;
using Wallet_Wrapper;

namespace Demo_App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Ensuring the Network is Running, this may take some time.\n");

            Cli_Manager.TryStart().Wait();

            Console.WriteLine("Network Started Successfully.\n");

            new Thread(AppThread).Start();

            while (true) { Thread.Sleep(Int32.MaxValue); }
        }

        private static async void AppThread()
        {
            //string txid = await Cli_Payments.PayOut("MPDfUYATrVaNG9pX3Vg76QDtyrwmkzbeWa", 0.5f);

            //Console.WriteLine(txid);

            dynamic p = await Cli_Gets.VerifyAddress("MPDfUYATrVaNG9pX3Vg76QDtyrwmkzbeWa");

            //Object acc1 = await Cli_Payments.ConfirmPayment("MPDfUYATrVaNG9pX3Vg76QDtyrwmkzbeWa", "e1ce2e0a6adc4ef95667c4ceedfb14ea01f612379e2d19ca26af1957030c9b4f");
            //Object acc2 = await Cli_Payments.ConfirmPayment("MPDfUYATrVaNG9pX3Vg76QDtyrwmkzbeWa", "e1ce2e0a6adc4ef95667c4ceedfb14ea01f612379e2d19ca26af1957030c9b4f");

            //Console.WriteLine(acc1.GetType() == typeof(Account) ? "Verified Transaction" : "Failed To Verify");
            //Console.WriteLine(acc2.GetType() == typeof(Account) ? "Verified Transaction" : "Failed To Verify");

            //Console.WriteLine((await Cli_Gets.GetTransaction("ecb25199f01d40a942cf35ddf649289a9dee468c76533d789729f7acd1946511")).amount);
        }
    }
}