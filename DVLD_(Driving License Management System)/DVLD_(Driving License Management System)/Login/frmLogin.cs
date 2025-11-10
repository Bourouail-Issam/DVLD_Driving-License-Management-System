using DVLD__Driving_License_Management_System_.Global_Classes;
using DVLD__Driving_License_Management_System_.User;
using DVLD_BuisnessDVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD__Driving_License_Management_System_.Login
{
    public partial class frmLogin : Form
    {
        private FormMover _formMover;

        public frmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _formMover = new FormMover(this,panelMoveForm);
            _formMover = new FormMover(this,panelSideBar);

            string Username = "", Password = "";
            if (clsGlobal.GetStoredCredential(ref Username, ref Password))
            {
                txtUserName.Text = Username;
                txtPassword.Text = Password;
                cbRememberMe.Checked = true;
            }
            else
                cbRememberMe.Checked = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!cbRememberMe.Checked)
            {
                //store empty username and password
                clsGlobal.RememberUsernameAndPassword("", "");
            }
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = txtUserName.Text , Password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(Username))
            {
                txtUserName.Focus();
                MessageBox.Show
                    ("Please enter username.",
                     "Missing Information",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning
                    );
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                txtPassword.Focus();
                MessageBox.Show
                    ("Please enter Password.",
                     "Missing Information",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning
                    );
                return;
            }

            clsUser user = clsUser.FindByUsernameAndPassword(Username, Password);

            if (user != null)
            {
                if (cbRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPassword(Username, Password);
                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                //incase the user is not active
                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show
                        (
                        "Your accound is not Active, Contact Admin.",
                        "In Active Account", 
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain();
                frm.ShowDialog();
                this.Show();
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show
                    (
                    "Invalid Username/Password.",
                    "Wrong Credintials", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
                txtPassword.PasswordChar = '\0';
            else
                txtPassword.PasswordChar = '*';
        }
    }
}
