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

namespace DVLD__Driving_License_Management_System_.User
{
    public partial class frmListUsers : Form
    {
        private frmMain _frmMain;
        private static DataTable _dtAllUsers;
        public frmListUsers(frmMain frmMain)
        {
            InitializeComponent();
            this._frmMain = frmMain;
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lbRecords.Text = dgvUsers.Rows.Count.ToString();


            dgvUsers.Columns[0].HeaderText = "User ID";
            dgvUsers.Columns[0].Width = 110;

            dgvUsers.Columns[1].HeaderText = "Person ID";
            dgvUsers.Columns[1].Width = 120;

            dgvUsers.Columns[2].HeaderText = "Full Name";
            dgvUsers.Columns[2].Width = 350;

            dgvUsers.Columns[3].HeaderText = "UserName";
            dgvUsers.Columns[3].Width = 120;

            dgvUsers.Columns[4].HeaderText = "Is Active";
            dgvUsers.Columns[4].Width = 120;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _frmMain.MakeMainPictureVisible();
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "IsActive")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                cbIsActive.Visible = false;
                txtFilterValue.Visible = (cbFilterBy.Text != "None");

                if (cbFilterBy.Text == "None")
                    txtFilterValue.Enabled = false;
                else
                    txtFilterValue.Enabled = true;

                _FilterDgvUser("", "");
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void _FilterDgvUser(string columnName, string txtFilterValue)
        {

            if (!String.IsNullOrWhiteSpace(txtFilterValue))
            {
                if (columnName == "IsActive")
                    _dtAllUsers.DefaultView.RowFilter = $"{columnName} = {txtFilterValue}";
                else
                    _dtAllUsers.DefaultView.RowFilter = $"CONVERT({columnName}, 'System.String') LIKE '{txtFilterValue}%'";
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = "";
            }
            lbRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lbRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }
            _FilterDgvUser(FilterColumn, txtFilterValue.Text);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                _FilterDgvUser(FilterColumn, FilterValue);
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmAddUpdateUser frm = new frmAddUpdateUser(UserID);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow?.Cells[0].Value;
            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show(
                    "User has been deleted successfully",
                    "Deleted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
                frmListUsers_Load(null, null);
            }
            else
                MessageBox.Show(
                    "User is not delted due to data connected to it.",
                    "Faild",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
        }
    }
}
