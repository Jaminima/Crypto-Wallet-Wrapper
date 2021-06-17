namespace Wallet_Wrapper.Objects
{
    public class Account
    {
        #region Properties

        public bool abandoned { get; set; }
        public string account { get; set; }
        public string address { get; set; }
        public float amount { get; set; }
        public string category { get; set; }
        public float fee { get; set; }
        public string label { get; set; }
        public long vout { get; set; }

        #endregion Properties
    }
}