using System;
using System.Windows.Forms;

namespace DVLD__Driving_License_Management_System_.Global_Classes
{
    /// <summary>
    /// This class lets you move a borderless form
    /// by dragging a control (for example, a panel or header).
    /// </summary>
    public class FormMover : IDisposable
    {
        #region Private Fields

        /// <summary>True while the user is actively dragging the form.</summary>
        private bool _isMoving = false;

        /// <summary>Mouse X offset within the drag control when dragging started.</summary>
        private int _startX = 0;

        /// <summary>Mouse Y offset within the drag control when dragging started.</summary>
        private int _startY = 0;

        /// <summary>The form that will be repositioned during drag.</summary>
        private readonly Form _targetForm;

        /// <summary>The control the user interacts with to drag the form.</summary>
        private readonly Control _dragArea;

        /// <summary>Prevents double disposal.</summary>
        private bool _disposed = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new <see cref="FormMover"/> and subscribes to
        /// the required mouse events on the specified drag area.
        /// </summary>
        /// <param name="form">The form to move. Must not be null.</param>
        /// <param name="dragArea">The control used as the drag handle. Must not be null.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="form"/> or <paramref name="dragArea"/> is null.
        /// </exception>
        public FormMover(Form form, Control dragArea)
        {
            _targetForm = form ?? throw new ArgumentNullException(nameof(form));
            _dragArea = dragArea ?? throw new ArgumentNullException(nameof(dragArea));

            SubscribeEvents();
        }

        #endregion

        #region Mouse Event Handlers

        /// <summary>
        /// Begins the drag operation on LEFT mouse button press only.
        /// Records the cursor offset within the control to allow smooth movement.
        /// </summary>
        private void DragArea_MouseDown(object sender, MouseEventArgs e)
        {
            // ✅ Ignore right-click and middle-click
            if (e.Button != MouseButtons.Left)
                return;

            _isMoving = true;
            _startX = e.X;
            _startY = e.Y;
        }

        /// <summary>
        /// Moves the form to follow the cursor while dragging.
        /// Uses Control.MousePosition for screen-relative coordinates.
        /// </summary>
        private void DragArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMoving)
                return;

            // Offset cursor position by the initial click position within the control
            _targetForm.SetDesktopLocation(
                Control.MousePosition.X - _startX,
                Control.MousePosition.Y - _startY
            );
        }

        /// <summary>
        /// Ends the drag operation when the LEFT mouse button is released.
        /// </summary>
        private void DragArea_MouseUp(object sender, MouseEventArgs e)
        {
            // ✅ Only stop on left button release
            if (e.Button == MouseButtons.Left)
                _isMoving = false;
        }

        /// <summary>
        /// Resets the cursor to default when the mouse leaves the drag area.
        /// Note: does NOT cancel an active drag — fast movement may temporarily
        /// exit the control while the button is still held.
        /// </summary>
        private void DragArea_MouseLeave(object sender, EventArgs e)
        {
            _dragArea.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Changes the cursor to a move icon when hovering over the drag area,
        /// providing a visual hint that the area can be used to move the form.
        /// </summary>
        private void DragArea_MouseEnter(object sender, EventArgs e)
        {
            _dragArea.Cursor = Cursors.SizeAll;
        }

        #endregion

        #region Event Management

        /// <summary>
        /// Subscribes all required mouse events on the drag area control.
        /// </summary>
        private void SubscribeEvents()
        {
            _dragArea.MouseDown += DragArea_MouseDown;
            _dragArea.MouseMove += DragArea_MouseMove;
            _dragArea.MouseUp += DragArea_MouseUp;
            _dragArea.MouseLeave += DragArea_MouseLeave;
            _dragArea.MouseEnter += DragArea_MouseEnter;
        }

        /// <summary>
        /// Unsubscribes all mouse events from the drag area.
        /// Called by <see cref="Dispose"/> to prevent memory leaks.
        /// </summary>
        private void UnsubscribeEvents()
        {
            _dragArea.MouseDown -= DragArea_MouseDown;
            _dragArea.MouseMove -= DragArea_MouseMove;
            _dragArea.MouseUp -= DragArea_MouseUp;
            _dragArea.MouseLeave -= DragArea_MouseLeave;
            _dragArea.MouseEnter -= DragArea_MouseEnter;
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Releases all event subscriptions to prevent memory leaks.
        /// Should be called when the parent form is closed.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            UnsubscribeEvents();
            _disposed = true;
        }

        #endregion
    }
}
