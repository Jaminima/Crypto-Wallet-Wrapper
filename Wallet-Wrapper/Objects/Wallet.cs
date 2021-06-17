namespace Wallet_Wrapper.Objects
{
    public class Wallet
    {
        #region Properties

        public float balance { get; set; }
        public string hdmasterkeyid { get; set; }
        public float immature_balance { get; set; }
        public long keypoololdest { get; set; }
        public long keypoolsize { get; set; }
        public long keypoolsize_hd_internal { get; set; }
        public float paytxfee { get; set; }
        public long txcount { get; set; }
        public float unconfimed_balance { get; set; }
        public string walletname { get; set; }
        public long walletversion { get; set; }

        #endregion Properties
    }
}