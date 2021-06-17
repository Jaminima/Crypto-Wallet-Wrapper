namespace Wallet_Wrapper.Objects
{
    public class Transaction
    {
        #region Properties

        public float amount { get; set; }
        public string blockhash { get; set; }
        public long blockindex { get; set; }
        public long blocktime { get; set; }
        public long confirmations { get; set; }
        public Account[] details { get; set; }
        public float fee { get; set; }
        public long time { get; set; }
        public long timereceived { get; set; }
        public string txid { get; set; }

        #endregion Properties
    }
}