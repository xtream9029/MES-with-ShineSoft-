using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Configuration;
using System.Net;

using SEComEnabler.SECSMsgLibrary;
using SEComEnabler.SEComUtility;
using SEComEnabler.SEComStructure;
using SEComEnabler.SEComPlugIn;

using VirtualMES.Common;
using VirtualMES.Forms;

namespace VirtualMES
{
    public partial class frmMain : Form
    {
        #region Define Global Variable
        // VARIABLES DECLARATION GOES HERE

        public static CMessageStats m_MsgStats;
        public static CLogColors m_LogColors;
        public static int IsConect;

        public static CCommunicationInfo m_CommnInfo;
        private CSEComConf m_SEComConf;
        public static CSEComPlugIn m_SEComPlugIn;
        private FilterData m_FilterData;

        public static int m_nMainThreadID;
        
        private char m_chPathSeparator;

        // used to handle locked and unlocked states of edit dialog
        public static bool m_blIsLocked = false;

        // used to handle the display last line in the logs
        public static bool m_blIsFocus = true;
        
        // this variable stores the name of the library file loaded/opened
        private string m_strCurLibFile;
        public static string m_strIdentity = "";
        
        // these variables are used to store the SECS1, SECS2 Log contents of Convert Library dialog
        public static string m_ConvertLibLog1, m_ConvertLibLog2;
        public static bool m_blIsLoadSECS2Log = false;
        public static bool m_blIsAppendSECS2Log = false;

        private const string m_strUserLib = "USER LIBRARY";
        private const string m_strDefaultLib = "DEFAULT LIBRARY";

        // this variable stores the SECOMID of the current instance
        public static string m_strCurSEComID;

        // Form
        private frmEdit m_objfrmEdit = null;

        public delegate void OnReplyEventHandler(SEComData sd);
        public static event OnReplyEventHandler ReplayReceived;
        //public delegate void OnS6F11EventHandler(SEComData sd);
        //public static event OnS6F11EventHandler S6F11Received;

        // Data
        public static VirtualMES.MesData.TB_EQUIP_INF_ROUTE TB_EQUIP_INF_ROUTE;
        public static VirtualMES.MesData.TB_TRAY_INF_ROUTE TB_TRAY_INF_ROUTE;
        public static VirtualMES.MesData.TB_CHARGE_STOCK TB_CHARGE_STOCK;
        #endregion

        private enum Connection_Status
        {
            SECS_OpenConnection,
            SECS_CloseConnection,
            SECS_Connected,
            SECS_DisConnected
        }

        #region Form Initilize
        public frmMain()
        {
            InitializeComponent();
            IsConect = 0;
            m_SEComConf = new CSEComConf();
            m_CommnInfo = new CCommunicationInfo();
            m_LogColors = new CLogColors();
            m_MsgStats = new CMessageStats();

            // Store the Current SEComID in a global variable
            m_strCurSEComID = this.m_SEComConf.GetLastSEComID().ToUpper();
            
            // deserialize the fiterdata
            this.m_FilterData = this.DeSerializeFilterData();
        }
        #endregion

        //******************************************************************
        // Form Load Event
        //******************************************************************
        #region Form Load
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                ////AutoUpdate();
                //BasicAuthentication basicAuthentication = new BasicAuthentication("ftpuser", "ecs723181");
                //AutoUpdater.BasicAuthXML = AutoUpdater.BasicAuthDownload = basicAuthentication;
                //AutoUpdater.Mandatory = true;
                //AutoUpdater.UpdateMode = Mode.Forced;
                //AutoUpdater.Start("ftp://192.168.11.131:8010/test/AutoUpdaterTest.xml", new NetworkCredential("ftpuser", "ecs723181"));

                // Get the Identity value for the Current SEComID
                m_strIdentity = this.GetIdentity();
                
                if (System.Environment.GetCommandLineArgs().Length > 1)
                {
                    if (System.IO.File.Exists(System.Environment.GetCommandLineArgs()[1]))
                    {
                        m_strCurLibFile = System.Environment.GetCommandLineArgs()[1];
                        this.LoadDefaultLibrary(m_strCurLibFile);
                    }
                }
                else
                {
                    if (System.IO.File.Exists(CCommonConst.UserDefaultLibraryFile)) // If User Default File exists, load USERLIB.SMD
                    {
                        m_strCurLibFile = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + CCommonConst.UserDefaultLibraryFile;
                        this.LoadDefaultLibrary(m_strCurLibFile);
                        this.tvwSECSMessage.Nodes[0].Text = m_strUserLib;
                    }
                    else    // else load DEFAULT.SMD
                    {
                        // Load the default library from resource
                        m_strCurLibFile = this.CopyResourceToFile(CCommonConst.DefaultLibraryFile);
                        this.LoadDefaultLibrary(m_strCurLibFile);
                    }
                }

                this.m_chPathSeparator = Convert.ToChar(this.tvwSECSMessage.PathSeparator);
                this.StartPosition = FormStartPosition.CenterScreen;

                // display the library file name and the connection status in the status bar
                toolStripStatusLabel1.Text = "File Name : " + m_strCurLibFile.Substring(m_strCurLibFile.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                toolStripStatusLabel2.Text = "SECS Connection Closed";
                toolStripStatusLabel3.Text = "DB Connection Closed";

                //// Set the Caption
                //this.SetFormTitle();

                //this.AddMenuItemsToContextMenu();

                //// DB Connection
                //string connection = ConfigurationManager.AppSettings["connectionString"].ToString();
                //if (VirtualMES.Util.OracleUtil.ConnectionDB(connection))
                //    toolStripStatusLabel3.Text = "DB Connection";


                // MES BASE DATA READ
                string FileName = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "Config" + System.IO.Path.DirectorySeparatorChar + "TB_EQUIP_INF_ROUTE.xlsx";
                TB_EQUIP_INF_ROUTE = new MesData.TB_EQUIP_INF_ROUTE(FileName);


                FileName = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "Config" + System.IO.Path.DirectorySeparatorChar + "TB_TRAY_INF_ROUTE.xlsx";
                TB_TRAY_INF_ROUTE = new MesData.TB_TRAY_INF_ROUTE(FileName);

                // MES CHARGE STOCK INIT : 충방전기
                TB_CHARGE_STOCK = new MesData.TB_CHARGE_STOCK();
                TB_CHARGE_STOCK.Init();

                //// AutoUpdate Check
                //AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
                //System.Timers.Timer timer = new System.Timers.Timer
                //{
                //    Interval = 10 * 1000,
                //    SynchronizingObject = this
                //};
                //timer.Elapsed += delegate
                //{
                //    AutoUpdater.Start("ftp://192.168.11.131:8010/test/AutoUpdaterTest.xml", new NetworkCredential("ftpuser", "ecs723181"));
                //};

                //timer.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }
        #endregion

        //******************************************************************
        // for handling filter data
        //******************************************************************
        private FilterData DeSerializeFilterData()
        {
            try
            {
                System.IO.Stream fs = new FileStream(CCommonConst.FILTERDATA_FILENAME, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);
                if (fs == null) return null;

                BinaryFormatter b = new BinaryFormatter();
                FilterData objFilterData = (FilterData)b.Deserialize(fs);
                fs.Close();
                return objFilterData;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
                return null;
            }
        } // end of DeSerializeFilterData()

        private string GetIdentity()
        {
            try
            {
                if (frmMain.m_strCurSEComID == "") return CCommonConst.Identity_Host;

                return this.m_SEComConf.ReadValue(frmMain.m_strCurSEComID, SXProFile.ProFileKey.KEY_COMMON_IDENTITY, CCommonConst.Identity_Host);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ GetIdentity ] - " + ex.GetBaseException().ToString());
                return null;
            }
        }

        //******************************************************************
        // tvwSECSMessage Initailize
        //******************************************************************

        private void LoadDefaultLibrary(string theLibFile)
        {
            this.LoadLibFile(m_strCurLibFile);
        }

        private void LoadLibFromSECS2Log(bool IsAppend)
        {
            try
            {
                XmlDocument xmlDoc = SEComEnabler.SEComUtility.SEComHandler.SECS2toSMD(frmMain.m_ConvertLibLog2, false);
                if (xmlDoc == null)
                {
                    MessageBox.Show("Error occured in Conversion", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!IsAppend)
                    this.LoadLibFile(xmlDoc, false);
                else
                    this.LoadLibFile(xmlDoc, true);

                this.ReLoadTree();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }

        private void LoadLibFile(XmlDocument doc, bool IsAppend)
        {
            try
            {
                //statusBarPanel1.Text = "Loading... ";

                CSECSLibMgr theLib = new CSECSLibMgr();
                this.tvwSECSMessage.Cursor = Cursors.WaitCursor;
                bool result = theLib.Construct_SMDLib(doc);

                if (result)
                {
                    if (!IsAppend)
                    {
                        this.DisplayLibrarys(theLib);
                    }
                    else
                    {
                        this.InsertLibraryNodes(theLib, this.tvwSECSMessage.TopNode);
                    }
                    
                    this.tvwSECSMessage.Cursor = Cursors.Arrow;
                }
                else
                {
                    this.tvwSECSMessage.Cursor = Cursors.Arrow;
                    MessageBox.Show("Error in Loading the File!!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //statusBarPanel1.Text = "File Name : " + m_strCurLibFile.Substring(m_strCurLibFile.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                theLib.Terminate();
                theLib = null;
                doc = null;
            }
            catch { }
        }

        private void LoadLibFile(string fileName)
        {
            try
            {
                //statusBarPanel1.Text = "Loading ";
                //statusBarPanel1.Text += fileName.Substring(fileName.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                //statusBarPanel1.Text += "....";

                FileInfo file = new FileInfo(fileName);
                CSECSLibMgr theLib = new CSECSLibMgr();
                SECSLibrary.LIBRARY_FORMAT Extension;

                if (file.Extension.ToUpper().Equals(CCommonConst.EXT_LOGFILE) || file.Extension.ToUpper().Equals(CCommonConst.EXT_FABLOGFILE))
                    Extension = SECSLibrary.LIBRARY_FORMAT.SECS2;
                else
                    Extension = SECSLibrary.String2LibFormat(file.Extension.Substring(1));

                this.tvwSECSMessage.Cursor = Cursors.WaitCursor;
                bool result = theLib.ConstructLibMessage(fileName, Extension);

                if (result)
                {
                    this.DisplayLibrarys(theLib);
                    m_strCurLibFile = fileName;
                    
                    this.tvwSECSMessage.Cursor = Cursors.Arrow;
                }
                else
                {
                    this.tvwSECSMessage.Cursor = Cursors.Arrow;
                    MessageBox.Show("Error in Loading the File!!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //statusBarPanel1.Text = "File Name : " + m_strCurLibFile.Substring(m_strCurLibFile.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                theLib.Terminate();
                theLib = null;
                file = null;

                this.ReLoadTree();
            }
            catch { }
        }

        private void DisplayLibrarys(CSECSLibMgr theLib)
        {
            try
            {
                this.tvwSECSMessage.Nodes.Clear();

                TreeNode RootNode = MakeRootNode(m_strDefaultLib);
                if (RootNode == null) return;

                this.InsertLibraryNodes(theLib, RootNode);

                this.tvwSECSMessage.Nodes.Add(RootNode);
                RootNode.Expand();

                RootNode = null;
            }
            catch { }
        }

        //******************************************************************************
        // Insert Library Nodes (PairNode, PrimaryMsgNode, SecondaryMsgNode)
        //******************************************************************************
        private void InsertLibraryNodes(CSECSLibMgr theLib, TreeNode rootNode)
        {
            try
            {
                while (theLib.LibMessages.Count != 0)
                {
                    bool flag = false;
                    CSECSMessage theMsg = (CSECSMessage)theLib.LibMessages[0];

                    // make a mate msg
                    CSECSMessage mateMsg = SearchMateMsg(theMsg, theLib);
                    if (mateMsg == null)
                    {
                        mateMsg = MakeHeaderOnlyMsg(theMsg, true);
                        if (mateMsg == null)
                        {
                            theLib.LibMessages.Remove(theMsg);
                            continue;
                        }
                    }
                    else
                    {
                        flag = true;
                    }

                    // insert PairNode, Primary MsgNode, Secondary MsgNode 
                    TreeNode PairNode = MakePairNode(theMsg);
                    if (PairNode != null)
                    {
                        bool result;
                        if (theMsg.Function % 2 == 1)
                            result = this.InsertMsgsNode(theMsg, mateMsg, PairNode);
                        else
                            result = InsertMsgsNode(mateMsg, theMsg, PairNode);
                        if (result) rootNode.Nodes.Add(PairNode);
                    }

                    // Remove Used Msgs
                    theLib.LibMessages.Remove(theMsg);
                    if (flag) theLib.LibMessages.Remove(mateMsg);
                }
            }
            catch { }
        }

        private bool InsertMsgsNode(CSECSMessage primaryMsg, CSECSMessage secondaryMsg, TreeNode parNode)
        {
            try
            {
                bool result;
                // Insert Primary Msg Node
                result = InsertOneMsgNode(primaryMsg, parNode);
                if (!result) return result;

                // Insert Secondary Msg Node
                result = InsertOneMsgNode(secondaryMsg, parNode);
                return result;
            }
            catch
            {
                return false;
            }
        }

        private bool InsertOneMsgNode(CSECSMessage theMsg, TreeNode parNode)
        {
            try
            {
                TreeNode MsgNode = MakeHeaderNode(theMsg);
                if (MsgNode == null) return false;

                m_LibIndex = 0;
                bool result = InsertItemNode(-1, theMsg.SECSItems, MsgNode);

                if (result) parNode.Nodes.Add(MsgNode);
                MsgNode = null;

                return result;
            }
            catch
            {
                return false;
            }
        }

        private int m_LibIndex;
        private bool InsertItemNode(int preLevel, ArrayList theSECSItems, TreeNode parNode)
        {
            try
            {
                if (theSECSItems == null) return true;

                while (m_LibIndex < theSECSItems.Count)
                {
                    CSECSDataItem theItem = (CSECSDataItem)theSECSItems[m_LibIndex];

                    if (theItem.ItemLevel < preLevel) return true;

                    // 다음 아이템을 위해 값을 변경..
                    m_LibIndex++;
                    preLevel = theItem.ItemLevel;

                    TreeNode itemNode = MakeItemNode(theItem);
                    if (itemNode == null) return false;
                    parNode.Nodes.Add(itemNode);

                    if (theItem.ItemType == SX.SECSFormat.L && theItem.ItemCount > 0) /***/
                    {
                        if (InsertItemNode(preLevel, theSECSItems, itemNode) == false) return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        //******************************************************************************
        // Make TreeNode (RootNode, PairNode, HeaderNode, ItemNode)
        //******************************************************************************
        private TreeNode MakeRootNode(string theValue)
        {
            try
            {
                return new TreeNode(theValue, 0, 0);
            }
            catch
            {
                return null;
            }
        }

        private TreeNode MakePairNode(CSECSMessage theMsg)
        {
            try
            {
                return new TreeNode(theMsg.PairMsg.GetPairMsgToString());
            }
            catch
            {
                return null;
            }
        }

        private TreeNode MakeHeaderNode(CSECSMessage theMsg)
        {
            try
            {
                if (theMsg.Function % 2 == 1)
                    return new TreeNode(theMsg.GetSECSMsgToString(), 3, 3);
                else
                    return new TreeNode(theMsg.GetSECSMsgToString(), 4, 4);
            }
            catch
            {
                return null;
            }
        }

        private TreeNode MakeItemNode(CSECSDataItem theItem)
        {   
            try
            {
                return new TreeNode(theItem.GetItemToString());
            }
            catch
            {
                return null;
            }
        }

        private void ReLoadTree()
        {
            try
            {
                this.tvwSECSMessage.Cursor = Cursors.WaitCursor;
                //this.SetFormTitle();
                m_strIdentity = this.GetIdentity();

                for (int i = 0; i < this.tvwSECSMessage.Nodes[CCommonConst.SECSLibrary_Level].Nodes.Count; i++)
                {
                    TreeNode tnPairNode = this.tvwSECSMessage.Nodes[CCommonConst.SECSLibrary_Level].Nodes[i];
                    if (m_strIdentity.ToUpper().Equals(CCommonConst.Identity_Host))
                    {
                        if (tnPairNode.FirstNode.Text.IndexOf(SEComEnabler.SEComStructure.ConstXML.DIRECTION_FromHost) != -1)
                            tnPairNode.ImageIndex = tnPairNode.SelectedImageIndex = 1;
                        else if (tnPairNode.FirstNode.Text.IndexOf(SEComEnabler.SEComStructure.ConstXML.DIRECTION_FromEquipment) != -1)
                            tnPairNode.ImageIndex = tnPairNode.SelectedImageIndex = 2;
                    }
                    else if (m_strIdentity.ToUpper().Equals(CCommonConst.Identity_Eqp))
                    {
                        if (tnPairNode.FirstNode.Text.IndexOf(SEComEnabler.SEComStructure.ConstXML.DIRECTION_FromHost) != -1)
                            tnPairNode.ImageIndex = tnPairNode.SelectedImageIndex = 2;
                        else if (tnPairNode.FirstNode.Text.IndexOf(SEComEnabler.SEComStructure.ConstXML.DIRECTION_FromEquipment) != -1)
                            tnPairNode.ImageIndex = tnPairNode.SelectedImageIndex = 1;
                    }

                }//for
                this.tvwSECSMessage.Refresh();
                this.tvwSECSMessage.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.GetBaseException().ToString());
            }
        }

        //******************************************************************************
        // Use to construct a Pair Msg
        //******************************************************************************
        private CSECSMessage MakeHeaderOnlyMsg(CSECSMessage theMsg, bool isMakeMate)
        {
            try
            {
                int iFunction;
                string msgName;
                SX.SECSDirection theDirection;

                if (!isMakeMate)
                {
                    msgName = theMsg.MessageName;
                    iFunction = theMsg.Function;
                    theDirection = theMsg.Direction;
                }
                else
                {
                    // Function
                    iFunction = MakeMateFunction(theMsg.Function);

                    // Message Name
                    msgName = "S" + theMsg.Stream.ToString() + "F" + iFunction.ToString();

                    // Direction
                    theDirection = SX.OppositeDirection(theMsg.Direction);
                    if (theDirection == SX.SECSDirection.Both) return null;
                }

                // make header only msg
                CSECSMessage headerMsg = new CSECSMessage();

                headerMsg.MessageName = msgName;
                headerMsg.Stream = theMsg.Stream;
                headerMsg.Function = iFunction;
                headerMsg.Direction = theDirection;
                headerMsg.Wait = false;
                headerMsg.AutoReply = theMsg.AutoReply;
                headerMsg.PairMsg = theMsg.PairMsg;

                return headerMsg;
            }
            catch
            {
                return null;
            }
        }

        private int MakeMateFunction(int theFunctionNum)
        {
            int nFunction = (theFunctionNum % 2) == 1 ? theFunctionNum + 1 : theFunctionNum - 1;
            if (nFunction < 0) nFunction = 0;

            return nFunction;
        }

        private CSECSMessage SearchMateMsg(CSECSMessage theMsg, CSECSLibMgr theLib)
        {
            int theFunction;
            SX.SECSDirection theDirection;

            try
            {
                // determine function
                theFunction = MakeMateFunction(theMsg.Function);

                // determine Direction
                theDirection = SX.OppositeDirection(theMsg.Direction);
                if (theDirection == SX.SECSDirection.Both) return null;

                return theLib.GetMessageWithSFD(theMsg.Stream, theFunction, theDirection);
            }
            catch
            {
                return null;
            }
        }

        private string CopyResourceToFile(string ResName)
        {
            try
            {
                string FileName = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "Config" + System.IO.Path.DirectorySeparatorChar + ResName;
                return FileName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
                return "";
            }
        }

        //******************************************************************************
        // Menu : Settings
        //******************************************************************************
        #region Setting - Menu Click Event
        private void configurationConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmConfigure objConfigure = new frmConfigure(ref this.m_SEComConf, m_CommnInfo.IsSECSConnected);
                objConfigure.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                objConfigure.ShowDialog(this);
            }
            catch { }
        }

        private void openConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmMain.m_strCurSEComID == "")
                {
                    MessageBox.Show("Configuration is not set!!!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SXProFile aProfile = m_SEComConf.ReadSectionData(frmMain.m_strCurSEComID);
                if (aProfile == null)
                {
                    MessageBox.Show("You must set the configuration!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                m_CommnInfo.Initialize(aProfile);
                this.SettingSEComDriver();

                SEComError.SEComPlugIn aErr = SEComError.SEComPlugIn.ERR_NONE;
                bool IsUseDispatcher = false;
                aProfile.Add(SXProFile.ProFileKey.KEY_COMMON_USEDISPATCHER, false.ToString());
                
                // deserialize the fiterdata	/***/
                this.m_FilterData = this.DeSerializeFilterData();
                aErr = m_SEComPlugIn.Initialize(frmMain.m_strCurSEComID, true, IsUseDispatcher, false, true, m_CommnInfo.Options.LogModeOption, new ArrayList(), aProfile, this.m_FilterData);
                //aErr = this.m_SEComPlugIn.Initialize(frmMain.m_strCurSEComID, true, IsUseDispatcher, false, true, this.m_CommnInfo.Options.LogModeOption, Application.StartupPath + "\\SEComINI.XML");

                if (aErr != SEComError.SEComPlugIn.ERR_NONE)
                {
                    MessageBox.Show("Failed to Initialize SEComEnabler Driver!!!\n\n" + aErr.ToString(),
                            "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Failed to Initialize SEComEnabler Driver!!!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return;
            }

            this.closeConnectionToolStripMenuItem.Enabled = true;
            this.openConnectionToolStripMenuItem.Enabled = false;
            this.Cursor = Cursors.Default;
            this.m_blIsAcceptDisConnect = true;
            this.SetConnection_StatusBar(Connection_Status.SECS_OpenConnection);
        }

        private void closeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SEComPlugIn == null) return;

                try
                {
                    m_SEComPlugIn.Terminate(frmMain.m_strCurSEComID);
                    m_SEComPlugIn = null;
                    System.GC.Collect(System.GC.GetGeneration(this));
                }
                catch
                {
                    Trace.WriteLine("Don`t Terminate SEComEnabler Driver");
                    return;
                }

                m_CommnInfo.IsSECSConnected = false;

                this.closeConnectionToolStripMenuItem.Enabled = false;
                this.openConnectionToolStripMenuItem.Enabled = true;
                this.m_blIsAcceptDisConnect = false;
                this.SetConnection_StatusBar(Connection_Status.SECS_CloseConnection);
            }
            catch { }
        }
        #endregion

        //******************************************************************************
        // Menu : Command
        //******************************************************************************
        #region Command - Menu Click Event
        private void sendMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode theNode = this.tvwSECSMessage.SelectedNode;
                if (theNode == null) return;

                int iNodeDepth = SEComEnabler.SEComUtility.CUtilities.getCharCount(theNode.FullPath, m_chPathSeparator);
                if (iNodeDepth != 1) return; // only in case of PairNode

                // check Direction
                int theIndex = theNode.FirstNode.Text.Trim().LastIndexOf(" ");
                string tmpValue = theNode.FirstNode.Text.Substring(theIndex + 1).Trim();

                if (tmpValue != SX.SECSDirection2String(m_CommnInfo.GetDirection()))
                {
                    MessageBox.Show("Direction did not match!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Primary Msg
                this.SendMessage(theNode.FirstNode, true);

                theNode = null;
                frmMain.m_blIsFocus = true;
            }
            catch { }
        }

        private void sendReplyMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode theNode = this.tvwSECSMessage.SelectedNode;
                if (theNode == null) return;

                int iNodeDepth = SEComEnabler.SEComUtility.CUtilities.getCharCount(theNode.FullPath, m_chPathSeparator);
                if (iNodeDepth != 1) return; // only in case of PairNode

                // check Direction
                int theIndex = theNode.FirstNode.Text.Trim().LastIndexOf(" ");
                string tmpValue = theNode.FirstNode.Text.Substring(theIndex + 1).Trim();

                if (tmpValue != SX.SECSDirection2String(SX.OppositeDirection(m_CommnInfo.GetDirection())))
                {
                    MessageBox.Show("Direction is not matched!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Secondary Msg
                this.SendMessage(theNode.LastNode, false);
                theNode = null;
            }
            catch { }
        }
        #endregion

        private bool m_blIsAcceptDisConnect = false;
        private void SetConnection_StatusBar(Connection_Status aConnectionStatus)
        {
            try
            {
                if (aConnectionStatus == Connection_Status.SECS_CloseConnection)
                {
                    this.toolStripStatusLabel2.Text = "SECS Connection Closed";
                }
                else
                {
                    SXProFile sxProfile = m_CommnInfo.ProFile;
                    string strIdentity = sxProfile.Read(SXProFile.ProFileKey.KEY_COMMON_IDENTITY);
                    string strSECSMODE = sxProfile.Read(SXProFile.ProFileKey.KEY_COMMON_SECSMODE);
                    string strSECSInfo, strHSMSMode = "";

                    if (strSECSMODE.ToUpper() == SX.SECSInfo.HSMS.ToString().ToUpper())
                    {
                        strHSMSMode = sxProfile.Read(SXProFile.ProFileKey.KEY_HSMS_HSMSMODE);
                        if (strHSMSMode.ToUpper() == SX.SECSInfo.ACTIVE.ToString().ToUpper())
                        {
                            strSECSInfo = sxProfile.Read(SXProFile.ProFileKey.KEY_HSMS_REMOTEIP) + " : " + sxProfile.Read(SXProFile.ProFileKey.KEY_HSMS_REMOTEPORT);
                        }
                        else
                        {
                            strSECSInfo = sxProfile.Read(SXProFile.ProFileKey.KEY_HSMS_LOCALPORT);
                        }
                    }
                    else
                    {
                        strSECSInfo = sxProfile.Read(SXProFile.ProFileKey.KEY_SECS1_MASTER)
                            + " " + sxProfile.Read(SXProFile.ProFileKey.KEY_SECS1_COMPORT)
                            + " " + sxProfile.Read(SXProFile.ProFileKey.KEY_SECS1_BAUDRATE);
                    }

                    string strConnectInfo = "";
                    switch (aConnectionStatus)
                    {
                        case Connection_Status.SECS_OpenConnection:
                            strConnectInfo = "Try SECS Connection";
                            break;
                        case Connection_Status.SECS_Connected:
                            strConnectInfo = "SECS Connected";
                            break;
                        case Connection_Status.SECS_DisConnected:
                            strConnectInfo = "SECS DisConnected";
                            break;
                        default:
                            return;
                    }

                    this.toolStripStatusLabel2.Text = strConnectInfo;
                    this.toolStripStatusLabel2.Text += "   " + strIdentity;
                    this.toolStripStatusLabel2.Text += "   " + strSECSMODE;
                    this.toolStripStatusLabel2.Text += "   " + strHSMSMode;
                    this.toolStripStatusLabel2.Text += "   " + strSECSInfo;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ SetConnection_StatusBar ] - " + ex.GetBaseException().ToString());
            }
        }

        //******************************************************************************
        // SEComDriver Setting & Event
        //******************************************************************************
        #region SEcomDriver Setting & Event Handler
        private void SettingSEComDriver()
        {
            try
            {
                if (m_SEComPlugIn != null)
                {
                    m_SEComPlugIn.Terminate(frmMain.m_strCurSEComID);
                    m_SEComPlugIn = null;
                }

                m_SEComPlugIn = new CSEComPlugIn();
                Hashtable htDrv = m_SEComPlugIn.SEComDriverLists();
                //this.m_SEComPlugIn.OnSECS1Log += new SEComEnabler.SEComPlugIn.CSEComPlugIn.DLGSECS1Log(OnSECS1Log);
                m_SEComPlugIn.OnSECS2Log += new SEComEnabler.SEComPlugIn.CSEComPlugIn.DLGSECS2Log(OnSECS2Log);
                m_SEComPlugIn.OnSECSConnected += new SEComEnabler.SEComPlugIn.CSEComPlugIn.DLGSECSConnected(OnSECSConnected);
                m_SEComPlugIn.OnSECSDisConnected += new SEComEnabler.SEComPlugIn.CSEComPlugIn.DLGSECSDisConnected(OnSECSDisConnected);
                m_SEComPlugIn.OnSECSReceived += new SEComEnabler.SEComPlugIn.CSEComPlugIn.DLGSECSReceived(OnSECSReceived);
                m_SEComPlugIn.OnSECSUnknownMessage += new SEComEnabler.SEComPlugIn.CSEComPlugIn.DLGSECSUnknownMessage(OnSECSUnknownMsgReceived);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }

        private void OnSECS2Log(string aEqpID, string aDirection, string aLog)
        {
            int nThreadID = System.Threading.Thread.CurrentThread.GetHashCode();
            if (frmMain.m_nMainThreadID != nThreadID)
            {
                if (!m_CommnInfo.Options.LogModeOption)
                    this.Invoke(new DLGSECSLog(OnUISECS2Log), new object[] { aEqpID, aDirection, aLog });
                else
                    this.Invoke(new DLGSECSLog(OnUIFABSECS2Log), new object[] { aEqpID, aDirection, aLog });
            }
            else
            {
                if (!m_CommnInfo.Options.LogModeOption)
                    OnUISECS2Log(aEqpID, aDirection, aLog);
                else
                    OnUIFABSECS2Log(aEqpID, aDirection, aLog);
            }
        }

        public delegate void DLGSECSLog(string aEqpID, string aDirection, string aLog);

        private void OnUISECS2Log(string aEqpID, string aDir, string aLog)
        {
            //			System.Diagnostics.Debug.WriteLine("SECS2Thread ID : " + System.Threading.Thread.CurrentThread.GetHashCode().ToString());
            if (aLog.Length == 0 || this.rtxtSECS2Log.IsDisposed) return;

            try
            {
                if (rtxtSECS2Log.Lines.GetLength(0) > m_CommnInfo.Options.SECS2LogLines)
                    this.rtxtSECS2Log.Clear();

                this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Other;

                int nDirIndex = aLog.IndexOf("]");
                if (nDirIndex != -1)
                {
                    string aDirection = aLog.Substring(nDirIndex + 1, 5).Trim();

                    // check for SECS2 Filter Messages for SEND, SENT, RECD
                    if (m_CommnInfo.Options.SX2LogFilter &&
                        m_CommnInfo.Options.SX2FilterMessages.Count != 0)
                    {
                        if (m_CommnInfo.Options.FilterRecvMsgsOnly &&
                            aDirection != CCommonConst.LogMsg_Recv) return;

                        if ((aDirection == CCommonConst.LogMsg_Send ||
                            aDirection == CCommonConst.LogMsg_Recv))
                        {
                            bool flag = false;
                            foreach (Object objMsg in m_CommnInfo.Options.SX2FilterMessages)
                            {
                                string strMsg = objMsg.ToString().Replace(" ", "");
                                int nIndex = strMsg.IndexOf(":");
                                if (nIndex == -1) continue;
                                strMsg = strMsg.Substring(nIndex + 1);

                                if (aLog.IndexOf(strMsg + " ") != -1)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag == false) return;
                        }
                    } // if(

                    if (aDirection == CCommonConst.LogMsg_Send)
                        this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Send;
                    else if (aDirection == CCommonConst.LogMsg_Recv)
                    {
                        if (aLog.IndexOf("SECS MESSAGE") == -1)
                            this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Recv;
                        else
                            this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Warn;
                    }
                    else if (aDirection == CCommonConst.LogMsg_HSMS
                        || aDirection == CCommonConst.LogMsg_SECS1)
                        this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Conn;
                    else if (aDirection == CCommonConst.LogMsg_Warn)
                        this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Warn;
                    else if (aDirection == CCommonConst.LogMsg_Info)
                        this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Info;

                    if (aDirection == CCommonConst.LogMsg_Send || aDirection == CCommonConst.LogMsg_Recv)
                    {
                        if (m_CommnInfo.Options.ShowSECS2DataItems == false)
                        {
                            int nIndex = aLog.LastIndexOf(":");
                            if (nIndex != -1)
                                aLog = aLog.Substring(0, nIndex + 1).Trim() + "\t\n";
                        }
                    }
                } // if(nDirIndex != -1)

                this.rtxtSECS2Log.AppendText(aLog);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[OnSECS2Log] - " + ex.GetBaseException().ToString());
            }
        }

        private void OnUIFABSECS2Log(string aEqpID, string aDir, string aLog)
        {
            if (aLog.Length == 0 || this.rtxtSECS2Log.IsDisposed) return;

            try
            {
                if (rtxtSECS2Log.Lines.GetLength(0) > m_CommnInfo.Options.SECS2LogLines)
                    this.rtxtSECS2Log.Clear();

                int nStart = SEComEnabler.SEComUtility.CUtilities.nthIndex(aLog, " ", 2);
                int nEnd = SEComEnabler.SEComUtility.CUtilities.nthIndex(aLog, " ", 3);
                if (nStart != -1 && nEnd != -1)
                {
                    string aDirection = aLog.Substring(nStart + 1, nEnd - nStart - 1);

                    // check for SECS2 Filter Messages for SEND, SENT, RECD
                    if (aDirection == CCommonConst.LogMsg_Send ||
                        aDirection == CCommonConst.FABLogMsg_Sent ||
                        aDirection == CCommonConst.FABLogMsg_Recd)
                    {
                        if (m_CommnInfo.Options.SX2LogFilter &&
                            m_CommnInfo.Options.SX2FilterMessages.Count != 0)
                        {
                            if (m_CommnInfo.Options.FilterRecvMsgsOnly &&
                                aDirection != CCommonConst.FABLogMsg_Recd) return;

                            if ((aDirection == CCommonConst.LogMsg_Send ||
                                aDirection == CCommonConst.FABLogMsg_Recd))
                            {
                                bool flag = false;
                                foreach (Object objMsg in m_CommnInfo.Options.SX2FilterMessages)
                                {
                                    string strMsg = objMsg.ToString().Replace(" ", "");
                                    int nIndex = strMsg.IndexOf(":");
                                    if (nIndex == -1) continue;
                                    strMsg = strMsg.Substring(nIndex + 1);

                                    if (aLog.IndexOf(strMsg + " ") != -1)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (flag == false) return;
                            }
                        }// if
                    } // if(aDirection == ...

                    // Set log colors
                    if (aDirection == CCommonConst.LogMsg_Send ||
                        aDirection == CCommonConst.FABLogMsg_Sent)
                        this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Send;
                    else if (aDirection == CCommonConst.FABLogMsg_Recd)
                    {
                        if (aLog.IndexOf("SECS MESSAGE") == -1)
                            this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Recv;
                        else
                            this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Warn;
                    }
                    else if (aDirection == CCommonConst.LogMsg_HSMS
                        || aDirection == CCommonConst.LogMsg_SECS1
                        || aDirection == CCommonConst.FABLogMsg_Connect
                        || aDirection == CCommonConst.FABLogMsg_Disconnect)
                        this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Conn;
                    else if (aDirection == CCommonConst.LogMsg_Warn)
                        this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Warn;
                    else if (aDirection == CCommonConst.LogMsg_Info)
                        this.rtxtSECS2Log.SelectionColor = m_LogColors.SECS2Info;

                    if (aDirection == CCommonConst.LogMsg_Send || aDirection == CCommonConst.LogMsg_Recv)
                    {
                        if (m_CommnInfo.Options.ShowSECS2DataItems == false)
                        {
                            int nIndex = aLog.LastIndexOf(":");
                            if (nIndex != -1)
                                aLog = aLog.Substring(0, nIndex + 1).Trim() + "\t\n";
                        }
                    }
                } // end of if(nStart != -1 && nEnd != -1)

                this.rtxtSECS2Log.AppendText(aLog);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[OnFABSECS2Log] - " + ex.GetBaseException().ToString());
            }
        }

        public delegate void DLGSECSConnected(string aEqpID, XmlDocument aXML);

        private void OnSECSConnected(string aEqpID, XmlDocument aXML)
        {
            int nThreadID = System.Threading.Thread.CurrentThread.GetHashCode();
            if (frmMain.m_nMainThreadID != nThreadID)
                this.Invoke(new DLGSECSConnected(OnUISECSConnected), new object[] { aEqpID, aXML });
            else
                OnUISECSConnected(aEqpID, aXML);
        }

        private void OnUISECSConnected(string aEqpID, XmlDocument aXML)
        {
            try
            {
                // SystemBytes 초기화.  
                SetConnection_StatusBar(Connection_Status.SECS_Connected);

                m_CommnInfo.ResetSystemBytes();

                m_CommnInfo.IsSECSConnected = true;
                
                //정상적으로 연결됐을때 S1F13메시지를 보냄
                // 연결되면 S1F13 보내기
                Thread.Sleep(1000);
                SXTransaction sxTrx = VirtualMES.BaseBiz.MakeS1F13();

                SEComError.SEComPlugIn err_Rtn = frmMain.m_SEComPlugIn.Request(frmMain.m_strCurSEComID, sxTrx);
                if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                {
                    frmMain.m_MsgStats.TransmissionError++;
                    //this.m_objStats.DisplayStats();
                    return;
                }
            }
            catch { }
        }

        public delegate void DLGSECSDisConnected(string aEqpID, XmlDocument aXML);

        private void OnSECSDisConnected(string aEqpID, XmlDocument aXML)
        {
            int nThreadID = System.Threading.Thread.CurrentThread.GetHashCode();
            if (frmMain.m_nMainThreadID != nThreadID)
                this.Invoke(new DLGSECSDisConnected(OnUISECSDisConnected), new object[] { aEqpID, aXML });
            else
                OnUISECSDisConnected(aEqpID, aXML);
        }

        private void OnUISECSDisConnected(string aEqpID, XmlDocument aXML)
        {
            if (this.m_blIsAcceptDisConnect == false)
                return;

            try
            {
                SetConnection_StatusBar(Connection_Status.SECS_DisConnected);

                m_CommnInfo.IsSECSConnected = false;
                
                //mnuItemSendPrimaryMessage.Enabled = false;
                //mnuItemSendReplyMessage.Enabled = false;
                //mnuItemSendControlMessage.Enabled = false;

            }
            catch { }
        }

        public delegate void DLGSECSReceived(string aEqpID, string aSECSMsgName, XmlDocument aXML);

        public void OnSECSReceived(string aEqpID, string aSECSMsgName, XmlDocument aXML)
        {
            int nThreadID = System.Threading.Thread.CurrentThread.GetHashCode();
            if (frmMain.m_nMainThreadID != nThreadID)
                this.Invoke(new DLGSECSReceived(OnUISECSReceived), new object[] { aEqpID, aSECSMsgName, aXML });
            else
                OnUISECSReceived(aEqpID, aSECSMsgName, aXML);
        }

        private void OnUISECSReceived(string aEqpID, string aSECSMsgName, XmlDocument aXML)
        {
            try
            {
                SEComData sd = new SEComData();
                sd.XmlToSECSData(aXML);

                // Reply Message 이면...
                if (SEComEnabler.SEComUtility.XMLUtility.isPrimary(aXML) == false)
                {
                    frmMain.m_MsgStats.SecondaryMsgReceived++;

                    //ReplyReceived Event
                    //ReplayReceived(sd);

                    return;
                }

                frmMain.m_MsgStats.PrimaryMsgReceived++;

                m_CommnInfo.ReceivedSystemByte = Convert.ToInt64(SEComEnabler.SEComUtility.XMLUtility.getValueOfCondition(aXML, CommonConst.SMD_CONDITIONS.SystemBytes));

                // if not wait, not send autoreply
                if (Convert.ToBoolean(SEComEnabler.SEComUtility.XMLUtility.getValueOfCondition(aXML, CommonConst.SMD_CONDITIONS.Wait)) == false)
                    return;
                
                int iStream = Convert.ToInt32(SEComEnabler.SEComUtility.XMLUtility.getValueOfCondition(aXML, CommonConst.SMD_CONDITIONS.Stream));
                int iFunction = Convert.ToInt32(SEComEnabler.SEComUtility.XMLUtility.getValueOfCondition(aXML, CommonConst.SMD_CONDITIONS.Function));
                SX.SECSDirection sDirection = SX.OppositeDirection(m_CommnInfo.GetDirection());
                
                SendAutoReply(iStream, iFunction, sDirection);

                //S6F11 메시지가 데이터폼이 다르게 여러번 들어오는데
                //이를 구분하기 위한 로직은 어떻게 작성해야할지 모르겠음
          
                //시나리오 3
                if (iStream == 6 && iFunction == 11)
                {
                    // Child Form Event
                    SXTransaction sxTrx = BaseBiz.MakeReplyS6F12(sd);

                    SEComError.SEComPlugIn err_Rtn = m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, sxTrx, m_CommnInfo.ReceivedSystemByte);
                    if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                    {
                        frmMain.m_MsgStats.TransmissionError++;
                        return;
                    }
                }

                //테스트 목적으로 만든 부분
                else if (iStream == 99 && iFunction == 99)
                {
                    SXTransaction sxTrx = BaseBiz.MakeReplyS99F100(sd);

                    Test_Form obj = new Test_Form();
                    obj.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    obj.Show(this);
                    
                    SEComError.SEComPlugIn err_Rtn = m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, sxTrx, m_CommnInfo.ReceivedSystemByte);
                    if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                    {
                        frmMain.m_MsgStats.TransmissionError++;
                        return;
                    }

                    Thread.Sleep(1000);
                    sxTrx = BaseBiz.MakeReplyS2F41(sd);
                    err_Rtn = m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, sxTrx, m_CommnInfo.ReceivedSystemByte);
                    if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                    {
                        frmMain.m_MsgStats.TransmissionError++;
                        return;
                    }
                }

                else if (iStream == 1 && iFunction == 13)
                {
                    SXTransaction sxTrx = BaseBiz.MakeReplyS1F14(sd);

                    SEComError.SEComPlugIn err_Rtn = m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, sxTrx, m_CommnInfo.ReceivedSystemByte);
                    if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                    {
                        frmMain.m_MsgStats.TransmissionError++;
                        return;
                    }
                }

                //시나리오 1
                else if (iStream == 2 && iFunction == 41)
                {
                    SXTransaction sxTrx = BaseBiz.MakeReplyS2F42(sd);

                    SEComError.SEComPlugIn err_Rtn = m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, sxTrx, m_CommnInfo.ReceivedSystemByte);
                    if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                    {
                        frmMain.m_MsgStats.TransmissionError++;
                        return;
                    }
                }

                //시나리오 2
                else if (iStream == 2 && iFunction == 49)
                {
                    SXTransaction sxTrx = BaseBiz.MakeReplyS2F50(sd);

                    SEComError.SEComPlugIn err_Rtn = m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, sxTrx, m_CommnInfo.ReceivedSystemByte);
                    if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                    {
                        frmMain.m_MsgStats.TransmissionError++;
                        return;
                    }
                }

                else if (iStream == 5 && iFunction == 1)
                {
                    SXTransaction sxTrx = new SXTransaction();
                    sxTrx.Stream = 5;
                    sxTrx.Function = 2;

                    sxTrx.WriteNode(SX.SECSFormat.B, 1, "0", "ACKC5");

                    SEComError.SEComPlugIn err_Rtn = frmMain.m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, sxTrx, frmMain.m_CommnInfo.ReceivedSystemByte);
                    if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                    {
                        frmMain.m_MsgStats.TransmissionError++;
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }

        private void SendAutoReply(int aStream, int aFunction, SX.SECSDirection aDirection)
        {
            try
            {
                int i = 0;
                TreeNode rootNode = this.tvwSECSMessage.Nodes[0];

                //자동 응답 메시지를 보내는 부분
                foreach (TreeNode curNode in rootNode.Nodes)
                {
                    SEComEnabler.SECSMsgLibrary.CSECSMessage curMsg = new SEComEnabler.SECSMsgLibrary.CSECSMessage();

                    if (curMsg.ParseHeader(curNode.FirstNode.Text) == false) continue;

                    if (curMsg.Stream == aStream && curMsg.Function == aFunction && curMsg.Direction == aDirection)
                    {
                        // If Use SECS Dispatcher is true and AutoReply is true then, don't send a Reply msg
                        // because, SECS Dispatcher automatically sends a Reply msg.
                        if (curMsg.AutoReply)
                        {
                            if (m_CommnInfo.Options.UseSECSDispatcher) return; /***/

                            this.SendMessage(curNode.LastNode, false);
                            frmMain.m_MsgStats.AutoReplySent++;
                        }
                        return;
                    }
                    if (i % 10 == 0)
                        System.Threading.Thread.Sleep(1);
                    i++;
                }
                // not find definitionmsg

                if (m_CommnInfo.Options.SendAbortMsg)
                {
                    XmlDocument abortmsg = makeAbortedMsg(aStream, aDirection);
                    if (abortmsg != null)
                    {
                        this.SendMessage(ref abortmsg, false);
                        frmMain.m_MsgStats.AbortMsgSent++;
                    }
                }
                return;
            }
            catch
            { }
        }

        private XmlDocument makeAbortedMsg(int aStream, SX.SECSDirection aDirection)
        {
            try
            {
                string strHeaderText = "S" + aStream.ToString() + "F0: S" +
                    aStream.ToString() + " F0 " + SX.SECSDirection2String(SX.OppositeDirection(aDirection));

                // make a DriverMsg with a object of CSECSMessage
                SEComEnabler.SECSMsgLibrary.CSEComDriverMsg sendMsg = new SEComEnabler.SECSMsgLibrary.CSEComDriverMsg();

                //				XmlDocument doc = sendMsg.constructDriverMsg(this.m_CommnInfo.GetSEComID(), this.m_CommnInfo.GetDirection(), 0, strHeaderText);
                XmlDocument doc = sendMsg.constructDriverMsg(frmMain.m_strCurSEComID, m_CommnInfo.GetDirection(), 0, strHeaderText);
                if (doc == null)
                {
                    //					MessageBox.Show("Send Message failed!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }

                sendMsg = null;

                return doc;
            }
            catch
            {
                return null;
            }
        }

        public delegate void DLGSECSUnknownMsgReceived(string aEqpID, XmlDocument aXML);

        private void OnSECSUnknownMsgReceived(string aEqpID, XmlDocument aXML)
        {
            int nThreadID = System.Threading.Thread.CurrentThread.GetHashCode();
            if (frmMain.m_nMainThreadID != nThreadID)
                this.Invoke(new DLGSECSUnknownMsgReceived(OnUISECSUnknownMsgReceived), new object[] { aEqpID, aXML });
            else
                OnUISECSUnknownMsgReceived(aEqpID, aXML);
        }

        private void OnUISECSUnknownMsgReceived(string aEqpID, XmlDocument aXML)
        {
            Debug.WriteLine(SEComEnabler.SEComUtility.CUtilities.getCurrentShortTime() + " " + "OnSECSUnknownMsgReceived");

            //			this.OnSECSReceived(aEqpID, aXML);
        }

        #endregion

        //******************************************************************************
        // Method - Send Message
        //******************************************************************************
        #region SendMessage
        /// <summary>
        /// Sends Message
        /// </summary>
        /// <param name="msgNode">Selected Node</param>
        /// <param name="isSendPrimaryMsg">true if sendind Primaray Message, else false</param>
        private void SendMessage(TreeNode msgNode, bool isSendPrimaryMsg)
        {
            try
            {
                // make a DriverMsg with a object of CSECSMessage
                SEComEnabler.SECSMsgLibrary.CSEComDriverMsg sendMsg = new SEComEnabler.SECSMsgLibrary.CSEComDriverMsg();

                XmlDocument doc = sendMsg.constructDriverMsg(frmMain.m_strCurSEComID, m_CommnInfo.GetDirection(), 0, msgNode);
                if (doc == null)
                {
                    frmMain.m_MsgStats.TransmissionError++;
                    return;
                }

                SendMessage(ref doc, isSendPrimaryMsg);

                doc = null;
                sendMsg = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }


        private void SendMessage(ref XmlDocument aXml, bool isSendPrimaryMsg)
        {
            try
            {
                // Set SystemBytes
                long curSystemBytes;
                if (isSendPrimaryMsg)
                    curSystemBytes = m_CommnInfo.calcSystemBytes(Common.Cal_SystemBytes_State.INCREASE);
                else
                    curSystemBytes = m_CommnInfo.ReceivedSystemByte;

                XmlNode headerNode = aXml.GetElementsByTagName(ConstXML.NODE_Header)[0];
                if (headerNode == null) return;

                headerNode[ConstXML.ELEMENT_SystemBytes].InnerText = curSystemBytes.ToString();

                // call to send a DriverMsg
                SEComError.SEComPlugIn err_Rtn = SendDriverMsg(aXml, isSendPrimaryMsg);
                if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                {
                    if (isSendPrimaryMsg)
                        m_CommnInfo.calcSystemBytes(Common.Cal_SystemBytes_State.REDUCE);
                    
                    frmMain.m_MsgStats.TransmissionError++;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }

        private SEComError.SEComPlugIn SendDriverMsg(XmlDocument aXML, bool isSendPrimaryMsg)
        {
            if (isSendPrimaryMsg)
            {
                frmMain.m_MsgStats.PrimaryMsgSent++;
                return m_SEComPlugIn.Request(frmMain.m_strCurSEComID, aXML);
            }
            else
            {
                frmMain.m_MsgStats.SecondaryMsgSent++;
                return m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, aXML);
            }
        }
        #endregion

        // ***************************************************************************
        // Treeview realted Events
        // ***************************************************************************
        #region Treeview Related Event
        private void tvwSECSMessage_Click(object sender, System.EventArgs e)
        {
            try
            {
                frmMain.m_blIsFocus = false;
                this.SetEnableMnuSendMessage();
            }
            catch { }
        }

        private void tvwSECSMessage_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if (this.tvwSECSMessage.SelectedNode != null)
                    this.tvwSECSMessage.SelectedNode.ExpandAll();
            }
            catch { }
        }

        private void SetEnableMnuSendMessage()
        {
            try
            {
                sendMessageToolStripMenuItem.Enabled = false;
                sendReplyMessageToolStripMenuItem.Enabled = false;
                
                if (m_CommnInfo.IsSECSConnected == false || this.tvwSECSMessage.SelectedNode == null) return;
                
                SX.SECSDirection secsDirection = m_CommnInfo.GetDirection();

                CSECSMessage oneMsg = new CSECSMessage();

                // 1. set enable 'mnuItemSendPrimaryMessage'
                if (oneMsg.ParseHeader(this.tvwSECSMessage.SelectedNode.FirstNode.Text)
                    && oneMsg.Direction == secsDirection)
                {
                    sendMessageToolStripMenuItem.Enabled = true;
                }

                // 2. set enable 'mnuItemSendReplyMessage'
                if (oneMsg.ParseHeader(this.tvwSECSMessage.SelectedNode.LastNode.Text)
                    && oneMsg.Direction == secsDirection)
                {
                    sendReplyMessageToolStripMenuItem.Enabled = true;
                }

                oneMsg.Terminate();
                oneMsg = null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
                return;
            }
        }

        private void tvwSECSMessage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Delete)
            //    {
            //        this.mnuItemClear_Click(sender, e);
            //    }
            //}
            //catch { }
        }

        private void tvwSECSMessage_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                this.tvwSECSMessage.SelectedNode = this.tvwSECSMessage.GetNodeAt(e.X, e.Y);
            }
            catch { }
        }

        private void tvwSECSMessage_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (this.tvwSECSMessage.SelectedNode == null) return;
                if (e.Button != MouseButtons.Right) return;
                
                // Display the popup menu
                if (this.tvwSECSMessage.SelectedNode != null)
                {
                    Point pt = new Point();
                    pt.X = e.X;
                    pt.Y = e.Y + (this.tvwSECSMessage.Top * 2);
                    mnuPopupEdit.Show(this.tvwSECSMessage, e.Location);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[tvwSECSMessage_MouseUp] - " + ex.GetBaseException().ToString());
            }
        }
        #endregion

        #region TreeNode MenuStrip Click Event
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_objfrmEdit == null && this.tvwSECSMessage.Focused)
                {
                    frmMain.m_blIsFocus = false;
                    this.m_objfrmEdit = new frmEdit(this.tvwSECSMessage.SelectedNode);
                    this.m_objfrmEdit.Owner = this;
                    this.m_objfrmEdit.Closing += new CancelEventHandler(Edit_Closing);
                    this.m_objfrmEdit.Show();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }

        private void Edit_Closing(object sender, CancelEventArgs e)
        {
            this.m_objfrmEdit = null;
            frmMain.m_blIsFocus = true;
        }

        private void sendMessageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sendMessageToolStripMenuItem_Click(sender, e);
        }
        
        private void sendReplyMessageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sendReplyMessageToolStripMenuItem_Click(sender, e);
        }

        private void insertTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add a node at the end of the Tree
            // Default Format : S99F99 - Temporary Message
            try
            {
                TreeNode pairNode = this.tvwSECSMessage.Nodes[CCommonConst.SECSLibrary_Level].Nodes.Add("S99F99 - New Transaction Message");

                CSECSMessage primaryMsg = new CSECSMessage();
                primaryMsg.MessageName = "S99F99";
                primaryMsg.Stream = 99;
                primaryMsg.Function = 99;
                primaryMsg.AutoReply = false;
                primaryMsg.Wait = false;
                primaryMsg.Direction = SX.SECSDirection.FromHost;

                CSECSMessage secondaryMsg = new CSECSMessage();
                secondaryMsg.MessageName = "S99F100";
                secondaryMsg.Stream = 99;
                secondaryMsg.Function = 100;
                secondaryMsg.AutoReply = false;
                secondaryMsg.Wait = false;
                secondaryMsg.Direction = SX.SECSDirection.FromEquipment;

                if (this.GetIdentity().ToUpper().Equals(CCommonConst.Identity_Host))
                    pairNode.ImageIndex = pairNode.SelectedImageIndex = 1;
                else
                    pairNode.ImageIndex = pairNode.SelectedImageIndex = 2;

                InsertMsgsNode(primaryMsg, secondaryMsg, pairNode);

                this.tvwSECSMessage.Refresh();
                this.tvwSECSMessage.SelectedNode = this.tvwSECSMessage.Nodes[CCommonConst.SECSLibrary_Level].LastNode;
            }
            catch { }
        }

        private void insertChildItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tnParentNode = this.tvwSECSMessage.SelectedNode;
                TreeNode tnNewChildNode = new TreeNode(CCommonConst.NewChildNodeText);

                tnParentNode.Nodes.Insert(tnParentNode.GetNodeCount(false) + 1, tnNewChildNode);
                this.UpdateParent(tnParentNode, true);
                this.tvwSECSMessage.Refresh();
            }
            catch { }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int nNodeLevel = SEComEnabler.SEComUtility.CUtilities.getCharCount(this.tvwSECSMessage.SelectedNode.FullPath, this.m_chPathSeparator);
                if (nNodeLevel == CCommonConst.SECSLibrary_Level)
                {
                    DialogResult result;
                    result = MessageBox.Show("Are you sure you want to Clear SECS Transaction Messages Library?",
                        "SEComSimulator", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                        this.tvwSECSMessage.Nodes[0].Nodes.Clear();
                        this.tvwSECSMessage.Refresh();
                        this.tvwSECSMessage.Focus();
                    }
                }
                else if (nNodeLevel != 2)
                {
                    this.UpdateParent(this.tvwSECSMessage.SelectedNode.Parent, false);
                    this.tvwSECSMessage.SelectedNode.Remove();
                    this.tvwSECSMessage.Refresh();
                }
            }
            catch { }
        }
        #endregion

        /// <summary>
        /// This function updates the Parent Node where a Cut/Paste operation occured.
        /// </summary>
        /// <param name="tnParentNode">Parent Node</param>
        /// <param name="IsAdd">Indicates if Node is added or deleted.
        /// TRUE if node is Pasted/Added, FALSE if Node is Cut</param>
        /// <remarks>Change the Parent node to List if not List
        /// If it is already a List Item, then add 1 to node count</remarks>
        public void UpdateParent(TreeNode tnParentNode, bool IsAdd)
        {
            try
            {
                int nNodeLevel = SEComEnabler.SEComUtility.CUtilities.getCharCount(tnParentNode.FullPath, m_chPathSeparator);
                string[] strNodeText = tnParentNode.Text.Split(' ');

                if (nNodeLevel >= CCommonConst.SECSDataItem_Level)
                {
                    if (strNodeText[0] == SX.SECSFormat.L.ToString())
                    {
                        if (IsAdd)
                            strNodeText[1] = Convert.ToString(Convert.ToInt32(strNodeText[1]) + 1);
                        else
                            strNodeText[1] = Convert.ToString(Convert.ToInt32(strNodeText[1]) - 1);
                    }
                    else
                    {
                        strNodeText[0] = SX.SECSFormat.L.ToString();
                        strNodeText[1] = "1";
                    }
                    string strTemp = "";
                    for (int i = 0; i < strNodeText.Length; i++)
                    {
                        strTemp += strNodeText[i] + " ";
                    }
                    tnParentNode.Text = strTemp.Trim();

                    // Change the Image Index
                    CSECSDataItem theDataItem = new CSECSDataItem();
                    theDataItem.ConstructDataItem(nNodeLevel, tnParentNode);
                }
            }
            catch { }
        }

        #region MES Form Menu Click Event
        private void s1F13CRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMES_S1F13 objS1F13 = new frmMES_S1F13();
            objS1F13.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            objS1F13.Show(this);
        }

        private void s2F13ECRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMES_S2F13 objS2F13 = new frmMES_S2F13();
            objS2F13.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            objS2F13.Show(this);
        }

        private void s2F15ECSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMES_S2F15 objS2F15 = new frmMES_S2F15();
            objS2F15.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            objS2F15.Show(this);
        }

        private void s1F101ESSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMES_S1F101 objS1F101 = new frmMES_S1F101();
            objS1F101.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            objS1F101.Show(this);
        }

        private void s1F103STRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMES_S1F103 objS1F103 = new frmMES_S1F103();
            objS1F103.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            objS1F103.Show(this);
        }

        private void s1F105STRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMES_S1F105 objS1F105 = new frmMES_S1F105();
            objS1F105.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            objS1F105.Show(this);
        }

        private void mainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsFormAlreadyOpen(typeof(frmMES_Main)) == null)
            {
                frmMES_Main objMain = new frmMES_Main();
                objMain.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                objMain.Show(this);
            }
            else
            {
                Form objMain = IsFormAlreadyOpen(typeof(frmMES_Main));
                objMain.BringToFront();

                if (objMain.WindowState == FormWindowState.Minimized)
                    objMain.WindowState = FormWindowState.Normal;

                objMain.Activate();
            }
        }

        // 자식 폼 중복 여부
        private Form IsFormAlreadyOpen(Type formType)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.GetType() == formType)
                    return openForm;
            }

            return null;
        }
        #endregion

        // ***************************************************************************
        // Excel Upload
        // ***************************************************************************
        #region Data
        private void excelUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmExcelUpload objExcelUplad = new frmExcelUpload();
                objExcelUplad.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                objExcelUplad.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
            }
        }

        private void dBDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }
        #endregion


        // ***************************************************************************
        // Action Menubar Click Event
        // ***************************************************************************
        private void actionOpen_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(null, null);
        }

        private void actionSave_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem_Click(null, null);
        }

        private void actionConnect_Click(object sender, EventArgs e)
        {
            openConnectionToolStripMenuItem_Click(null, null);
        }

        private void actionDisconnect_Click(object sender, EventArgs e)
        {
            closeConnectionToolStripMenuItem_Click(null, null);
        }

        private void actionConfiguration_Click(object sender, EventArgs e)
        {
            configurationConnectionToolStripMenuItem_Click(null, null);
        }

        private void actionSendMessage_Click(object sender, EventArgs e)
        {
            sendMessageToolStripMenuItem_Click(null, null);
        }
        // ***************************************************************************

        // ***************************************************************************
        // SMD File Open & Save
        // ***************************************************************************
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string strFilter = "AIM SECS Message Definition Files (*.SMD)|*.SMD";

                string strTitle = "Open File";
                this.OpenFile(strFilter, strTitle);
            }
            catch { }
        }

        private bool OpenFile(string theFilter, string theTitle)
        {
            try
            {
                openFileDialog1.InitialDirectory = Application.StartupPath;
                openFileDialog1.Filter = theFilter;
                openFileDialog1.Title = theTitle;

                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.LoadLibFile(openFileDialog1.FileName);
                    this.tvwSECSMessage.Nodes[0].Text = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string theFilter = "AIM SECS Message Definition Files (*.SMD)|*.SMD";
                string theTitle = "Save As";

                this.SaveFile(theFilter, theTitle);
            }
            catch { }
        }

        private bool SaveFile(string theFilter, string theTitle)
        {
            try
            {
                saveFileDialog1.InitialDirectory = Application.StartupPath;
                saveFileDialog1.Filter = theFilter;
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.Title = theTitle;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() != DialogResult.OK) return false;

                this.tvwSECSMessage.Cursor = Cursors.WaitCursor;
                toolStripStatusLabel1.Text = "Saving ";
                toolStripStatusLabel1.Text += saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                toolStripStatusLabel1.Text += "....";

                CSECSDefMgr theDefMgr = new CSECSDefMgr();
                if (!theDefMgr.ConstructDefMessage(this.tvwSECSMessage.Nodes[0], saveFileDialog1.FileName))
                {
                    this.tvwSECSMessage.Cursor = Cursors.Arrow;
                    MessageBox.Show("File Save Error!!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else this.LoadLibFile(saveFileDialog1.FileName);

                theDefMgr.Terminate();
                theDefMgr = null;

                this.tvwSECSMessage.Cursor = Cursors.Arrow;
                toolStripStatusLabel1.Text = "File Name : ";
                toolStripStatusLabel1.Text += this.m_strCurLibFile.Substring(this.m_strCurLibFile.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);

                // Added by smkang on 2007.08.03 - 저장을 마치면 플래그를 false로 변경
                frmEdit.bChangedSMD = false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        // ***************************************************************************

        private void rtxtSECS2Log_MouseDown(object sender, MouseEventArgs e)
        {
            // 우클릭일 경우
            if (e.Button.Equals(MouseButtons.Right))
            {
                // ContextMenu 생성
                ContextMenu m = new ContextMenu();

                MenuItem m1 = new MenuItem();

                m1.Text = "지우기";

                m1.Click += (senders, es) =>
                {
                    this.rtxtSECS2Log.Clear();
                };

                m.MenuItems.Add(m1);
                m.Show(rtxtSECS2Log, new Point(e.X, e.Y));
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tvwSECSMessage_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void s5F101MARSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMES_S5F101 objS5F101 = new frmMES_S5F101();
            objS5F101.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            objS5F101.Show(this);
        }

        private void s6F12EventReportAckToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmMES_S6F12 objS6F12 = new frmMES_S6F12();
            objS6F12.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            objS6F12.Show(this);
        }

        private void rtxtSECS2Log_TextChanged(object sender, EventArgs e)
        {
            // Log 자동 스크롤
            rtxtSECS2Log.SelectionStart = rtxtSECS2Log.Text.Length;
            rtxtSECS2Log.ScrollToCaret();
            //rtxtSECS2Log.Refresh();
        }
    }
}
