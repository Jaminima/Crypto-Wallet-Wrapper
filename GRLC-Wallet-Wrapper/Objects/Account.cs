namespace GRLC_Wallet_Wrapper.Objects
{
    public class Account
    {
        public string account, category, address, label;

        public float amount, fee;

        public bool abandoned;

        public long vout;
    }
}