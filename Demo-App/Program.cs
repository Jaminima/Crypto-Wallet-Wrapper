﻿using System;
using GRLC_Wallet_Wrapper;

namespace Demo_App
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cli_Manager.Start();

            Console.WriteLine(Cli_Manager.GetWalletInfo());

            while (true) { }
        }
    }
}
