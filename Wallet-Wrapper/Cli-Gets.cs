using System.Threading.Tasks;

namespace Wallet_Wrapper
{
    public static class Cli_Gets
    {
        #region Methods

        public static async Task<string> GetAccounts()
        {
            var t = await Cli_Manager.DoAndReadClientRequest<object>("listaccounts");
            return t.result.ToString();
        }

        public static async Task<string> GetBlockChainInfo()
        {
            var t = await Cli_Manager.DoAndReadClientRequest<object>("getblockchaininfo");
            return t.result.ToString();
        }

        public static async Task<string> GetNetworkInfo()
        {
            var t = await Cli_Manager.DoAndReadClientRequest<object>("getnetworkinfo");
            if (t.error != null) throw new System.Exception(t.error.message);
            return t.result.ToString();
        }

        public static async Task<string> GetNewWalletAddress()
        {
            var t = await Cli_Manager.DoAndReadClientRequest<object>("getnewaddress");
            return t.result.ToString();
        }

        public static async Task<float> GetWalletBalance(string account = "balanceIN", int confirmations = 10)
        {
            var t = await Cli_Manager.DoAndReadClientRequest<string>("getbalance", account, confirmations);
            return float.Parse(t.result);
        }

        public static async Task<Objects.Transaction> GetTransaction(string txid)
        {
            var t = await Cli_Manager.DoAndReadClientRequest<Objects.Transaction>("gettransaction", txid);
            return t.result;
        }

        public static async Task<Objects.Wallet> GetWalletInfo()
        {
            var t = await Cli_Manager.DoAndReadClientRequest<Objects.Wallet>("getwalletinfo");
            return t.result;
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

        public static async Task<Objects.Address> VerifyAddress(string address)
        {
            var t = await Cli_Manager.DoAndReadClientRequest<Objects.Address>("validateaddress", address);
            return t.result;
        }

        #endregion Methods
    }
}