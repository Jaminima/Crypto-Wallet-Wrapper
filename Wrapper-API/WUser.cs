using System.Collections.Generic;

namespace Wrapper_API
{
    public class WUser
    {
        public static List<WUser> users = new List<WUser>();

        public static WUser GetUser(string nick)
        {
            return users.Find(x => x.nickname == nick);
        }

        public static void AddUser(WUser u)
        {
            users.Add(u);
        }

        public float balance { get; set; }
        public string inAddress { get; set; }
        public string outAddress { get; set; }

        public string nickname { get; set; }
        public string password { get; set; }
    }
}