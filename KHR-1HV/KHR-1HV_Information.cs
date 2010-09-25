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
    public partial class KHR_1HV_Information : Form
    {
        private Roboard.TimeOut watchDogTimer;
        private static string sReturnMessage;
        private string[] saReturnMessage;
        private string sendString;

        private Bitmap Logo;

        public KHR_1HV_Information()
        {
            InitializeComponent();
        }

        private void KHR_1HV_Information_Load(object sender, EventArgs e)
        {
            Logo = new Bitmap(pictureBox1.Image);
            Logo.MakeTransparent(Logo.GetPixel(1, 1));
            pictureBox1.Image = (Image)Logo;

            Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(client_messageHandler);
            sendString = "Information";
            if (handleNetworkMessage(sendString))
            {
                lblCPUId.Text = saReturnMessage[0];
                lblVersion.Text = saReturnMessage[1];
                cbRCServoLib.Checked = Convert.ToBoolean(saReturnMessage[2]);
                cbI2CLib.Checked = Convert.ToBoolean(saReturnMessage[3]);
                cbADLib.Checked = Convert.ToBoolean(saReturnMessage[4]);
                cbSPILib.Checked = Convert.ToBoolean(saReturnMessage[5]);
            }
            Roboard.NetworkClient.messageHandler -= new NetworkClient.NewMessageEventHandler(client_messageHandler);
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

        private static void client_messageHandler(object sender, NewMessageEventsArgs e)
        {
            sReturnMessage = Convert.ToString(e.NewMessage);
        }
    }
}
