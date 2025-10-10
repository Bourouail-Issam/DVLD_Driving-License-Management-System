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

namespace DVLD__Driving_License_Management_System_.People
{
    public partial class frmPeople : Form
    {
        private frmMain _frmMain;
        private static DataTable _dtAllPeople = clsPersoon.GetAllPersons();


        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GendorCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");

        public frmPeople(frmMain frmMain)
        {
            InitializeComponent();
            this._frmMain = frmMain;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

      
        private void frmPeople_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            dgvAllPeopleData.DataSource = _dtPeople;
            cbFilter.SelectedIndex = 0;
            lbRecords.Text = dgvAllPeopleData.Rows.Count.ToString();

            if(dgvAllPeopleData.Rows.Count > 0)
            {
                dgvAllPeopleData.Columns[0].HeaderText = "Person ID";
                dgvAllPeopleData.Columns[0].Width = 110;

                dgvAllPeopleData.Columns[1].HeaderText = "National NO";
                dgvAllPeopleData.Columns[1].Width = 120;

                dgvAllPeopleData.Columns[2].HeaderText = "First Name";
                dgvAllPeopleData.Columns[2].Width = 120;

                dgvAllPeopleData.Columns[3].HeaderText = "Second Name";
                dgvAllPeopleData.Columns[3].Width = 140;

                dgvAllPeopleData.Columns[4].HeaderText = "Third Name";
                dgvAllPeopleData.Columns[4].Width = 120;

                dgvAllPeopleData.Columns[5].HeaderText = "Last Name";
                dgvAllPeopleData.Columns[5].Width = 120;

                dgvAllPeopleData.Columns[6].HeaderText = "Gendor";
                dgvAllPeopleData.Columns[6].Width = 120;

                dgvAllPeopleData.Columns[7].HeaderText = "Date of Birth";
                dgvAllPeopleData.Columns[7].Width = 140;

                dgvAllPeopleData.Columns[8].HeaderText = "Nationalty";
                dgvAllPeopleData.Columns[8].Width = 120;

                dgvAllPeopleData.Columns[9].HeaderText = "Phone";
                dgvAllPeopleData.Columns[9].Width = 120;

                dgvAllPeopleData.Columns[10].HeaderText = "Email";
                dgvAllPeopleData.Columns[10].Width = 170;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _frmMain.MakeMainPictureVisible();
            this.Close();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cbFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lbRecords.Text = dgvAllPeopleData.Rows.Count.ToString();
                return;
            }

            _FilterDgvPeople(FilterColumn, txtFilterValue.Text);
        }

        private void _FilterDgvPeople(string columnName, string txtFilterValue)
        { 

            if (!String.IsNullOrWhiteSpace(txtFilterValue))
            {
                if (columnName == "PersoonID")
                    _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}",columnName,txtFilterValue);
                else
                    _dtPeople.DefaultView.RowFilter = $"CONVERT({columnName}, 'System.String') LIKE '{txtFilterValue}%'";

                dgvAllPeopleData.DataSource = _dtPeople;
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = "";
            }
            lbRecords.Text = dgvAllPeopleData.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilter.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
