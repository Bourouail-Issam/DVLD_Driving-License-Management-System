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

namespace DVLD__Driving_License_Management_System_.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        // Declare a delegate
        public delegate void DataBack_allowChangeTab();
        // Declare an event using the delegate
        public event DataBack_allowChangeTab allowChangeTab;

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private int _PersonID = -1;
        public int PersonID
        {
            get { return ctrPersonCard1.PersonId; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrPersonCard1.SelectedPersonInfo; }
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void FindNow()
        {
        
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ctrPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));                
                    break;

                case "National No.":
                     ctrPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }
            // Trigger the event to send data back to the caller form.
            allowChangeTab?.Invoke();
        }

        public void LoadPersonInfo(int PersonID)
        {

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show(
                    "Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                    );
                return;
            }
            FindNow();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.DataBack += DataBackEvent; // Subscribe to the event
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Trigger the event to send data back to the caller form.
            allowChangeTab?.Invoke();

            // Handle the data received
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrPersonCard1.LoadPersonInfo(PersonID);
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (cbFilterBy.Text == "National No.")
                e.Handled = (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
              
        }
    }
}
