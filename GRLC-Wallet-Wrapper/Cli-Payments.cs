using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRLC_Wallet_Wrapper
{
    public static class Cli_Payments
    {
        public static async Task<string> PayOut(string address, float amount, bool deductFees = true)
        {
            return await Cli_Manager.DoAndReadClientRequest(new string[] { "sendtoaddress", address, amount.ToString(), "Pay Out", "Customer", deductFees.ToString().ToLower() });
        }

        public static async Task<object> ConfirmPayment(string receiveAddress, string txId)
        {
            if (ConfirmedTxs.AlreadyConfirmed(txId)) { return "Transaction has already been redeemed"; }

            Objects.Transaction t = await Cli_Gets.GetTransaction(txId);

            if (t.confirmations < 5) { 
                return "Wait Till 5 Confimations"; 
            }

            IEnumerable<Objects.Account> f = t.details.Where(x => x.category == "receive" && x.address == receiveAddress);
            if (f.Any())
            {
                ConfirmedTxs.AppendConfirmed(txId);
                return f.First();
            }
            else return "Transaction does not check out";
        }
    }
}