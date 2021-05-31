using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace GRLC_Wallet_Wrapper
{
    public static class Cli_Payments
    {
        public static async Task<string> PayOut(string address, float amount, bool deductFees = true)
        {
            return await Cli_Manager.DoAndReadClientRequest(new string[] { "sendtoaddress", address, amount.ToString(), "Pay Out", "Customer", deductFees.ToString() });
        }

        public static async Task<Objects.Account> ConfirmPayment(string receiveAddress, string txId)
        {
            Objects.Transaction t = await Cli_Gets.GetTransaction(txId);

            IEnumerable<Objects.Account> f = t.details.Where(x => x.category == "receive" && x.address == receiveAddress);
            if (f.Any())
            {
                return f.First();
            }
            else return null;
        }
    }
}