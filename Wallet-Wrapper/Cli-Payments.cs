using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet_Wrapper
{
    public static class Cli_Payments
    {
        #region Methods

        public static async Task<object> ConfirmPayment(string receiveAddress, string txId)
        {
            if (TXConfimer.confimerInstance.IsTXUsed(txId)) { return "Transaction has already been redeemed"; }

            Objects.Transaction t = await Cli_Gets.GetTransaction(txId);

            if (t == null) return "Tx Id Is Invalid";

            if (t.confirmations < 5)
            {
                return "Wait Till 5 Confimations";
            }

            IEnumerable<Objects.Account> f = t.details.Where(x => x.category == "receive" && x.address == receiveAddress);
            if (f.Any())
            {
                TXConfimer.confimerInstance.UseTX(txId);
                return f.First();
            }
            else return "Transaction does not check out";
        }

        public static async Task<Cli_Manager.ResponseBody<object>> PayOut(string address, float amount, bool deductFees = true)
        {
            var t = await Cli_Manager.DoAndReadClientRequest<object>(new object[] { "sendtoaddress", address, amount, "Pay Out", "Customer", deductFees });
            return t;
        }

        #endregion Methods
    }
}