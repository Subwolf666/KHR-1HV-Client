using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Roboard;
using Roboard.Events;

namespace KHR_1HV
{
    public partial class KHR_1HV_XBox360Controller : Form
    {
        private Roboard.XBox360Controller Controller;

        public KHR_1HV_XBox360Controller()
        {
            InitializeComponent();
        }

        private void KHR_1HV_XBox360Controller_Load(object sender, EventArgs e)
        {
            X_Axis1.Label = string.Empty;
            Y_Axis1.Label = string.Empty;
            X_Axis2.Label = string.Empty;
            Y_Axis2.Label = string.Empty;
            LeftTrigger.Label = string.Empty;
            RightTrigger.Label = string.Empty;
            tbInputButton.Text = "0";
            X_Axis1.sliderMinimumRange = 0;
            X_Axis1.sliderMaximumRange = 100;
            Y_Axis1.sliderMinimumRange = 0;
            Y_Axis1.sliderMaximumRange = 100;
            X_Axis2.sliderMinimumRange = 0;
            X_Axis2.sliderMaximumRange = 100;
            Y_Axis2.sliderMinimumRange = 0;
            Y_Axis2.sliderMaximumRange = 100;
            LeftTrigger.sliderMinimumRange = 0;
            LeftTrigger.sliderMaximumRange = 100;
            RightTrigger.sliderMinimumRange = 0;
            RightTrigger.sliderMaximumRange = 100;

            Controller = new Roboard.XBox360Controller();
            Controller.XBox360ControllerChange += new XBox360ControllerChangeEventHandler(Controller_ReceiverChange);
            Controller.Start();
        }

        void Controller_ReceiverChange(object sender, XBox360ControllerChangeEventArgs e)
        {
            XBox360ControllerText(e.XBox360Controller);
        }

        private void KHR_1HV_XBox360Controller_FormClosing(object sender, FormClosingEventArgs e)
        {
            string tmp = string.Format("{0},{1},{2},{3},{4},{5}",
                this.X_Axis1.Reference,
                this.Y_Axis1.Reference,
                this.X_Axis2.Reference,
                this.Y_Axis2.Reference,
                this.LeftTrigger.Reference,
                this.RightTrigger.Reference);
            
            Controller.SaveStop(tmp);
            Controller.XBox360ControllerChange -= new XBox360ControllerChangeEventHandler(Controller_ReceiverChange);
        }

        private delegate void controllerTextDelegate(string[] XBox360Controller);
        private void XBox360ControllerText(string[] XBox360Controller)
        {
            if (this.tbInputButton.InvokeRequired)
            {
                this.tbInputButton.BeginInvoke(new controllerTextDelegate(XBox360ControllerText), new object[] { XBox360Controller });
            }
            else
            {
                this.lblConnected.Visible = !Convert.ToBoolean(XBox360Controller[0]);
                this.tbInputButton.Text = XBox360Controller[1];
                this.X_Axis1.Measure = Convert.ToInt32(XBox360Controller[2]);
                this.Y_Axis1.Measure = Convert.ToInt32(XBox360Controller[3]);
                this.X_Axis2.Measure = Convert.ToInt32(XBox360Controller[4]);
                this.Y_Axis2.Measure = Convert.ToInt32(XBox360Controller[5]);
                this.LeftTrigger.Measure = Convert.ToInt32(XBox360Controller[6]);
                this.RightTrigger.Measure = Convert.ToInt32(XBox360Controller[7]);
                if (XBox360Controller.Length > 8)
                {
                    this.X_Axis1.Reference = Convert.ToInt32(XBox360Controller[8]);
                    this.Y_Axis1.Reference = Convert.ToInt32(XBox360Controller[9]);
                    this.X_Axis2.Reference = Convert.ToInt32(XBox360Controller[10]);
                    this.Y_Axis2.Reference = Convert.ToInt32(XBox360Controller[11]);
                    this.LeftTrigger.Reference = Convert.ToInt32(XBox360Controller[12]);
                    this.RightTrigger.Reference = Convert.ToInt32(XBox360Controller[13]);
                }
            }
        }
    }
}
