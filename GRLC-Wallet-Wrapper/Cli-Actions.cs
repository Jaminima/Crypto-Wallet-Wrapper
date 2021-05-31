using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GRLC_Wallet_Wrapper
{
    public static class Cli_Actions
    {

        public static async Task<Objects.Wallet> GetWalletInfo()
        {
            return await Cli_Manager.DoAndReadClientRequest<Objects.Wallet>("getwalletinfo");
        }

        public static async Task<string> GetNetworkInfo()
        {
            return await Cli_Manager.DoAndReadClientRequest("getnetworkinfo");
        }

        public static async Task<string> GetBlockChainInfo()
        {
            return await Cli_Manager.DoAndReadClientRequest("getblockchaininfo");
        }

        public static async Task<bool> IsNetworkRunning()
        {
            try
            {
                await GetNetworkInfo();
                return true;
            }
            catch { return false; }
        }

        public static async Task<string> GetNewWalletAddress()
        {
            return await Cli_Manager.DoAndReadClientRequest("getnewaddress");
        }

        public static async Task<string> GetAccounts()
        {
            return await Cli_Manager.DoAndReadClientRequest("listaccounts");
        }

        public static async Task<Objects.Transaction> GetTransaction(string txid)
        {
            return await Cli_Manager.DoAndReadClientRequest<Objects.Transaction>($"gettransaction {txid}");
        }
    }
}
