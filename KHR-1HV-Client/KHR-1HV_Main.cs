using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Roboard;

public enum eToolStripMenu : int
{
    NONE = 0,
    LOAD,
    SAVE,
    PROPERTIES,
    INFORMATION,
    EXIT,
    TRIM,
    OPTION,
    ANALOG,
    ICS,
    RECEIVER,
    TABLE,
//    SYNC,
//    CONNECT,
    SELECT,
    GRID,
    STARTFLAG,
    POSITION,
    SET,
    MIX,
    COMPARE,
    FLOWWIRING,
    BRANCHWIRING,
    COMPILE,
    HOMEPOSITION,
    TRIMPOSITION,
    WRITE,
    READ,
    DELETE,
    STOP,
    PAUSE,
    PLAY
}

namespace KHR_1HV
{
    public partial class KHR_1HV_Main : Form
    {
        //===================
        //
        enum MyComboBox
        {
            Close = 0,
            Open
        }

        //===================
        //  CLASS VARIABLES 
        //===================        
        KHR_1HV_IniFile readIni;
        IniFile currentMotion;
        KHR_1HV_MotionFile readsMotion;
        private string IPAddress;
        private string fileName;
        private bool RoboardConnected;
        private bool MotionDataTableLoaded;
        private bool playingMotion = false;
        
        //=======================
        //  DEFAULT CONSTRUCTOR
        //=======================
        public KHR_1HV_Main()
        {
            InitializeComponent();
        }

        private void KHR_1HV_Main_Load(object sender, EventArgs e)
        {
            RoboardConnected = false; // no roboard connected
            MotionDataTableLoaded = false; // no Motion DataTable loaded
            DataSheet.Image = null; // Clean Datasheet

            // Read the KHR-1HV initialization file.
            readIni = new KHR_1HV_IniFile();
            readIni.Read();

            // Create the motion file 
            readsMotion = new KHR_1HV_MotionFile();
            currentMotion = readsMotion.newMotion();

            this.Closing += new System.ComponentModel.CancelEventHandler(this.KHR_1HV_Main_Closing);
            displayMessage("No Connection");

            //m_theRect = new Rectangle[numOfPos];
            //for (int i = 0; i < numOfPos; i++)
            //{
            //    // Initialize one variable
            //    m_theRect[i] = new Rectangle();
            //}
            DataSheet.Size = new Size(Convert.ToInt32(currentMotion["GraphicalEdit"]["Width"]) + 1, Convert.ToInt32(currentMotion["GraphicalEdit"]["Height"]) + 1);
            this.Size = new Size((27 + Convert.ToInt32(currentMotion["GraphicalEdit"]["Width"])), Convert.ToInt32(currentMotion["GraphicalEdit"]["Height"]) + 175);
            this.IPAddress = readIni.IPAddress;
            Roboard.NetworkClient.ServerIPAddress = this.IPAddress;

            Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
        }

        void NetworkClient_messageHandler(object sender, NewMessageEventsArgs e)
        {
            if (e.NewMessage == "Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host.")
            {
                Roboard.NetworkClient.CloseConnection();
                setConnect((int)MyComboBox.Close);
            }
        }

        //==========================
        //  EXIT'S THE APPLICATION
        //==========================
        private void KHR_1HV_Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (exitForm() == false)
            {
                e.Cancel = true;
            }
            else
            {
                Application.DoEvents();
                Application.Exit();
            }
            Roboard.NetworkClient.messageHandler -= new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
            Roboard.NetworkClient.CloseConnection();
            readIni.WriteGrid();
            readIni.Save();
        }
        
        private bool exitForm()
        {
            if (MessageBox.Show("Are you sure you want to exit?",
                               "Confirm exit",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
                return false;
        }

        private static int numOfPos = 100;
        private int[] m_theShape = new int[numOfPos];
        //private Rectangle[] m_theRect;
        private Color m_theColor = Color.Red;

        private void contextMenuHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = sender as ToolStripMenuItem;
            if (mi != null)
            {
                Color c;
                if (mi.Text == "Red")
                    c = Color.Red;
                else if (mi.Text == "Blue")
                    c = Color.Blue;
                else
                    c = Color.Yellow;
                this.BackColor = c;

                DataSheet.Invalidate();
            }
        }

        private void contextMenu2Handler(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = sender as ToolStripMenuItem;

            if (mi != null)
            {
                for (int i = 0; i < numOfPos; i++)
                {
                    if (mi == circleItem)
                        m_theShape[i] = 1;
                    else
                        m_theShape[i] = 0;

                    Invalidate();
                }
            }            
        }
        /// <summary>
        /// Verander hier tussen de twee verschillende contextmenu's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataSheet_MouseUp(object sender, MouseEventArgs e)
        {
            bool contextmenu = false;
            if (e.Button != MouseButtons.Right) return;

            //for (int i = 0; i < numOfPos; i++)
            //{
            //    if (m_theRect[i].Contains(e.X, e.Y))
            //    {
            //        contextMenu1.Show(DataSheet, new Point(e.X, e.Y));
            //        contextmenu = true;
            //        break;
            //    }
            //}
            if (!contextmenu)
            {
                contextMenu2.Show(DataSheet, new Point(e.X, e.Y));
            }
            contextmenu = false;
        }

        // Method
        //
        private void convertMotionFile()
        {
            int Items = Convert.ToInt32(currentMotion[StaticUtilities.SectionGraphicalEdit][StaticUtilities.GraphicalEditItems]);
            for (int i = 0; i < Items; i++)
            {
                string Item = string.Format("Item{0}", i);
                if (Convert.ToInt32(currentMotion[Item][StaticUtilities.ItemType]) == 0)
                {
                    string[] Prm = currentMotion[Item][StaticUtilities.ItemPrm].Split(',');
                    int Speed = Convert.ToInt32(Prm[0]) * StaticUtilities.conversionSpeed;
                    
                    int Channel;
                    string[] strChannel = new string[StaticUtilities.numberOfServos];
                    for (int j = 0; j < StaticUtilities.numberOfServos; j++)
                    {
                        double kut = Convert.ToDouble(Prm[j + 1]);
                        if (kut != 0)
                        {
                            Channel = Convert.ToInt32((kut - 16384) * StaticUtilities.conversionAngle) + 16384;
                            strChannel[j] = Convert.ToString(Channel);
                        }
                        else
                            strChannel[j] = "0";
                    }
                    string kutje = Convert.ToString(Speed) + "," + string.Join(",", strChannel);
                    currentMotion[Item][StaticUtilities.ItemPrm] = kutje;
                }

            }
        }

        private string chosenFile = string.Empty;
        private void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Roboard.NetworkClient.messageHandler -= new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
            ToolStripItem item = e.ClickedItem;
            
            // parse the string sender to enum
            eToolStripMenu tsMenuNum = (eToolStripMenu)Enum.Parse(typeof(eToolStripMenu), item.Text.ToUpper());

            switch (tsMenuNum)
            {
                //
                //  MAIN MENU
                //
                case eToolStripMenu.LOAD:
                    this.openFD.Title = "Open a Roboard Motion File";
                    this.openFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    this.openFD.FileName = string.Empty;
                    this.openFD.Filter = "Roboard Motion File (RMF)|*.RMF|Kondo RCB files|*.RCB|All files|*.*";
                    if (this.openFD.ShowDialog() != DialogResult.Cancel)
                    {
                        fileName = this.openFD.SafeFileName;
                        chosenFile = this.openFD.FileName;
                        currentMotion = new IniFile(chosenFile);
                        if (currentMotion.Exists())
                        {
                            currentMotion.Load();
                            // check of de geladen rcb file wel een goede is.
                            this.tcDataSheet.TabPages[0].Text = "EDIT " + currentMotion["GraphicalEdit"]["Name"];
                            this.DataSheet.Size = new Size(Convert.ToInt32(currentMotion["GraphicalEdit"]["Width"]), Convert.ToInt32(currentMotion["GraphicalEdit"]["Height"]));
                            // Get file extension
                            string fileExtension =  Path.GetExtension(chosenFile);
                            if (fileExtension == ".RCB") // This file needs to be converted.
                            // convert
                            {
                                convertMotionFile();
                            }
                            // teken de motion.
                        }
                    }
                    break;
                case eToolStripMenu.SAVE:
                    this.saveFD.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    this.saveFD.Title = "Save the Roboard Motion File";
                    this.saveFD.AddExtension = true;
                    this.saveFD.DefaultExt = ".RMF";
                    this.saveFD.FileName = chosenFile;
                    this.saveFD.Filter = "Roboard Motion File (RMF) files|*.RMF|All files|*.*";
                    if (this.saveFD.ShowDialog() != DialogResult.Cancel)
                    {
                        chosenFile = saveFD.FileName;
                        currentMotion.FileName = chosenFile;
                        currentMotion.Save();
                    }
                    break;
                case eToolStripMenu.PROPERTIES:
                    KHR_1HV_Properties propForm = new KHR_1HV_Properties();
                    propForm.sizeWidth = this.DataSheet.Width;
                    propForm.sizeHeight = this.DataSheet.Height;
                    propForm.GridX = this.readIni.GridWidth;
                    propForm.GridY = this.readIni.GridHeight;
                    propForm.ToolMenu = this.tsToolMenu.Visible;
                    propForm.PartsMenu = this.tsObjectMenu.Visible;
                    propForm.CommSettings = this.tsCommunicationsMenu.Visible;
                    propForm.CommandMenu = this.tsCommandMenu.Visible;
                    propForm.ShowDialog();
                    if (propForm.DialogResult == DialogResult.OK)
                    {
                        this.DataSheet.Size = new System.Drawing.Size(propForm.sizeWidth, propForm.sizeHeight);
                        this.currentMotion[Roboard.StaticUtilities.SectionGraphicalEdit][Roboard.StaticUtilities.GraphicalEditWidth] = propForm.sizeWidth.ToString();
                        this.currentMotion[Roboard.StaticUtilities.SectionGraphicalEdit][Roboard.StaticUtilities.GraphicalEditHeight] = propForm.sizeHeight.ToString();
                        this.tsToolMenu.Visible = propForm.ToolMenu;
                        this.tsObjectMenu.Visible = propForm.PartsMenu;
                        this.tsCommunicationsMenu.Visible = propForm.CommSettings;
                        this.tsCommandMenu.Visible = propForm.CommandMenu;
                        this.readIni.GridWidth = propForm.GridX;
                        this.readIni.GridHeight = propForm.GridY;
                    }
                    break;
                case eToolStripMenu.INFORMATION:
                    if (this.RoboardConnected)
                    {
                        KHR_1HV_Information InformationForm = new KHR_1HV_Information();
                        InformationForm.ShowDialog();
                    }
                    break;
                case eToolStripMenu.EXIT:
                    if (exitForm() == true)
                    {
                        Application.Exit();
                    }
                    break;
                //
                //  COMMAND MENU
                //
                case eToolStripMenu.TRIM:
                    if (RoboardConnected)
                    {
                        // get trim values from the server
                        KHR_1HV_Trim secondForm = new KHR_1HV_Trim();
                        secondForm.ShowDialog();
                    }
                    break;
                case eToolStripMenu.OPTION:
                    if (RoboardConnected)
                    {
                        KHR_1HV_Options optionsForm = new KHR_1HV_Options();
                        optionsForm.ShowDialog();
                    }
                    break;
                case eToolStripMenu.ANALOG:
                    if (RoboardConnected)
                    {
                        KHR_1HV_Sensors AnalogForm = new KHR_1HV_Sensors();
                        AnalogForm.ShowDialog();
                    }
                    break;
                case eToolStripMenu.ICS:
                    break;
                case eToolStripMenu.RECEIVER:
                    if (RoboardConnected)
                    {
                        KHR_1HV_XBox360Controller myXBox360 = new KHR_1HV_XBox360Controller();
                        myXBox360.ShowDialog();
                    }
                    break;
                case eToolStripMenu.TABLE:
                    if (RoboardConnected)
                    {
                        KHR_1HV_DataTable myDataTable = new KHR_1HV_DataTable();
                        myDataTable.ShowDialog();
                        this.MotionDataTableLoaded = myDataTable.Done;
                    }
                    break;
                //
                //  OBJECT MENU
                //
                case eToolStripMenu.SELECT:
                    break;
                case eToolStripMenu.GRID:
                    break;
                case eToolStripMenu.STARTFLAG:
                    break;
                case eToolStripMenu.POSITION:
                    tsSelect.CheckState = CheckState.Unchecked;
                    tsPos.CheckState = CheckState.Checked;
                    KHR_1HV_Position PositionForm = new KHR_1HV_Position();
                    // de juiste position uit de readini halen voor de form.
                    PositionForm.IniBestand = readIni;
                    PositionForm.ShowDialog();
                    break;
                case eToolStripMenu.SET:
                    KHR_1HV_Set SetForm = new KHR_1HV_Set();
                    SetForm.ShowDialog();
                    break;
                case eToolStripMenu.MIX:
                    break;
                case eToolStripMenu.COMPARE:
                    break;
                case eToolStripMenu.FLOWWIRING:
                    break;
                case eToolStripMenu.BRANCHWIRING:
                    break;
                case eToolStripMenu.COMPILE:
                    break;
                //
                //  TOOL MENU
                //
                case eToolStripMenu.HOMEPOSITION:
                    if (RoboardConnected)
                    {
                    }
                    break;
                case eToolStripMenu.TRIMPOSITION:
                    if (RoboardConnected)
                    {
                    }
                    break;
                case eToolStripMenu.WRITE:
                    if ((RoboardConnected) && (MotionDataTableLoaded))
                    {
                        KHR_1HV_ToolMenu myWrite = new KHR_1HV_ToolMenu();
                        myWrite.Text = "Write";
                        myWrite.Label = "Select Roboard memory location";
                        myWrite.Motion = currentMotion;
                        myWrite.ShowDialog();
                    }
                    break;
                case eToolStripMenu.READ:
                    if ((RoboardConnected) && (MotionDataTableLoaded))
                    {
                        KHR_1HV_ToolMenu myRead = new KHR_1HV_ToolMenu();
                        myRead.Text = "Read";
                        myRead.Label = "Select Roboard memory location";
                        myRead.ShowDialog();
                        if (myRead.Motion != null)
                        {
                            currentMotion = myRead.Motion;
                            this.tcDataSheet.TabPages[0].Text = "EDIT " + currentMotion["GraphicalEdit"]["Name"];
                            this.DataSheet.Size = new Size(Convert.ToInt32(currentMotion["GraphicalEdit"]["Width"]), Convert.ToInt32(currentMotion["GraphicalEdit"]["Height"]));
                            // teken de motion
                        }
                    }
                    break;
                case eToolStripMenu.DELETE:
                    if ((RoboardConnected) && (MotionDataTableLoaded))
                    {
                        KHR_1HV_ToolMenu myDelete = new KHR_1HV_ToolMenu();
                        myDelete.Text = "Delete";
                        myDelete.Label = "Select the motion or scenario to be deleted";
                        myDelete.ShowDialog();
                    }
                    break;
                case eToolStripMenu.STOP:
                    if ((RoboardConnected) && (playingMotion))
                    {
                        ToolMenu stopMotion = new ToolMenu();
                        stopMotion.Stop();
                    }
                    // in welke class moet stop en pause zitten
                    break;
                case eToolStripMenu.PAUSE:
                    if ((RoboardConnected) && (playingMotion))
                    {
                        ToolMenu pauseMotion = new ToolMenu();
                        pauseMotion.Pause();
                    }
                    break;
                case eToolStripMenu.PLAY:
                    if (RoboardConnected)
                    {
                        KHR_1HV_ToolMenu myPlay = new KHR_1HV_ToolMenu();
                        myPlay.Text = "Play";
                        myPlay.Label = "Select the motion or scenario to play";
                        myPlay.SelectedMotionIndex = 0;
                        myPlay.ShowDialog();
                        playingMotion = false;
                        if (myPlay.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            playingMotion = true;
                    }
                    break;
                default:
                    break;
            }
            Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
        }

        private void DataSheet_Click(object sender, EventArgs e)
        {
            tsSelect.CheckState = CheckState.Checked;
            tsPos.CheckState = CheckState.Unchecked;
        }

        //===============================
        //  COMMUNICATION SETTINGS MENU
        //===============================

        private void tsConnect_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            KHR_1HV_Lib init_roboard = new KHR_1HV_Lib();
            string returnMsg = init_roboard.InitializeLibraries(tsConnect.SelectedItem.ToString());
            RoboardConnected = init_roboard.Connected;
            this.Cursor = Cursors.Default;
            displayMessage(returnMsg);
        }

        private void tsSync_CheckedChanged(object sender, EventArgs e)
        {
            //====================================
            //  SYNCHRONIZATION WITH THE ROBOARD
            //====================================
//            client.Synchronization = tsSync.Checked;
        }

        private void DataSheet_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            KHR_1HV_DataDialog DataDialog = new KHR_1HV_DataDialog();
            DataDialog.dataName = currentMotion["GraphicalEdit"]["Name"];
            if ((currentMotion["GraphicalEdit"]["Ctrl"] == string.Empty) || (currentMotion["GraphicalEdit"]["Ctrl"] == "0"))
                DataDialog.remoteControllerCode = "65535";
            else
                DataDialog.remoteControllerCode = currentMotion["GraphicalEdit"]["Ctrl"];

            dr = DataDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (DataDialog.dataName == string.Empty)
                    this.tcDataSheet.TabPages[0].Text = "EDIT " + "no title";
                else
                    this.tcDataSheet.TabPages[0].Text = "EDIT " + DataDialog.dataName;

                currentMotion["GraphicalEdit"]["Name"] = DataDialog.dataName;
                currentMotion["GraphicalEdit"]["Ctrl"] = DataDialog.remoteControllerCode;

            }
            else if (dr == DialogResult.Cancel)
            {
            }
        }

        private void DataSheet_MouseMove(object sender, MouseEventArgs e)
        {
            //=======================================================
            //  DISPLAYS THE X AND Y COORDINATES IN THE STATUSSTRIP
            //=======================================================
            tsStatusLabelX.Text = "X = " + e.X.ToString();
            tsStatusLabelY.Text = "Y = " + e.Y.ToString();
        }

        //  METHOD
        //
        private delegate void setConnectDelegate(int item);
        private void setConnect(int item)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new setConnectDelegate(setConnect), new object[] { item });
            }
            else
            {
                this.tsConnect.SelectedIndex = item;
            }
        }

        //  METHOD
        //
        private delegate void displayMessageDelegate(string messageToDisplay);
        private void displayMessage(string messageToDisplay)
        {
            // 24 hour time (16:12:11)
            DateTime currentTime = DateTime.Now;
            // logging naar file??? wel leuk....

            if (this.tsMessage.InvokeRequired)
            {
                this.tsMessage.BeginInvoke(new displayMessageDelegate(displayMessage), new object[] { messageToDisplay });
            }
            else
            {
                this.tsTextBoxMessage.Text = currentTime.ToString("T") + "> " + messageToDisplay;
            }
        }
    }
}
