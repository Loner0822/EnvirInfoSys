using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;

namespace PublishSys
{
    public partial class PublishSelect : DevExpress.XtraEditors.XtraForm
    {
        private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
        public List<string> proglist = new List<string>();

        public PublishSelect()
        {
            InitializeComponent();
        }

        private void PublishSelect_Load(object sender, EventArgs e)
        {
            checkedListBoxControl1.Items.Clear();
            foreach (string prog in proglist)
                checkedListBoxControl1.Items.Add(prog);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxControl1.Items.Count; ++i)
            {
                string progname = checkedListBoxControl1.Items[i].Value.ToString();
                if (checkedListBoxControl1.Items[i].CheckState == CheckState.Checked)
                {
                    Process proc = Process.Start(WorkPath + "Inno Setup\\Compil32.exe", "/cc \"" + WorkPath + progname + ".iss\"");
                    proc.WaitForExit();
                }
            }
        }
    }
}