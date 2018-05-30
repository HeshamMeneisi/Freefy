using Freefy.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freefy;
using System.Drawing;
using System.Dynamic;

namespace Utilities
{
    class ServerProxy
    {
        public static async Task<Dictionary<string, double>> GetPredictions(string url)
        {
            try
            {
                string str = "{\"url\":\"" + url + "\"}";

                var r = await RESTCaller.GetResponseAsync<Dictionary<string, double>>(Settings.Default.ServerHost, "/getPred", str);

                return r;
            }
            catch (Exception ex)
            { Reporter.Report(ex); }
            return new Dictionary<string, double>();
        }

        public static async Task<Dictionary<string, double>> GetPredictions(Image img)
        {
            try
            {
                var r = await RESTCaller.GetResponseAsync<Dictionary<string, double>>(Settings.Default.ServerHost, "/getPred", img);
                return r;
            }
            catch (Exception ex)
            { Reporter.Report(ex); }
            return new Dictionary<string, double>();
        }

        public static async Task<int> GetRecommended(Image img, Image[] matches)
        {
            try
            {
                var token_resp = await RESTCaller.GetResponseAsync<IDictionary<string, string>>(Settings.Default.ServerHost, "/getRecToken");
                var token = token_resp["token"];
                await RESTCaller.GetResponseAsync<IDictionary<string, int>>(Settings.Default.ServerHost, "/postImage", img, token);
                Dictionary<int, Image> ids = new Dictionary<int, Image>();
                foreach (Image i in matches)
                {
                    var id_resp = await RESTCaller.GetResponseAsync<IDictionary<string, int>>(Settings.Default.ServerHost, "/postImage", i, token);
                    var id = id_resp["id"];
                    ids[id] = i;
                }

                var resp = await RESTCaller.GetResponseAsync<IDictionary<string, int>>(Settings.Default.ServerHost, "/getRecByToken", "{\"token\":\"" + token + "\"}");
                var im = ids[resp["id"]];
                return Array.IndexOf(matches, im);
            }
            catch (Exception ex)
            { Reporter.Report(ex); return 0; }
        }
    }
}
