using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

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

        public static async Task<string> DoAndReadClientRequest(string command)
        {
            Process Req = garlicli;
            Req.StartInfo.Arguments = command;
            Req.Start();

            return await Req .StandardOutput.ReadToEndAsync();
        }

        public static async Task<T> DoAndReadClientRequest<T>(string command)
        {
            string s = await DoAndReadClientRequest(command);
            return JsonConvert.DeserializeObject<T>(s);
        }
        public static void Start()
        {
            garlicoind = new Process();
            garlicoind.StartInfo = new ProcessStartInfo("D:/Garlicoin/garlicoind.exe");
            garlicoind.Start();
        }

        public static async Task<Objects.Wallet> GetWalletInfo()
        {
            return await DoAndReadClientRequest<Objects.Wallet>("getwalletinfo");
        }

        public static async Task<string> GetNetworkInfo()
        {
            return await DoAndReadClientRequest("getnetworkinfo");
        }

        public static async Task<string> GetBlockChainInfo()
        {
            return await DoAndReadClientRequest("getblockchaininfo");
        }

        public static async Task<bool> IsNetworkRunning()
        {
            return (await GetNetworkInfo()).StartsWith("{");
        }

        public static async Task<string> GetNewWalletAddress()
        {
            return await DoAndReadClientRequest("getnewaddress");
        }
    }
}
