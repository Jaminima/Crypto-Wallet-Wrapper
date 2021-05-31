using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GRLC_Wallet_Wrapper
{
    public static class Cli_Payments
    {
        public static async Task<string> PayOut(string address, float amount, bool deductFees = true)
        {
            return await Cli_Manager.DoAndReadClientRequest(new string[] { "sendtoaddress", address, amount.ToString(), "Pay Out", "Customer", deductFees.ToString() });
        }

        private static HashSet<string> confirmedTxIds = ReadConfimed();

        public static HashSet<string> ReadConfimed()
        {
            HashSet<string> set = new HashSet<string>();

            if (File.Exists("./confirmedTxIds.txt"))
            {
                StreamReader reader = new StreamReader(new FileStream("./confirmedTxIds.txt", FileMode.Open));
                string s = reader.ReadToEnd();

                foreach (string line in s.Split("\n")) set.Add(line);
            }
            return set;
        }

        public static void StoreConfimed()
        {
            StreamWriter writer = new StreamWriter(new FileStream("./confirmedTxIds.txt", FileMode.CreateNew));
            writer.Write(string.Join("\n", confirmedTxIds));
            writer.Flush();
            writer.Close();
        }

        public static void AppendConfirmed(string txId)
        {
            confirmedTxIds.Add(txId);

            StreamWriter writer = new StreamWriter(new FileStream("./confirmedTxIds.txt", FileMode.Append));
            writer.Write(txId);
            writer.Flush();
            writer.Close();
        }

        public static async Task<Objects.Account> ConfirmPayment(string receiveAddress, string txId)
        {
            if (confirmedTxIds.Contains(txId)) return null;

            Objects.Transaction t = await Cli_Gets.GetTransaction(txId);

            IEnumerable<Objects.Account> f = t.details.Where(x => x.category == "receive" && x.address == receiveAddress);
            if (f.Any())
            {
                AppendConfirmed(txId);
                return f.First();
            }
            else return null;
        }
    }
}