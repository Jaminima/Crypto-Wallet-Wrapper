namespace GRLC_Wallet_Wrapper.Objects
{
    public class Transaction
    {
        public float amount { get; set; }
        public float fee { get; set; }

        public string blockhash { get; set; }
        public string txid { get; set; }

        public long confirmations { get; set; }
        public long blockindex { get; set; }
        public long blocktime { get; set; }
        public long time { get; set; }
        public long timereceived { get; set; }

        public Account[] details { get; set; }
    }
}