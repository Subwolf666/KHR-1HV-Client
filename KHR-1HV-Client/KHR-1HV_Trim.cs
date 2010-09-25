using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HorizontalSlider;
using Ini;
using Roboard;

namespace KHR_1HV
{
    public partial class KHR_1HV_Trim : Form
    {
        //===================
        //  CLASS VARIABLES 
        //===================
        private HorizontalSlider.SliderControl[] horizontalSliderArray;
        private TrimCommandMenu.TrimCommandMenuControl cmdMnuControl = new TrimCommandMenu.TrimCommandMenuControl();

        private Roboard.TrimServos trimservos;
        private IniFile trimFile;
        private string[] saServo = new string[StaticUtilities.numberOfServos];

        //public delegate void horizontalSliderEventHandler(object sender, ScrollEventHandler e);

        //=======================
        //  DEFAULT CONSTRUCTOR
        //=======================
        public KHR_1HV_Trim()
        {
            InitializeComponent();
        }

        private void KHR_1HV_Trim_Load(object sender, EventArgs e)
        {
            this.Closing += new System.ComponentModel.CancelEventHandler(this.KHR_1HV_Trim_Closing);

            ShowHorizontalSliders();

            trimservos = new TrimServos();
            trimservos.TrimServosHandler += new TrimServos.TrimServosEventHandler(trimservos_TrimServosHandler);
            trimservos.Start();
        }

        // Closing Method
        //
        private void KHR_1HV_Trim_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // zorg hier dat de data terug gestuurd word naar de server
            trimservos.Stop();
        }

        // Method
        // Send the changed trim channel to the server
        //
        private void KHR_1HV_Trim_SliderControlChanged(object sender, SliderControlEventsArgs e)
        {
            // which channel is changed
            int channelNumber = (int)(sender as SliderControl).Tag - 1;
            // and what is the value of that channel
            int channelValue = e.Value;
            // Add the changed value to the servo channel array
            saServo.SetValue(Convert.ToString(channelValue), channelNumber);
            // send the servo channel array to the server
            trimservos.changeAllChannels(saServo);
        }

        //==========
        //  METHOD
        //==========
        private void ShowHorizontalSliders()
        {
            int xPos = 0;
            int yPos = 0;

            horizontalSliderArray = new HorizontalSlider.SliderControl[StaticUtilities.numberOfServos];
            for (int i = 0; i < StaticUtilities.numberOfServos; i++)
            {
                // Initialize one variable
                horizontalSliderArray[i] = new HorizontalSlider.SliderControl();
            }

            int n = 0;
            while (n < StaticUtilities.numberOfServos)
            {
                horizontalSliderArray[n].Tag = n + 1;
                horizontalSliderArray[n].TabIndex = n;
                horizontalSliderArray[n].sliderLabel = "CH" + horizontalSliderArray[n].Tag.ToString();
                if (yPos > 352) // Twelve Sliders in one column
                {
                    yPos = 0;
                    xPos = xPos + horizontalSliderArray[n].Width - 1;
                }
                horizontalSliderArray[n].Left = xPos;
                horizontalSliderArray[n].Top = yPos;
                horizontalSliderArray[n].sliderFunction = (int)Function.SERVO;
                yPos = yPos + horizontalSliderArray[n].Height - 1;
                this.Controls.Add(horizontalSliderArray[n]);
                horizontalSliderArray[n].BringToFront();
                horizontalSliderArray[n].SliderControlChanged += new SliderControl.SliderControlEventHandler(KHR_1HV_Trim_SliderControlChanged);
                n++;
            }
            cmdMnuControl.Tag = n + 1;
            cmdMnuControl.TabIndex = n;
            cmdMnuControl.Left = 0;
            cmdMnuControl.Top = yPos + 31;
            this.Controls.Add(cmdMnuControl);
            cmdMnuControl.BringToFront();
            cmdMnuControl.TrimFileDialogPressed += new TrimCommandMenu.TrimCommandMenuControl.TrimFileDialogEventHandler(cmdMnuControl_TrimFileDialogPressed);
        }

        void cmdMnuControl_TrimFileDialogPressed(object sender, TrimCommandMenu.TrimFileDialogEventsArgs e)
        {
            if (e.Filedialog == "Open")
            {
                string _fileName = string.Empty;
                this.openFD.Title = "Open a Robot Trim File";
                this.openFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                this.openFD.FileName = string.Empty;
                this.openFD.Filter = "Robot Trim files|*.rcb|All files|*.*";
                if (this.openFD.ShowDialog() != DialogResult.Cancel)
                {
                    trimFile = new IniFile(this.openFD.FileName);
                    if (trimFile.Exists())
                    {
                        if (trimFile.Load())
                        {
                            try
                            {
                                // Load the selected trim file
                                saServo = trimFile["Trim"]["Prm"].Split(',');
                                // Position the sliders on the UI
                                trimservosSlider(saServo);
                                // Send the servo trim values to the server
                                trimservos.changeAllChannels(saServo);
                            }
                            catch
                            {
                                MessageBox.Show("No Trim file...");
                            }
                        }
                    }
                }

            }
            else if (e.Filedialog == "Save")
            {
                string str = string.Empty;
                string[] strArray = new string[StaticUtilities.numberOfServos];
                for (int i = 0; i < StaticUtilities.numberOfServos; i++)
                {
                    strArray[i] = horizontalSliderArray[i].sliderValue.ToString();
                }
                str = string.Join(",", strArray);
                this.saveFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                this.saveFD.Title = "Save the Robot Trim File";
                this.saveFD.Filter = "Robot Trim Files|*.rcb|All files|*.*";
                this.saveFD.AddExtension = true;
                if (this.saveFD.ShowDialog() != DialogResult.Cancel)
                {
                    trimFile = new IniFile(this.saveFD.FileName);
                    IniSection section = new IniSection();
                    section.Add("Prm", str);
                    trimFile.Add("Trim", section);
                    trimFile.Save();                    
                }
            }
        }

        // Method
        // Receive and display the trim servo's from the server onto the UI
        //
        void trimservos_TrimServosHandler(object sender, TrimServosEventArgs e)
        {            
            Array.Copy(e.TrimServos, saServo, saServo.Length);
            trimservosSlider(saServo);
        }

        // Method om de threading te omzeilen.
        // probeer eens om de delegate weg te halen.
        private delegate void trimservosSliderDelegate(string[] trimservos);
        private void trimservosSlider(string[] trimservos)
        {
            int i = 0;
            foreach (string value in trimservos)
            {
                if (this.horizontalSliderArray[i].InvokeRequired)
                {
                    this.horizontalSliderArray[i].BeginInvoke(new trimservosSliderDelegate(trimservosSlider), new object[] { trimservos });
                }
                else
                {
                    this.horizontalSliderArray[i].sliderValue = int.Parse(value);
                }
                i++;
            }
        }
    }
}