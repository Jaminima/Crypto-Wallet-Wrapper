namespace Wallet_Wrapper.Objects
{
    public class Account
    {
        public string account { get; set; }
        public string category { get; set; }
        public string address { get; set; }
        public string label { get; set; }

        public float amount { get; set; }
        public float fee { get; set; }

        public bool abandoned { get; set; }

        public long vout { get; set; }
    }
}