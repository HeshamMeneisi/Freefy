using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Freefy
{
    class Labeler
    {
        static ImageLabeler labeler = new DummyLabeler();

        public static async Task<Dictionary<string, double>> GetLabelsAsync(string url)
        {
            return await labeler.GetLabelsAsync(url);
        }

        public static async Task<Dictionary<string, double>> GetLabelsAsync(Image img)
        {
            return await labeler.GetLabelsAsync(img);
        }
    }

    class URLWrapper
    {

        public delegate void WrapperUpdatedEventHandler(URLWrapper self);
        public event WrapperUpdatedEventHandler ChildrenUpdated;

        public string URL { get; set; }

        private string[] keys = new string[] { "url", "URL", "Url" };

        private List<ImageWrapper> children;

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

        public void SetChildren(List<ImageWrapper> children)
        {
            this.children = children;
            ChildrenUpdated?.Invoke(this);
        }

        public List<ImageWrapper> GetChildren()
        {
            return children;
        }

        internal void AddChild(ImageWrapper w)
        {
            if (children == null)
                children = new List<ImageWrapper>();
            children.Add(w);
            ChildrenUpdated?.Invoke(this);
        }
    }

    class ImageWrapper : URLWrapper
    {
        public event WrapperUpdatedEventHandler ImageUpdated;

        static int P_DIM = 50;

        private Image img;

        public Image ImagePreview { get; private set; }

        private Dictionary<string, double> Labels;

        public ImageWrapper(string url) : base(url)
        {
            RetrieveImage();
            RetrieveLabels();
        }
        bool retrievingLabels = false;
        public async Task RetrieveLabels()
        {
            if(retrievingLabels) return;
            retrievingLabels = true;
            try
            {
                Labels = await Labeler.GetLabelsAsync(URL);
                foreach (var k in Labels.Keys)
                    Debug.WriteLine(k + " " + Labels[k]);
            }
            catch(Exception ex) { Reporter.Report(ex); }
            finally { retrievingLabels = false; }
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
            return Labels;
        }

        bool retrievingImage;
        public async Task RetrieveImage()
        {
            if (retrievingImage) return;
            retrievingImage = true;
            try
            {
                HttpHelper.TryGetImage(URL, out img);
                UpdatePreview();
                ImageUpdated?.Invoke(this);
            }
            catch
            {
                ImageScraper.TryGetImage(URL, (bmp) =>
                 {
                     img = bmp;
                     UpdatePreview();
                     ImageUpdated?.Invoke(this);
                 });
            }
            finally
            {
                retrievingImage = false;
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
    }
}
