using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Wrapper_API
{
    public class WUser
    {
        private static List<WUser> users = LoadUsers();

        private static List<WUser> LoadUsers()
        {
            List<WUser> set = new List<WUser>();

            if (File.Exists("./users.json"))
            {
                StreamReader reader = new StreamReader(new FileStream("./users.json", FileMode.Open));
                string s = reader.ReadToEnd();

                set = JsonConvert.DeserializeObject<List<WUser>>(s);
                reader.Close();
            }
            return set;
        }

        private static void SaveUsers()
        {
            if (File.Exists("./users.json")) File.Delete("./users.json");

            StreamWriter writer = new StreamWriter(new FileStream("./users.json", FileMode.CreateNew));
            writer.Write(JsonConvert.SerializeObject(users));
            writer.Flush();
            writer.Close();
        }

        public static WUser GetUser(string nick)
        {
            return users.Find(x => x.nickname == nick);
        }

        public static void AddUser(WUser u)
        {
            users.Add(u);
            SaveUsers();
        }

        public void Updated()
        {
            SaveUsers();
        }

        public float balance { get; set; }
        public string inAddress { get; set; }
        public string outAddress { get; set; }

        public string nickname { get; set; }
        public string password { get; set; }
    }
}