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
    public partial class frmAddUpdatePerson : Form
    {


        public enum enMode { AddNew = 0, Update = 1 };
        private int _PersonID = -1;

        public frmAddUpdatePerson()
        {
            InitializeComponent();
        }
        public frmAddUpdatePerson(int PersonID )
        {
            InitializeComponent();
            this._PersonID = PersonID;
        }
    }
}
