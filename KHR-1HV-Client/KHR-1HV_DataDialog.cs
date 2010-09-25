using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Roboard.Events;

namespace KHR_1HV
{
    public partial class KHR_1HV_DataDialog : Form
    {
        private Roboard.XBox360Controller Controller;

        public KHR_1HV_DataDialog()
        {
            InitializeComponent();
        }

        private void KHR_1HV_DataDialog_Load(object sender, EventArgs e)
        {
            btnCapture.Appearance = System.Windows.Forms.Appearance.Button; 
            tbDataName.Text = _dataName;
            tbRemoteControlCode.Text = _remoteControllerCode;
            previousReceiver[0] = _remoteControllerCode;

            Controller = new Roboard.XBox360Controller();
        }

        private void KHR_1HV_DataDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controller.Stop();
            Controller.ReceiverChange -= new ReceiverChangeEventHandler(Controller_ReceiverChange);
        }

        private delegate void controllerTextDelegate(string[] Receiver);
        private void controllerText(string[] Receiver)
        {
            if (this.tbRemoteControlCode.InvokeRequired)
            {
                this.tbRemoteControlCode.BeginInvoke(new controllerTextDelegate(controllerText), new object[] { Receiver });
            }
            else
            {
                this.tbRemoteControlCode.Text = Receiver[0];
            }
        }

        string[] previousReceiver = new string[8];
        string[] currentReceiver = new string[8];
        void Controller_ReceiverChange(object sender, ReceiverChangeEventArgs e)
        {
            currentReceiver = e.Receiver;
            if (currentReceiver[0] == "65535")
                controllerText(previousReceiver);
            else
            {
                previousReceiver = currentReceiver;
                controllerText(currentReceiver);
            }
        }


        private void btnCapture_CheckedChanged(object sender, EventArgs e)
        {
            if (btnCapture.Checked)
            {
                btnCapture.Text = "Capturing";
                Controller.Start();
                Controller.ReceiverChange += new ReceiverChangeEventHandler(Controller_ReceiverChange);
            }
            else
            {
                Controller.Stop();
                Controller.ReceiverChange -= new ReceiverChangeEventHandler(Controller_ReceiverChange);
                btnCapture.Text = "Capture";
                _remoteControllerCode = tbRemoteControlCode.Text;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _dataName = tbDataName.Text;
            if ((tbRemoteControlCode.Text == string.Empty) || (tbRemoteControlCode.Text == "0"))
                _remoteControllerCode = "65535";
            else
                _remoteControllerCode = tbRemoteControlCode.Text;
            Controller.ReceiverChange -= new ReceiverChangeEventHandler(Controller_ReceiverChange);
            Controller.Stop();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Controller.ReceiverChange -= new ReceiverChangeEventHandler(Controller_ReceiverChange);
            Controller.Stop();
            this.DialogResult = DialogResult.Cancel;
        }

        //=============================
        //  PROPERTIES
        //=============================
        private string _dataName;
        public string dataName
        {
            get { return _dataName; }
            set { _dataName = value; }
        }

        private string _remoteControllerCode;
        public string remoteControllerCode
        {
            get { return _remoteControllerCode; }
            set { _remoteControllerCode = value; }
        }

        private void tbRemoteControlCode_KeyDown(object sender, KeyEventArgs e)
        {
            int number;
            if (e.KeyCode == Keys.Enter)
            {
                if (isNumeric(tbRemoteControlCode.Text))
                {
                    number = int.Parse(tbRemoteControlCode.Text);
                    if (number < 0)
                        number = 0;
                    if (number > 65535)
                        number = 65535;
                }
                else
                {
                    number = Convert.ToInt32(previousReceiver[0]);
                }
                tbRemoteControlCode.Text = number.ToString();
            }
        }

        private System.Boolean isNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }

    }
}
