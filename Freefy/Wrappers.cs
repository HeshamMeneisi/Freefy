using Freefy.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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

        internal void ClearChildren()
        {
            children = null;
            ChildrenUpdated?.Invoke(this);
        }

        public URLWrapper() { }

        public BindingList<ImageWrapper> GetChildren()
        {
            return children;
        }

        internal void AddChild(ImageWrapper w)
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

        static int P_DIM = 80;

        private Image img;

        public Image ImagePreview { get; private set; }

        private Dictionary<string, double> labels;

        bool similarRetrieved = false;
        private BindingList<ImageWrapper> matches = new BindingList<ImageWrapper>();
        private int selectedMatch = 0;

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
                if (Labeler.PreferredMethod == Labeler.Method.ByUrl)
                    labels = await Labeler.GetLabelsAsync(URL);
                else if (img != null)
                    labels = await Labeler.GetLabelsAsync(img);

                if (labels != null)
                    labels = labels.OrderByDescending(kv => kv.Value)
                        .Take(Settings.Default.NFirstLabels)
                        .ToDictionary(kv => kv.Key, kv => kv.Value);
            }
            catch (Exception ex) { Reporter.Report(ex); }
            finally { retrievingLabels = false; }
        }
        public async Task RetrieveMatches()
        {
            while (retrievingLabels) ;
            if (retrievingSimilar || labels == null) return;
            retrievingSimilar = true;
            await ImageLookup.GetSimilar(labels.Keys.ToArray(), async (imgUrl, size) =>
            {
                if (imgUrl == null)
                {
                    similarRetrieved = true;
                                    
                    if (Labeler.CurrentLabeler.CanRecommend)
                        SetSelectedMatch(await Labeler.GetRecommended(img, matches.Take(Settings.Default.RecCap).Select(i=>i.GetFullImage()).ToArray()));

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

        public Dictionary<string, double> GetLabels()
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

        internal BindingList<ImageWrapper> GetMatches()
        {
            return matches;
        }

        internal bool HasRetrievedMatches()
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

        internal ImageWrapper GetSelectedMatch()
        {
            return matches[selectedMatch];
        }
    }
}
