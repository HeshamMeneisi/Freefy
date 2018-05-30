using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Utilities
{
    internal class Helper
    {
        internal static byte[] GetImageBytes(Image img)
        {
            using (var ms = new MemoryStream())
            {
                try
                {
                    img.Save(ms, img.RawFormat);
                }
                catch
                {
                    img.Save(ms, ImageFormat.Bmp);
                }
                return ms.ToArray();
            }
        }
    }
}