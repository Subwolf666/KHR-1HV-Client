using System;
using System.Collections.Generic;
using System.Text;
using Roboard;

namespace KHR_1HV
{
    static class KHR_1HV_MotionStopPause
    {
//        private static Roboard.NetworkClient networkClient = new Roboard.NetworkClient();
        private static int _motionBeingPlayed;
        private static string sendString;

        private static string[] strMessage = new string[50];

        // Property
        //
        public static int MotionBeingPlayed
        {
            get
            {
                return _motionBeingPlayed;
            }
            set
            {
                _motionBeingPlayed = value;
            }
        }

        // method
        //
        public static bool Stop()
        {
//            networkClient.UserName = "MotionStopPause";
            //if (!networkClient.Connect())
            //{
            //    Close();
            //    return false;
            //}
            
//            Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(WriteNetworkClient_messageHandler);
            try
            {
                sendString = "StopMotionFile";
                Roboard.NetworkClient.SendMessage(sendString);
                // als ik antwoord wil of het wel of niet gelukt is hier een handleNetworkMessage plaatsen (zie NetworkFile.cs)
                Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(Convert.ToString(ex));
            }
            finally
            {
                sendString = "StopMotionFile";
                Roboard.NetworkClient.SendMessage(sendString);
                // als ik antwoord wil of het wel of niet gelukt is hier een handleNetworkMessage plaatsen (zie NetworkFile.cs)
                Close();
            }
            return true;
        }

        // Method
        //
        public static bool Pause()
        {
//            networkClient.UserName = "MotionStopPause";
            //if (!networkClient.Connect())
            //{
            //    Close();
            //    return false;
            //}
//            Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(WriteNetworkClient_messageHandler);
            sendString = "PauseMotion";
            Roboard.NetworkClient.SendMessage(sendString);
            // als ik antwoord wil of het wel of niet gelukt is hier een handleNetworkMessage plaatsen (zie NetworkFile.cs)
            Close();
            return true;            
        }

        // Methods
        //
        private static void Close()
        {
//            Roboard.NetworkClient.messageHandler -= new NetworkClient.NewMessageEventHandler(WriteNetworkClient_messageHandler);
//            networkClient.CloseConnection();
        }

        //=========================
        private static void WriteNetworkClient_messageHandler(object sender, NewMessageEventsArgs e)
        {
//            if (e.NewUser == "Admin")
            {
                strMessage = e.NewMessage.Split(',');
            }
        }

    }
}
