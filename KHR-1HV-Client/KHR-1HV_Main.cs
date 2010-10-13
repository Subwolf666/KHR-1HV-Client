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
public enum MyComboBox : int
{
    Close = 0,
    Open
}

namespace KHR_1HV
{
    public partial class KHR_1HV_Main : Form
    {
        //===================
        //  CLASS VARIABLES 
        //===================        
        KHR_1HV_IniFile KHR1HV_Ini;
        IniFile currentMotion;
        KHR_1HV_MotionFile readsMotion;
        private string IPAddress;
        private string fileName;
        private bool RoboardConnected;
        private bool MotionDataTableLoaded;
        private string chosenFile = string.Empty;

        private static int numOfPos = 100;
        private int[] m_theShape = new int[numOfPos];
        private Color m_theColor = Color.Red;

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
            KHR1HV_Ini = new KHR_1HV_IniFile();
            KHR1HV_Ini.Read();

            // Create the motion file 
            readsMotion = new KHR_1HV_MotionFile();
            currentMotion = readsMotion.newMotion();
            
            this.Closing += new System.ComponentModel.CancelEventHandler(this.KHR_1HV_Main_Closing);
            displayMessage("No Connection");

            DataSheet.Size = new Size(Convert.ToInt32(currentMotion["GraphicalEdit"]["Width"]) + 1, Convert.ToInt32(currentMotion["GraphicalEdit"]["Height"]) + 1);
            this.Size = new Size((27 + Convert.ToInt32(currentMotion["GraphicalEdit"]["Width"])), Convert.ToInt32(currentMotion["GraphicalEdit"]["Height"]) + 175);
            this.IPAddress = KHR1HV_Ini.IPAddress;
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
            KHR1HV_Ini.WriteGrid();
            KHR1HV_Ini.Save();
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
                    propForm.GridX = this.KHR1HV_Ini.GridWidth;
                    propForm.GridY = this.KHR1HV_Ini.GridHeight;
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
                        this.KHR1HV_Ini.GridWidth = propForm.GridX;
                        this.KHR1HV_Ini.GridHeight = propForm.GridY;
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
//=============================================================================
//  OBJECT MENU
//=============================================================================
                case eToolStripMenu.SELECT:
                    tsSelect.CheckState = CheckState.Checked;
                    tsPos.CheckState = CheckState.Unchecked;
                    tsSet.CheckState = CheckState.Unchecked;
                    tsMix.CheckState = CheckState.Unchecked;
                    tsCompare.CheckState = CheckState.Unchecked;
                    tsFlowWiring.CheckState = CheckState.Unchecked;
                    tsBranchWiring.CheckState = CheckState.Unchecked;
                    break;
                case eToolStripMenu.GRID:
                    break;
                case eToolStripMenu.STARTFLAG:
                    break;
                case eToolStripMenu.POSITION:
                    tsSelect.CheckState = CheckState.Unchecked;
                    tsPos.CheckState = CheckState.Checked;
                    tsSet.CheckState = CheckState.Unchecked;
                    tsMix.CheckState = CheckState.Unchecked;
                    tsCompare.CheckState = CheckState.Unchecked;
                    tsFlowWiring.CheckState = CheckState.Unchecked;
                    tsBranchWiring.CheckState = CheckState.Unchecked;
                    break;
                case eToolStripMenu.SET:
                    tsSelect.CheckState = CheckState.Unchecked;
                    tsPos.CheckState = CheckState.Unchecked;
                    tsSet.CheckState = CheckState.Checked;
                    tsMix.CheckState = CheckState.Unchecked;
                    tsCompare.CheckState = CheckState.Unchecked;
                    tsFlowWiring.CheckState = CheckState.Unchecked;
                    tsBranchWiring.CheckState = CheckState.Unchecked;
                    break;
                case eToolStripMenu.MIX:
                    tsSelect.CheckState = CheckState.Unchecked;
                    tsPos.CheckState = CheckState.Unchecked;
                    tsSet.CheckState = CheckState.Unchecked;
                    tsMix.CheckState = CheckState.Checked;
                    tsCompare.CheckState = CheckState.Unchecked;
                    tsFlowWiring.CheckState = CheckState.Unchecked;
                    tsBranchWiring.CheckState = CheckState.Unchecked;
                    break;
                case eToolStripMenu.COMPARE:
                    tsSelect.CheckState = CheckState.Unchecked;
                    tsPos.CheckState = CheckState.Unchecked;
                    tsSet.CheckState = CheckState.Unchecked;
                    tsMix.CheckState = CheckState.Unchecked;
                    tsCompare.CheckState = CheckState.Checked;
                    tsFlowWiring.CheckState = CheckState.Unchecked;
                    tsBranchWiring.CheckState = CheckState.Unchecked;
                    break;
                case eToolStripMenu.FLOWWIRING:
                    tsSelect.CheckState = CheckState.Unchecked;
                    tsPos.CheckState = CheckState.Unchecked;
                    tsSet.CheckState = CheckState.Unchecked;
                    tsMix.CheckState = CheckState.Unchecked;
                    tsCompare.CheckState = CheckState.Unchecked;
                    tsFlowWiring.CheckState = CheckState.Checked;
                    tsBranchWiring.CheckState = CheckState.Unchecked;
                    break;
                case eToolStripMenu.BRANCHWIRING:
                    tsSelect.CheckState = CheckState.Unchecked;
                    tsPos.CheckState = CheckState.Unchecked;
                    tsSet.CheckState = CheckState.Unchecked;
                    tsMix.CheckState = CheckState.Unchecked;
                    tsCompare.CheckState = CheckState.Unchecked;
                    tsFlowWiring.CheckState = CheckState.Unchecked;
                    tsBranchWiring.CheckState = CheckState.Checked;
                    break;
                case eToolStripMenu.COMPILE:
                    break;
//=============================================================================
//  TOOL MENU
//=============================================================================
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
                    if (RoboardConnected)
                    {
                        ToolMenu.Stop();
                    }
                    break;
                case eToolStripMenu.PAUSE:
                    if (RoboardConnected)
                    {
                        ToolMenu.Pause();
                    }
                    break;
                case eToolStripMenu.PLAY:
                    if ((RoboardConnected) && (MotionDataTableLoaded))
                    {
                        KHR_1HV_ToolMenu myPlay = new KHR_1HV_ToolMenu();
                        myPlay.Text = "Play";
                        myPlay.Label = "Select the motion or scenario to play";
                        myPlay.SelectedMotionIndex = 0;
                        myPlay.ShowDialog();
                    }
                    break;
                default:
                    break;
            }
            Roboard.NetworkClient.messageHandler += new NetworkClient.NewMessageEventHandler(NetworkClient_messageHandler);
        }

//=============================================================================
//  COMMUNICATION SETTINGS MENU
//=============================================================================
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

        private void DataSheet_MouseMove(object sender, MouseEventArgs e)
        {
            //=======================================================
            //  DISPLAYS THE X AND Y COORDINATES IN THE STATUSSTRIP
            //=======================================================
            tsStatusLabelX.Text = "X = " + e.X.ToString();
            tsStatusLabelY.Text = "Y = " + e.Y.ToString();
        }

//=============================================================================
//
//=============================================================================
        // The Item number for the iniFile
        int Item = 0;
        // The Pos number for the rectangle position name
        int Pos = 1;
        // Standard width and height
        int iWidth = 50;
        int iHeight = 30;

        // Keep track of when fake drag or resize mode is enabled.
        private bool isDragging = false;
//        private bool isResizing = false;

        // Store the location where the user clicked on the control.
        private int clickOffsetX, clickOffsetY;


        private void contextMenuHandler(object sender, EventArgs e)
        {
            DataSheet.Invalidate();
        }

        /// <summary>
        /// In deze functie wordt de keuze gemaakt welk menu item gekozen wordt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenu2Handler(object sender, EventArgs e)
        {
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

        //bool firstPos = true;
        private void DataSheet_MouseDown(object sender, MouseEventArgs e)
        {
            //Control currentCtrl;
            //currentCtrl = (Control)sender;
            if (e.Button == MouseButtons.Right)
            {
                DataSheet.ContextMenuStrip.Show(DataSheet, new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Left)
            {
                // Create and configure the shape with some defaults.
                Shape newShape = new Shape();
                newShape.Size = new Size(iWidth, iHeight);
                bool objectMenuSelected = false;

                if (tsPos.Checked == true)
                {
                    newShape.Name = "Position";// misschien moet hier de itemnummer komen te staan, zodat het uniek blijft.
                    newShape.Type = Shape.ShapeType.PosRectangle;
                    newShape.ForeColor = Color.Coral;
                    newShape.Tag = string.Format("Item{0}", Item);
                    objectMenuSelected = true;
                    //if (firstPos)
                    {
                        IniSection section = new IniSection();
                        section.Add("Name", string.Format("POS{0}",Pos));
                        section.Add("Width", iWidth.ToString());
                        section.Add("Height", iHeight.ToString());
                        section.Add("Left", e.X.ToString());
                        section.Add("Top", e.Y.ToString());
                        section.Add("Color", ColorTranslator.ToOle(Color.Black).ToString());
                        section.Add("Type", "0");
                        section.Add("Prm", "100,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384,16384");
                        currentMotion.Add(string.Format("Item{0}", Item), section);
                        Item++;
                        Pos++;
                        //firstPos = false;
                    }
                    //else
                    {
                        // copy van de vorige?
                    }
                    //currentMotion[string.Format("Item{0}", Item)]["Name"] = "POS1";
                    //currentMotion[string.Format("Item{0}", Item)]["Width"] = iWidth.ToString();
                    //currentMotion[string.Format("Item{0}", Item)]["Height"] = iHeight.ToString();
                    //currentMotion[string.Format("Item{0}", Item)]["Left"] = e.X.ToString();
                    //currentMotion[string.Format("Item{0}", Item)]["Top"] = e.Y.ToString();
                    //currentMotion[string.Format("Item{0}", Item)]["Color"] = ColorTranslator.ToOle(Color.Black).ToString();
                    //currentMotion[string.Format("Item{0}", Item)]["Type"] = "0";
                    //currentMotion[string.Format("Item{0}", Item)]["Prm"] = "100,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0";

                }
                else if (tsSet.Checked == true)
                {
                    newShape.Name = "Set";
                    newShape.Type = Shape.ShapeType.Ellipse;
                    newShape.ForeColor = Color.Blue;
                    objectMenuSelected = true;
                }
                else if (tsMix.Checked == true)
                {
                    newShape.Name = "Mix";
                    newShape.Type = Shape.ShapeType.Ellipse;
                    newShape.ForeColor = Color.Red;
                    objectMenuSelected = true;
                }
                else if (tsCompare.Checked == true)
                {
                    newShape.Name = "Compare";
                    newShape.Type = Shape.ShapeType.Ellipse;
                    newShape.ForeColor = Color.SlateGray;
                    objectMenuSelected = true;
                }
                if (objectMenuSelected == true)
                {
                    // To determine where to place the shape, you need to convert the 
                    // current screen-based mouse coordinates into relative form coordinates.
                    newShape.Location = DataSheet.PointToClient(Control.MousePosition);

                    // Attach a context menu to the shape.
                    newShape.ContextMenuStrip = contextMenu1;

                    // Connect the shape to all its event handlers.
                    newShape.MouseDown += new MouseEventHandler(newShape_MouseDown);
                    newShape.MouseMove += new MouseEventHandler(newShape_MouseMove);
                    newShape.MouseUp += new MouseEventHandler(newShape_MouseUp);
                    newShape.MouseDoubleClick += new MouseEventHandler(newShape_MouseDoubleClick);
                    // Add the shape to the form.
                    DataSheet.Controls.Add(newShape);

                    //currentMotion
                }
            }
        }

        void newShape_MouseDown(object sender, MouseEventArgs e)
        {
            // Retrieve a reference to the active label.
            Control currentCtrl;
            currentCtrl = (Control)sender;

            if (e.Button == MouseButtons.Right)
            {
                // Show the context menu.
                currentCtrl.ContextMenuStrip.Show(currentCtrl, new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Left)
            {
                clickOffsetX = e.X;
                clickOffsetY = e.Y;

                //if ((e.X + 5) > currentCtrl.Width || (e.Y + 5) > currentCtrl.Height)
                //{
                //    // The mouse pointer is in the bottom right corner,
                //    // so resizing mode is appropriate.
                //    isResizing = true;
                //}
                //else
                {
                    // The mouse is somewhere else, so dragging mode is
                    // appropriate.
                    isDragging = true;
                }
            }
        }

        void newShape_MouseMove(object sender, MouseEventArgs e)
        {
            // Retrieve a reference to the active shape.
            Control currentCtrl;
            currentCtrl = (Control)sender;

            if (isDragging)
            {
                // Move the control.
                currentCtrl.Left = e.X + currentCtrl.Left - clickOffsetX;
                currentCtrl.Top = e.Y + currentCtrl.Top - clickOffsetY;
            }
            //else if (isResizing)
            //{
            //    // Resize the control, according to the resize mode.
            //    if (currentCtrl.Cursor == Cursors.SizeNWSE)
            //    {
            //        currentCtrl.Width = e.X;
            //        currentCtrl.Height = e.Y;
            //    }
            //    else if (currentCtrl.Cursor == Cursors.SizeNS)
            //    {
            //        currentCtrl.Height = e.Y;
            //    }
            //    else if (currentCtrl.Cursor == Cursors.SizeWE)
            //    {
            //        currentCtrl.Width = e.X;
            //    }
            //}
            //else
            //{
            //    // Change the cursor if the mouse pointer is on one of the edges
            //    // of the control.
            //    if (((e.X + 5) > currentCtrl.Width) &&
            //        ((e.Y + 5) > currentCtrl.Height))
            //    {
            //        currentCtrl.Cursor = Cursors.SizeNWSE;
            //    }
            //    else if ((e.X + 5) > currentCtrl.Width)
            //    {
            //        currentCtrl.Cursor = Cursors.SizeWE;
            //    }
            //    else if ((e.Y + 5) > currentCtrl.Height)
            //    {
            //        currentCtrl.Cursor = Cursors.SizeNS;
            //    }
            //    else
            //    {
            //        currentCtrl.Cursor = Cursors.Arrow;
            //    }
            //}
        }

        void newShape_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            //isResizing = false;
        }

        void newShape_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control currentCtrl;
            currentCtrl = (Control)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (currentCtrl.Name == "Position")
                {
                    // Show the KHR-1HV_Position Form
                    KHR_1HV_Position myPosition = new KHR_1HV_Position();
                    myPosition.IniBestand = KHR1HV_Ini;
                    myPosition.Parameter = currentMotion[currentCtrl.Tag.ToString()]["Prm"].Split(',');
                    myPosition.ShowDialog();
                    currentMotion[currentCtrl.Tag.ToString()]["Prm"] = string.Join(",", myPosition.Parameter);
                    // in de delete functie worden de items allemaal teruggebracht naar een mooie rij.
                    // zodat als er een nieuwe geplaatst wordt hier gemakkelijk in de rij toegevoegd kan worden.

                }
                else if (currentCtrl.Name == "Set")
                {
                    KHR_1HV_Set mySet = new KHR_1HV_Set();
                    mySet.ShowDialog();
                }
                else if (currentCtrl.Name == "Mix")
                {
                    KHR_1HV_Mix myMix = new KHR_1HV_Mix();
                    myMix.ShowDialog();
                }
                else if (currentCtrl.Name == "Compare")
                {
                    KHR_1HV_Compare myCompare = new KHR_1HV_Compare();
                    myCompare.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Deletes the shape that is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shape ctrlShape = (Shape)contextMenu1.SourceControl;
            string piet = ctrlShape.Name;
            DataSheet.Controls.Remove(ctrlShape);
            // Do something with the POS and Item variables.
            // But what??????

        }
    }
}

//===================================================================

public class Shape : System.Windows.Forms.UserControl
{
    // The types of shapes supported by this control.
    public enum ShapeType
    {
        PosRectangle,
        Ellipse,
        Triangle
    }

    private ShapeType shape = ShapeType.PosRectangle;
    private GraphicsPath path = null;
    private string item = string.Empty;

    public ShapeType Type
    {
        get
        {
            return shape;
        }
        set
        {
            shape = value;
            RefreshPath();
            this.Invalidate();
        }
    }

    /// <summary>
    /// Set/Get the Item number of the frame
    /// </summary>
    public string Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            RefreshPath();
            this.Invalidate();
        }
    }

    // Create the corresponding GraphicsPath for the shape, and apply
    // it to the control by setting the Region property.
    private void RefreshPath()
    {
        path = new GraphicsPath();
        switch (shape)
        {
            case ShapeType.PosRectangle:
                path.AddRectangle(this.ClientRectangle);
                break;
            case ShapeType.Ellipse:
                path.AddEllipse(this.ClientRectangle);
                break;
            case ShapeType.Triangle:
                Point pt1 = new Point(this.Width / 2, 0);
                Point pt2 = new Point(0, this.Height);
                Point pt3 = new Point(this.Width, this.Height);
                path.AddPolygon(new Point[] { pt1, pt2, pt3 });
                break;
        }
        this.Region = new Region(path);
    }

    protected override void OnResize(System.EventArgs e)
    {
        base.OnResize(e);
        RefreshPath();
        this.Invalidate();
    }

    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
        base.OnPaint(e);
        if (path != null)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillPath(new SolidBrush(this.BackColor), path);
            e.Graphics.DrawPath(new Pen(this.ForeColor, 4), path);
        }
    }

}
