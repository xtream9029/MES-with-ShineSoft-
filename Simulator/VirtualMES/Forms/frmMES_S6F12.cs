using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

using SEComEnabler.SEComStructure;

namespace VirtualMES
{
    public partial class frmMES_S6F12 : Form
    {
        public bool mdiFlag { get; set; }

        public frmMES_S6F12()
        {
            InitializeComponent();

            mdiFlag = true;
            //frmMain.S6F11Received += new frmMain.OnS6F11EventHandler(OnSECSReceived);
        }

        private void frmMES_S6F12_Load(object sender, EventArgs e)
        {
            if (mdiFlag)
                this.pnlClose.Visible = true;
            else
                this.pnlClose.Visible = false;

            this.grdRouteInfo.DataSource = frmMain.TB_EQUIP_INF_ROUTE.dtEquipInfRoute;
            this.grdTrayRouteInfo.DataSource = frmMain.TB_TRAY_INF_ROUTE.dtTrayInfRoute;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SXTransaction sxTrx = new SXTransaction();
                sxTrx.Stream = 6;
                sxTrx.Function = 12;
                //sxTrx.Direction = SX.SECSDirection.FromHost;

                sxTrx.WriteNode(SX.SECSFormat.L, 6, "", "");
                sxTrx.WriteNode(SX.SECSFormat.U2, 1, this.txtRAck.Text.Trim(), "ERACK6");
                sxTrx.WriteNode(SX.SECSFormat.U2, 1, this.cboRCeid.Text.Trim(), "CEID");
                sxTrx.WriteNode(SX.SECSFormat.A, 5, this.txtRFromTrackNo.Text.Trim().PadRight(5), "FROM_TRACK");
                sxTrx.WriteNode(SX.SECSFormat.A, 5, this.txtRTrackNo.Text.Trim().PadRight(5), "TRACK_NO");    // 이동할 곳을 찾아와야 함..
                sxTrx.WriteNode(SX.SECSFormat.U2, 1, this.cboRResponse.Text.Trim(), "RESPONSE");
                sxTrx.WriteNode(SX.SECSFormat.L, 1, "", "");
                sxTrx.WriteNode(SX.SECSFormat.L, 5, "", "");
                sxTrx.WriteNode(SX.SECSFormat.A, 6, this.txtRBottomTrayId.Text.Trim().PadRight(6), "BOTTOM_TRAYID");
                sxTrx.WriteNode(SX.SECSFormat.A, 6, this.txtRTopTrayId.Text.Trim().PadRight(6), "TOP_TRAYID");
                sxTrx.WriteNode(SX.SECSFormat.A, 11, this.txtRLocation.Text.Trim().PadRight(11), "LOCATION");
                sxTrx.WriteNode(SX.SECSFormat.A, 14, this.txtRTime.Text.Trim().PadRight(14), "TIME");
                sxTrx.WriteNode(SX.SECSFormat.A, 9, this.txtRDuration.Text.Trim().PadRight(9), "DURATION");

                SEComError.SEComPlugIn err_Rtn = frmMain.m_SEComPlugIn.Reply(frmMain.m_strCurSEComID, sxTrx, frmMain.m_CommnInfo.ReceivedSystemByte);
                if (err_Rtn != SEComError.SEComPlugIn.ERR_NONE)
                {
                    frmMain.m_MsgStats.TransmissionError++;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().ToString());
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //frmMain.S6F11Received -= OnSECSReceived;
            this.Close();
        }

        // ***************************************************************************
        // OnMessage Event
        // ***************************************************************************
        //public void OnSECSReceived(SEComData sd)
        //{
        //    Console.WriteLine("Starting thread...");
        //    Thread t1 = new Thread(() => AutoReplyCommand(sd));
        //    t1.Start();
            
        //    Console.WriteLine("Wating until thread stops...");
        //    t1.Join();
        //}

        private void AutoReplyCommand(SEComData sd)
        {
            try
            {
                int ceid = sd.DataItems[0][0].Value;

                string strRCeid = string.Empty;
                string strRResponse = string.Empty;
                string strRFromTrackNo = string.Empty;
                string strRTrackNo = string.Empty;
                string strRBottomTrayId = string.Empty;
                string strRTopTrayId = string.Empty;
                string strRLocation = string.Empty;
                string strRTime = string.Empty;
                string strRDuration = string.Empty;

                if (ceid == 101)
                {
                    int work_type = sd.DataItems[0][1][2].Value;

                    if (work_type == 1) // 입고
                    {
                        strRCeid = ceid.ToString();
                        strRResponse = "4";
                        strRFromTrackNo = sd.DataItems[0][1][0].Value;
                        strRTrackNo = "";
                        strRBottomTrayId = sd.DataItems[0][1][3].Value;
                        strRTopTrayId = sd.DataItems[0][1][4].Value;
                        strRLocation = "";
                        strRTime = "0";    // 출고 예정 시간, yyyyMMddHHmmss
                        strRDuration = "0";   // Aging 시간, dddHHmmss
                    }
                    else if (work_type == 3) // Tray 도착 보고
                    {
                        strRCeid = ceid.ToString();

                        if (sd.DataItems[0][1][0].Value == null || sd.DataItems[0][1][0].Value.ToString().Trim() == "")
                            strRFromTrackNo = sd.DataItems[0][1][1].Value;
                        else
                            strRFromTrackNo = sd.DataItems[0][1][0].Value;

                        strRBottomTrayId = sd.DataItems[0][1][3].Value;

                        if (this.radBase.Checked)
                        {
                            // TB_EQUIP_INF_ROUTE.xlsx 파일 기준

                            if (VirtualMES.BaseBiz.FindDestMCType(strRFromTrackNo, "") == "MC")
                            {
                                strRResponse = "4";
                                strRTrackNo = "";
                                strRTime = DateTime.Now.AddDays(1).ToString("yyyyMMddHHmmss");    // "20191230000000";
                                strRDuration = "000115959";

                                // DEST가 충방전기(29600)이면 LOCATION 줄 것.
                                string trackNo = VirtualMES.BaseBiz.FindDestTrackNo(strRFromTrackNo, "");
                                if (trackNo.StartsWith("296"))
                                {
                                    strRLocation = "01-001-01";
                                    strRTrackNo = trackNo;
                                }
                                else
                                    strRLocation = "";
                            }
                            else
                            {
                                strRResponse = "1";
                                strRTrackNo = VirtualMES.BaseBiz.FindDestTrackNo(strRFromTrackNo, "");  // 이동 대상 TrackNo 찾기
                                strRTime = "";    // SPACE
                                strRDuration = "";   // SPACE
                                strRLocation = "";
                            }
                        }
                        else
                        {
                            // TB_TRAY_INF_ROUTE.xlsx 파일 기준
                            DataRow drTmp = VirtualMES.BaseBiz.FindDestRowData(strRFromTrackNo, sd.DataItems[0][1][3].Value.ToString().Trim());

                            if (drTmp["TYPE"].ToString().Equals("MC") || drTmp["TYPE"].ToString().Equals("IN"))
                            {
                                strRResponse = "4";
                                strRTrackNo = "";

                                if (drTmp["OUT_PLAN_TIME"] == null || String.IsNullOrEmpty(drTmp["OUT_PLAN_TIME"].ToString()))
                                    strRTime = DateTime.Now.AddDays(1).ToString("yyyyMMddHHmmss");    // "20191230000000";
                                else
                                    strRTime = drTmp["OUT_PLAN_TIME"].ToString();

                                if (drTmp["DURATION"] == null || String.IsNullOrEmpty(drTmp["DURATION"].ToString()))
                                    strRDuration = "000115959";
                                else
                                    strRDuration = drTmp["DURATION"].ToString();

                                // DEST가 충방전기(29600)이면 LOCATION 줄 것.
                                string trackNo = VirtualMES.BaseBiz.FindDestTrackNo(strRFromTrackNo, sd.DataItems[0][1][3].Value.ToString().Trim());
                                if (trackNo.StartsWith("296"))
                                {
                                    strRLocation = frmMain.TB_CHARGE_STOCK.FindEmptyLocation(trackNo);
                                    strRTrackNo = trackNo;
                                }
                                else
                                    strRLocation = "";
                            }
                            else
                            {
                                strRResponse = "1";
                                strRTrackNo = VirtualMES.BaseBiz.FindDestTrackNo(strRFromTrackNo, sd.DataItems[0][1][3].Value.ToString().Trim());  // 이동 대상 TrackNo 찾기
                                strRTime = "";    // SPACE
                                strRDuration = "";   // SPACE
                                strRLocation = "";
                            }
                        }

                        strRTopTrayId = sd.DataItems[0][1][4].Value;
                    }
                }
                else if (ceid == 102 || ceid == 103)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";
                    strRFromTrackNo = sd.DataItems[0][1][0].Value;
                    strRTrackNo = sd.DataItems[0][1][1].Value;
                    strRBottomTrayId = sd.DataItems[0][1][3].Value;
                    strRTopTrayId = sd.DataItems[0][1][4].Value;
                    strRLocation = "";
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 104)
                {
                    strRCeid = ceid.ToString();

                    try
                    {
                        if (VirtualMES.BaseBiz.CheckMGType(sd.DataItems[0][1][0].Value.ToString().Trim(), sd.DataItems[0][1][1].Value.ToString().Trim()) == "N")
                        {
                            strRResponse = "0";   // 2 : 적재이동, 0 : 이동
                            strRTrackNo = VirtualMES.BaseBiz.FindDestTrackNo(sd.DataItems[0][1][0].Value.ToString().Trim(), sd.DataItems[0][1][1].Value.ToString().Trim());
                        }
                        else
                        {
                            strRResponse = "2";
                            strRTrackNo = sd.DataItems[0][1][0].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());

                        strRResponse = "2";
                        strRTrackNo = sd.DataItems[0][1][0].Value;
                    }

                    strRFromTrackNo = sd.DataItems[0][1][0].Value;

                    strRBottomTrayId = sd.DataItems[0][1][1].Value;
                    strRTopTrayId = ""; // sd.DataItems[0][1][2].Value;
                    strRLocation = "";
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 105 || ceid == 107)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "1";
                    strRFromTrackNo = sd.DataItems[0][1][0].Value;

                    // 이동 대상 TrackNo 찾기
                    if (radBase.Checked)
                        strRTrackNo = VirtualMES.BaseBiz.FindDestTrackNo(strRFromTrackNo, "");
                    else
                        strRTrackNo = VirtualMES.BaseBiz.FindDestTrackNo(strRFromTrackNo, sd.DataItems[0][1][1].Value);

                    strRBottomTrayId = sd.DataItems[0][1][1].Value;
                    strRTopTrayId = sd.DataItems[0][1][2].Value;
                    strRLocation = "";
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 106)
                {
                    strRCeid = ceid.ToString();

                    try
                    {
                        if (VirtualMES.BaseBiz.CheckDPType(sd.DataItems[0][1][0].Value.ToString().Trim(), sd.DataItems[0][1][1].Value.ToString().Trim()) == "N")
                        {
                            strRResponse = "0";   // 3 : 분리이동, 0 : 이동
                            strRTrackNo = VirtualMES.BaseBiz.FindDestTrackNo(sd.DataItems[0][1][0].Value.ToString().Trim(), sd.DataItems[0][1][1].Value.ToString().Trim());
                        }
                        else
                        {
                            strRResponse = "3";
                            strRTrackNo = sd.DataItems[0][1][0].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());

                        strRResponse = "3";
                        strRTrackNo = sd.DataItems[0][1][0].Value;
                    }

                    strRFromTrackNo = sd.DataItems[0][1][0].Value;
                    strRBottomTrayId = sd.DataItems[0][1][1].Value;
                    strRTopTrayId = sd.DataItems[0][1][2].Value;
                    strRLocation = "";
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 201 || ceid == 202)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";

                    if (sd.DataItems[0][3][0][0] == null || sd.DataItems[0][3][0][0].Value == null)
                        strRFromTrackNo = "";
                    else
                        strRFromTrackNo = sd.DataItems[0][3][0][0].Value;

                    strRTrackNo = "";  // SPACE
                    strRBottomTrayId = sd.DataItems[0][3][0][1].Value;
                    strRTopTrayId = sd.DataItems[0][3][0][2].Value;
                    strRLocation = sd.DataItems[0][3][0][3].Value;

                    // TB_TRAY_INF_ROUTE.xlsx 파일 기준
                    DataRow drTmp = VirtualMES.BaseBiz.FindDestRowData(strRFromTrackNo, strRBottomTrayId);

                    strRTime = drTmp["OUT_PLAN_TIME"].ToString();    // SPACE
                    strRDuration = drTmp["DURATION"].ToString();   // SPACE

                    // 충방전기 입고완료
                    String scNo = sd.DataItems[0][2].Value;
                    if (ceid == 202 && scNo.StartsWith("296"))
                        frmMain.TB_CHARGE_STOCK.UpdateInStock(scNo, strRLocation, strRBottomTrayId, strRTopTrayId, strRDuration, strRTime);
                }
                else if (ceid == 203)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";
                    strRFromTrackNo = "";

                    if (sd.DataItems[0][3][0][0] == null || sd.DataItems[0][3][0][0].Value == null)
                        strRTrackNo = "";
                    else
                        strRTrackNo = sd.DataItems[0][3][0][0].Value;

                    strRBottomTrayId = sd.DataItems[0][3][0][1].Value;
                    strRTopTrayId = sd.DataItems[0][3][0][2].Value;
                    strRLocation = sd.DataItems[0][3][0][3].Value;
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 204)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";

                    strRFromTrackNo = sd.DataItems[0][2].Value;
                    strRTrackNo = sd.DataItems[0][3][0][0].Value;
                    strRBottomTrayId = sd.DataItems[0][3][0][1].Value;
                    strRTopTrayId = sd.DataItems[0][3][0][2].Value;
                    strRLocation = sd.DataItems[0][3][0][3].Value;
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE

                    // 충방전기 출고완료
                    String scNo = strRFromTrackNo;
                    if (scNo.StartsWith("296"))
                        frmMain.TB_CHARGE_STOCK.DeleteStock(scNo, strRLocation);
                }
                else if (ceid == 205)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";
                    strRFromTrackNo = sd.DataItems[0][1][0].Value;
                    strRTrackNo = "";  // SPACE
                    strRBottomTrayId = sd.DataItems[0][1][1].Value;
                    strRTopTrayId = sd.DataItems[0][1][2].Value;
                    strRLocation = sd.DataItems[0][1][4].Value;
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 206 || ceid == 209)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";
                    strRFromTrackNo = sd.DataItems[0][1][0].Value;
                    strRTrackNo = "";  // SPACE
                    strRBottomTrayId = sd.DataItems[0][1][1].Value;
                    strRTopTrayId = sd.DataItems[0][1][2].Value;
                    strRLocation = sd.DataItems[0][1][5].Value;
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 301)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";
                    strRFromTrackNo = "";
                    strRTrackNo = "";  // SPACE
                    strRBottomTrayId = "";
                    strRTopTrayId = "";
                    strRLocation = "";
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 303)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";
                    strRFromTrackNo = "";
                    strRTrackNo = "";  // SPACE
                    strRBottomTrayId = sd.DataItems[0][8][0][1].Value;
                    strRTopTrayId = sd.DataItems[0][8][0][2].Value;
                    strRLocation = sd.DataItems[0][8][0][0].Value;
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }
                else if (ceid == 501)
                {
                    strRCeid = ceid.ToString();
                    strRResponse = "0";
                    strRFromTrackNo = sd.DataItems[0][1][0][0].Value;
                    strRTrackNo = "";  // SPACE
                    strRBottomTrayId = "";
                    strRTopTrayId = "";
                    strRLocation = "";
                    strRTime = "";    // SPACE
                    strRDuration = "";   // SPACE
                }

                // AutoReply 체크시 자동 응답
                if (chkAutoReply.Checked == true)
                {
                    SXTransaction sxTrx = new SXTransaction();
                    sxTrx.Stream = 6;
                    sxTrx.Function = 12;
                    //sxTrx.Direction = SX.SECSDirection.FromHost;

                    sxTrx.WriteNode(SX.SECSFormat.L, 6, "", "");
                    sxTrx.WriteNode(SX.SECSFormat.U2, 1, "0", "ERACK6");
                    sxTrx.WriteNode(SX.SECSFormat.U2, 1, strRCeid.Trim(), "CEID");
                    sxTrx.WriteNode(SX.SECSFormat.A, 5, strRFromTrackNo.Trim().PadRight(5), "FROM_TRACK");
                    sxTrx.WriteNode(SX.SECSFormat.A, 5, strRTrackNo.Trim().PadRight(5), "TRACK_NO");    // 이동할 곳을 찾아와야 함..
                    sxTrx.WriteNode(SX.SECSFormat.U2, 1, strRResponse.Trim(), "RESPONSE");
                    sxTrx.WriteNode(SX.SECSFormat.L, 1, "", "");
                    sxTrx.WriteNode(SX.SECSFormat.L, 5, "", "");
                    sxTrx.WriteNode(SX.SECSFormat.A, 6, strRBottomTrayId.Trim().PadRight(6), "BOTTOM_TRAYID");
                    sxTrx.WriteNode(SX.SECSFormat.A, 6, strRTopTrayId.Trim().PadRight(6), "TOP_TRAYID");
                    sxTrx.WriteNode(SX.SECSFormat.A, 11, strRLocation.Trim().PadRight(11), "LOCATION");
                    sxTrx.WriteNode(SX.SECSFormat.A, 14, strRTime.Trim().PadRight(14), "TIME");
                    sxTrx.WriteNode(SX.SECSFormat.A, 9, strRDuration.Trim().PadRight(9), "DURATION");

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
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
                return;
            }
        }

        private void btnExcelReload_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.grdRouteInfo.DataSource = null;
            this.grdTrayRouteInfo.DataSource = null;
            
            // MES EXCEL DATA READ - Base Route
            string FileName = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "Config" + System.IO.Path.DirectorySeparatorChar + "TB_EQUIP_INF_ROUTE.xlsx";
            frmMain.TB_EQUIP_INF_ROUTE = new MesData.TB_EQUIP_INF_ROUTE(FileName);

            // MES EXCEL DATA READ - Tray Route
            FileName = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "Config" + System.IO.Path.DirectorySeparatorChar + "TB_TRAY_INF_ROUTE.xlsx";
            frmMain.TB_TRAY_INF_ROUTE = new MesData.TB_TRAY_INF_ROUTE(FileName);

            this.grdRouteInfo.DataSource = frmMain.TB_EQUIP_INF_ROUTE.dtEquipInfRoute;
            this.grdTrayRouteInfo.DataSource = frmMain.TB_TRAY_INF_ROUTE.dtTrayInfRoute;

            this.Cursor = Cursors.Default;
        }

        private void txtRAck_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkAutoReply_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void grdRouteInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdTrayRouteInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
