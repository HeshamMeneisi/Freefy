using CSV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Freefy
{
    public partial class MainFrm
    {
        BindingList<URLWrapper> urls = new BindingList<URLWrapper>();
        BindingList<ImageWrapper> current_images = new BindingList<ImageWrapper>();

        int selectedUrlIndex = -1;
        int selectedImgIndex = -1;

        private async Task ReloadURL(int index)
        {
            if (index < urls.Count)
            {
                if (urls[index] is ImageWrapper)
                    Cache.Remove<ImageWrapper>(urls[index].URL);
                else
                    urls[index].ClearChildren();
                await SetSelectedURL(index);
            }
        }

        private async Task SetSelectedImg(int index)
        {
            imgList.Enabled = false;
            if (index < current_images.Count)
            {
                selectedImgIndex = index;
                SetStatus("Loading Image...");
                var w = current_images[index];
                if (w.GetFullImage() == null)
                    w.RetrieveImage();
                else
                {
                    imgPrev.Image = w.GetThumb(imgPrev.Width, imgPrev.Height);
                }
                if (w.GetLabels() == null)
                    w.RetrieveLabels();
                else
                {
                    string s = "";
                    var l = w.GetLabels();
                    foreach (var k in l.Keys)
                        s += l[k] * 100 + "% " + k + "\r\n";
                    labelsText.Text = s;
                }
                SetStatus("Idle");
            }
            imgList.Enabled = true;
        }

        private async Task SetSelectedURL(int index)
        {
            if (index < urls.Count)
            {
                selectedUrlIndex = index;
                urlGrid.Enabled = imgList.Enabled = false;
                string url = urls[index].URL;
                current_images.Clear();
                if (HttpHelper.IsImage(url))
                {
                    ImageWrapper w;
                    if (!Cache.Lookup(url, out w))
                    {
                        SetStatus("Scraping Image...");
                        w = new ImageWrapper(url);
                        Cache.Stash(url, w);
                    }
                    else SetStatus("Cache hit!");

                    current_images.Add(w);
                }
                else
                {
                    if (urls[index].GetChildren() == null)
                    {
                        await Scrap(index);
                    }
                    else
                    {
                        SetStatus("Cache hit!");
                        foreach (ImageWrapper iw in urls[index].GetChildren())
                            current_images.Add(iw);
                    }
                }
                urlGrid.Enabled = imgList.Enabled = true;
                SetStatus("Idle");
            }
            await SetSelectedImg(0);
        }

        private async Task Scrap(int index)
        {
            SetStatus("Scraping Images...");

            Size minSize;
            try
            {
                minSize = new Size(int.Parse(mWidth.Text), int.Parse(mHeight.Text));
            }
            catch { minSize = new Size(200, 200); }
            await ImageScraper.GetImageURLs(urls[index].URL, (imgUrl, size) =>
            {
                if (size.Width < minSize.Width || size.Height < minSize.Height) return;
                ImageWrapper iw;
                if (!Cache.Lookup(imgUrl, out iw))
                {
                    iw = new ImageWrapper(imgUrl);
                    Cache.Stash(imgUrl, iw);
                }
                urls[index].AddChild(iw);
            });
        }

        private void SetStatus(string v)
        {
            Invoke(new MethodInvoker(()=>
            {
                statusText.Text = v;
                Application.DoEvents();
            }));
        }

        private async Task LoadFile(string path)
        {
            urls.Clear();
            if (path.ToLower().EndsWith(".csv"))
            {
                statusText.Text = "Reading CSV...";
                try
                {
                    foreach (var row in CSVDecoder.DecodeSLConst<URLWrapper>(path))
                    {
                        var u = row;
                        if (!Cache.TryUpdate(row.URL, ref u))
                            Cache.Stash(u.URL, u);
                        urls.Add(u);
                        u.ChildrenUpdated += urlChildUpdated;
                    }
                }
                catch (Exception ex)
                {
                    Reporter.Report(ex);
                }
            }
            else
            {
                using (var sr = new StreamReader(File.OpenRead(path)))
                    while (!sr.EndOfStream)
                    {
                        string url = sr.ReadLine();
                        urls.Add(new URLWrapper(url));
                    }
            }
            SetStatus("Idle");
            SetSelectedURL(0);
        }

        private void urlChildUpdated(URLWrapper sender)
        {
            if (urls.IndexOf(sender) == selectedUrlIndex)
                SetSelectedURL(selectedUrlIndex);
        }
    }
}
