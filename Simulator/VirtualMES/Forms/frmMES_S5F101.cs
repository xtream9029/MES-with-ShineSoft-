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
    public partial class frmMES_S5F101 : Form
    {
        public bool mdiFlag { get; set; }

        public frmMES_S5F101()
        {
            InitializeComponent();

            mdiFlag = true;
            frmMain.ReplayReceived += new frmMain.OnReplyEventHandler(OnSECSReceived);
        }

        //단순히 격자무늬만 사라지게하는 부분
        private void frmMES_S5F101_Load(object sender, EventArgs e)
        {
            if (mdiFlag)
                this.pnlClose.Visible = true;
            else
                this.pnlClose.Visible = false;
        }

        //메시지 보내기
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtRAck.Clear();

                SXTransaction sxTrx = new SXTransaction();
                sxTrx.Stream = 5;
                sxTrx.Function = 101;
                sxTrx.Direction = SX.SECSDirection.FromHost;
                sxTrx.Wait = true;
                
                sxTrx.WriteNode(SX.SECSFormat.L, 3, "", "");
                sxTrx.WriteNode(SX.SECSFormat.A, 6, this.txtBottomTrayId.Text.Trim().PadRight(6), "BOTTOM_TRAYID");
                sxTrx.WriteNode(SX.SECSFormat.A, 6, this.txtTopTrayId.Text.Trim().PadRight(6), "TOP_TRAYID");
                sxTrx.WriteNode(SX.SECSFormat.U2, 1, this.txtMesCode.Text.Trim(), "MES_CODE");

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
                if (sd.HeaderItems.Stream == 5 && sd.HeaderItems.Function == 102)
                {
                    this.txtRAck.Text = sd.DataItems[0].Value;
                }
            }
            catch
            {
                return;
            }
        }
    }
}
