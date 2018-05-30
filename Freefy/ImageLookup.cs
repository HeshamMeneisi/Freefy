using Freefy.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Freefy
{
    class ImageLookup
    {
        public static async Task GetSimilar(string[] labels, Action<string, Size> callback)
        {
            string urlQuery = HttpUtility.UrlEncode(String.Join(" ", labels));
            string url = String.Format(Settings.Default.LookupQuery, urlQuery);

            await ImageScraper.GetImageURLs(url, callback);
        }
    }
}
