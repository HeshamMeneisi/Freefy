using Freefy.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Freefy
{
    public partial class settingsFrm : Form
    {
        class ComboItem
        {
            private string n;
            private object t;

            public ComboItem(string n, object t)
            {
                this.n = n;
                this.t = t;
            }

            public string Display { get => n; set => n = value; }
            public object Underlying { get => t; set => t = value; }

            public override string ToString()
            {
                return Display;
            }
        }
        public settingsFrm()
        {
            InitializeComponent();
            foreach(var n in Enum.GetNames(typeof(Labeler.LabelerType)))
            {
                var t = Enum.Parse(typeof(Labeler.LabelerType), n);
                labelerCombo.Items.Add(new ComboItem(n, t));
            }

            foreach (var n in Enum.GetNames(typeof(Labeler.Method)))
            {
                var t = Enum.Parse(typeof(Labeler.Method), n);
                methodCombo.Items.Add(new ComboItem(n, t));
            }
        }

        private void settingsFrm_Load(object sender, EventArgs e)
        {
            labelerCombo.Text = Enum.GetName(typeof(Labeler.LabelerType), (Labeler.LabelerType)Settings.Default.APIType);
            methodCombo.Text = Enum.GetName(typeof(Labeler.Method), (Labeler.Method)Settings.Default.APIMethod);
            serverTxt.Text = Settings.Default.ServerHost;
            CAPITxt.Text = Settings.Default.ClarifaiAPIKey;
            LQTxt.Text = Settings.Default.LookupQuery;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Settings.Default.APIType = (int)Enum.Parse(typeof(Labeler.LabelerType), labelerCombo.Text);
            Settings.Default.APIMethod = (int)Enum.Parse(typeof(Labeler.Method), methodCombo.Text);
            Settings.Default.ServerHost = serverTxt.Text;
            Settings.Default.ClarifaiAPIKey = CAPITxt.Text;
            Settings.Default.LookupQuery = LQTxt.Text;
            Settings.Default.Save();

            Labeler.Reset();
        }
    }
}
