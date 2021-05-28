using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SEComEnabler.SEComStructure;

namespace VirtualMES
{
    public partial class frmMES_S1F13 : Form
    {
        //S1F13:Establish Communications Request(CR)

        public bool mdiFlag { get; set; }

        public frmMES_S1F13()
        {
            InitializeComponent();

            mdiFlag = true;
            frmMain.ReplayReceived += new frmMain.OnReplyEventHandler(OnSECSReceived);
        }

        private void frmMES_S1F13_Load(object sender, EventArgs e)
        {
            if (mdiFlag)
                this.pnlClose.Visible = true;
            else
                this.pnlClose.Visible = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtRAck.Clear();
                this.txtRMdln.Clear();
                this.txtRSoftRev.Clear();

                SXTransaction sxTrx = new SXTransaction();
                sxTrx.Stream = 1;
                sxTrx.Function = 13;
                sxTrx.Direction = SX.SECSDirection.FromHost;
                sxTrx.Wait = true;

                sxTrx.WriteNode(SX.SECSFormat.L, 2, "", "");
                sxTrx.WriteNode(SX.SECSFormat.A, 4, this.txtMdln.Text.Trim().PadRight(4), "MDLN");
                sxTrx.WriteNode(SX.SECSFormat.A, 6, this.txtSoftRev.Text.Trim().PadRight(6), "SOFTREV");

                SEComError.SEComPlugIn err_Rtn = frmMain.m_SEComPlugIn.Request(frmMain.m_strCurSEComID, sxTrx);
                if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                {
                    frmMain.m_MsgStats.TransmissionError++;
                    //this.m_objStats.DisplayStats();
                    return;
                }
            }
            catch
            {
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmMain.ReplayReceived -= OnSECSReceived;
            this.Close();
        }

        // ***************************************************************************
        // OnMessage Event
        // ***************************************************************************
        public void OnSECSReceived(SEComData sd)
        {
            try
            {
                if (sd.HeaderItems.Stream == 1 && sd.HeaderItems.Function == 14)
                {
                    this.txtRAck.Text = sd.DataItems[0][0].Value;           // COMMACK
                    this.txtRMdln.Text = sd.DataItems[0][1][0].Value;       // MDLN
                    this.txtRSoftRev.Text = sd.DataItems[0][1][1].Value;    // SOFTREV
                }
            }
            catch
            {
                return;
            }
        }
    }
}
