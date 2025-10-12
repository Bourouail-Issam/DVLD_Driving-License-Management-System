using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Driving_License_Management_System_
{
    public partial class controlMoveForm : UserControl
    {
        public controlMoveForm()
        {
            InitializeComponent();
        }

        private bool _MovePosition;
        private int _xCoordinate;
        private int _yCoordinate;
 

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panelMoveForm.Cursor = Cursors.Hand;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panelMoveForm.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
                parentForm.Close();
        }

        private void panelMoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            _MovePosition = true;
            _xCoordinate = e.X;
            _yCoordinate = e.Y;
        }
        qq


        private void panelMoveForm_MouseMove(object sender, MouseEventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
            {
                if (_MovePosition)
                {
                    parentForm.SetDesktopLocation(MousePosition.X - _xCoordinate, MousePosition.Y - _yCoordinate);
                }
            }
        }

        private void panelMoveForm_MouseUp(object sender, MouseEventArgs e)
        {
            _MovePosition = false;
        }

        private void panelMoveForm_MouseEnter(object sender, EventArgs e)
        {
            panelMoveForm.Cursor = Cursors.Hand;
        }

        private void panelMoveForm_MouseLeave(object sender, EventArgs e)
        {
            panelMoveForm.Cursor = Cursors.Default;
        }
    }
}
