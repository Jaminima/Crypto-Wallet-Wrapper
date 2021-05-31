using System.Threading.Tasks;

namespace GRLC_Wallet_Wrapper
{
    public static class Cli_Payments
    {
        public static async Task<string> PayOut(string address, float amount, bool deductFees = true)
        {
            return await Cli_Manager.DoAndReadClientRequest(new string[] { "sendtoaddress", address, amount.ToString(), "Pay Out", "Customer", deductFees.ToString() });
        }
    }
}