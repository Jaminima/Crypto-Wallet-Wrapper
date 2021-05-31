using System.IO;
using System.Diagnostics;

namespace GRLC_Wallet_Wrapper
{
    public static class Cli_Manager
    {
        static Process garlicoind;

        public static Process garlicli
        {
            get
            {
                Process cli = new Process();
                cli.StartInfo = new ProcessStartInfo("D:/Garlicoin/garlicoin-cli.exe");
                cli.StartInfo.RedirectStandardOutput = true;
                return cli;
            }
        }

        public static string DoAndReadClientRequest(string command)
        {
            Process Req = garlicli;
            Req.StartInfo.Arguments = command;
            Req.Start();

            string s = Req.StandardOutput.ReadToEnd();
            return s;
        }

        public static void Start()
        {
            garlicoind = new Process();
            garlicoind.StartInfo = new ProcessStartInfo("D:/Garlicoin/garlicoind.exe");
            garlicoind.Start();
        }

        public static string GetWalletInfo()
        {
            return DoAndReadClientRequest("getwalletinfo");
        }

        public static string GetNetworkInfo()
        {
            return DoAndReadClientRequest("getnetworkinfo");
        }

        public static string GetNewWalletAddress()
        {
            return DoAndReadClientRequest("getnewaddress");
        }
    }
}
