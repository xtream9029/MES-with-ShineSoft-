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
    public partial class frmMES_S1F101 : Form
    {
        //S1F101: Equipment Status Send (S->E)

        public bool mdiFlag { get; set; }

        //생성자
        public frmMES_S1F101()
        {
            InitializeComponent();

            mdiFlag = true;
            frmMain.ReplayReceived += new frmMain.OnReplyEventHandler(OnSECSReceived);
        }

        private void frmMES_S1F101_Load(object sender, EventArgs e)
        {
            if (mdiFlag)
                this.pnlClose.Visible = true;
            else
                this.pnlClose.Visible = false;
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cboState.Text.Trim()))
                return;

            string[] value = this.cboState.Text.Split(':');
            this.cboState.Text = value[0];
        }

        //Click SendMessage
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.cboState.Text.Trim()))
                {
                    MessageBox.Show("STATE 값을 선택 해 주세요.");
                    return;
                }

                this.txtRAck.Clear();

                string[] value = this.cboState.Text.Split(':');

                SXTransaction sxTrx = new SXTransaction();
                sxTrx.Stream = 1;
                sxTrx.Function = 101;
                sxTrx.Direction = SX.SECSDirection.FromHost;
                sxTrx.Wait = true;

                sxTrx.WriteNode(SX.SECSFormat.L, 1, "", "");
                sxTrx.WriteNode(SX.SECSFormat.L, 2, "", "");
                sxTrx.WriteNode(SX.SECSFormat.A, 6, this.txtEqpId.Text.Trim().PadRight(6), "EQPID");
                sxTrx.WriteNode(SX.SECSFormat.U2, 1, value[0].Trim().PadRight(1), "STATE");

                SEComError.SEComPlugIn err_Rtn = frmMain.m_SEComPlugIn.Request(frmMain.m_strCurSEComID, sxTrx);
                if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                {
                    frmMain.m_MsgStats.TransmissionError++;
                    //this.m_objStats.DisplayStats();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }//Cilck Send Message

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
                if (sd.HeaderItems.Stream == 1 && sd.HeaderItems.Function == 102)
                {
                    this.txtRAck.Text = sd.DataItems[0].Value;
                }
            }
            catch
            {
                return;
            }
        }

        private void txtEqpId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
