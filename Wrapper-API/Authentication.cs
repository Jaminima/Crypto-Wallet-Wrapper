using System;
using System.Collections.Generic;

namespace Wrapper_API
{
    public static class Authentication
    {
        private static Random rnd = new Random();

        private static Dictionary<string, WUser> authedUsers = new Dictionary<string, WUser>();

        public static WUser CheckAuthed(string authstr)
        {
            if (authedUsers.ContainsKey(authstr))
                return authedUsers[authstr];
            else
                return null;
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
            string s = "";
            for (uint i = 0; i < 32; i++) s += rnd.Next(65, 90);
            return s;
        }
    }
}