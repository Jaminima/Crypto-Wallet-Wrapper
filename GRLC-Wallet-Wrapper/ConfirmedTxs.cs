using System.Collections.Generic;
using System.IO;

namespace GRLC_Wallet_Wrapper
{
    public static class ConfirmedTxs
    {
        private static HashSet<string> confirmedTxIds = ReadConfimed();

        public static bool AlreadyConfirmed(string txId)
        {
            return confirmedTxIds.Contains(txId);
        }

        private static HashSet<string> ReadConfimed()
        {
            HashSet<string> set = new HashSet<string>();

            if (File.Exists("./confirmedTxIds.txt"))
            {
                StreamReader reader = new StreamReader(new FileStream("./confirmedTxIds.txt", FileMode.Open));
                string s = reader.ReadToEnd();

                foreach (string line in s.Split("\n")) set.Add(line);
                reader.Close();
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
    }
}