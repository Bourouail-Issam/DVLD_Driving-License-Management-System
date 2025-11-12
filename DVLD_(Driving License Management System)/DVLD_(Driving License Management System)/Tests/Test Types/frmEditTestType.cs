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

namespace DVLD__Driving_License_Management_System_.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
        private clsTestType.enTestType _testTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _testType;
        public frmEditTestType(clsTestType.enTestType testTypeID)
        {
            InitializeComponent();
            _testTypeID = testTypeID;
        }

     
    }
}
