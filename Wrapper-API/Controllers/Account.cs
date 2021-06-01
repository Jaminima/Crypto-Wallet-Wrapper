using Microsoft.AspNetCore.Mvc;

namespace Wrapper_API.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("Register")]
        public WUser Register([FromQuery] string nick)
        {
            WUser u = new WUser() { balance = 0, nickname = nick.Trim() };
            WUser.AddUser(u);
            string rstr = Authentication.TrackAuthentication(u);
            Response.Cookies.Append("authkey", rstr);
            return u;
        }

        [HttpGet("Login")]
        public WUser Login([FromQuery] string nick)
        {
            WUser user = WUser.GetUser(nick);

            if (user == null)
            {
                return Register(nick);
            }

            string rstr = Authentication.TrackAuthentication(user);
            Response.Cookies.Append("authkey", rstr);
            return user;
        }

        [HttpGet("Account")]
        public WUser SeeAccount()
        {
            WUser user = Authentication.CheckAuthed(Request.Cookies["authkey"]);
            if (user == null) Response.StatusCode = 401;
            return user;
        }
    }
}