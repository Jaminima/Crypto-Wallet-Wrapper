using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scrypt;

namespace Wrapper_API
{
    public static class Authentication
    {
        private static Scrypt.ScryptEncoder encoder = new ScryptEncoder();
        private static Random rnd = new Random();

        private static Dictionary<string, WUser> authedUsers = new Dictionary<string, WUser>();

        public static WUser CheckAuthed(string authstr)
        {
            return authedUsers[authstr];
        }

        private static void AuthTrackUser(WUser user, string rstr)
        {
            authedUsers.Add(rstr, user);
        }

        public static string TrackAuthentication(WUser user)
        {
            string s = RndString();
            AuthTrackUser(user, s);
            return s;
        }

        public static string RndString()
        {
            return encoder.Encode(rnd.Next().ToString());
        }
    }
}
