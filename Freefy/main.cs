using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Freefy
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
            urlGrid.DataSource = urls;
            imgList.DataSource = current_images;

            urls.AllowNew = urls.AllowEdit = urls.AllowRemove = true;

            Icon appIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            iconBox.Image = appIcon.ToBitmap();
        }

        #region Window
        bool moving = false;
        int x, y;
        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to exit?", "Exiting", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                Application.Exit();
        }

        private void minBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        private void toolStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }

        private void toolStrip1_MouseLeave(object sender, EventArgs e)
        {
            moving = false;
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                int dx = e.X - x;
                int dy = e.Y - y;
                Location = new Point(Location.X + dx, Location.Y + dy);
            }
        }

        private void topBtn_MouseEnterLeave(object sender, EventArgs e)
        {
            var c = (Button)sender;
            var b = c.BackColor;
            c.BackColor = c.ForeColor;
            c.ForeColor = b;
        }
        #endregion   

        private async void openBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Comma separated file (*.csv)|*.csv|Text files (*.txt)|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                await LoadFile(dlg.FileName);
            }
        }

        private async void imgList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (imgList.SelectedRows.Count == 1)
                await SetSelectedImg(imgList.SelectedRows[0].Index);
        }

        private void clearCache_Click(object sender, EventArgs e)
        {
            WebBrowserHelper.ClearCache();
        }

        private async void clearUrl_Click(object sender, EventArgs e)
        {
            if (urlGrid.SelectedRows.Count == 1)
            {
                await ReloadURL(urlGrid.SelectedRows[0].Index);
            }
        }

        private void mWidth_TextChanged(object sender, EventArgs e)
        {
            var t = (TextBox)sender;
            int i;
            while (!int.TryParse(t.Text, out i))
                t.Text = t.Text.Substring(0, t.Text.Length - 1);
        }

        private async void urlGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (urlGrid.SelectedRows.Count == 1)
                await SetSelectedURL(urlGrid.SelectedRows[0].Index);
        }
    }
}