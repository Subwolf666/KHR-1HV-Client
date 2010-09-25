using System;
using System.Collections.Generic;
using System.Text;

namespace KHR_1HV
{
    class RobotTrim
    {
        //===================
        //  CLASS VARIABLES 
        //===================
        private string trimText;
        private string parameter;
        private char charSeparator;
        private string trimFilename;
        private static string[] trimServo = new string[24];

        //=======================
        //  DEFAULT CONSTRUCTOR
        //=======================
        public RobotTrim()
        {
            trimText = string.Empty;
            parameter = "Prm=";
            charSeparator = ',';
            trimFilename = string.Empty;
        }

        //==========
        //  METHOD
        //==========
        public bool loadServoTrimFile()
        //==================================================
        //  LOAD THE TRIM FILE FOR THE SERVOS OF THE RB100
        //==================================================
        {
            System.IO.StreamReader objReader;

            if (System.IO.File.Exists(trimFilename) == true)
            {
                // If filename exists load it.
                objReader = new System.IO.StreamReader(trimFilename);
                trimText = objReader.ReadToEnd();

                string dummy = trimText.Substring(parameter.Length, trimText.Length - parameter.Length);
                trimServo = dummy.Split(charSeparator);

                objReader.Close();
                return true;
            }
            else
            {
                // If it doesn't exists then create a one.
                return false;
            }
        }

        public bool saveServoTrimFile()
        //=====================================================
        //  SAVES A NEW TRIM FILE FOR THE SERVOS OF THE RB100
        //=====================================================
        {
            System.IO.StreamWriter objWriter;

            objWriter = new System.IO.StreamWriter(trimFilename);

            trimText = parameter + string.Join(",", trimServo);
            objWriter.Write(trimText);
            objWriter.Close();
            return true;
        }

        //===========================
        //  READ AND WRITE PROPERTY
        //===========================
        public string ServoTrimFilename
        {
            get { return trimFilename; }
            set { trimFilename = value; }
        }

        public string[] ServoTrimValues
        {
            get { return trimServo; }
            set { trimServo = value; }
        }
    }
}
