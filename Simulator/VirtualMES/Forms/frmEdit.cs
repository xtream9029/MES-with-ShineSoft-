using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SEComEnabler.SECSMsgLibrary;
using SEComEnabler.SEComStructure;
using SEComEnabler.SEComUtility;
using VirtualMES.Common;

namespace VirtualMES
{
    public partial class frmEdit : Form
    {
        private System.Windows.Forms.TreeNode m_tnItemNode;
        private int m_nNodeLevel;
        public static bool bChangedSMD = false;

        public frmEdit()
        {
            InitializeComponent();
        }

        public frmEdit(System.Windows.Forms.TreeNode SelectedNode)
        {
            InitializeComponent();

            this.m_tnItemNode = SelectedNode;
        }

        // ***************************************************************************
        // Form & Common Part
        // ***************************************************************************
        private void frmEdit_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.EnablePanels(this.m_tnItemNode);
                this.DisplaySECSMessageProperty();
                this.CenterToParent();
                this.ShowInTaskbar = false;
                this.Focus();

                if (this.m_nNodeLevel >= CCommonConst.SECSDataItem_Level)
                {
                    this.txtItemValue.Focus();
                    this.txtItemValue.SelectAll();
                }
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void frmEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    frmMain.m_blIsLocked = false;
        //}

        /// <summary>
		/// Enables/Disables respective panels, based on the selected tree node
		/// </summary>
		/// <param name="SelectedNode">Selected Tree Node</param>
		private void EnablePanels(System.Windows.Forms.TreeNode SelectedNode)
        {
            this.m_nNodeLevel = SEComEnabler.SEComUtility.CUtilities.getCharCount(this.m_tnItemNode.FullPath, '\\');

            if (this.m_nNodeLevel == CCommonConst.SECSPairMsg_Level)
            {
                this.pnlPairMessage.Visible = true;
                this.pnlHeaderMessage.Visible = false;
                this.pnlDataItem.Visible = false;
            }
            else if (this.m_nNodeLevel == CCommonConst.SECSMessage_Level)
            {
                this.pnlHeaderMessage.Visible = true;
                this.pnlPairMessage.Visible = false;
                this.pnlDataItem.Visible = false;
            }
            else if (this.m_nNodeLevel >= CCommonConst.SECSDataItem_Level)
            {
                this.pnlDataItem.Visible = true;
                this.pnlPairMessage.Visible = false;
                this.pnlHeaderMessage.Visible = false;
            }

        }

        /// <summary>
        /// Reloads the form
        /// </summary>
        /// <param name="SelectedNode">Selected Tree Node</param>
        public void ReLoadForm(System.Windows.Forms.TreeNode SelectedNode)
        {
            try
            {
                this.m_tnItemNode = SelectedNode;
                this.EnablePanels(this.m_tnItemNode);
                this.DisplaySECSMessageProperty();
            }
            catch
            {
                this.Dispose();
            }
        }

        /// <summary>
		/// Fills the controls in the Edit dialog based on the selected node
		/// </summary>
		private void DisplaySECSMessageProperty()
        {
            try
            {
                string theText = this.m_tnItemNode.Text;
                // display the message as a heading
                this.Text = theText;

                if (this.m_nNodeLevel == CCommonConst.SECSLibrary_Level)
                {
                    this.Close();
                }
                else if (this.m_nNodeLevel == CCommonConst.SECSPairMsg_Level)
                {
                    CSECSPairMsg thePairMsg = new CSECSPairMsg();
                    thePairMsg.ConstructPairMsg(theText);

                    txtPairName.Text = thePairMsg.PairName;
                    txtPairDescription.Text = thePairMsg.Description;

                    thePairMsg = null;
                }
                else if (this.m_nNodeLevel == CCommonConst.SECSMessage_Level)
                {
                    CSECSMessage theMsg = new CSECSMessage();
                    theMsg.ParseHeader(this.m_tnItemNode.Text);

                    this.txtHeaderName.Text = theMsg.MessageName;
                    txtHeaderStream.Text = theMsg.Stream.ToString();
                    txtHeaderFunction.Text = theMsg.Function.ToString();

                    if (Convert.ToInt32(txtHeaderFunction.Text) % 2 == 0)
                    {
                        theMsg.Wait = false;
                        theMsg.AutoReply = false;
                    }
                    chkHeaderWait.Checked = theMsg.Wait;
                    chkHeaderAutoReply.Checked = theMsg.AutoReply;

                    // if it is secondary message, disable Wait bit and Auto Reply
                    if (this.m_tnItemNode.Index == 1)
                    {
                        chkHeaderAutoReply.Enabled = false;
                        chkHeaderWait.Enabled = false;
                    }
                    else
                    {
                        chkHeaderWait.Enabled = true;
                        if (chkHeaderWait.Checked)
                            chkHeaderAutoReply.Enabled = true;
                        else
                            chkHeaderAutoReply.Enabled = false;
                    }

                    chkHeaderNoLogging.Checked = theMsg.NoLogging;
                    chkHeaderNoLogging.Enabled = false;

                    if (theMsg.Direction == SX.SECSDirection.FromEquipment)
                        rdHeaderEH.Checked = true;
                    else if (theMsg.Direction == SX.SECSDirection.FromHost)
                        rdHeaderHE.Checked = true;

                    theMsg.Terminate();
                    theMsg = null;
                    this.ChangeHeaderNodeText();

                }
                else if (this.m_nNodeLevel >= CCommonConst.SECSDataItem_Level)
                {
                    CSECSDataItem theDataItem = new CSECSDataItem();
                    theDataItem.ConstructDataItem(this.m_nNodeLevel, this.m_tnItemNode);

                    if (theDataItem.ItemType == SX.SECSFormat.L)
                    {
                        rdList.Checked = true;
                        rdList.ForeColor = Color.Blue;
                        this.txtItemValue.Enabled = false;

                        // Disable other Item Types, if the selected node has child nodes
                        if (this.m_tnItemNode.GetNodeCount(false) > 0)
                            DisableRadioButtons(false);
                        else
                            DisableRadioButtons(true);

                    }
                    else
                    {
                        this.txtItemValue.Enabled = true;
                        DisableRadioButtons(true);
                        if (theDataItem.ItemType == SX.SECSFormat.BOOLEAN)
                        {
                            rdBool.Checked = true;
                            rdBool.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.A)
                        {
                            rdAscii.Checked = true;
                            rdAscii.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.B)
                        {
                            rdBin.Checked = true;
                            rdBin.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.I1)
                        {
                            rdI1.Checked = true;
                            rdI1.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.I2)
                        {
                            rdI2.Checked = true;
                            rdI2.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.I4)
                        {
                            rdI4.Checked = true;
                            rdI4.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.I8)
                        {
                            rdI8.Checked = true;
                            rdI8.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.F4)
                        {
                            rdF4.Checked = true;
                            rdF4.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.F8)
                        {
                            rdF8.Checked = true;
                            rdF8.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.U1)
                        {
                            rdU1.Checked = true;
                            rdU1.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.U2)
                        {
                            rdU2.Checked = true;
                            rdU2.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.U4)
                        {
                            rdU4.Checked = true;
                            rdU4.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.U8)
                        {
                            rdU8.Checked = true;
                            rdU8.ForeColor = Color.Blue;
                        }
                        else if (theDataItem.ItemType == SX.SECSFormat.J)
                        {
                            rdJis.Checked = true;
                            rdJis.ForeColor = Color.Blue;
                        }
                        else
                        {
                            rdAny.Checked = true;
                            rdAny.ForeColor = Color.Blue;
                        }
                    }

                    // ItemCount
                    txtItemCount.Text = theDataItem.ItemCount.ToString();
                    // ItemName
                    txtItemName.Text = theDataItem.ItemName.Trim();
                    // ItemValue
                    txtItemValue.Text = theDataItem.ItemValue;
                    txtItemValue.Focus();
                    txtItemValue.SelectAll();

                    theDataItem = null;

                    this.ChangeItemNodeText(true);
                }
            }
            catch
            {
                this.Dispose();
            }

        }


        /// <summary>
        /// This function will update the text of the selected node.
        /// </summary>
        /// <param name="strNodeText">String to be written as the selected node text</param>
        /// <remarks>Set the Heading label of the Edit SECS Properities dailog also</remarks>
        private void setNodeText(string strNodeText)
        {
            try
            {
                this.m_tnItemNode.Text = strNodeText;

                // Change the image of the pair msg. if the direction is changed
                if (this.m_nNodeLevel == CCommonConst.SECSMessage_Level)
                {
                    this.ReLoadParentNode();
                }
                else if (this.m_nNodeLevel >= CCommonConst.SECSDataItem_Level)
                {
                    string aFormat = this.m_tnItemNode.Text.Substring(0, this.m_tnItemNode.Text.IndexOf(" ")).Trim();
                }
                this.Text = strNodeText;
                this.ActiveControl.Focus();

                // Added by smkang on 2007.07.30 - SEComSimulator를 종료하기 전 SMD의 변경이 있었는지 확인하고 변경되었다면 저장할 것인지 묻기 위해
                //								   flag를 유지, SMD가 변경된다면 setNodeText 메소드를 반드시 호출하므로 setNodeText 메소드 내에서 true로 변경
                bChangedSMD = true;
            }
            catch { }
        }


        private void ReLoadParentNode()
        {
            try
            {
                if (this.m_tnItemNode.Index != 0) return;
                TreeNode tnPairNode = this.m_tnItemNode.Parent;

                if (frmMain.m_strIdentity.ToUpper().Equals(CCommonConst.Identity_Host))
                {
                    if (this.m_tnItemNode.Text.IndexOf(SEComEnabler.SEComStructure.ConstXML.DIRECTION_FromHost) != -1)
                        tnPairNode.ImageIndex = tnPairNode.SelectedImageIndex = 1;
                    else if (this.m_tnItemNode.Text.IndexOf(SEComEnabler.SEComStructure.ConstXML.DIRECTION_FromEquipment) != -1)
                        tnPairNode.ImageIndex = tnPairNode.SelectedImageIndex = 2;
                }
                else if (frmMain.m_strIdentity.ToUpper().Equals(CCommonConst.Identity_Eqp))
                {
                    if (this.m_tnItemNode.Text.IndexOf(SEComEnabler.SEComStructure.ConstXML.DIRECTION_FromHost) != -1)
                        tnPairNode.ImageIndex = tnPairNode.SelectedImageIndex = 2;
                    else if (this.m_tnItemNode.Text.IndexOf(SEComEnabler.SEComStructure.ConstXML.DIRECTION_FromEquipment) != -1)
                        tnPairNode.ImageIndex = tnPairNode.SelectedImageIndex = 1;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.GetBaseException().ToString());
            }
        }



        // ****************************************************************************
        // Pair Message Part 
        // ****************************************************************************
        private void txtPairName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }

        private void txtPairName_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ChangePairNodeText();
        }

        private void txtPairDescription_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }

        private void txtPairDescription_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ChangePairNodeText();
        }

        private void ChangePairNodeText()
        {
            if (this.m_nNodeLevel != CCommonConst.SECSPairMsg_Level) return;

            bool bChangeFlag = false;

            CSECSPairMsg pairMsg = new CSECSPairMsg();
            pairMsg.ConstructPairMsg(this.m_tnItemNode.Text);


            // PairName
            if (pairMsg.PairName != this.txtPairName.Text)
            {
                bChangeFlag = true;
                pairMsg.PairName = this.txtPairName.Text;
                if (pairMsg.PairName != this.txtPairName.Text)
                    this.txtPairName.Text = pairMsg.PairName;
            }

            // PairDescription
            if (pairMsg.Description != txtPairDescription.Text)
            {
                bChangeFlag = true;
                pairMsg.Description = txtPairDescription.Text;
                if (pairMsg.Description != txtPairDescription.Text)
                    txtPairDescription.Text = pairMsg.Description;
            }


            if (bChangeFlag)
                this.setNodeText(pairMsg.GetPairMsgToString());

            pairMsg = null;
        }

        // ****************************************************************************
        // Header Message Part
        // ****************************************************************************
        private void txtHeaderName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }

        private void txtHeaderName_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.ChangeHeaderNodeText();
        }

        private void txtHeaderStream_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }

        private void txtHeaderStream_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void txtHeaderFunction_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }

        private void txtHeaderFunction_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }

        private void chkHeaderWait_Click(object sender, System.EventArgs e)
        {

        }


        private void chkHeaderAutoReply_Click(object sender, System.EventArgs e)
        {

        }

        private void rdHeaderHE_Click(object sender, System.EventArgs e)
        {
            ChangeHeaderNodeText();
        }

        private void rdHeaderEH_Click(object sender, System.EventArgs e)
        {
            ChangeHeaderNodeText();
        }


        private void ChangeHeaderNodeText()
        {

            if (this.m_nNodeLevel != CCommonConst.SECSMessage_Level) return;

            bool bChangeFlag = false;

            CSECSMessage oneMsg = new CSECSMessage();
            oneMsg.ParseHeader(this.m_tnItemNode.Text);

            // 1. MessageName
            if (oneMsg.MessageName != this.txtHeaderName.Text)
            {
                bChangeFlag = true;
                oneMsg.MessageName = this.txtHeaderName.Text;
                if (oneMsg.MessageName != this.txtHeaderName.Text)
                    this.txtHeaderName.Text = oneMsg.MessageName;
            }

            // 2. Stream
            if (oneMsg.Stream.ToString() != this.txtHeaderStream.Text)
            {
                bChangeFlag = true;
                if (this.txtHeaderStream.Text == "")
                {
                    oneMsg.Stream = 0;
                }
                else
                {
                    try
                    {
                        oneMsg.Stream = Convert.ToInt32(this.txtHeaderStream.Text);
                    }
                    catch
                    {
                        oneMsg.Stream = 0;
                    }

                    if (oneMsg.Stream.ToString() != this.txtHeaderStream.Text)
                        this.txtHeaderStream.Text = oneMsg.Stream.ToString();
                }
            }

            // 3. Function
            if (oneMsg.Function.ToString() != this.txtHeaderFunction.Text)
            {
                bChangeFlag = true;
                if (this.txtHeaderFunction.Text == "")
                {
                    oneMsg.Function = 0;
                }
                else
                {
                    try
                    {
                        oneMsg.Function = Convert.ToInt32(this.txtHeaderFunction.Text);
                    }
                    catch
                    {
                        oneMsg.Function = 0;
                    }

                    if (oneMsg.Function.ToString() != this.txtHeaderFunction.Text)
                        this.txtHeaderFunction.Text = oneMsg.Function.ToString();
                }
            }

            // 4. Wait
            if (oneMsg.Wait != this.chkHeaderWait.Checked)
            {
                bChangeFlag = true;
                oneMsg.Wait = this.chkHeaderWait.Checked;
            }

            // 5. AutoReply
            if (oneMsg.AutoReply != this.chkHeaderAutoReply.Checked)
            {
                bChangeFlag = true;
                oneMsg.AutoReply = this.chkHeaderAutoReply.Checked;
            }

            // 6. NoLogging
            if (oneMsg.NoLogging != this.chkHeaderNoLogging.Checked)
            {
                bChangeFlag = true;
                oneMsg.NoLogging = this.chkHeaderNoLogging.Checked;
            }

            // 7. Direction
            SX.SECSDirection secsDirection;
            if (this.rdHeaderEH.Checked)
                secsDirection = SX.SECSDirection.FromEquipment;
            else
                secsDirection = SX.SECSDirection.FromHost;
            if (oneMsg.Direction != secsDirection)
            {
                bChangeFlag = true;
                oneMsg.Direction = secsDirection;
            }

            if (bChangeFlag)
                this.setNodeText(oneMsg.GetSECSMsgToString());

            oneMsg.Terminate();
            oneMsg = null;
        }



        // ****************************************************************************
        // DataItem Part
        // ****************************************************************************

        private void txtItemName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // accept only alphabets or numbers
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != 8 || e.KeyChar == (char)13)
                //if(e.KeyChar == ' ' || e.KeyChar == '\\')
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtItemName_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.ChangeItemNodeText(true);
        }


        private void txtItemValue_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //			if(e.KeyChar == '\'' || e.KeyChar == '\\' || e.KeyChar == ':' || e.KeyChar == (char)13)	
            // Deleted by smkang on 2007.07.30 - 모든 캐릭터가 전송 가능하도록 변경
            //			if(e.KeyChar == '\\' || e.KeyChar == ':' || e.KeyChar == (char)13)	
            //			{
            //				e.Handled = true;
            //				return;
            //			}
            //			else if(e.KeyChar == 8)	
            //			{
            //				e.Handled = false;
            //				return;
            //			}

            /// uses setItemCount to set the value for Item Count
            /// true is passed for count of words in Item Value
            /// false is passed for the count of characters in Item Value
            if (rdBool.Checked)
            {
                if (e.KeyChar == 'T' || e.KeyChar == 't' || e.KeyChar == 'R' || e.KeyChar == 'r'
                    || e.KeyChar == 'U' || e.KeyChar == 'u' || e.KeyChar == 'E' || e.KeyChar == 'e'
                    || e.KeyChar == 'F' || e.KeyChar == 'f' || e.KeyChar == 'A' || e.KeyChar == 'a'
                    || e.KeyChar == 'L' || e.KeyChar == 'l' || e.KeyChar == 'S' || e.KeyChar == 's'
                    || e.KeyChar == 'E' || e.KeyChar == 'e' || e.KeyChar == ' ')
                    e.Handled = false;
                else if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == ' ')
                {
                    e.Handled = false;
                }
                else
                    e.Handled = true;
            }
            else if (rdI1.Checked || rdI2.Checked || rdI4.Checked || rdI8.Checked)
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                    (e.KeyChar == ' ' || e.KeyChar == '-'))
                    e.Handled = false;
                else
                    e.Handled = true;

            }
            else if (rdU1.Checked || rdU2.Checked || rdU4.Checked || rdU8.Checked)
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == ' ')
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            else if (rdF4.Checked || rdF8.Checked || rdBin.Checked)
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                    (e.KeyChar == '.' || e.KeyChar == ' ' || e.KeyChar == '-'
                    || e.KeyChar == 'e' || e.KeyChar == 'E'))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }

        private void txtItemValue_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.ChangeItemNodeText(false);
        }




        /// <summary>
        /// This function is used to handle the click event of the radio buttons.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radio_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                ResetRadioColorBlack();

                if (this.m_tnItemNode.GetNodeCount(false) > 0)
                {
                    rdList.Checked = true;
                    rdList.ForeColor = Color.Blue;

                    // reset the node text
                    this.ChangeItemNodeText(true);
                    return;
                }


                RadioButton rd = (RadioButton)sender;

                if (rd.Name == "rdList")
                    rdList.ForeColor = Color.Blue;
                else if (rd.Name == "rdAscii")
                    rdAscii.ForeColor = Color.Blue;
                else if (rd.Name == "rdBin")
                    rdBin.ForeColor = Color.Blue;
                else if (rd.Name == "rdBool")
                    rdBool.ForeColor = Color.Blue;
                else if (rd.Name == "rdI1")
                    rdI1.ForeColor = Color.Blue;
                else if (rd.Name == "rdI2")
                    rdI2.ForeColor = Color.Blue;
                else if (rd.Name == "rdI4")
                    rdI4.ForeColor = Color.Blue;
                else if (rd.Name == "rdI8")
                    rdI8.ForeColor = Color.Blue;
                else if (rd.Name == "rdU1")
                    rdU1.ForeColor = Color.Blue;
                else if (rd.Name == "rdU2")
                    rdU2.ForeColor = Color.Blue;
                else if (rd.Name == "rdU4")
                    rdU4.ForeColor = Color.Blue;
                else if (rd.Name == "rdU8")
                    rdU8.ForeColor = Color.Blue;
                else if (rd.Name == "rdF4")
                    rdF4.ForeColor = Color.Blue;
                else if (rd.Name == "rdF8")
                    rdF8.ForeColor = Color.Blue;
                else if (rd.Name == "rdJis")
                    rdJis.ForeColor = Color.Blue;
                else if (rd.Name == "rdAny")
                    rdAny.ForeColor = Color.Blue;

                if (rd.Name == "rdList")
                {
                    txtItemValue.Text = "";
                    txtItemValue.Enabled = false;
                }
                else
                {
                    txtItemValue.Enabled = true;
                }

                ChangeItemNodeText(true);
            }
            catch { }
        }

        /// <summary>
        /// This will set the ForeColor of all the radio buttons to Balck
        /// </summary>
        private void ResetRadioColorBlack()
        {
            rdList.ForeColor = Color.Black;
            rdAscii.ForeColor = Color.Black;
            rdBin.ForeColor = Color.Black;
            rdBool.ForeColor = Color.Black;
            rdAny.ForeColor = Color.Black;
            rdF4.ForeColor = Color.Black;
            rdF8.ForeColor = Color.Black;
            rdI1.ForeColor = Color.Black;
            rdI2.ForeColor = Color.Black;
            rdI4.ForeColor = Color.Black;
            rdI8.ForeColor = Color.Black;
            rdJis.ForeColor = Color.Black;
            rdU1.ForeColor = Color.Black;
            rdU2.ForeColor = Color.Black;
            rdU4.ForeColor = Color.Black;
            rdU8.ForeColor = Color.Black;
        }


        /// <summary>
        /// This will disable all the radio buttons, except List
        /// </summary>
        private void DisableRadioButtons(bool IsChecked)
        {
            rdAscii.Enabled = IsChecked;
            rdBin.Enabled = IsChecked;
            rdBool.Enabled = IsChecked;
            rdAny.Enabled = IsChecked;
            rdF4.Enabled = IsChecked;
            rdF8.Enabled = IsChecked;
            rdI1.Enabled = IsChecked;
            rdI2.Enabled = IsChecked;
            rdI4.Enabled = IsChecked;
            rdI8.Enabled = IsChecked;
            rdJis.Enabled = IsChecked;
            rdU1.Enabled = IsChecked;
            rdU2.Enabled = IsChecked;
            rdU4.Enabled = IsChecked;
            rdU8.Enabled = IsChecked;

            if (IsChecked)
            {
                this.ResetRadioColorBlack();
            }
        }



        private bool ChangeItemNodeText(bool IsAlwaysChange)
        {
            try
            {
                if (this.m_nNodeLevel < CCommonConst.SECSDataItem_Level) return false;
                bool bChangeFlag = false;

                CSECSDataItem oneItem = new CSECSDataItem();
                oneItem.ConstructDataItem(0, this.m_tnItemNode);

                // 1. ItemType
                SX.SECSFormat secsFormat = this.GetSECSFormat();
                if (oneItem.ItemType != secsFormat)
                {
                    bChangeFlag = true;
                    oneItem.ItemType = secsFormat;
                }

                // 2. ItemName
                if (oneItem.ItemName != this.txtItemName.Text)
                {
                    bChangeFlag = true;
                    oneItem.ItemName = this.txtItemName.Text;
                    if (oneItem.ItemName != this.txtItemName.Text)
                        this.txtItemName.Text = oneItem.ItemName;
                }

                // 3. ItemCount, ItemValue;
                if (oneItem.ItemType == SX.SECSFormat.L)
                {
                    if (oneItem.ItemCount != this.m_tnItemNode.GetNodeCount(false))
                    {
                        bChangeFlag = true;
                        oneItem.ItemCount = this.m_tnItemNode.GetNodeCount(false);
                    }

                    if (oneItem.ItemCount.ToString() != txtItemCount.Text)
                        txtItemCount.Text = oneItem.ItemCount.ToString();
                }
                else
                {
                    string tmpValue = txtItemValue.Text;
                    CUtilities.validateValue(oneItem.ItemType, ref tmpValue, false);

                    if (oneItem.ItemValue != tmpValue)
                    {
                        bChangeFlag = true;
                        oneItem.ItemValue = tmpValue;
                        txtItemValue.Text = oneItem.ItemValue;
                    }
                    else if (oneItem.ItemValue != this.txtItemValue.Text && IsAlwaysChange)
                    {
                        txtItemValue.Text = oneItem.ItemValue;
                    }

                    //ItemCount
                    int tmpCount = CUtilities.getCountOfValue(oneItem.ItemType, oneItem.ItemValue);
                    if (oneItem.ItemCount != tmpCount)
                    {
                        bChangeFlag = true;
                        oneItem.ItemCount = tmpCount;
                    }

                    if (oneItem.ItemCount.ToString() != this.txtItemCount.Text)
                        this.txtItemCount.Text = oneItem.ItemCount.ToString();
                }

                if (bChangeFlag)
                    setNodeText(oneItem.GetItemToString());

                oneItem = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private SX.SECSFormat GetSECSFormat()
        {
            try
            {
                if (this.rdList.Checked)
                    return SX.SECSFormat.L;
                else if (this.rdAscii.Checked)
                    return SX.SECSFormat.A;
                else if (this.rdBin.Checked)
                    return SX.SECSFormat.B;
                else if (this.rdBool.Checked)
                    return SX.SECSFormat.BOOLEAN;
                else if (this.rdU1.Checked)
                    return SX.SECSFormat.U1;
                else if (this.rdU2.Checked)
                    return SX.SECSFormat.U2;
                else if (this.rdU4.Checked)
                    return SX.SECSFormat.U4;
                else if (this.rdU8.Checked)
                    return SX.SECSFormat.U8;
                else if (this.rdI1.Checked)
                    return SX.SECSFormat.I1;
                else if (this.rdI2.Checked)
                    return SX.SECSFormat.I2;
                else if (this.rdI4.Checked)
                    return SX.SECSFormat.I4;
                else if (this.rdI8.Checked)
                    return SX.SECSFormat.I8;
                else if (this.rdF4.Checked)
                    return SX.SECSFormat.F4;
                else if (this.rdF8.Checked)
                    return SX.SECSFormat.F8;
                else if (this.rdJis.Checked)
                    return SX.SECSFormat.J;
                else
                    return SX.SECSFormat.X;
            }
            catch
            {
                return SX.SECSFormat.X;
            }
        }

        private void frmEdit_Activated(object sender, System.EventArgs e)
        {
            frmMain.m_blIsFocus = false;
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain.m_blIsLocked = false;
        }
    }
}
