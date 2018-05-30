using Freefy.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freefy;
using System.Drawing;

namespace Utilities
{
    class ServerProxy
    {
        internal static async Task<Dictionary<string, double>> GetPredictions(string url)
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

        internal static async Task<Dictionary<string, double>> GetPredictions(Image img)
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
    }
}
