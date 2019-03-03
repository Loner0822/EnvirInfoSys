using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PublishSys
{
    public partial class ucPictureBox : UserControl
    {
        private string _iconname = string.Empty;
        private string _iconpguid = string.Empty;
        private string _iconpath = string.Empty;
        private bool _iconcheck = false;

        public ucPictureBox()
        {
            InitializeComponent();
        }

        public string IconName
        {
            get
            {
                return _iconname;
            }
            set
            {
                _iconname = value;
                toolTip1.SetToolTip(label1, _iconname);
                this.label1.Text = _iconname;
            }
        }

        public string IconPguid
        {
            get
            {
                return _iconpguid;
            }
            set
            {
                _iconpguid = value;
            }
        }

        public string IconPath
        {
            get
            {
                return _iconpath;
            }
            set
            {
                _iconpath = value;
                this.pictureBox1.Load(_iconpath);
            }
        }

        public bool IconCheck
        {
            get
            {
                return _iconcheck;
            }
            set
            {
                _iconcheck = value;
                if (_iconcheck)
                    this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                else
                    this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }
        }

        public delegate void ClickHandle(object sender, EventArgs e, string iconguid);
        public event ClickHandle Single_Click;
        private void PB_Click(object sender, EventArgs e)
        {
            MouseEventArgs Mouse_e = (MouseEventArgs)e;  
            if (Single_Click != null && Mouse_e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Single_Click((object)this, new EventArgs(), _iconpguid);
            }
        }

        public delegate void DoubleClickHandle(object sender, EventArgs e, string iconguid);
        public event DoubleClickHandle Double_Click;
        private void PB_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs Mouse_e = (MouseEventArgs)e;
            if (Double_Click != null && Mouse_e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Double_Click((object)this, new EventArgs(), _iconpguid);
            }
        }

    }
}
