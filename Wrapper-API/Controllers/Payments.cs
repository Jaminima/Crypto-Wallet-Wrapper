﻿using GRLC_Wallet_Wrapper;
using GRLC_Wallet_Wrapper.Objects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Wrapper_API.Controllers
{
    public class Payments : Controller
    {
        [HttpGet("Register")]
        public void Register([FromQuery] string nick)
        {
            WUser u = new WUser() { balance = 0, nickname = nick.Trim() };
            WUser.AddUser(u);
            string rstr = Authentication.TrackAuthentication(u);
            Response.Cookies.Append("authkey", rstr);
        }

        [HttpGet("Account")]
        public WUser Account()
        {
            WUser user = Authentication.CheckAuthed(Request.Cookies["authkey"]);
            return user;
        }

        [HttpGet("Balance")]
        public async Task<float> Balance()
        {
            return (await Cli_Gets.GetWalletInfo()).balance;
        }

        [HttpGet("PayInAddress")]
        public async Task<string> PayIn()
        {
            WUser u = Authentication.CheckAuthed(Request.Cookies["authkey"]);
            if (u == null)
            {
                Response.StatusCode = 401;
                return "";
            }

            string addr = (await Cli_Gets.GetNewWalletAddress()).Trim();
            u.inAddress = addr;
            u.Updated();
            return addr;
        }

        [HttpGet("Confirm")]
        public async Task<object> ConfirmTransaction([FromQuery] string txId)
        {
            WUser u = Authentication.CheckAuthed(Request.Cookies["authkey"]);
            if (u == null)
            {
                Response.StatusCode = 401;
                return false;
            }

            Object a = await Cli_Payments.ConfirmPayment(u.inAddress, txId.Trim());
            if (a.GetType() == typeof(Account))
            {
                u.balance += ((Account)a).amount;
                u.Updated();
                return u;
            }
            Response.StatusCode = 401;
            return a;
        }

        [HttpGet("Withdraw")]
        public async Task<string> Withdraw([FromQuery] string outAddr, [FromQuery] float amount)
        {
            amount = Math.Abs(amount);

            WUser u = Authentication.CheckAuthed(Request.Cookies["authkey"]);
            if (u == null)
            {
                Response.StatusCode = 401;
                return "Not Signed In";
            }

            if (u.balance >= amount)
            {
                u.balance -= amount;
                u.Updated();
                return await Cli_Payments.PayOut(outAddr, amount);
            }
            else
            {
                Response.StatusCode = 401;
                return "You dont have enough balance.";
            }
        }
    }
}