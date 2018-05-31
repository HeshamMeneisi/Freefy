using Freefy.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace Freefy
{
    class URLWrapper
    {

        public delegate void WrapperUpdatedEventHandler(URLWrapper self);
        public event WrapperUpdatedEventHandler ChildrenUpdated;

        public string URL { get; set; }

        private string[] keys = new string[] { "url", "URL", "Url" };

        private BindingList<ImageWrapper> children;

        public URLWrapper(Dictionary<string, string> row)
        {
            foreach (var key in keys)
                if (row.Keys.Contains(key))
                {
                    URL = row[key];
                    break;
                }
        }

        public URLWrapper(string url)
        {
            URL = url;
        }

        public void ClearChildren()
        {
            children = null;
            ChildrenUpdated?.Invoke(this);
        }

        public URLWrapper() { }

        public BindingList<ImageWrapper> GetChildren()
        {
            return children;
        }

        public void AddChild(ImageWrapper w)
        {
            if (children == null)
                children = new BindingList<ImageWrapper>();
            children.Add(w);
            ChildrenUpdated?.Invoke(this);
        }
    }

    class ImageWrapper : URLWrapper
    {
        public event WrapperUpdatedEventHandler ImageUpdated;
        public event WrapperUpdatedEventHandler SimilarFound;
        public event WrapperUpdatedEventHandler RecommendationMade;
        public event WrapperUpdatedEventHandler LabelsRetrieved;

        static int P_DIM = 80;

        private Image img;

        public Image ImagePreview { get; private set; }

        bool labelsRetrieved = false;
        private BindingList<ImageLabel> labels = new BindingList<ImageLabel>();

        bool similarRetrieved = false;
        private BindingList<ImageWrapper> matches = new BindingList<ImageWrapper>();
        private int selectedMatch = 0;

        private static string[] wordBlackList = new string[]
        {
            "compressed","compress","auto","big","medium","small","large","jpeg","jpg","png","bmp","gif",
            "tinysrgb","rgb","argb","srgb","gallery","horizontal","vertical", "body", "story", "copyright",
            "pexels", "photo", "http", "com", "net", "org", "cnn", "bbc", "fcdn", "fdam", "fcnnnext", "fassets"
        };
        public string FileLabels
        {
            get
            {
                string l = "";
                string fname = URL.Split('/').Last();
                foreach (Match m in Regex.Matches(fname.ToLower(), "(^|[^a-zA-Z])([a-zA-z]{3,8})([^a-zA-Z]|$)"))
                    if (!wordBlackList.Contains(m.Groups[2].Value))
                        l += m.Groups[2].Value + " ";
                return l.Trim();
            }
        }

        public ImageWrapper(string url) : base(url)
        {
            RetrieveImage();
        }
        bool retrievingLabels = false;
        public async Task RetrieveLabels()
        {
            if (retrievingLabels) return;
            retrievingLabels = true;
            try
            {
                Dictionary<string, double> r = null;
                if (Labeler.PreferredMethod == Labeler.Method.ByUrl)
                    r = await Labeler.GetLabelsAsync(URL);
                else if (img != null)
                    r = await Labeler.GetLabelsAsync(img);

                if (r != null)
                {
                    r = r.OrderByDescending(kv => kv.Value)
                        .Take(Settings.Default.NFirstLabels)
                        .ToDictionary(kv => kv.Key, kv => kv.Value);
                    foreach (var keyvalue in r)
                    {
                        labels.Add(new ImageLabel(keyvalue.Key, keyvalue.Value));
                    }
                    labelsRetrieved = true;
                    LabelsRetrieved?.Invoke(this);
                }
            }
            catch (Exception ex) { Reporter.Report(ex); }
            finally { retrievingLabels = false; }
        }

        internal bool HasRetrievedLabels()
        {
            return labelsRetrieved;
        }

        public async Task RetrieveMatches()
        {
            while (retrievingLabels) ;
            if (retrievingSimilar || labels == null) return;
            retrievingSimilar = true;
            var list = new List<string>();
            list.AddRange(labels.Select(l => l.Label));
            bool skip = false;
            var flist = new List<string>();
            foreach (string fl in FileLabels.Split(' ').Take(Settings.Default.FileNameLabels))
            {
                foreach (string label in list)
                    if (label.Contains(fl))
                    {
                        skip = true;
                        break;
                    }
                if (skip)
                    break;
                flist.Add(fl);
            }
            if (!skip)
                list.AddRange(flist);

            await ImageLookup.GetSimilar(list.ToArray(), async (imgUrl, size) =>
             {
                 if (imgUrl == null)
                 {
                     similarRetrieved = true;

                     if (Labeler.CurrentLabeler.CanRecommend)
                     {
                         var thread = new Thread(() =>
                         {
                             var r = Labeler.GetRecommended(img, matches.Take(Settings.Default.RecCap).Select(i => i.GetFullImage()).ToArray()).Result;
                             SetSelectedMatch(r);
                             RecommendationMade?.Invoke(this);
                         });
                         thread.Start();
                     }

                     retrievingSimilar = false;
                     return;
                 }
                 //if (size.Width < Settings.Default.MinWidth && size.Height < Settings.Default.MinHeight) return;
                 ImageWrapper iw;
                 if (!Cache.Lookup(imgUrl, out iw))
                 {
                     iw = new ImageWrapper(imgUrl);
                     Cache.Stash(imgUrl, iw);
                 }
                 matches.Add(iw);
                 SimilarFound?.Invoke(this);
             });
        }
        public ImageWrapper(string url, Image img) : base(url)
        {
            this.img = img;
        }

        public Image GetFullImage()
        {
            return img;
        }

        public Image GetThumb(int w, int h)
        {
            if (img == null)
                return null;
            return img.GetThumbnailImage(w, h, null, IntPtr.Zero);
        }

        public BindingList<ImageLabel> GetLabels()
        {
            return labels;
        }

        bool retrievingImage;
        private bool retrievingSimilar;

        public async Task RetrieveImage()
        {
            if (retrievingImage) return;
            retrievingImage = true;
            try
            {
                HttpHelper.TryGetImage(URL, out img);
                UpdatePreview();
                ImageUpdated?.Invoke(this);
                retrievingImage = false;
            }
            catch
            {
                ImageScraper.TryGetImage(URL, (bmp) =>
                 {
                     if (bmp == null) return;
                     img = bmp;
                     UpdatePreview();
                     ImageUpdated?.Invoke(this);
                     retrievingImage = false;
                 });
            }
        }

        private void UpdatePreview()
        {
            if (img != null)
            {
                float w = img.Width, h = img.Height;
                float r = w / h;
                if (w > h)
                {
                    w = P_DIM;
                    h = w / r;
                }
                else
                {
                    h = P_DIM;
                    w = h * r;
                }
                ImagePreview = img.GetThumbnailImage((int)w, (int)h, null, IntPtr.Zero);
            }
        }

        public BindingList<ImageWrapper> GetMatches()
        {
            return matches;
        }

        public bool HasRetrievedMatches()
        {
            return similarRetrieved;
        }

        public int GetSelectedMatchIndex()
        {
            return selectedMatch;
        }

        public void SetSelectedMatch(int idx)
        {
            if (idx < matches.Count && idx >= 0)
                selectedMatch = idx;
        }

        public ImageWrapper GetSelectedMatch()
        {
            return matches[selectedMatch];
        }
    }

    public class ImageLabel
    {
        private double value;

        public ImageLabel(string key, double value)
        {
            Label = key;
            this.value = value;
        }

        public string Label { get; set; }
        public string Confidence { get { return (value * 100).ToString("0.00") + "%"; } }
    }
}
