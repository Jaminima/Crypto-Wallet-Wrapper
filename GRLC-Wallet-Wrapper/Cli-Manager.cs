using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GRLC_Wallet_Wrapper
{
    public static class Cli_Manager
    {
        private static Process garlicoind;

        public static Process garlicli
        {
            get
            {
                Process cli = new Process();
                cli.StartInfo = new ProcessStartInfo("D:/Garlicoin/garlicoin-cli.exe");
                cli.StartInfo.RedirectStandardOutput = true;
                cli.StartInfo.RedirectStandardError = true;
                return cli;
            }
        }

        public static async Task<string> DoAndReadClientRequest(string command)
        {
            return await DoAndReadClientRequest(new string[] { command });
        }

        public static async Task<string> DoAndReadClientRequest(string command1, string command2)
        {
            return await DoAndReadClientRequest(new string[] { command1, command2 });
        }

        public static async Task<string> DoAndReadClientRequest(string command1, string command2, string command3)
        {
            return await DoAndReadClientRequest(new string[] { command1, command2, command3 });
        }

        public static async Task<string> DoAndReadClientRequest(string[] commands)
        {
            Process Req = garlicli;

            foreach (string c in commands) Req.StartInfo.ArgumentList.Add(c);

            Req.Start();

            Req.WaitForExit();

            if (Req.ExitCode != 0)
            {
                throw new System.Exception(Req.StandardError.ReadToEnd().Split("error message:")[1]);
            }

            string s = await Req.StandardOutput.ReadToEndAsync();

            return s;
        }

        public static async Task<T> DoAndReadClientRequest<T>(string command)
        {
            return await DoAndReadClientRequest<T>(new string[] { command });
        }

        public static async Task<T> DoAndReadClientRequest<T>(string command1, string command2)
        {
            return await DoAndReadClientRequest<T>(new string[] { command1, command2 });
        }

        public static async Task<T> DoAndReadClientRequest<T>(string command1, string command2, string command3)
        {
            return await DoAndReadClientRequest<T>(new string[] { command1, command2, command3 });
        }

        public static async Task<T> DoAndReadClientRequest<T>(string[] commands)
        {
            string s = await DoAndReadClientRequest(commands);

            return JsonConvert.DeserializeObject<T>(s);
        }

        public static async void Start()
        {
            if (await Cli_Gets.IsNetworkRunning()) throw new System.Exception("Network is already running!");

            garlicoind = new Process();
            garlicoind.StartInfo = new ProcessStartInfo("D:/Garlicoin/garlicoind.exe");
            garlicoind.Start();
        }
    }
}