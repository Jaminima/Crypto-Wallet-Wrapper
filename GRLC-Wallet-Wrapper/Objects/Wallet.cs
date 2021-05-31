using System;
using System.Collections.Generic;
using System.Text;

namespace GRLC_Wallet_Wrapper.Objects
{
    public class Wallet
    {
        public string walletname, hdmasterkeyid;

        public float balance, unconfimed_balance, immature_balance, paytxfee;

        public long walletversion, txcount, keypoololdest, keypoolsize, keypoolsize_hd_internal;
    }
}
