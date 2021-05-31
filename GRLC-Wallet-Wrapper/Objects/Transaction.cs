using System;
using System.Collections.Generic;
using System.Text;

namespace GRLC_Wallet_Wrapper.Objects
{
    public class Transaction
    {
        public float amount, fee;

        public string blockhash, txid;

        public long confirmations, blockindex, blocktime, time, timereceived;

        public Account[] details;
    }
}
