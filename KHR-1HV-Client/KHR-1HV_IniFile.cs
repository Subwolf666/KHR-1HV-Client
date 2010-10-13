using System;
using System.Collections.Generic;
using System.Text;
using Roboard;

namespace KHR_1HV
{
    public class KHR_1HV_IniFile
    {
        //===================
        //  CLASS VARIABLES 
        //===================
        IniFile khr_1hv_ini;
        private static string _IPAddress;
        private static int _Grid;
        private static int _GridWidth;
        private static int _GridHeight;
        private static int _PosWidth;
        private static int _PosHeight;
        private static int _PosColor;
        private static string _PosPicture;
        private static bool _PosBackground;

        private static bool[] _PosChannelVisible = new bool[24];
        private static int[] _PosChannelLocationX = new int[24];
        private static int[] _PosChannelLocationY = new int[24];
        private static Int32[] _PosChannelColor = new Int32[24];
        private static int[] _PosChannelFunction = new int[24];
        private static int[] _PosChannelNotUsed = new int[24];
        private static string[] _PosChannelName = new string[24];

        private static bool _PosSpeedVisible;
        private static int _PosSpeedLocationX;
        private static int _PosSpeedLocationY;
        private static int _PosSpeedColor;

        private static bool _PosCMDVisible;
        private static int _PosCMDLocationX;
        private static int _PosCMDLocationY;
        private static int _PosCMDColor;

        private static bool _PosLinkVisible;
        private static int _PosLinkLocationX;
        private static int _PosLinkLocationY;
        private static int _PosLinkColor;


        //=======================
        //  DEFAULT CONSTRUCTOR
        //=======================
        public KHR_1HV_IniFile()
        {
            khr_1hv_ini = new IniFile("./KHR-1HV.ini");
            if (khr_1hv_ini.Exists())
                khr_1hv_ini.Load();
            else
            {
                IniSection section = new IniSection();
                section.Add("IPAddress", "0.0.0.0");
                section.Add("Grid", "1");
                section.Add("GridWidth", "5");
                section.Add("GridHeight", "5");
                section.Add("Form", "804,900,-2147483633");
                section.Add("Bmp", "");
                section.Add("Background", "0");
                section.Add("CH1", "1,0,0,-2147483633,0,1,CH1");
                section.Add("CH2", "1,0,31,-2147483633,0,1,CH2");
                section.Add("CH3", "1,0,62,-2147483633,0,1,CH3");
                section.Add("CH4", "1,0,93,-2147483633,0,1,CH4");
                section.Add("CH5", "1,0,124,-2147483633,0,1,CH5");
                section.Add("CH6", "1,0,155,-2147483633,0,1,CH6");
                section.Add("CH7", "1,0,186,-2147483633,0,1,CH7");
                section.Add("CH8", "1,0,217,-2147483633,0,1,CH8");
                section.Add("CH9", "1,0,248,-2147483633,0,1,CH9");
                section.Add("CH10", "1,0,279,-2147483633,0,1,CH10");
                section.Add("CH11", "1,0,310,-2147483633,0,1,CH11");
                section.Add("CH12", "1,0,341,-2147483633,0,1,CH12");
                section.Add("CH13", "1,329,0,-2147483633,0,1,CH13");
                section.Add("CH14", "1,329,31,-2147483633,0,1,CH14");
                section.Add("CH15", "1,329,62,-2147483633,0,1,CH15");
                section.Add("CH16", "1,329,93,-2147483633,0,1,CH16");
                section.Add("CH17", "1,329,124,-2147483633,0,1,CH17");
                section.Add("CH18", "1,329,155,-2147483633,0,1,CH18");
                section.Add("CH19", "1,329,186,-2147483633,0,1,CH19");
                section.Add("CH20", "1,329,217,-2147483633,0,1,CH20");
                section.Add("CH21", "1,329,248,-2147483633,0,1,CH21");
                section.Add("CH22", "1,329,279,-2147483633,0,1,CH22");
                section.Add("CH23", "1,329,310,-2147483633,0,1,CH23");
                section.Add("CH24", "1,329,341,-2147483633,0,1,CH24");
                section.Add("SPEED", "1,0,372,-2147483633");
                section.Add("CMD", "1,0,403,-2147483633");
                section.Add("LINK", "1,329,372,-2147483633");
                khr_1hv_ini.Add("KHR-1HV", section);

                khr_1hv_ini.Save();
            }
        }

        //==========
        //  METHOD
        //==========
        // lees eerst de complete ini file in en geef
        // de key's door via constructors.
        public void Read()
        {
            string tmpText;
            string[] tmpStringArray = new string[24];

            // Read the GRID data
            try
            {
                
            _IPAddress = khr_1hv_ini["KHR-1HV"]["IPAddress"];
            _Grid = int.Parse(khr_1hv_ini["KHR-1HV"]["Grid"]);
            _GridWidth = int.Parse(khr_1hv_ini["KHR-1HV"]["GridWidth"]);
            _GridHeight = int.Parse(khr_1hv_ini["KHR-1HV"]["GridHeight"]);
            // Read FORM data
            tmpText = khr_1hv_ini["KHR-1HV"]["Form"];
            tmpStringArray = tmpText.Split(',');
            _PosHeight = int.Parse(tmpStringArray[0]);
            _PosWidth = int.Parse(tmpStringArray[1]);
            _PosColor = int.Parse(tmpStringArray[2]);
            _PosPicture = khr_1hv_ini["KHR-1HV"]["Bmp"];
            _PosBackground = khr_1hv_ini["KHR-1HV"]["Background"].Equals("1");
            //// Read CHANNEL data
            for (int i = 0; i < 24; i++)
            {
                tmpText = khr_1hv_ini["KHR-1HV"]["CH" + (i + 1).ToString()];
                tmpStringArray = tmpText.Split(',');
                _PosChannelVisible[i] = tmpStringArray[0].Equals("1");
                _PosChannelLocationX[i] = int.Parse(tmpStringArray[1]);
                _PosChannelLocationY[i] = int.Parse(tmpStringArray[2]);
                _PosChannelColor[i] = int.Parse(tmpStringArray[3]);
                _PosChannelFunction[i] = int.Parse(tmpStringArray[4]);
                _PosChannelNotUsed[i] = int.Parse(tmpStringArray[5]);
                _PosChannelName[i] = tmpStringArray[6];
            }

            // Read SPEED data
            tmpText = khr_1hv_ini["KHR-1HV"]["SPEED"];
            tmpStringArray = tmpText.Split(',');
            _PosSpeedVisible = tmpStringArray[0].Equals("1");
            _PosSpeedLocationX = int.Parse(tmpStringArray[1]);
            _PosSpeedLocationY = int.Parse(tmpStringArray[2]);
            _PosSpeedColor = int.Parse(tmpStringArray[3]);

            // Read CMD data
            tmpText = khr_1hv_ini["KHR-1HV"]["CMD"];
            tmpStringArray = tmpText.Split(',');
            _PosCMDVisible = tmpStringArray[0].Equals("1");
            _PosCMDLocationX = int.Parse(tmpStringArray[1]);
            _PosCMDLocationY = int.Parse(tmpStringArray[2]);
            _PosCMDColor = int.Parse(tmpStringArray[3]);

            // Read LINK data
            tmpText = khr_1hv_ini["KHR-1HV"]["LINK"];
            tmpStringArray = tmpText.Split(',');
            _PosLinkVisible = tmpStringArray[0].Equals("1");
            _PosLinkLocationX = int.Parse(tmpStringArray[1]);
            _PosLinkLocationY = int.Parse(tmpStringArray[2]);
            _PosLinkColor = int.Parse(tmpStringArray[3]);
            }
            catch
            {
                // error in the ini file
            }

        }

        public void Save()
        {
            khr_1hv_ini.Save();
        }

        public void WriteGrid()
        {
            khr_1hv_ini["KHR-1HV"]["Grid"] = _Grid.ToString();
            khr_1hv_ini["KHR-1HV"]["GridWidth"] = _GridWidth.ToString();
            khr_1hv_ini["KHR-1HV"]["GridHeight"] = _GridHeight.ToString();
        }

        public void WritePos()
        {
            khr_1hv_ini["KHR-1HV"]["Form"] = _PosHeight.ToString() + "," + 
                                             _PosWidth.ToString() + "," +
                                             _PosColor.ToString();
            khr_1hv_ini["KHR-1HV"]["Bmp"] = _PosPicture;
            khr_1hv_ini["KHR-1HV"]["Background"] = (_PosBackground ? 1 : 0).ToString();
            for (int i = 0; i < 24; i++)
            {
                khr_1hv_ini["KHR-1HV"]["CH" + (i + 1).ToString()] = (_PosChannelVisible[i] ? 1 : 0).ToString() + "," +
                                                _PosChannelLocationX[i].ToString() + "," +
                                                _PosChannelLocationY[i].ToString() + "," +
                                                _PosChannelColor[i].ToString() + "," +
                                                _PosChannelFunction[i].ToString() + "," +
                                                _PosChannelNotUsed[i].ToString() + "," +
                                                _PosChannelName[i];
            }
            khr_1hv_ini["KHR-1HV"]["SPEED"] = (_PosSpeedVisible ? 1 : 0).ToString() + "," +
                                               _PosSpeedLocationX.ToString() + "," +
                                               _PosSpeedLocationY.ToString() + "," +
                                               _PosSpeedColor.ToString();
            khr_1hv_ini["KHR-1HV"]["CMD"] = (_PosCMDVisible ? 1 : 0).ToString() + "," +
                                             _PosCMDLocationX.ToString() + "," +
                                             _PosCMDLocationY.ToString() + "," +
                                             _PosCMDColor.ToString();
            khr_1hv_ini["KHR-1HV"]["LINK"] = (_PosLinkVisible ? 1 : 0).ToString() + "," +
                                              _PosLinkLocationX.ToString() + "," +
                                              _PosLinkLocationY.ToString() + "," +
                                              _PosLinkColor.ToString();
        }

        //===========================
        //  READ AND WRITE PROPERTY
        //===========================
        //==============================
        //  GRID PROPERTIES
        //==============================
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        public int Grid
        {
            get { return _Grid; }
            set { _Grid = value; }
        }

        public int GridWidth
        {
            get { return _GridWidth; }
            set { _GridWidth = value; }
        }

        public int GridHeight
        {
            get { return _GridHeight; }
            set { _GridHeight = value; }
        }

        //==============================
        //  POS FORM PROPERTIES
        //==============================
        public int PosHeight
        {
            get { return _PosHeight; }

            set { _PosHeight = value; }
        }
        public int PosWidth
        {
            get { return _PosWidth; }

            set { _PosWidth = value; }
        }
        public Int32 PosColor
        {
            get { return _PosColor; }

            set { _PosColor = value; }
        }
        public string PosPicture
        {
            get { return _PosPicture; }

            set { _PosPicture = value; }
        }
        public bool PosBackground
        {
            get { return _PosBackground; }

            set { _PosBackground = value; }
        }

        //==============================
        //  CHANNEL SLIDER PROPERTIES
        //==============================
        public bool[] PosChannelVisible
        {
            get { return _PosChannelVisible; }

            set { _PosChannelVisible = value; }
        }

        public int[] PosChannelLocationX
        {
            get { return _PosChannelLocationX; }

            set { _PosChannelLocationX = value; }
        }

        public int[] PosChannelLocationY
        {
            get { return _PosChannelLocationY; }

            set { _PosChannelLocationY = value; }
        }

        public int[] PosChannelColor
        {
            get { return _PosChannelColor; }

            set { _PosChannelColor = value; }
        }

        public int[] PosChannelFunction
        {
            get { return _PosChannelFunction; }

            set { _PosChannelFunction = value; }
        }

        public int[] PosChannelNotUsed
        {
            get { return _PosChannelNotUsed; }

            set { _PosChannelNotUsed = value; }
        }

        public string[] PosChannelName
        {
            get { return _PosChannelName; }

            set { _PosChannelName = value; }
        }

        //==============================
        //  SPEED SLIDER PROPERTIES
        //==============================
        public bool PosSpeedVisible
        {
            get { return _PosSpeedVisible; }

            set { _PosSpeedVisible = value; }
        }

        public int PosSpeedLocationX
        {
            get { return _PosSpeedLocationX; }

            set { _PosSpeedLocationX = value; }
        }

        public int PosSpeedLocationY
        {
            get { return _PosSpeedLocationY; }

            set { _PosSpeedLocationY = value; }
        }

        public int PosSpeedColor
        {
            get { return _PosSpeedColor; }

            set { _PosSpeedColor = value; }
        }

        //==============================
        //  COMMAND SLIDER PROPERTIES
        //==============================
        public bool PosCMDVisible
        {
            get { return _PosCMDVisible; }

            set { _PosCMDVisible = value; }
        }

        public int PosCMDLocationX
        {
            get { return _PosCMDLocationX; }

            set { _PosCMDLocationX = value; }
        }

        public int PosCMDLocationY
        {
            get { return _PosCMDLocationY; }

            set { _PosCMDLocationY = value; }
        }

        public int PosCMDColor
        {
            get { return _PosCMDColor; }

            set { _PosCMDColor = value; }
        }

        //==============================
        //  LINK SLIDER PROPERTIES
        //==============================
        public bool PosLinkVisible
        {
            get { return _PosLinkVisible; }

            set { _PosLinkVisible = value; }
        }

        public int PosLinkLocationX
        {
            get { return _PosLinkLocationX; }

            set { _PosLinkLocationX = value; }
        }

        public int PosLinkLocationY
        {
            get { return _PosLinkLocationY; }

            set { _PosLinkLocationY = value; }
        }

        public int PosLinkColor
        {
            get { return _PosLinkColor; }

            set { _PosLinkColor = value; }
        }
        // derde deel:
        // bij het afsluiten van het pos window en het complete programma
        // de ini file opnieuw schrijven.
        //public bool iniSave()
        //{
        //}
    }

}
