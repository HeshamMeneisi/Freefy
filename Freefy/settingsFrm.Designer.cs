namespace Freefy
{
    partial class settingsFrm
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
            this.labelerCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.methodCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.serverTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CAPITxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LQTxt = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.filenameLabels = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.filenameLabels)).BeginInit();
            this.SuspendLayout();
            // 
            // labelerCombo
            // 
            this.labelerCombo.FormattingEnabled = true;
            this.labelerCombo.Location = new System.Drawing.Point(124, 9);
            this.labelerCombo.Name = "labelerCombo";
            this.labelerCombo.Size = new System.Drawing.Size(148, 21);
            this.labelerCombo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Labeler";
            // 
            // methodCombo
            // 
            this.methodCombo.FormattingEnabled = true;
            this.methodCombo.Location = new System.Drawing.Point(124, 36);
            this.methodCombo.Name = "methodCombo";
            this.methodCombo.Size = new System.Drawing.Size(148, 21);
            this.methodCombo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Method";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Server";
            // 
            // serverTxt
            // 
            this.serverTxt.Location = new System.Drawing.Point(124, 63);
            this.serverTxt.Name = "serverTxt";
            this.serverTxt.Size = new System.Drawing.Size(148, 20);
            this.serverTxt.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Clarifai API Key";
            // 
            // CAPITxt
            // 
            this.CAPITxt.Location = new System.Drawing.Point(124, 89);
            this.CAPITxt.Name = "CAPITxt";
            this.CAPITxt.Size = new System.Drawing.Size(148, 20);
            this.CAPITxt.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Lookup Query";
            // 
            // LQTxt
            // 
            this.LQTxt.Location = new System.Drawing.Point(124, 115);
            this.LQTxt.Name = "LQTxt";
            this.LQTxt.Size = new System.Drawing.Size(148, 20);
            this.LQTxt.TabIndex = 2;
            // 
            // saveBtn
            // 
            this.saveBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.saveBtn.Location = new System.Drawing.Point(0, 183);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(284, 23);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "File name labels";
            // 
            // filenameLabels
            // 
            this.filenameLabels.Location = new System.Drawing.Point(124, 147);
            this.filenameLabels.Name = "filenameLabels";
            this.filenameLabels.Size = new System.Drawing.Size(148, 20);
            this.filenameLabels.TabIndex = 5;
            // 
            // settingsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 206);
            this.Controls.Add(this.filenameLabels);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.LQTxt);
            this.Controls.Add(this.CAPITxt);
            this.Controls.Add(this.serverTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.methodCombo);
            this.Controls.Add(this.labelerCombo);
            this.Name = "settingsFrm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.settingsFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.filenameLabels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox labelerCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox methodCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox serverTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CAPITxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LQTxt;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown filenameLabels;
    }
}