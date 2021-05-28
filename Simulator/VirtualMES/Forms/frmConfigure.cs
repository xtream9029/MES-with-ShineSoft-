using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

//using SEComEnabler;
using SEComEnabler.SEComStructure;
using VirtualMES.Common;

using Microsoft.WindowsAPICodePack.Dialogs;

namespace VirtualMES
{
    public partial class frmConfigure : Form
    {
        private CSEComConf m_curConf;
        private bool m_blIsSECSConnected;

        public frmConfigure()
        {
            InitializeComponent();
        }

        public frmConfigure(ref CSEComConf theConf, bool IsSECSConnected)
        {
            InitializeComponent();
            this.m_curConf = theConf;
            this.m_blIsSECSConnected = IsSECSConnected;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateConfigData() == false) return;

            if (SaveConfig() == false)
            {
                MessageBox.Show("Error Occurred In Saving SEComSimulator Configuration!!!", "SEComSimulator",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            // Save the current Simulator's SEComID
            frmMain.m_strCurSEComID = this.cboSEComID.Text.ToUpper().Trim();

            MessageBox.Show("SEComSimulator Configuration is Saved!!!", "SEComSimulator",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            //			this.CompareConnectionValues();
            
            RefreshForm(cboSEComID.Text);
        }

        private bool ValidateConfigData()
        {
            if (!this.ValidateSEComID()) return false;

            if (txtRemotePort.Enabled)
            {
                if (!CheckPortNumberRange(txtRemotePort, null))
                    return false;
                if (this.IsValidIPAddress(this.txtRemoteIP1.Text.Trim()) == false ||
                    this.IsValidIPAddress(this.txtRemoteIP2.Text.Trim()) == false ||
                    this.IsValidIPAddress(this.txtRemoteIP3.Text.Trim()) == false ||
                    this.IsValidIPAddress(this.txtRemoteIP4.Text.Trim()) == false)
                    return false;
            }
            else if (txtLocalPort.Enabled)
            {
                if (!CheckPortNumberRange(txtLocalPort, null))
                    return false;
            }

            //if (cboSEComLogMode.SelectedIndex != 2 || cboSECS1LogMode.SelectedIndex != 2 || cboSECS2LogMode.SelectedIndex != 2)
            //{
            //    if (txtLogDir.Text.Length == 0)
            //    {
            //        MessageBox.Show("SECom Log Folder Cannot be Empty!!!", "SEComSimulator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.txtLogDir.Focus();
            //        return false;
            //    }
            //}

            return true;
        }

        private bool ValidateSEComID()
        {
            // SEComID cannot be null
            if (cboSEComID.Text.Trim() == "")
            {
                MessageBox.Show("Please insert a SEComID!!!", "SEComSimulator",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboSEComID.Focus();
                return false;
            }

            // SEComID cannot start with a number
            if (!Char.IsLetter(cboSEComID.Text.Trim(), 0))
            {
                MessageBox.Show("SEComID Must Start with an Alphabet!!!", "SEComSimulator",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cboSEComID.Focus();
                return false;
            }

            return true;
        }

        private bool CheckPortNumberRange(object sender, System.EventArgs e)
        {
            System.Windows.Forms.TextBox txtPort = (System.Windows.Forms.TextBox)sender;
            if (txtPort.Text.Length == 0 || Convert.ToInt32(txtPort.Text) < 1025 || Convert.ToInt32(txtPort.Text) > 65535)
            {
                MessageBox.Show("Please Enter a value between 1025 and 65535 for Port Number", "SEComSimulator",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPort.Text = "";
                txtPort.Focus();
                return false;
            }
            return true;
        }

        private bool IsValidIPAddress(string aOctet)
        {
            try
            {
                if (aOctet == "" || Convert.ToInt32(aOctet) > 255)
                {
                    MessageBox.Show("Invalid Octet value in Remote IPAddress!!  Please enter a vlaue between 0 and 255", "SEComSimulator",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
                return false;
            }
        }

        private bool SaveConfig()
        {
            try
            {
                string curSEComID = cboSEComID.Text.ToUpper().Trim();
                bool bAddFlag = true;

                ArrayList theList = this.m_curConf.GetSectionList();
                if (theList != null)
                {
                    for (int i = 0; i < theList.Count; i++)
                    {
                        if (curSEComID.Equals(theList[i].ToString().ToUpper()))
                        {
                            bAddFlag = false;
                            break;
                        }
                    }
                }

                // Add the section if not exists already, else modify
                if (bAddFlag == true)   //Add a new Section
                {
                    if (!this.m_curConf.AddSection(curSEComID))
                        return false;
                }

                // 1. General Tab
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_COMMON_IDENTITY, cboIdentity.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_COMMON_SECSMODE, cboProtocolType.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_T3, txtT3.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_COMMON_DEVICEID, txtDeviceId.Text);

                // 2. HSMS Tab
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_T5, txtT5.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_T6, txtT6.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_T7, txtT7.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_T8, txtT8.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_HSMS_HSMSMODE, cboConnectMode.Text);
                string strIPAddr = txtRemoteIP1.Text.Trim() + "." + txtRemoteIP2.Text.Trim() + "." + txtRemoteIP3.Text.Trim() + "." + txtRemoteIP4.Text.Trim();
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_HSMS_REMOTEIP, strIPAddr);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_HSMS_REMOTEPORT, txtRemotePort.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_HSMS_LOCALPORT, txtLocalPort.Text);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_EXT_LINKTEST, txtLinkTestInterval.Text);

                //// 3. SECS1 Tab
                //this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_T1, txtT1.Text);
                //this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_T2, txtT2.Text);
                //this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_TIME_T4, txtT4.Text);
                //this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_SECS1_COMPORT, cboSerialPort.Text);
                //this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_SECS1_BAUDRATE, cboBaudRate.Text);
                //this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_SECS1_RETRYCOUNT, txtRetryCount.Text);
                //if (chkMaster.Checked)
                //    this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_SECS1_MASTER, true.ToString());
                //else
                //    this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_SECS1_MASTER, false.ToString());
                //if (chkInterleave.Checked)
                //    this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_SECS1_INTERLEAVE, true.ToString());
                //else
                //    this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_SECS1_INTERLEAVE, false.ToString());

                // 4. Log Tab
                string strNewLogDir = this.txtLogDir.Text.Trim();
                strNewLogDir += System.IO.Path.DirectorySeparatorChar + this.cboSEComID.Text.Trim();
                if (!System.IO.Directory.Exists(strNewLogDir))
                {
                    System.IO.Directory.CreateDirectory(strNewLogDir);
                }

                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_LOG_DIR, strNewLogDir);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_LOG_DRIVERMODE, cboSEComLogMode.Text.Substring(0, 1));
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_LOG_SECSIMODE, cboSECS1LogMode.Text.Substring(0, 1));
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_LOG_SECSIIMODE, cboSECS2LogMode.Text.Substring(0, 1));
                string strLogBackup = "0";
                if (cboLogBackup.SelectedIndex == 0) strLogBackup = "1";
                else if (cboLogBackup.SelectedIndex == 1) strLogBackup = "2";
                else if (cboLogBackup.SelectedIndex == 2) strLogBackup = "3";
                else if (cboLogBackup.SelectedIndex == 3) strLogBackup = "4";
                else if (cboLogBackup.SelectedIndex == 4) strLogBackup = "5";
                else if (cboLogBackup.SelectedIndex == 5) strLogBackup = "6";
                else if (cboLogBackup.SelectedIndex == 6) strLogBackup = "7";
                else if (cboLogBackup.SelectedIndex == 7) strLogBackup = "14";
                else if (cboLogBackup.SelectedIndex == 8) strLogBackup = "21";
                else if (cboLogBackup.SelectedIndex == 9) strLogBackup = "28";
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_LOG_BACKUP, strLogBackup);
                this.m_curConf.WriteValue(curSEComID, SXProFile.ProFileKey.KEY_LOG_DRIVERLEVEL,
                    cboSEComLogLevel.Text.Substring(cboSEComLogLevel.Text.Length - 1));

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
                return false;
            }
        }

        private void RefreshForm(string aCurSEComID)
        {
            try
            {
                bool bFlag = false;

                aCurSEComID = aCurSEComID.ToUpper();

                // 1. Display the SEComIDs of Ini file
                ArrayList arlSEComIDs = this.m_curConf.GetSectionList();

                cboSEComID.Items.Clear();
                if (arlSEComIDs != null && arlSEComIDs.Count > 0)
                {
                    for (int i = 0; i < arlSEComIDs.Count; i++)
                    {
                        string oneSEComID = arlSEComIDs[i].ToString().ToUpper();

                        this.cboSEComID.Items.Add(oneSEComID);
                        if (oneSEComID.Equals(aCurSEComID)) bFlag = true;
                    }
                }

                // 2. Display curSEComID
                SXProFile theInfo = null;
                if (aCurSEComID.Trim() != "" && bFlag == true)
                    theInfo = m_curConf.ReadSectionData(aCurSEComID);

                Display(aCurSEComID, theInfo);

            }
            catch
            {
                cboSEComID.Text = "";
            }
        }

        private void Display(string aSEComID, SXProFile aConfInfo)
        {
            try
            {
                if (aSEComID == "" || aSEComID == null || aConfInfo == null)
                {
                    this.LoadDefaultValues();
                    return;
                }
                else
                {
                    //General
                    this.cboSEComID.Text = aSEComID;
                    if (aConfInfo.Read(SXProFile.ProFileKey.KEY_COMMON_IDENTITY).ToUpper().Equals(CCommonConst.Identity_Eqp))
                        this.cboIdentity.SelectedIndex = 0;
                    else
                        this.cboIdentity.SelectedIndex = 1;
                    if (aConfInfo.Read(SXProFile.ProFileKey.KEY_COMMON_SECSMODE).ToUpper().Equals(CConfigConst.SECTION_HSMS))
                        this.cboProtocolType.SelectedIndex = 0;
                    else
                        this.cboProtocolType.SelectedIndex = 1;
                    this.txtDeviceId.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_COMMON_DEVICEID);

                    //HSMS 
                    this.txtT3.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_TIME_T3);
                    this.txtT5.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_TIME_T5);
                    this.txtT6.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_TIME_T6);
                    this.txtT7.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_TIME_T7);
                    this.txtT8.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_TIME_T8);
                    this.cboConnectMode.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_HSMS_HSMSMODE);
                    EnableIPAddrandPort();
                    string[] strRemoteIP = aConfInfo.Read(SXProFile.ProFileKey.KEY_HSMS_REMOTEIP).Split('.');
                    this.txtRemoteIP1.Text = strRemoteIP[0];
                    this.txtRemoteIP2.Text = strRemoteIP[1];
                    this.txtRemoteIP3.Text = strRemoteIP[2];
                    this.txtRemoteIP4.Text = strRemoteIP[3];
                    this.txtRemotePort.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_HSMS_REMOTEPORT);
                    this.txtLocalPort.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_HSMS_LOCALPORT);
                    this.txtLinkTestInterval.Text = aConfInfo.Read(SXProFile.ProFileKey.KEY_TIME_EXT_LINKTEST);

                    // Log
                    string strLogDir = aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DIR);
                    int nIndex = strLogDir.LastIndexOf("\\");
                    if (nIndex != -1)
                    {
                        strLogDir = strLogDir.Substring(0, nIndex);
                        this.txtLogDir.Text = strLogDir;
                    }

                    if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DRIVERMODE).ToUpper() == "H")
                        this.cboSEComLogMode.SelectedIndex = 0;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DRIVERMODE).ToUpper() == "D")
                        this.cboSEComLogMode.SelectedIndex = 1;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DRIVERMODE).ToUpper() == "N")
                        this.cboSEComLogMode.SelectedIndex = 2;

                    if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DRIVERLEVEL) == "1")
                        this.cboSEComLogLevel.SelectedIndex = 0;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DRIVERLEVEL) == "2")
                        this.cboSEComLogLevel.SelectedIndex = 1;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DRIVERLEVEL) == "3")
                        this.cboSEComLogLevel.SelectedIndex = 2;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DRIVERLEVEL) == "4")
                        this.cboSEComLogLevel.SelectedIndex = 3;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_DRIVERLEVEL) == "5")
                        this.cboSEComLogLevel.SelectedIndex = 4;

                    if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_SECSIMODE).ToUpper() == "H")
                        this.cboSECS1LogMode.SelectedIndex = 0;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_SECSIMODE).ToUpper() == "D")
                        this.cboSECS1LogMode.SelectedIndex = 1;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_SECSIMODE).ToUpper() == "N")
                        this.cboSECS1LogMode.SelectedIndex = 2;

                    if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_SECSIIMODE).ToUpper() == "H")
                        this.cboSECS2LogMode.SelectedIndex = 0;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_SECSIIMODE).ToUpper() == "D")
                        this.cboSECS2LogMode.SelectedIndex = 1;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_SECSIIMODE).ToUpper() == "N")
                        this.cboSECS2LogMode.SelectedIndex = 2;

                    if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "1")
                        this.cboLogBackup.SelectedIndex = 0;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "2")
                        this.cboLogBackup.SelectedIndex = 1;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "3")
                        this.cboLogBackup.SelectedIndex = 2;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "4")
                        this.cboLogBackup.SelectedIndex = 3;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "5")
                        this.cboLogBackup.SelectedIndex = 4;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "6")
                        this.cboLogBackup.SelectedIndex = 5;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "7")
                        this.cboLogBackup.SelectedIndex = 6;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "14")
                        this.cboLogBackup.SelectedIndex = 7;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "21")
                        this.cboLogBackup.SelectedIndex = 8;
                    else if (aConfInfo.Read(SXProFile.ProFileKey.KEY_LOG_BACKUP) == "28")
                        this.cboLogBackup.SelectedIndex = 0;
                } // end of else
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
            }

        }

        private void LoadDefaultValues()
        {
            // General
            this.cboIdentity.SelectedIndex = 1;
            this.txtDeviceId.Text = "1";
            this.cboProtocolType.SelectedIndex = 0;

            // HSMS
            this.txtT3.Text = "45";
            this.txtT5.Text = "10";
            this.txtT6.Text = "5";
            this.txtT7.Text = "10";
            this.txtT8.Text = "10";
            this.cboConnectMode.SelectedIndex = 1;
            this.txtLinkTestInterval.Text = "120";
            this.txtRemoteIP1.Text = "127";
            this.txtRemoteIP2.Text = "0";
            this.txtRemoteIP3.Text = "0";
            this.txtRemoteIP4.Text = "1";
            
            //// Log
            //this.cboLogBackup.SelectedIndex = 1;
            //this.cboSECS1LogMode.SelectedIndex = 1;
            //this.cboSECS2LogMode.SelectedIndex = 1;
            //this.cboSEComLogMode.SelectedIndex = 2;
            //this.cboSEComLogLevel.SelectedIndex = 4;
        }

        private void cboConnectMode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.EnableIPAddrandPort();
        }

        private void EnableIPAddrandPort()
        {
            if (this.cboConnectMode.SelectedIndex == 1)
            {
                this.txtRemoteIP1.Enabled = false;
                this.txtRemoteIP2.Enabled = false;
                this.txtRemoteIP3.Enabled = false;
                this.txtRemoteIP4.Enabled = false;
                this.txtRemotePort.Enabled = false;
                this.txtLocalPort.Enabled = true;
            }
            else
            {
                this.txtRemoteIP1.Enabled = true;
                this.txtRemoteIP2.Enabled = true;
                this.txtRemoteIP3.Enabled = true;
                this.txtRemoteIP4.Enabled = true;
                this.txtRemotePort.Enabled = true;
                this.txtLocalPort.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConfigure_Load(object sender, EventArgs e)
        {
            try
            {
                this.StartPosition = FormStartPosition.CenterParent;
                this.m_curConf.FlagOpenConnection = false;

                RefreshForm(frmMain.m_strCurSEComID);
            }
            catch
            {
                MessageBox.Show("Could not load Configuration!!!", "SEComSimulator",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.SetLogDirValue();
        }

        private void txtLogDir_MouseDown(object sender, MouseEventArgs e)
        {
            this.SetLogDirValue();
        }

        private void SetLogDirValue()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.DefaultDirectory = "..\\";

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtLogDir.Text = dialog.FileName;
            }
        }

        private void txtT3_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboSEComID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDeviceId_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtRemoteIP1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLinkTestInterval_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLogDir_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboSEComLogMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboSECS1LogMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboLogBackup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboSECS2LogMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cboIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
