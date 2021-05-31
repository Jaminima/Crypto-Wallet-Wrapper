using GRLC_Wallet_Wrapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Wrapper_API.Controllers
{
    public class Payments : Controller
    {
        [HttpGet("Balance")]
        public async Task<float> Balance()
        {
            return (await Cli_Gets.GetWalletInfo()).balance;
        }

        [HttpGet("PayInAddress")]
        public async Task<string> PayIn()
        {
            return await Cli_Gets.GetNewWalletAddress();
        }

        [HttpGet("Confirm")]
        public async Task<bool> ConfirmTransaction([FromQuery] string txId, [FromQuery] string destAddr)
        {
            return (await Cli_Payments.ConfirmPayment(destAddr, txId)) != null;
        }
    }
}