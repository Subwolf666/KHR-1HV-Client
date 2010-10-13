using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Roboard;
using Roboard.Events;
using HorizontalSlider;

namespace KHR_1HV
{
    public partial class KHR_1HV_Position : Form
    {
        //===================
        //  CLASS VARIABLES 
        //===================
        private HorizontalSlider.SliderControl[] horizontalSliderArray;
        private HorizontalSlider.SliderControl speedSlider  = new HorizontalSlider.SliderControl();
        private LabeledComboBox.LabeledComboBoxControl linkControl = new LabeledComboBox.LabeledComboBoxControl();
        private PosCommandMenu.PosCommandMenuControl cmdMnuControl = new PosCommandMenu.PosCommandMenuControl();
        private Roboard.Servos servos;

        private bool wEdge = false;
        private bool eEdge = false;
        private bool nEdge = false;
        private bool sEdge = false;
        private bool Sizing = false;
        private const int EdgeSize = 6;
        Rectangle StartCordRect = new Rectangle();
        Rectangle rect = new Rectangle();

        private bool movePicture = false;
        private bool moveSlider = false;
        private bool SizePicture = false;
        private string[] iniForm = new string[3];
        private bool panelLock = true;
        private int[] sliderControl1Function = new int[8];

        //=======================
        //  DEFAULT CONSTRUCTOR
        //=======================
        public KHR_1HV_Position()
        {
            InitializeComponent();
            //for (int index = 0; index < Roboard.StaticUtilities.numberOfServos; index++)
            //{
            //    // Initialize one variable
            //    horizontalSliderArray[index] = new HorizontalSlider.SliderControl();
            //}
        }

        private void KHR_1HV_Position_Load(object sender, EventArgs e)
        {
            
            // wat betekend dit
//            displayBackgroundToolStripMenuItem.Checked = _iniPosValues.PosBackground;
//            pictureBox1.Visible = false;
            panelLockToolStripMenuItem.Checked = true;
            panelLockToolStripMenuItem1.Checked = true;

            horizontalSliderArray = new HorizontalSlider.SliderControl[Roboard.StaticUtilities.numberOfServos];

            //load form size and color
            this.Size = new System.Drawing.Size(_iniPosValues.PosWidth, _iniPosValues.PosHeight);
            this.BackColor = ColorTranslator.FromOle(_iniPosValues.PosColor);
            
            // if there is a backgroundPicture in the ini file and it exists as file then load it.
            if ((_iniPosValues.PosPicture != string.Empty) && (System.IO.File.Exists(_iniPosValues.PosPicture) == true))
            {
                if (_iniPosValues.PosBackground)
                {
                    displayBackgroundToolStripMenuItem.Checked = true;
                    pictureBox1.Visible = true;
                }
                else
                {
                    displayBackgroundToolStripMenuItem.Checked = false;
                    pictureBox1.Visible = false;
                }
                pictureBox1.Image = Image.FromFile(_iniPosValues.PosPicture);
                pictureBox1.SendToBack();
            }

            horizontalSliderArray = new HorizontalSlider.SliderControl[Roboard.StaticUtilities.numberOfServos];
            int index;
            for (index = 0; index < Roboard.StaticUtilities.numberOfServos; index++)
            {
                // Initialize one variable
                horizontalSliderArray[index] = new HorizontalSlider.SliderControl();

                horizontalSliderArray[index].Tag = index + 1;
                horizontalSliderArray[index].Left = _iniPosValues.PosChannelLocationX[index];
                horizontalSliderArray[index].Top = _iniPosValues.PosChannelLocationY[index];
                horizontalSliderArray[index].BackColor = ColorTranslator.FromOle(_iniPosValues.PosChannelColor[index]);
                horizontalSliderArray[index].sliderFunction = _iniPosValues.PosChannelFunction[index];
                horizontalSliderArray[index].sliderLabel = _iniPosValues.PosChannelName[index];
                horizontalSliderArray[index].Visible = _iniPosValues.PosChannelVisible[index];
                horizontalSliderArray[index].sliderValue = int.Parse(saWidth[index]);
                this.Controls.Add(horizontalSliderArray[index]);

                // the event for the sliderchange
                horizontalSliderArray[index].SliderControlChanged += new HorizontalSlider.SliderControl.SliderControlEventHandler(KHR_1HV_Position_SliderControlChanged);
                // the Event of MouseDown
                horizontalSliderArray[index].MouseDown += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseDown);
                // the Event of MouseMove
                horizontalSliderArray[index].MouseMove += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseMove);
                // the Event of MouseUp
                horizontalSliderArray[index].MouseUp += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseUp);
                // the control for the contextMenuStrip
                horizontalSliderArray[index].ContextMenuStrip = this.contextMenuStrip2;

                horizontalSliderArray[index].BringToFront();
            }

            servos = new Servos();
            //servos.ServosHandler += new Servos.ServosEventHandler(servos_ServosHandler);
            servos.Start();

            speedSlider.Tag = index + 1;
            speedSlider.sliderLabel = "SPEED";
            speedSlider.Left = _iniPosValues.PosSpeedLocationX;
            speedSlider.Top = _iniPosValues.PosSpeedLocationY;
            speedSlider.BackColor = ColorTranslator.FromOle(_iniPosValues.PosSpeedColor);
            speedSlider.sliderFunction = 1;
            speedSlider.sliderMinRange = 1;
            speedSlider.sliderMaxRange = 10000;
            speedSlider.sliderValue = _speedSliderValue; 
            this.Controls.Add(speedSlider);
            speedSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseDown);
            speedSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseMove);
            speedSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseUp);
            speedSlider.ContextMenuStrip = this.contextMenuStrip2;
            speedSlider.BringToFront();
            index++;

            cmdMnuControl.Tag = index + 1;
            cmdMnuControl.Left = _iniPosValues.PosCMDLocationX;
            cmdMnuControl.Top = _iniPosValues.PosCMDLocationY;
            cmdMnuControl.BackColor = ColorTranslator.FromOle(_iniPosValues.PosCMDColor);
            this.Controls.Add(cmdMnuControl);
            cmdMnuControl.MouseDown += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseDown);
            cmdMnuControl.MouseMove += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseMove);
            cmdMnuControl.MouseUp += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseUp);
            cmdMnuControl.ContextMenuStrip = this.contextMenuStrip2;
            cmdMnuControl.BringToFront();
            index++;

            linkControl.Tag = index + 1;
            linkControl.Left = _iniPosValues.PosLinkLocationX;
            linkControl.Top = _iniPosValues.PosLinkLocationY;
            linkControl.BackColor = ColorTranslator.FromOle(_iniPosValues.PosLinkColor);
            this.Controls.Add(linkControl);
            linkControl.MouseDown += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseDown);
            linkControl.MouseMove += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseMove);
            linkControl.MouseUp += new System.Windows.Forms.MouseEventHandler(sliderControl_MouseUp);
            linkControl.ContextMenuStrip = this.contextMenuStrip2;
            linkControl.BringToFront();

            cH1ToolStripMenuItem1.Checked = cH1ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[0];
            cH2ToolStripMenuItem1.Checked = cH2ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[1];
            cH3ToolStripMenuItem1.Checked = cH3ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[2];
            cH4ToolStripMenuItem1.Checked = cH4ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[3];
            cH5ToolStripMenuItem1.Checked = cH5ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[4];
            cH6ToolStripMenuItem1.Checked = cH6ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[5];
            cH7ToolStripMenuItem1.Checked = cH7ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[6];
            cH8ToolStripMenuItem1.Checked = cH8ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[7];
            cH9ToolStripMenuItem1.Checked = cH9ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[8];
            cH10ToolStripMenuItem1.Checked = cH10ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[9];
            cH11ToolStripMenuItem1.Checked = cH11ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[10];
            cH12ToolStripMenuItem1.Checked = cH12ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[11];
            cH13ToolStripMenuItem1.Checked = cH13ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[12];
            cH14ToolStripMenuItem1.Checked = cH14ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[13];
            cH15ToolStripMenuItem1.Checked = cH15ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[14];
            cH16ToolStripMenuItem1.Checked = cH16ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[15];
            cH17ToolStripMenuItem1.Checked = cH17ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[16];
            cH18ToolStripMenuItem1.Checked = cH18ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[17];
            cH19ToolStripMenuItem1.Checked = cH19ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[18];
            cH20ToolStripMenuItem1.Checked = cH20ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[19];
            cH21ToolStripMenuItem1.Checked = cH21ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[20];
            cH22ToolStripMenuItem1.Checked = cH22ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[21];
            cH23ToolStripMenuItem1.Checked = cH23ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[22];
            cH24ToolStripMenuItem1.Checked = cH24ToolStripMenuItem.Checked = _iniPosValues.PosChannelVisible[23];
            speedToolStripMenuItem1.Checked = sPEEDToolStripMenuItem.Checked = _iniPosValues.PosSpeedVisible;
            cOMMANDToolStripMenuItem1.Checked = cOMMANDToolStripMenuItem.Checked = _iniPosValues.PosCMDVisible;
            lINKToolStripMenuItem1.Checked = lINKToolStripMenuItem.Checked = _iniPosValues.PosLinkVisible;

            servos.changeAllChannels(saWidth);

        }

        // Property
        //
        public string[] Parameter
        {
            get
            {
                string[] piet = new string[25];
                piet[0] = speedSlider.sliderValue.ToString();
                for (int index = 0; index < Roboard.StaticUtilities.numberOfServos; index++)
                {
                    piet[index + 1] = (horizontalSliderArray[index].sliderValue + 16384).ToString();
                }
                return piet;
            }
            set
            {
                _speedSliderValue = int.Parse(value[0]);
                for (int index = 0; index < Roboard.StaticUtilities.numberOfServos; index++)
                {
                    saWidth[index] = (int.Parse(value[index + 1]) - 16384).ToString();
                }
            }
        }

        int _speedSliderValue = 100;
        string[] saWidth = new string[StaticUtilities.numberOfServos]
        {
            "0","0","0","0","0","0","0","0",
            "0","0","0","0","0","0","0","0",
            "0","0","0","0","0","0","0","0"
        };

        void KHR_1HV_Position_SliderControlChanged(object sender, SliderControlEventsArgs e)
        {
            int channelNumber = (int)(sender as SliderControl).Tag - 1;
            int channelValue = e.Value;
            saWidth.SetValue(channelValue.ToString(), channelNumber);
            servos.changeAllChannels(saWidth);
        }

        private static KHR_1HV_IniFile _iniPosValues;
        public KHR_1HV_IniFile IniBestand
        {
            get { return _iniPosValues; }
            set { _iniPosValues = value; }
        }

        //============
        //  Properties
        //============
        //private int[] _getSliderValues = new int[24];
        //private int[] gethSliderValues()
        //{
        //    // maak hier de uiteindelijke waardes
        //    // 16384 etc voor servo standen, free, hoog en laag
        //    for (int i = 0; i < Roboard.StaticUtilities.numberOfServos; i++)
        //    {
        //        _getSliderValues[i] = horizontalSliderArray[i].sliderValue + 16384;
        //    }
        //    return _getSliderValues;
        //}

        private void backgroundImageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string chosenFile = "";

            openFD.Title = "Open a background image";
            openFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFD.FileName = "";
            openFD.Filter = "jpg|*.jpg";
            if (openFD.ShowDialog() != DialogResult.Cancel)
            {
                chosenFile = openFD.FileName;
                pictureBox1.Image = Image.FromFile(chosenFile);
                displayBackgroundToolStripMenuItem.Checked = true;
                _iniPosValues.PosBackground = true;
                _iniPosValues.PosPicture = chosenFile;
                pictureBox1.Visible = true;
                pictureBox1.SendToBack();
            }

        }

        // Turn on or off the background picture
        private void displayBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (displayBackgroundToolStripMenuItem.Checked == true)
            {
                displayBackgroundToolStripMenuItem.Checked = false;
                pictureBox1.Visible = false;
                _iniPosValues.PosBackground = false;
            }
            else
            {
                displayBackgroundToolStripMenuItem.Checked = true;
                pictureBox1.Visible = true;
                _iniPosValues.PosBackground = true;
            }
        }

        private void setHomePositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Set Home Position");
        }

        // Set or reset the Panel Lock in the context menu
        private void panelLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panelLock)
            {
                panelLock = false;
                panelLockToolStripMenuItem.Checked = false;
                panelLockToolStripMenuItem1.Checked = false;
            }
            else
            {
                panelLock = true;
                panelLockToolStripMenuItem.Checked = true;
                panelLockToolStripMenuItem1.Checked = true;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (panelLock == false)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    StartCordRect.X = e.Location.X;
                    StartCordRect.Y = e.Location.Y;
                    if (Sizing)
                        SizePicture = true;
                    else
                    {
                        movePicture = true;
                        pictureBox1.Cursor = Cursors.SizeAll;
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (panelLock == false)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.None)
                {
                    if ((e.Location.X < EdgeSize) && (e.Location.X > 0))
                        wEdge = true;
                    else
                        wEdge = false;
                    if ((e.Location.X > this.pictureBox1.Size.Width - EdgeSize) && (e.Location.X < this.pictureBox1.Size.Width))
                        eEdge = true;
                    else
                        eEdge = false;
                    if ((e.Location.Y < EdgeSize) && (e.Location.Y > 0))
                        nEdge = true;
                    else
                        nEdge = false;
                    if ((e.Location.Y > this.pictureBox1.Size.Height - EdgeSize) && (e.Location.Y < this.pictureBox1.Size.Height))
                        sEdge = true;
                    else
                        sEdge = false;
                    Sizing = true;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (movePicture)
                    {
                        pictureBox1.Cursor = Cursors.SizeAll;
                        pictureBox1.Top += e.Location.Y - StartCordRect.Y;
                        pictureBox1.Left += e.Location.X - StartCordRect.X;
                    }
                }
                if (SizePicture)
                {
                    rect.Y = pictureBox1.Top;
                    rect.Height = pictureBox1.Height;
                    rect.X = pictureBox1.Left;
                    rect.Width = pictureBox1.Width;
                    if (eEdge)
                        rect.Width = e.Location.X;
                    if (sEdge)
                        rect.Height = e.Location.Y;
                    if (wEdge)
                    {
                        rect.X = rect.X - (StartCordRect.X - e.Location.X);
                        rect.Width = rect.Width + (StartCordRect.X - e.Location.X);
                    }
                    if (nEdge)
                    {
                        rect.Y = rect.Y - (StartCordRect.Y - e.Location.Y);
                        rect.Height = rect.Height + (StartCordRect.Y - e.Location.Y);
                    }
                    if (rect.Height < 0)
                    {
                        rect.Y = rect.Y + rect.Height;
                        rect.Height = Math.Abs(rect.Height);
                        eEdge = !eEdge;
                        sEdge = !sEdge;
                        StartCordRect.Y = 0;
                    }
                    if (rect.Width < 0)
                    {
                        rect.X = rect.X + rect.Width;
                        rect.Width = Math.Abs(rect.Width);
                        eEdge = !eEdge;
                        wEdge = !wEdge;
                        StartCordRect.X = 0;
                    }
                    pictureBox1.Top = rect.Y;
                    pictureBox1.Height = rect.Height;
                    pictureBox1.Left = rect.X;
                    pictureBox1.Width = rect.Width;
                }

                if ((nEdge || sEdge) && !(wEdge || eEdge))
                    pictureBox1.Cursor = Cursors.SizeNS;
                else if ((wEdge || eEdge) && !(nEdge || sEdge))
                    pictureBox1.Cursor = Cursors.SizeWE;
                else if ((nEdge && eEdge) || (sEdge && wEdge))
                    pictureBox1.Cursor = Cursors.SizeNESW;
                else if ((nEdge && wEdge) || (sEdge && eEdge))
                    pictureBox1.Cursor = Cursors.SizeNWSE;
                else
                {
                    if (movePicture)
                        pictureBox1.Cursor = Cursors.SizeAll;
                    else
                    {
                        pictureBox1.Cursor = Cursors.Default;
                        Sizing = false;
                    }
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (panelLock == false)
            {
                SizePicture = false;
                movePicture = false;
            }
        }

        private void sliderControl_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == System.Windows.Forms.MouseButtons.Left) && (panelLock == false))
            {
                StartCordRect.X = e.Location.X;
                StartCordRect.Y = e.Location.Y;
                Cursor.Current = Cursors.SizeAll;
                moveSlider = true;
            }
        }

        private void sliderControl_MouseMove(object sender, MouseEventArgs e)
        {
            if ((moveSlider) && (e.Button == System.Windows.Forms.MouseButtons.Left) && (panelLock == false))
            {
                if (sender.ToString() != "LabeledComboBox.LabeledComboBoxControl")
                {
                    int tempVar = int.Parse(((HorizontalSlider.SliderControl)sender).Tag.ToString()) - 1;
                    if (tempVar < 24)
                    {
                        horizontalSliderArray[tempVar].Top += e.Location.Y - StartCordRect.Y;
                        horizontalSliderArray[tempVar].Left += e.Location.X - StartCordRect.X;
                        horizontalSliderArray[tempVar].BringToFront();
                    }
                    else
                    {
                        speedSlider.Top += e.Location.Y - StartCordRect.Y;
                        speedSlider.Left += e.Location.X - StartCordRect.X;
                        speedSlider.BringToFront();
                    }
                }
                else
                {
                    linkControl.Top += e.Location.Y - StartCordRect.Y;
                    linkControl.Left += e.Location.X - StartCordRect.X;
                    linkControl.BringToFront();
                }
            }
        }

        private void sliderControl_MouseUp(object sender, MouseEventArgs e)
        {
            moveSlider = false;
        }

        private void cHxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!panelLock)
            {
                switch (sender.ToString())
                {
                    case "CH1":
                        cH1ToolStripMenuItem.Checked = cH1ToolStripMenuItem1.Checked = !cH1ToolStripMenuItem.Checked;
                        horizontalSliderArray[0].Visible = cH1ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[0] = cH1ToolStripMenuItem.Checked;
                        break;
                    case "CH2":
                        cH2ToolStripMenuItem.Checked = cH2ToolStripMenuItem1.Checked = !cH2ToolStripMenuItem.Checked;
                        horizontalSliderArray[1].Visible = cH2ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[1] = cH2ToolStripMenuItem.Checked;
                        break;
                    case "CH3":
                        cH3ToolStripMenuItem.Checked = cH3ToolStripMenuItem1.Checked = !cH3ToolStripMenuItem.Checked;
                        horizontalSliderArray[2].Visible = cH3ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[2] = cH3ToolStripMenuItem.Checked;
                        break;
                    case "CH4":
                        cH4ToolStripMenuItem.Checked = cH4ToolStripMenuItem1.Checked = !cH4ToolStripMenuItem.Checked;
                        horizontalSliderArray[3].Visible = cH4ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[3] = cH4ToolStripMenuItem.Checked;
                        break;
                    case "CH5":
                        cH5ToolStripMenuItem.Checked = cH5ToolStripMenuItem1.Checked = !cH5ToolStripMenuItem.Checked;
                        horizontalSliderArray[4].Visible = cH5ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[4] = cH5ToolStripMenuItem.Checked;
                        break;
                    case "CH6":
                        cH6ToolStripMenuItem.Checked = cH6ToolStripMenuItem1.Checked = !cH6ToolStripMenuItem.Checked;
                        horizontalSliderArray[5].Visible = cH6ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[5] = cH6ToolStripMenuItem.Checked;
                        break;
                    case "CH7":
                        cH7ToolStripMenuItem.Checked = cH7ToolStripMenuItem1.Checked = !cH7ToolStripMenuItem.Checked;
                        horizontalSliderArray[6].Visible = cH7ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[6] = cH7ToolStripMenuItem.Checked;
                        break;
                    case "CH8":
                        cH8ToolStripMenuItem.Checked = cH8ToolStripMenuItem1.Checked = !cH8ToolStripMenuItem.Checked;
                        horizontalSliderArray[7].Visible = cH8ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[7] = cH8ToolStripMenuItem.Checked;
                        break;
                    case "CH9":
                        cH9ToolStripMenuItem.Checked = cH9ToolStripMenuItem1.Checked = !cH9ToolStripMenuItem.Checked;
                        horizontalSliderArray[8].Visible = cH9ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[8] = cH9ToolStripMenuItem.Checked;
                        break;
                    case "CH10":
                        cH10ToolStripMenuItem.Checked = cH10ToolStripMenuItem1.Checked = !cH10ToolStripMenuItem.Checked;
                        horizontalSliderArray[9].Visible = cH10ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[9] = cH10ToolStripMenuItem.Checked;
                        break;
                    case "CH11":
                        cH11ToolStripMenuItem.Checked = cH11ToolStripMenuItem1.Checked = !cH11ToolStripMenuItem.Checked;
                        horizontalSliderArray[10].Visible = cH11ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[10] = cH11ToolStripMenuItem.Checked;
                        break;
                    case "CH12":
                        cH12ToolStripMenuItem.Checked = cH12ToolStripMenuItem1.Checked = !cH12ToolStripMenuItem.Checked;
                        horizontalSliderArray[11].Visible = cH12ToolStripMenuItem.Checked;
                        break;
                    case "CH13":
                        cH13ToolStripMenuItem.Checked = cH13ToolStripMenuItem1.Checked = !cH13ToolStripMenuItem.Checked;
                        horizontalSliderArray[12].Visible = cH13ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[12] = cH13ToolStripMenuItem.Checked;
                        break;
                    case "CH14":
                        cH14ToolStripMenuItem.Checked = cH14ToolStripMenuItem1.Checked = !cH14ToolStripMenuItem.Checked;
                        horizontalSliderArray[13].Visible = cH14ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[13] = cH14ToolStripMenuItem.Checked;
                        break;
                    case "CH15":
                        cH15ToolStripMenuItem.Checked = cH15ToolStripMenuItem1.Checked = !cH15ToolStripMenuItem.Checked;
                        horizontalSliderArray[14].Visible = cH15ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[14] = cH15ToolStripMenuItem.Checked;
                        break;
                    case "CH16":
                        cH16ToolStripMenuItem.Checked = cH16ToolStripMenuItem1.Checked = !cH16ToolStripMenuItem.Checked;
                        horizontalSliderArray[15].Visible = cH16ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[15] = cH16ToolStripMenuItem.Checked;
                        break;
                    case "CH17":
                        cH17ToolStripMenuItem.Checked = cH17ToolStripMenuItem1.Checked = !cH17ToolStripMenuItem.Checked;
                        horizontalSliderArray[16].Visible = cH17ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[16] = cH17ToolStripMenuItem.Checked;
                        break;
                    case "CH18":
                        cH18ToolStripMenuItem.Checked = cH18ToolStripMenuItem1.Checked = !cH18ToolStripMenuItem.Checked;
                        horizontalSliderArray[17].Visible = cH18ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[17] = cH18ToolStripMenuItem.Checked;
                        break;
                    case "CH19":
                        cH19ToolStripMenuItem.Checked = cH19ToolStripMenuItem1.Checked = !cH19ToolStripMenuItem.Checked;
                        horizontalSliderArray[18].Visible = cH19ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[18] = cH19ToolStripMenuItem.Checked;
                        break;
                    case "CH20":
                        cH20ToolStripMenuItem.Checked = cH20ToolStripMenuItem1.Checked = !cH20ToolStripMenuItem.Checked;
                        horizontalSliderArray[19].Visible = cH20ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[19] = cH20ToolStripMenuItem.Checked;
                        break;
                    case "CH21":
                        cH21ToolStripMenuItem.Checked = cH21ToolStripMenuItem1.Checked = !cH21ToolStripMenuItem.Checked;
                        horizontalSliderArray[20].Visible = cH21ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[20] = cH21ToolStripMenuItem.Checked;
                        break;
                    case "CH22":
                        cH22ToolStripMenuItem.Checked = cH22ToolStripMenuItem1.Checked = !cH22ToolStripMenuItem.Checked;
                        horizontalSliderArray[21].Visible = cH22ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[21] = cH22ToolStripMenuItem.Checked;
                        break;
                    case "CH23":
                        cH23ToolStripMenuItem.Checked = cH23ToolStripMenuItem1.Checked = !cH23ToolStripMenuItem.Checked;
                        horizontalSliderArray[22].Visible = cH23ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[22] = cH23ToolStripMenuItem.Checked;
                        break;
                    case "CH24":
                        cH24ToolStripMenuItem.Checked = cH24ToolStripMenuItem1.Checked = !cH24ToolStripMenuItem.Checked;
                        horizontalSliderArray[23].Visible = cH24ToolStripMenuItem.Checked;
                        _iniPosValues.PosChannelVisible[23] = cH24ToolStripMenuItem.Checked;
                        break;
                    case "SPEED":
                        sPEEDToolStripMenuItem.Checked = speedToolStripMenuItem1.Checked = !sPEEDToolStripMenuItem.Checked;
                        speedSlider.Visible = sPEEDToolStripMenuItem.Checked;
                        _iniPosValues.PosSpeedVisible = sPEEDToolStripMenuItem.Checked;
                        break;
                    case "COMMAND":
                        cOMMANDToolStripMenuItem.Checked = cOMMANDToolStripMenuItem1.Checked = !cOMMANDToolStripMenuItem.Checked;
                        cmdMnuControl.Visible = cOMMANDToolStripMenuItem.Checked;
                        _iniPosValues.PosCMDVisible = cOMMANDToolStripMenuItem.Checked;
                        break;
                    case "LINK":
                        lINKToolStripMenuItem.Checked = lINKToolStripMenuItem1.Checked =! lINKToolStripMenuItem.Checked;
                        linkControl.Visible = lINKToolStripMenuItem.Checked;
                        _iniPosValues.PosLinkVisible = lINKToolStripMenuItem.Checked;
                        break;
                    default:
                        break;
                }
            }
        }

        // Sliderfunction selector
        private void functionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // parse the string sender to enum
            Function functionNum = (Function)Enum.Parse(typeof(Function), sender.ToString());

            for (int i = 0; i < 24; i++)
            {
                if (this.ActiveControl.Tag.ToString() == (i + 1).ToString())
                {
                    horizontalSliderArray[i].sliderFunction = (int)functionNum;
                }
            }
        }

        private void bgColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.ToString() == "Etc")
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() != DialogResult.Cancel)
                {
                    this.BackColor = colorDialog.Color;
                }
            }
            else
            {
                this.BackColor = Color.FromName(sender.ToString());
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.ToString() == "Etc")
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() != DialogResult.Cancel)
                {
                    this.ActiveControl.BackColor = colorDialog.Color;
                }
            }
            else
            {
                this.ActiveControl.BackColor = Color.FromName(sender.ToString());
            }
        }

        /// <summary>
        /// Change the name of the channel sliders on the POS form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KHR_1HV_NameChannels channelNameForm = new KHR_1HV_NameChannels();
            channelNameForm.ShowDialog();

            if (channelNameForm.DialogResult == DialogResult.OK)
            {
                for (int i = 0; i < 24; i++)
                {
                    if (channelNameForm.getChannelName[i] != string.Empty)
                        horizontalSliderArray[i].sliderLabel = channelNameForm.getChannelName[i];
                }
            }
            channelNameForm.Dispose();
        }

        private void KHR_1HV_Position_FormClosing(object sender, FormClosingEventArgs e)
        {
            servos.Stop();
           // _getSliderValues = gethSliderValues();
            _iniPosValues.PosWidth = this.Width;
            _iniPosValues.PosHeight = this.Height;
            _iniPosValues.PosColor = ColorTranslator.ToOle(this.BackColor);
            for (int i = 0; i < Roboard.StaticUtilities.numberOfServos; i++)
            {
                horizontalSliderArray[i].MouseDown -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseDown);
                horizontalSliderArray[i].MouseMove -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseMove);
                horizontalSliderArray[i].MouseUp -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseUp);
                _iniPosValues.PosChannelLocationX[i] = horizontalSliderArray[i].Left;
                _iniPosValues.PosChannelLocationY[i] = horizontalSliderArray[i].Top;
                _iniPosValues.PosChannelColor[i] = ColorTranslator.ToOle(horizontalSliderArray[i].BackColor);
                _iniPosValues.PosChannelName[i] = horizontalSliderArray[i].sliderLabel;
                _iniPosValues.PosChannelFunction[i] = horizontalSliderArray[i].sliderFunction;
            }
            _iniPosValues.PosSpeedLocationX = speedSlider.Left;
            _iniPosValues.PosSpeedLocationY = speedSlider.Top;
            _iniPosValues.PosSpeedColor = ColorTranslator.ToOle(speedSlider.BackColor);
            speedSlider.MouseDown -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseDown);
            speedSlider.MouseMove -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseMove);
            speedSlider.MouseUp -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseUp);

            _iniPosValues.PosCMDLocationX = cmdMnuControl.Left;
            _iniPosValues.PosCMDLocationY = cmdMnuControl.Top;
            _iniPosValues.PosCMDColor = ColorTranslator.ToOle(cmdMnuControl.BackColor);
            cmdMnuControl.MouseDown -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseDown);
            cmdMnuControl.MouseMove -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseMove);
            cmdMnuControl.MouseUp -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseUp);

            _iniPosValues.PosLinkLocationX = linkControl.Left;
            _iniPosValues.PosLinkLocationY = linkControl.Top;
            _iniPosValues.PosLinkColor = ColorTranslator.ToOle(linkControl.BackColor);
            linkControl.MouseDown -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseDown);
            linkControl.MouseMove -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseMove);
            linkControl.MouseUp -= new System.Windows.Forms.MouseEventHandler(sliderControl_MouseUp);

            IniBestand.WritePos();
        }
    }
}