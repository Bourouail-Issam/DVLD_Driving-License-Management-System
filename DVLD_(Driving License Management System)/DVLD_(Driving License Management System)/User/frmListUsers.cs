using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Driving_License_Management_System_.User
{
    public partial class frmListUsers : Form
    {
        private frmMain _frmMain;
        public frmListUsers(frmMain frmMain)
        {
            InitializeComponent();
            this._frmMain = frmMain;
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _frmMain.MakeMainPictureVisible();
            this.Close();
        }
    }
}
