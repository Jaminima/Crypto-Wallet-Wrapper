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
            return (await GetNetworkInfo()).StartsWith("{");
        }

        public static async Task<string> GetNewWalletAddress()
        {
            return await Cli_Manager.DoAndReadClientRequest("getnewaddress");
        }
    }
}
