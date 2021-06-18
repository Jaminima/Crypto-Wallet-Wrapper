using System.Collections.Generic;
using System.IO;

namespace Wallet_Wrapper
{
    public class FlatFileTXConfimer : TXConfimer
    {
        #region Fields

        private HashSet<string> confirmedTxIds = ReadConfimed();

        #endregion Fields

        #region Methods

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

        public override bool IsTXUsed(string tx)
        {
            return confirmedTxIds.Contains(tx);
        }

        public void StoreConfimed()
        {
            StreamWriter writer = new StreamWriter(new FileStream("./confirmedTxIds.txt", FileMode.CreateNew));
            writer.Write(string.Join("\n", confirmedTxIds));
            writer.Flush();
            writer.Close();
        }

        public override void UseTX(string tx)
        {
            confirmedTxIds.Add(tx);

            StreamWriter writer = new StreamWriter(new FileStream("./confirmedTxIds.txt", FileMode.Append));
            writer.WriteLine(tx);
            writer.Flush();
            writer.Close();
        }

        #endregion Methods
    }

    public class TXConfimer
    {
        #region Fields

        public static TXConfimer confimerInstance = new FlatFileTXConfimer();

        #endregion Fields

        #region Methods

        public virtual bool IsTXUsed(string tx)
        {
            return false;
        }

        public virtual void UseTX(string tx)
        {
        }

        #endregion Methods
    }
}