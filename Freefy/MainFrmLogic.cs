using CSV;
using Freefy.Properties;
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

        int selectedUrlIndex = -1;
        int selectedImgIndex = -1;
        int selectedMatchIndex = -1;
        private static readonly string FOLDER_MARKER = "_FREEFY_OUTPUT";

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

        private async Task SetSelectedMatch(int index)
        {
            matchGrid.Enabled = false;
            var matches = urls[selectedUrlIndex].GetChildren()[selectedImgIndex].GetMatches();
            if (index < matches.Count)
            {
                var m = matches[index];
                matchPrev.Image = m.GetThumb(matchPrev.Width, matchPrev.Height);
                selectedMatchIndex = index;

                foreach (DataGridViewRow row in matchGrid.Rows)
                    row.Selected = row.Index == index;

                pickMatch.Enabled = true;
            }
            matchGrid.Enabled = true;
        }

        private async Task SetSelectedImg(int index)
        {
            imgList.Enabled = false;
            if (urls[selectedUrlIndex].GetChildren() != null && index < urls[selectedUrlIndex].GetChildren().Count)
            {
                pickMatch.Enabled = false;
                selectedImgIndex = index;
                SetStatus("Finding matches...");
                var w = urls[selectedUrlIndex].GetChildren()[index];
                if (w.GetFullImage() == null)
                    await w.RetrieveImage();
                else
                {
                    imgPrev.Image = w.GetThumb(imgPrev.Width, imgPrev.Height);
                }
                if (!w.HasRetrievedLabels())
                    await w.RetrieveLabels();

                matchGrid.DataSource = w.GetMatches();
                labelGrid.DataSource = w.GetLabels();
                if (!w.HasRetrievedMatches())
                    await w.RetrieveMatches();
                else
                {
                    var s = w.GetSelectedMatchIndex();
                    await SetSelectedMatch(s);
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
                if (urls[index].GetChildren() == null && HttpHelper.IsImage(url))
                {
                    ImageWrapper iw;
                    if (!Cache.Lookup(url, out iw))
                    {
                        SetStatus("Scraping Image...");
                        iw = new ImageWrapper(url);
                        Cache.Stash(url, iw);
                        SetStatus("Idle");

                    }
                    else SetStatus("Cache hit!");

                    urls[index].AddChild(iw);
                    iw.RecommendationMade += recMade;
                    await iw.RetrieveLabels();
                }
                else
                {
                    if (urls[index].GetChildren() == null)
                        await Scrap(index);
                    else
                    {
                        imgList.DataSource = urls[index].GetChildren();
                        await SetSelectedImg(0);
                    }
                }
                imgList.DataSource = urls[index].GetChildren();
                urlGrid.Enabled = imgList.Enabled = true;
            }
        }

        private void recMade(URLWrapper sender)
        {
            if (selectedUrlIndex >= 0 && selectedUrlIndex < urls.Count)
            {
                var children = urls[selectedUrlIndex].GetChildren();
                if (children != null)
                {
                    var iw = (ImageWrapper)sender;
                    var idx = children.IndexOf(iw);
                    if (idx >= 0 && idx == selectedImgIndex)
                        Invoke(new MethodInvoker(async () =>
                        {
                            await SetSelectedMatch(iw.GetSelectedMatchIndex());
                        }));
                }
            }
        }

        private async Task Scrap(int index)
        {
            SetStatus("Scraping Images...");

            Size minSize = new Size(Settings.Default.MinWidth, Settings.Default.MinHeight);
            ImageScraper.GetImageURLs(urls[index].URL, async (imgUrl, size) =>
            {
                if (imgUrl == null)
                {
                    SetStatus("Idle");
                    return;
                }
                if ((size.Width > 0 && size.Width < minSize.Width) || (size.Height > 0 && size.Height < minSize.Height)) return;
                ImageWrapper iw;
                if (!Cache.Lookup(imgUrl, out iw))
                {
                    iw = new ImageWrapper(imgUrl);
                    iw.RecommendationMade += recMade;
                    Cache.Stash(imgUrl, iw);
                }
                await iw.RetrieveLabels();
                await iw.RetrieveMatches();
                urls[index].AddChild(iw);
            });
        }

        private void SetStatus(string v)
        {
            Invoke(new MethodInvoker(() =>
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
            await SetSelectedURL(0);
        }

        private void urlChildUpdated(URLWrapper sender)
        {
            if (urls.IndexOf(sender) == selectedUrlIndex)
            {
                var w = urls[selectedUrlIndex];
                if (w.GetChildren() != null && w.GetChildren().Count == 1)
                    SetSelectedURL(selectedUrlIndex);
            }
        }

        private void SaveCurrentURL()
        {
            var w = urls[selectedUrlIndex];
            if (w.GetChildren() == null)
                MessageBox.Show("There are no images to save yet.", "Nothing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
            {
                bool notloaded = false;
                foreach (var c in w.GetChildren())
                {
                    if (c.GetFullImage() == null || !c.HasRetrievedMatches())
                    {
                        notloaded = true;
                        break;
                    }
                }
                if (notloaded && MessageBox.Show("Some images have not been scraped yet. Do you want to continue anyways?", "Missing", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                    return;
            }

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Please select a folder to save the sheet and the images.";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = dlg.SelectedPath;
                    string mp = Path.Combine(path, FOLDER_MARKER);
                    if (File.Exists(mp))
                    {
                        if (MessageBox.Show("The folder is already used by another URL, are sure that you want to overwrite the data?", "Used", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    }
                    else
                        File.Create(mp).Close();
                    File.SetAttributes(mp, FileAttributes.Hidden);
                    List<IWMatch> matches = new List<IWMatch>();
                    foreach (var iw in w.GetChildren())
                    {
                        if (iw.HasRetrievedMatches() && iw.GetSelectedMatch().GetFullImage() != null)
                        {
                            var m = iw.GetSelectedMatch();
                            string filename = GetFileName(path, matches, iw);
                            matches.Add(new IWMatch(iw, m, filename));
                        }
                    }

                    CSVEncoder.EncodeToFileAsync(matches, Path.Combine(path, "Log.csv"));

                    foreach (var m in matches)
                    {
                        string n = m.FilePrefix;
                        m.Original.GetFullImage().Save(Path.Combine(path, n + "_Original.jpg"));
                        m.Match.GetFullImage().Save(Path.Combine(path, n + "_Free.jpg"));
                    }
                }
                catch (Exception ex)
                {
                    Reporter.Report(ex);
                }
            }
        }

        public void LoadMissing()
        {
            if (selectedUrlIndex >= 0 && selectedUrlIndex < urls.Count)
            {
                var w = urls[selectedUrlIndex];
                if (w.GetChildren() == null)
                    SetSelectedURL(selectedUrlIndex);
                else
                    foreach (var iw in w.GetChildren())
                    {
                        if (iw.GetLabels() == null)
                            iw.RetrieveLabels();
                        if (iw.GetMatches() == null)
                            iw.RetrieveMatches();
                    }
            }
        }

        private string GetFileName(string path, List<IWMatch> matches, ImageWrapper original)
        {
            string[] files = Directory.GetFiles(path);
            List<string> taken = new List<string>();

            taken.AddRange(matches.Select(iw => iw.FilePrefix));

            string name = string.Join("_", original.GetLabels().Take(2).Select(x => x.Label));

            if (!taken.Contains(name))
                return name;

            int i = 1;
            while (taken.Contains(name + "(" + i + ")"))
                i++;

            return name + "(" + i + ")";
        }
    }
}
