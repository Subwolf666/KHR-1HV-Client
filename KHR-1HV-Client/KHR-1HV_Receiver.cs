using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using Roboard;
using Roboard.Events;

namespace KHR_1HV
{
    public partial class KHR_1HV_Receiver : Form
    {
        private Roboard.XBox360Controller Controller;

        public KHR_1HV_Receiver()
        {
            InitializeComponent();
        }

        private void KHR_1HV_Receiver_Load(object sender, EventArgs e)
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
            Controller.ReceiverChange += new ReceiverChangeEventHandler(Controller_ReceiverChange);
            Controller.Start();

        }

        void Controller_ReceiverChange(object sender, ReceiverChangeEventArgs e)
        {
            controllerText(e.Receiver);
        }

        private void KHR_1HV_Receiver_FormClosing(object sender, FormClosingEventArgs e)
        {
            string tmp = string.Format("{0},{1},{2},{3},{4},{5}",
                this.X_Axis1.Reference,
                this.Y_Axis1.Reference,
                this.X_Axis2.Reference,
                this.Y_Axis2.Reference,
                this.LeftTrigger.Reference,
                this.RightTrigger.Reference);
            
            Controller.SaveStop(tmp);
            Controller.ReceiverChange -= new ReceiverChangeEventHandler(Controller_ReceiverChange);
        }

        private delegate void controllerTextDelegate(string[] Receiver);
        private void controllerText(string[] Receiver)
        {
            if (this.tbInputButton.InvokeRequired)
            {
                this.tbInputButton.BeginInvoke(new controllerTextDelegate(controllerText), new object[] { Receiver });
            }
            else
            {
                this.lblConnected.Visible = (Receiver[0] == "0");
                this.tbInputButton.Text = Receiver[0];
                this.X_Axis1.Measure = int.Parse(Receiver[1]);
                this.Y_Axis1.Measure = int.Parse(Receiver[2]);
                this.X_Axis2.Measure = int.Parse(Receiver[3]);
                this.Y_Axis2.Measure = int.Parse(Receiver[4]);
                this.LeftTrigger.Measure = int.Parse(Receiver[5]);
                this.RightTrigger.Measure = int.Parse(Receiver[6]);
                if (Receiver.Length > 7)
                {
                    this.X_Axis1.Reference = Convert.ToInt32(Receiver[7]);
                    this.Y_Axis1.Reference = Convert.ToInt32(Receiver[8]);
                    this.X_Axis2.Reference = Convert.ToInt32(Receiver[9]);
                    this.Y_Axis2.Reference = Convert.ToInt32(Receiver[10]);
                    this.LeftTrigger.Reference = Convert.ToInt32(Receiver[11]);
                    this.RightTrigger.Reference = Convert.ToInt32(Receiver[12]);
                }
            }
        }
    }
}
