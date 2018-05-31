using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Freefy
{
    internal class Reporter
    {
        static DateTime lastShown;
        static TimeSpan coolDown = new TimeSpan(0, 0, 5);
        internal static void Report(Exception ex)
        {
            if (lastShown == null || DateTime.Now - lastShown > coolDown)
            {
                Debug.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lastShown = DateTime.Now;
            }
        }
    }
}