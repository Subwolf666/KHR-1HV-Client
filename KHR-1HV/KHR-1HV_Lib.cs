using System;
using System.Collections.Generic;
using System.Text;
using Roboard;

namespace KHR_1HV
{
    public class KHR_1HV_Lib
    {
        private Roboard.TimeOut watchDogTimer;
        private static string sReturnMessage;
        private string[] saReturnMessage;
        private string sendString;

        private bool _connected;
        
        public string InitializeLibraries(string selItem)
        {
            switch (selItem)
            {
                case "Close":
                    Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(client_messageHandler);
                    _connected = Roboard.NetworkClient.Connected;
                    if (_connected)
                    {
                        this.sendString = "Close";
                        if (handleNetworkMessage(this.sendString))
                        {
                            if (sReturnMessage == "No Connection")
                            {
                                Roboard.NetworkClient.CloseConnection();
                            }
                            else
                            {
                                sReturnMessage = "No Connection";
                            }
                        }
                    }
                    else
                    {
                        sReturnMessage = "No Connection";
                    }
                    Roboard.NetworkClient.messageHandler -= new NetworkClient.NewMessageEventHandler(client_messageHandler);
                    _connected = Roboard.NetworkClient.Connected;
                    break;
                case "Open":
                    _connected = Roboard.NetworkClient.Connect();
                    if (!_connected)
                    {
                        sReturnMessage = "No Connection";
                        break;
                    }
                    Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(client_messageHandler);
                    this.sendString = "Open";
                    if (!handleNetworkMessage(this.sendString))
                    {
                        sReturnMessage = "No Connection";
                    }
                    Roboard.NetworkClient.messageHandler -= new NetworkClient.NewMessageEventHandler(client_messageHandler);
                    break;
                default:
                    Roboard.NetworkClient.SendMessage(null);
                    sReturnMessage = "What the fuck.";
                    break;
            }

            return sReturnMessage;
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

        // Property
        //
        public bool Connected
        {
            get
            {
                return _connected;
            }
        }

        private static void client_messageHandler(object sender, NewMessageEventsArgs e)
        {
            sReturnMessage = Convert.ToString(e.NewMessage);
        }
    }
}
