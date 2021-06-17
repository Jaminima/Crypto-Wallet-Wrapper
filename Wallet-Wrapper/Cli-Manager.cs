using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wallet_Wrapper
{
    public static class Cli_Manager
    {
        #region Methods

        private static async Task<ResponseBody<T>> DoReadClientRequest<T>(object[] commands)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), Config.conf.rpcAddress))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Config.conf.username}:{Config.conf.password}"));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    RequestBody requestBody = new RequestBody() { method = commands[0].ToString(), paramaters = commands.TakeLast(commands.Length - 1).ToArray() };

                    request.Content = new StringContent(JsonConvert.SerializeObject(requestBody));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    string s;
                    try
                    {
                        var response = await httpClient.SendAsync(request);
                        s = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseBody<T>>(s);
                    }
                    catch (WebException e)
                    {
                        StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                        s = reader.ReadToEnd();
                        reader.Close();
                        ResponseBody<T> error = JsonConvert.DeserializeObject<ResponseBody<T>>(s);
                        throw new Exception(error.error.message);
                    }
                    catch (JsonException e)
                    {
                        throw e;
                    }
                }
            }
        }

        #endregion Methods

        #region Classes

        private class RequestBody
        {
            #region Fields

            public string jsonrpc = "1.0", id = "wrapper", method;

            [JsonProperty("params")]
            public object[] paramaters;

            #endregion Fields
        }

        #endregion Classes

        public static async Task<ResponseBody<T>> DoAndReadClientRequest<T>(object command)
        {
            return await DoAndReadClientRequest<T>(new object[] { command });
        }

        public static async Task<ResponseBody<T>> DoAndReadClientRequest<T>(object command1, object command2)
        {
            return await DoAndReadClientRequest<T>(new object[] { command1, command2 });
        }

        public static async Task<ResponseBody<T>> DoAndReadClientRequest<T>(object command1, object command2, object command3)
        {
            return await DoAndReadClientRequest<T>(new object[] { command1, command2, command3 });
        }

        public static async Task<ResponseBody<T>> DoAndReadClientRequest<T>(object[] commands)
        {
            ResponseBody<T> s = await DoReadClientRequest<T>(commands);

            return s;
        }

        public static async Task<bool> TryStart()
        {
            while (!await Cli_Gets.IsNetworkRunning())
            {
                Console.WriteLine("Waiting for Network Start");
                Thread.Sleep(5000);
            }
            return true;
        }

        public class ResponseBody<T>
        {
            #region Fields

            public ResponseError error;
            public string id;
            public T result;

            #endregion Fields
        }

        public class ResponseError
        {
            #region Fields

            public int code;
            public string message;

            #endregion Fields
        }
    }
}