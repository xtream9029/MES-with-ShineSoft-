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
    public partial class frmMES_S2F13 : Form
    {
        public bool mdiFlag { get; set; }

        public frmMES_S2F13()
        {
            InitializeComponent();

            mdiFlag = true;
            frmMain.ReplayReceived += new frmMain.OnReplyEventHandler(OnSECSReceived);
        }

        private void frmMES_S2F13_Load(object sender, EventArgs e)
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
                this.txtRTrackNo.Clear();
                this.txtRSec.Clear();

                SXTransaction sxTrx = new SXTransaction();
                sxTrx.Stream = 2;
                sxTrx.Function = 13;
                sxTrx.Direction = SX.SECSDirection.FromHost;
                sxTrx.Wait = true;
                //sxTrx.MessageName = "S6F12";
                //sxTrx.MessageData = "test";

                sxTrx.WriteNode(SX.SECSFormat.L, 2, "", "");
                sxTrx.WriteNode(SX.SECSFormat.A, 5, this.txtTrackNo.Text.Trim().PadRight(5), "TRACK_NO");

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
                if (sd.HeaderItems.Stream == 2 && sd.HeaderItems.Function == 14)
                {
                    this.txtRTrackNo.Text = sd.DataItems[0][0][0].Value;
                    this.txtRSec.Text = sd.DataItems[0][0][1].Value;
                }
            }
            catch
            {
                return;
            }
        }
    }
}
