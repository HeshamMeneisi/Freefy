using System;
using System.Windows.Forms;

namespace Freefy
{
    internal class Reporter
    {
        internal static void Report(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}