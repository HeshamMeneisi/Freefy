using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Freefy
{
    internal class ImageScraper
    {
        static TextBox urlText = new TextBox { Dock = DockStyle.Top };
        static SimpleMutex jobInProgress = new SimpleMutex();
        static ExtendedWebBrowser browser;

        public static async Task GetImageURLs(string url, Action<string, Size> callback)
        {
            jobInProgress.Acquire();

            browser = new ExtendedWebBrowser
            {
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true
            };
            browser.PageLoaded += (s, e) =>
            {
                try
                {
                    urlText.Text = e.Url.ToString();
                    HashSet<string> urls = new HashSet<string>();
                    if (HasCaptcha())
                        if (MessageBox.Show("Captcha detected! Please solve it to load pages from this website.", "Captcha") == DialogResult.OK)
                            ShowPreview();

                    foreach (HtmlElement el in browser.Document.Images)
                    {
                        string imgUrl = el.GetAttribute("src");
                        if (imgUrl == string.Empty)
                            imgUrl = el.GetAttribute("data-src");
                        if (imgUrl != string.Empty && !urls.Contains(imgUrl))
                        {
                            if (imgUrl.Contains("w_auto"))
                                imgUrl = imgUrl.Replace("w_auto", "w_800");
                            urls.Add(imgUrl);
                            callback(imgUrl, el.ClientRectangle.Size);
                        }
                    }
                }
                finally
                {
                    browser.DisableEvents("PageLoaded");
                    jobInProgress.Release();
                }
            };
            browser.Navigate(url);
        }

        private static bool HasCaptcha()
        {
            foreach (HtmlElement el in browser.Document.All)
                if (el.Id != null && el.Id.ToLower().Contains("captcha"))
                    return true;
            return false;
        }

        public static void ShowPreview()
        {
            Form m = new Form();
            urlText = new TextBox { Dock = DockStyle.Top };
            m.Controls.Add(browser);
            m.Controls.Add(urlText);
            m.Text = "Preview";
            //m.WindowState = FormWindowState.Maximized;
            m.Size = new Size(800, 600);
            m.Show();
        }

        public static void TryGetImage(string url, Action<Bitmap> callback)
        {
            Debug.WriteLine("Waiting");
            jobInProgress.Acquire();
            Debug.WriteLine("Access");
            browser = new ExtendedWebBrowser
            {
                Dock = DockStyle.Fill,
                ScriptErrorsSuppressed = true
            };
            browser.PageLoaded += (s, e) =>
            {
               try
               {
                   urlText.Text = e.Url.ToString();
                   if (HasCaptcha())
                   {
                       if (MessageBox.Show("Captcha detected! Please solve it to load pages from this website.", "Captcha") == DialogResult.OK)
                           ShowPreview();
                   }
                   else
                   {
                       var images = browser.Document.Images;
                       if (images.Count > 0)
                       {
                           var el = images[0];
                           var d = browser.Dock;
                           var en = browser.ScrollBarsEnabled;
                           browser.Dock = DockStyle.None;
                           browser.ScrollBarsEnabled = false;
                           browser.Document.Body.Style = "overflow:hidden";
                           browser.Size = el.ClientRectangle.Size + new Size(50, 50);
                           Bitmap temp = new Bitmap(browser.Width, browser.Height);
                           browser.DrawToBitmap(temp, new Rectangle(0, 0, temp.Width, temp.Height));
                           Bitmap bmp = new Bitmap(temp.Width - 50, temp.Height - 60);
                           Graphics gfx = Graphics.FromImage(bmp);
                           gfx.DrawImage(temp, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(10, 20, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                           gfx.Dispose();
                           //string path = Path.GetTempFileName();
                           //bmp.Save(path);
                           browser.Dock = d;
                           browser.ScrollBarsEnabled = en;
                           callback(bmp);
                       }
                   }
               }
               finally
               {
                   browser.DisableEvents("PageLoaded");
                   jobInProgress.Release();
               }
            };
            browser.Navigate(url);
        }
    }
}