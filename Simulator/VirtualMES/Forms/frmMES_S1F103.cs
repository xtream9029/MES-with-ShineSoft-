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
    //S1F103 : Selected TRACK Status Request (S->E)

    public partial class frmMES_S1F103 : Form
    {
        public bool mdiFlag { get; set; }

        public frmMES_S1F103()
        {
            InitializeComponent();

            mdiFlag = true;
            frmMain.ReplayReceived += new frmMain.OnReplyEventHandler(OnSECSReceived);
        }

        private void frmMES_S1F103_Load(object sender, EventArgs e)
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
                this.txtRBottomTrayId.Clear();
                this.txtRTopTrayId.Clear();
                this.txtRStatus.Clear();

                SXTransaction sxTrx = new SXTransaction();
                sxTrx.Stream = 1;
                sxTrx.Function = 103;
                sxTrx.Direction = SX.SECSDirection.FromHost;
                sxTrx.Wait = true;

                sxTrx.WriteNode(SX.SECSFormat.L, 1, "", "");
                sxTrx.WriteNode(SX.SECSFormat.A, 5, this.txtTrackNo.Text.Trim().PadRight(5), "TRACK_NO");

                SEComError.SEComPlugIn err_Rtn = frmMain.m_SEComPlugIn.Request(frmMain.m_strCurSEComID, sxTrx);
                if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                {
                    frmMain.m_MsgStats.TransmissionError++;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                if (sd.HeaderItems.Stream == 1 && sd.HeaderItems.Function == 104)
                {
                    this.txtRTrackNo.Text = sd.DataItems[0][0][0].Value;
                    this.txtRBottomTrayId.Text = sd.DataItems[0][0][1].Value;
                    this.txtRTopTrayId.Text = sd.DataItems[0][0][2].Value;
                    this.txtRStatus.Text = sd.DataItems[0][0][3].Value;
                }
            }
            catch
            {
                return;
            }
        }

        private void txtTrackNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRTrackNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRBottomTrayId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRTopTrayId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRStatus_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
