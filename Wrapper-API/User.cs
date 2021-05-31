using System.Collections.Generic;

namespace Wrapper_API
{
    public class User
    {
        public static List<User> users = new List<User>();
        public static Dictionary<string, User> userPayInAddresses = new Dictionary<string, User>();

        public float balance { get; set; }
        public string nick { get; set; }
    }
}