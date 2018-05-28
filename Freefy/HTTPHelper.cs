using Freefy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    class HttpHelper
    {
        public static bool TryGetImage(string url, out Image img)
        {
            if (Cache.Lookup<Image>(url, out img))
                return true;

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            using (var resp = req.GetResponse())
            {
                bool is_image = resp.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/");

                img = null;
                if (is_image)
                {
                    try
                    {
                        using (var imgResp = req.GetResponse())
                            img = Image.FromStream(imgResp.GetResponseStream());
                        Cache.Stash(url, img);
                    }
                    catch (Exception ex)
                    {
                        Reporter.Report(ex);
                    }
                }

                return is_image;
            }
        }

        public static bool IsImage(string url)
        {
            if (url == null) return false;
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "HEAD";
                using (var resp = req.GetResponse())
                {
                    return resp.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/");
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
