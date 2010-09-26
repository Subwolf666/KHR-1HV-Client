using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Roboard;

namespace KHR_1HV
{
    public partial class KHR_1HV_Options : Form
    {
        private TimeOut watchDogTimer;
        private static string sReturnMessage;
        private string[] saReturnMessage;
        private string sendString;

        public KHR_1HV_Options()
        {
            InitializeComponent();
        }

        private void KHR_1HV_Options_Load(object sender, EventArgs e)
        {
            // Create or copy the motion data table for this class
            if (Roboard.DataTable.Done)
            {
                for (int i = 0; i < StaticUtilities.numberOfMotions; i++)
                {
                    comboBox1.Items.Add(string.Format("{0}   {1}", Roboard.DataTable.motionDataTable[i, 0], Roboard.DataTable.motionDataTable[i, 3]));
                    comboBox2.Items.Add(string.Format("{0}   {1}", Roboard.DataTable.motionDataTable[i, 0], Roboard.DataTable.motionDataTable[i, 3]));
                }
            }
            nudLowPowerValues.Minimum = 5.5M;
            nudLowPowerValues.Maximum = 15.0M;
            nudLowPowerValues.Increment = 0.1M;
            nudLowPowerValues.Value = 13.8M;

            for (int i = 10; i < 210; i += 10)
                comboBox3.Items.Add(i);

            Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
            sendString = "Options,Read";
            if (handleNetworkMessage(this.sendString))
            {
                cbMotionReplay.Checked = Convert.ToBoolean(saReturnMessage[0]);
                cbEnableRemoteControl.Checked = Convert.ToBoolean(saReturnMessage[1]);
                if (Roboard.DataTable.Done)
                {
                    comboBox1.SelectedIndex = Convert.ToInt32(saReturnMessage[2]);
                    comboBox2.SelectedIndex = Convert.ToInt32(saReturnMessage[3]);
                }
                decimal LowPowerValue = Convert.ToDecimal(saReturnMessage[4]) / 10;
                if ((LowPowerValue >= nudLowPowerValues.Minimum) && (LowPowerValue <= nudLowPowerValues.Maximum))
                    nudLowPowerValues.Value = LowPowerValue;
                else
                    nudLowPowerValues.Value = 10.0M;

                comboBox3.SelectedText = saReturnMessage[5];
                // GroupBox Set Servos
                cbCH1.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[6]));
                cbCH2.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[7]));
                cbCH3.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[8]));
                cbCH4.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[9]));
                cbCH5.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[10]));
                cbCH6.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[11]));
                cbCH7.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[12]));
                cbCH8.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[13]));
                cbCH9.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[14]));
                cbCH10.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[15]));
                cbCH11.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[16]));
                cbCH12.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[17]));
                cbCH13.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[18]));
                cbCH14.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[19]));
                cbCH15.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[20]));
                cbCH16.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[21]));
                cbCH17.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[22]));
                cbCH18.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[23]));
                cbCH19.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[24]));
                cbCH20.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[25]));
                cbCH21.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[26]));
                cbCH22.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[27]));
                cbCH23.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[28]));
                cbCH24.Checked = Convert.ToBoolean(Convert.ToInt32(saReturnMessage[29]));

            }
            Roboard.NetworkClient.messageHandler -= new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
        }

        //
        //
        private void KHR_1HV_Options_FormClosing(object sender, FormClosingEventArgs e)
        {

            int dummy = Convert.ToInt32(this.nudLowPowerValues.Value * 10);

            this.sendString = this.cbMotionReplay.Checked.ToString() + "," +
                                this.cbEnableRemoteControl.Checked.ToString() + "," +
                                this.comboBox1.SelectedIndex.ToString() + "," +
                                this.comboBox2.SelectedIndex.ToString() + "," +
                                dummy.ToString().Replace(',', '.') + "," +
                                this.comboBox3.Text.ToString() + ",";

            this.sendString += Convert.ToInt32(cbCH1.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH2.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH3.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH4.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH5.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH6.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH7.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH8.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH9.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH10.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH11.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH12.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH13.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH14.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH15.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH16.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH17.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH18.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH19.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH20.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH21.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH22.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH23.Checked).ToString() + ",";
            this.sendString += Convert.ToInt32(cbCH24.Checked).ToString();

            Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
            this.sendString = "Options,Write," + this.sendString;
            this.handleNetworkMessage(this.sendString);

            Roboard.NetworkClient.messageHandler -= new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
        }

        // Function
        //
        private bool handleNetworkMessage(string sendString)
        {
            watchDogTimer = new TimeOut();

            sReturnMessage = string.Empty;
            Roboard.NetworkClient.SendMessage(sendString);
            watchDogTimer.Start(1000);
            // wait till ok received
            while ((sReturnMessage == string.Empty) && (!watchDogTimer.Done)) ;
            saReturnMessage = sReturnMessage.Split(',');
            if (watchDogTimer.Done)
            {
                return false;
            }
            return true;
        }

        //
        //
        void NetworkClient_messageHandler(object sender, NewMessageEventsArgs e)
        {
            sReturnMessage = Convert.ToString(e.NewMessage);
        }
    }
}
