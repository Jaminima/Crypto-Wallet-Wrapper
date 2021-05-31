using GRLC_Wallet_Wrapper;
using GRLC_Wallet_Wrapper.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Wrapper_API.Controllers
{
    public class Payments : Controller
    {
        [HttpGet("Register")]
        public void Register([FromQuery] string nick)
        {
            WUser u = new WUser() { balance = 0, nickname = nick.Trim() };
            WUser.users.Add(u);
            string rstr = Authentication.TrackAuthentication(u);
            Response.Cookies.Append("authkey",rstr);
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
            if (u == null) { 
                Response.StatusCode = 401; 
                return ""; 
            }

            string addr = (await Cli_Gets.GetNewWalletAddress()).Trim();
            u.inAddress = addr;
            return addr;
        }

        [HttpGet("Confirm")]
        public async Task<bool> ConfirmTransaction([FromQuery] string txId)
        {
            WUser u = Authentication.CheckAuthed(Request.Cookies["authkey"]);
            if (u == null) {
                Response.StatusCode = 401;
                return false;
            }

            Account a = await Cli_Payments.ConfirmPayment(u.inAddress, txId.Trim());
            if (a != null)
            {
                u.balance += a.amount;
                return true;
            }
            return false;
        }
    }
}