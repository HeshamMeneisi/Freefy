namespace Freefy
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.urlGrid = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.openBtn = new System.Windows.Forms.ToolStripButton();
            this.clearCache = new System.Windows.Forms.ToolStripButton();
            this.clearUrl = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.imgList = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.mWidth = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.mHeight = new System.Windows.Forms.ToolStripTextBox();
            this.topPan = new System.Windows.Forms.Panel();
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.formTitle = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.splitContainerTop = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.labelsText = new System.Windows.Forms.RichTextBox();
            this.imgPrev = new System.Windows.Forms.PictureBox();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.urlGrid)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgList)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.topPan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTop)).BeginInit();
            this.splitContainerTop.Panel1.SuspendLayout();
            this.splitContainerTop.Panel2.SuspendLayout();
            this.splitContainerTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPrev)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText});
            this.statusStrip.Location = new System.Drawing.Point(0, 576);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(814, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "Status";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.urlGrid);
            this.groupBox1.Controls.Add(this.toolStrip2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 195);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Source";
            // 
            // urlGrid
            // 
            this.urlGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.urlGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.urlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlGrid.Location = new System.Drawing.Point(3, 41);
            this.urlGrid.Name = "urlGrid";
            this.urlGrid.Size = new System.Drawing.Size(394, 151);
            this.urlGrid.TabIndex = 0;
            this.urlGrid.SelectionChanged += new System.EventHandler(this.urlGrid_SelectionChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBtn,
            this.clearUrl,
            this.clearCache});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(394, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // openBtn
            // 
            this.openBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openBtn.Image = ((System.Drawing.Image)(resources.GetObject("openBtn.Image")));
            this.openBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(23, 22);
            this.openBtn.Text = "Open File";
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // clearCache
            // 
            this.clearCache.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearCache.Image = ((System.Drawing.Image)(resources.GetObject("clearCache.Image")));
            this.clearCache.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearCache.Name = "clearCache";
            this.clearCache.Size = new System.Drawing.Size(23, 22);
            this.clearCache.Text = "Clear Cache";
            this.clearCache.Click += new System.EventHandler(this.clearCache_Click);
            // 
            // clearUrl
            // 
            this.clearUrl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearUrl.Image = ((System.Drawing.Image)(resources.GetObject("clearUrl.Image")));
            this.clearUrl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearUrl.Name = "clearUrl";
            this.clearUrl.Size = new System.Drawing.Size(23, 22);
            this.clearUrl.Text = "Reload Images";
            this.clearUrl.Click += new System.EventHandler(this.clearUrl_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imgList);
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 195);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Images";
            // 
            // imgList
            // 
            this.imgList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.imgList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.imgList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.imgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgList.Location = new System.Drawing.Point(3, 41);
            this.imgList.Name = "imgList";
            this.imgList.Size = new System.Drawing.Size(404, 151);
            this.imgList.TabIndex = 0;
            this.imgList.SelectionChanged += new System.EventHandler(this.imgList_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.mWidth,
            this.toolStripLabel2,
            this.mHeight});
            this.toolStrip1.Location = new System.Drawing.Point(3, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(404, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(51, 22);
            this.toolStripLabel1.Text = "Min Size";
            // 
            // mWidth
            // 
            this.mWidth.Name = "mWidth";
            this.mWidth.Size = new System.Drawing.Size(100, 25);
            this.mWidth.Text = "200";
            this.mWidth.TextChanged += new System.EventHandler(this.mWidth_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(14, 22);
            this.toolStripLabel2.Text = "X";
            // 
            // mHeight
            // 
            this.mHeight.Name = "mHeight";
            this.mHeight.Size = new System.Drawing.Size(100, 25);
            this.mHeight.Text = "200";
            this.mHeight.TextChanged += new System.EventHandler(this.mWidth_TextChanged);
            // 
            // topPan
            // 
            this.topPan.BackColor = System.Drawing.Color.Orange;
            this.topPan.Controls.Add(this.iconBox);
            this.topPan.Controls.Add(this.formTitle);
            this.topPan.Controls.Add(this.button2);
            this.topPan.Controls.Add(this.closeBtn);
            this.topPan.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.topPan.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPan.Location = new System.Drawing.Point(0, 0);
            this.topPan.Name = "topPan";
            this.topPan.Size = new System.Drawing.Size(814, 39);
            this.topPan.TabIndex = 5;
            this.topPan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseDown);
            this.topPan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseMove);
            this.topPan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseUp);
            // 
            // iconBox
            // 
            this.iconBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.iconBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.iconBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconBox.Location = new System.Drawing.Point(0, 0);
            this.iconBox.Name = "iconBox";
            this.iconBox.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.iconBox.Size = new System.Drawing.Size(45, 39);
            this.iconBox.TabIndex = 2;
            this.iconBox.TabStop = false;
            // 
            // formTitle
            // 
            this.formTitle.AutoSize = true;
            this.formTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.formTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.formTitle.Location = new System.Drawing.Point(51, 7);
            this.formTitle.Name = "formTitle";
            this.formTitle.Size = new System.Drawing.Size(73, 26);
            this.formTitle.TabIndex = 1;
            this.formTitle.Text = "Freefy";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Orange;
            this.button2.Location = new System.Drawing.Point(724, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(45, 39);
            this.button2.TabIndex = 0;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.minBtn_Click);
            this.button2.MouseEnter += new System.EventHandler(this.topBtn_MouseEnterLeave);
            this.button2.MouseLeave += new System.EventHandler(this.topBtn_MouseEnterLeave);
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.Black;
            this.closeBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.closeBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.ForeColor = System.Drawing.Color.Orange;
            this.closeBtn.Location = new System.Drawing.Point(769, 0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(45, 39);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.Text = "X";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            this.closeBtn.MouseEnter += new System.EventHandler(this.topBtn_MouseEnterLeave);
            this.closeBtn.MouseLeave += new System.EventHandler(this.topBtn_MouseEnterLeave);
            // 
            // groupBox3
            // 
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(539, 342);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Matches";
            // 
            // splitContainerTop
            // 
            this.splitContainerTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainerTop.Location = new System.Drawing.Point(0, 39);
            this.splitContainerTop.Name = "splitContainerTop";
            // 
            // splitContainerTop.Panel1
            // 
            this.splitContainerTop.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainerTop.Panel2
            // 
            this.splitContainerTop.Panel2.Controls.Add(this.groupBox2);
            this.splitContainerTop.Size = new System.Drawing.Size(814, 195);
            this.splitContainerTop.SplitterDistance = 400;
            this.splitContainerTop.TabIndex = 7;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 234);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.labelsText);
            this.splitContainer2.Panel1.Controls.Add(this.imgPrev);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(814, 342);
            this.splitContainer2.SplitterDistance = 271;
            this.splitContainer2.TabIndex = 8;
            // 
            // labelsText
            // 
            this.labelsText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelsText.Location = new System.Drawing.Point(0, 0);
            this.labelsText.Name = "labelsText";
            this.labelsText.ReadOnly = true;
            this.labelsText.Size = new System.Drawing.Size(271, 112);
            this.labelsText.TabIndex = 1;
            this.labelsText.Text = "";
            // 
            // imgPrev
            // 
            this.imgPrev.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.imgPrev.Location = new System.Drawing.Point(0, 112);
            this.imgPrev.Name = "imgPrev";
            this.imgPrev.Size = new System.Drawing.Size(271, 230);
            this.imgPrev.TabIndex = 0;
            this.imgPrev.TabStop = false;
            // 
            // statusText
            // 
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(26, 17);
            this.statusText.Text = "Idle";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(814, 598);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.splitContainerTop);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.topPan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrm";
            this.Text = "Freefy";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.urlGrid)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgList)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.topPan.ResumeLayout(false);
            this.topPan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            this.splitContainerTop.Panel1.ResumeLayout(false);
            this.splitContainerTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTop)).EndInit();
            this.splitContainerTop.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgPrev)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView urlGrid;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton openBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel topPan;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label formTitle;
        private System.Windows.Forms.PictureBox iconBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainerTop;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox labelsText;
        private System.Windows.Forms.PictureBox imgPrev;
        private System.Windows.Forms.DataGridView imgList;
        private System.Windows.Forms.ToolStripButton clearCache;
        private System.Windows.Forms.ToolStripButton clearUrl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox mWidth;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox mHeight;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
    }
}

