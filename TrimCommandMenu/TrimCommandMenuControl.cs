using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TrimCommandMenu
{
    public partial class TrimCommandMenuControl : UserControl
    {
        private string _fileName = string.Empty;

        public TrimCommandMenuControl()
        {
            InitializeComponent();
        }

        // Event method
        //
        private void tsTrimOpen_Click(object sender, EventArgs e)
        {
            this.TrimFileDialogControl("Open");
        }

        // Event Method
        //
        private void tsTrimSave_Click(object sender, EventArgs e)
        {
            this.TrimFileDialogControl("Save");
        }

        // A delegate type for hooking up slidercontrol notifications.
        //
        public delegate void TrimFileDialogEventHandler(object sender, TrimFileDialogEventsArgs e);

        // Now, create a public event "SliderControlChanged" 
        // whose type is our SliderControlEventHandler.
        //
        public event TrimFileDialogEventHandler TrimFileDialogPressed;

        private void TrimFileDialogControl(string fileDialog)
        {
            TrimFileDialogEventsArgs TrimFileDialogEvents = new TrimFileDialogEventsArgs(fileDialog);

            // Now, raise the event by invoking the delegate. Pass in
            // the objects that initiated the event (this) as wel as
            // AccelerationChangeEventArgs.
            // The call must match the signature of the AccelerationChangeEventHandler.
            if (TrimFileDialogPressed != null)
            {
                TrimFileDialogPressed(this, TrimFileDialogEvents);
            }
        }
    }
    // Class that contains the data for the change slidercontrol events.
    // Derives from System.Eventargs.
    //
    public class TrimFileDialogEventsArgs : EventArgs
    {
        // The slidercontrol event will have two pieces of information--
        // 1) the function and 3) the value of that function.
        private string fileDialog;

        // Default Constructor
        //
        public TrimFileDialogEventsArgs(string fileDialog)
        {
            this.fileDialog = fileDialog;
        }

        // The Function property returns the reference to the slidercontrol function
        // from which this event originated.
        //
        public string Filedialog
        {
            get { return this.fileDialog; }
            set { this.fileDialog = value; }
        }
    }
}
