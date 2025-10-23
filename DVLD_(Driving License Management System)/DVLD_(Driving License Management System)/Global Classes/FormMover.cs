using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Driving_License_Management_System_.Global_Classes
{
    /// <summary>
    /// This class lets you move a borderless form
    /// by dragging a control (for example, a panel or header).
    /// </summary>
    public class FormMover
    {
        private bool _isMoving = false; // True while dragging the form 

        private int _x = 0, _y = 0; // Mouse position inside the control

        private Form _targetForm; // The form to move

        private Control _targetMoveArea; // The control used to move the form


        public FormMover(Form form, Control moveArea)
        {
            _targetForm = form ?? throw new ArgumentNullException(nameof(form));
            _targetMoveArea = moveArea ?? throw new ArgumentNullException(nameof(moveArea));

            // Connect mouse events to the move area
            moveArea.MouseDown += MoveArea_MouseDown;
            moveArea.MouseMove += MoveArea_MouseMove;
            moveArea.MouseUp += MoveArea_MouseUp;
            moveArea.MouseLeave += MoveArea_MouseLeave;
            moveArea.MouseEnter += MoveArea_MouseEnter;
        }

        private void MoveArea_MouseDown(object sender, MouseEventArgs e)
        {
            _isMoving = true;
            _x = e.X;
            _y = e.Y;
        }

        private void MoveArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMoving)
            {
                _targetForm.SetDesktopLocation(
                    Control.MousePosition.X - _x,
                    Control.MousePosition.Y - _y
                );
            }
        }

        private void MoveArea_MouseUp(object sender, MouseEventArgs e)
        {
            _isMoving = false;
        }

        private void MoveArea_MouseLeave(object sender, EventArgs e)
        {
            _targetMoveArea.Cursor = Cursors.Default;
        }

        private void MoveArea_MouseEnter(object sender, EventArgs e)
        {
            _targetMoveArea.Cursor = Cursors.SizeAll;
        }
    }
}
