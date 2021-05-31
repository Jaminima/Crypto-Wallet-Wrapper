using GRLC_Wallet_Wrapper;
using GRLC_Wallet_Wrapper.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Wrapper_API.Controllers
{
    public class Payments : Controller
    {
        [HttpGet("Register")]
        public void Register([FromQuery] string _nick)
        {
            Wrapper_API.User.users.Add(new Wrapper_API.User() { balance = 0, nick = _nick });
        }

        [HttpGet("Account")]
        public Wrapper_API.User Account([FromQuery] string _nick)
        {
            return Wrapper_API.User.users.Find(x => x.nick == _nick);
        }

        [HttpGet("Balance")]
        public async Task<float> Balance()
        {
            return (await Cli_Gets.GetWalletInfo()).balance;
        }

        [HttpGet("PayInAddress")]
        public async Task<string> PayIn([FromQuery] string _nick)
        {
            string addr = await Cli_Gets.GetNewWalletAddress();
            Wrapper_API.User.userPayInAddresses.Add(addr.Trim(), Wrapper_API.User.users.Find(x => x.nick == _nick));
            return addr;
        }

        [HttpGet("Confirm")]
        public async Task<bool> ConfirmTransaction([FromQuery] string txId, [FromQuery] string destAddr)
        {
            Wrapper_API.User u = Wrapper_API.User.userPayInAddresses[destAddr];

            Account a = await Cli_Payments.ConfirmPayment(destAddr, txId);
            if (a != null)
            {
                u.balance += a.amount;
                return true;
            }
            return false;
        }
    }
}