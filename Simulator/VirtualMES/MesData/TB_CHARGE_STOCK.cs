using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;

namespace VirtualMES.MesData
{
    public class TB_CHARGE_STOCK
    {
        public DataTable dtChargeStock { get; set; }

        private int MAX_BAY = 15;
        private int MAX_LVL = 10;

        public TB_CHARGE_STOCK()
        {

        }

        public void Init()
        {
            dtChargeStock = null;
            dtChargeStock = new DataTable("TB_CHARGE_STOCK");

            DataColumn col1 = new DataColumn("SC_NO");
            DataColumn col2 = new DataColumn("LOC");
            DataColumn col3 = new DataColumn("BOTTOM_TRAY_ID");
            DataColumn col4 = new DataColumn("TOP_TRAY_ID");
            DataColumn col5 = new DataColumn("IN_TIME");
            DataColumn col6 = new DataColumn("OUT_FLAG");
            DataColumn col7 = new DataColumn("DURATION");
            DataColumn col8 = new DataColumn("OUT_PLAN_TIME");

            dtChargeStock.Columns.Add(col1);
            dtChargeStock.Columns.Add(col2);
            dtChargeStock.Columns.Add(col3);
            dtChargeStock.Columns.Add(col4);
            dtChargeStock.Columns.Add(col5);
            dtChargeStock.Columns.Add(col6);
            dtChargeStock.Columns.Add(col7);
            dtChargeStock.Columns.Add(col8);

            for (int bay = 1; bay <= MAX_BAY; bay++)
            {
                for (int lvl = 1; lvl <= MAX_LVL; lvl++)
                {
                    String location = String.Format("{0}-{1}-{2}", "02", bay.ToString().PadLeft(3, '0'), lvl.ToString().PadLeft(2, '0'));

                    DataRow dr = dtChargeStock.NewRow();
                    dr["SC_NO"] = "29611";
                    dr["LOC"] = location;

                    dtChargeStock.Rows.Add(dr);
                }
            }

            for (int bay = 1; bay <= MAX_BAY; bay++)
            {
                for (int lvl = 1; lvl <= MAX_LVL; lvl++)
                {
                    String location = String.Format("{0}-{1}-{2}", "03", bay.ToString().PadLeft(3, '0'), lvl.ToString().PadLeft(2, '0'));

                    DataRow dr = dtChargeStock.NewRow();
                    dr["SC_NO"] = "29621";
                    dr["LOC"] = location;

                    dtChargeStock.Rows.Add(dr);
                }
            }

            for (int bay = 1; bay <= MAX_BAY; bay++)
            {
                for (int lvl = 1; lvl <= MAX_LVL; lvl++)
                {
                    String location = String.Format("{0}-{1}-{2}", "05", bay.ToString().PadLeft(3, '0'), lvl.ToString().PadLeft(2, '0'));

                    DataRow dr = dtChargeStock.NewRow();
                    dr["SC_NO"] = "29631";
                    dr["LOC"] = location;

                    dtChargeStock.Rows.Add(dr);
                }
            }
        }
        
        /// <summary>
        /// 충방전기 입고시 빈 Location 정보를 찾음
        /// LOCATION = BANK-BAY-LVL
        /// </summary>
        /// <returns></returns>
        public String FindEmptyLocation(String scNo)
        {
            String retValue = String.Empty;
            IEnumerable<DataRow> drs = null;

            //// 트레이 목적지
            //// 트레이 ID 범위 값으로 지정
            drs = from chargeStock in dtChargeStock.AsEnumerable()
                  where Convert.ToInt32(chargeStock.Field<string>("SC_NO")) == Convert.ToInt32(scNo.Trim())
                        //&& chargeStock.Field<string>("BOTTOM_TRAY_ID") == ""
                  orderby chargeStock.Field<string>("LOC")
                  select chargeStock;

            foreach (DataRow dr in drs)
            {
                if (String.IsNullOrEmpty(dr["BOTTOM_TRAY_ID"].ToString()))
                {
                    retValue = dr["LOC"].ToString();
                    return retValue;
                }
            }

            return retValue;
        }

        /// <summary>
        /// 충방전기에서 출고 대상 Tray를 찾음
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataRow> FindOutTray(String scNo)
        {
            IEnumerable<DataRow> drs = null;

            //// 트레이 목적지
            //// 트레이 ID 범위 값으로 지정
            drs = from chargeStock in dtChargeStock.AsEnumerable()
                  where Convert.ToInt32(chargeStock.Field<string>("SC_NO")) == Convert.ToInt32(scNo.Trim()) &&
                        chargeStock.Field<string>("BOTTOM_TRAY_ID") != "" &&
                        chargeStock.Field<string>("OUT_FLAG") != "Y"
                  orderby chargeStock.Field<string>("IN_TIME")
                  select chargeStock;

            //foreach (DataRow dr in drs)
            //{
            //    return dr;
            //}

            return drs;
        }

        /// <summary>
        /// 충방전기 Stock 정보 조회
        /// </summary>
        /// <returns></returns>
        public DataRow FindStockInfo(String fromDttm, String toDttm)
        {
            IEnumerable<DataRow> drs = null;

            //// 트레이 목적지
            //// 트레이 ID 범위 값으로 지정
            drs = from chargeStock in dtChargeStock.AsEnumerable()
                  where Convert.ToDateTime(chargeStock.Field<string>("IN_TIME")) >= Convert.ToDateTime(fromDttm.Trim()) &&
                        Convert.ToDateTime(chargeStock.Field<string>("IN_TIME")) <= Convert.ToDateTime(toDttm.Trim())
                  select chargeStock;

            foreach (DataRow dr in drs)
            {
                return dr;
            }

            return null;
        }

        public void UpdateInStock(String scNo, String location, String bottomTrayId, String topTrayId)
        {
            UpdateInStock(scNo, location, bottomTrayId, topTrayId, "", "");
        }

        public void UpdateInStock(String scNo, String location, String bottomTrayId, String topTrayId, String duration, String outPlanTime)
        {
            try
            {
                DataRow[] rows = dtChargeStock.Select("SC_NO = '" + scNo + "' AND LOC = '" + location + "'");

                rows[0]["BOTTOM_TRAY_ID"] = bottomTrayId;
                rows[0]["TOP_TRAY_ID"] = topTrayId;
                rows[0]["IN_TIME"] = DateTime.Now.AddDays(1).ToString("yyyyMMddHHmmss");
                rows[0]["OUT_FLAG"] = "";
                rows[0]["DURATION"] = duration;
                rows[0]["OUT_PLAN_TIME"] = outPlanTime;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
                return;
            }
        }

        public void UpdateOutFlagStock(String scNo, String location)
        {
            try
            {
                DataRow[] rows = dtChargeStock.Select("SC_NO = '" + scNo + "' AND LOC = '" + location + "'");
                
                rows[0]["OUT_FLAG"] = "Y";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
                return;
            }
        }

        public void DeleteStock(String scNo, String location)
        {
            try
            {
                DataRow[] rows = dtChargeStock.Select("SC_NO = '" + scNo + "' AND LOC = '" + location + "'");

                rows[0]["BOTTOM_TRAY_ID"] = String.Empty;
                rows[0]["TOP_TRAY_ID"] = String.Empty;
                rows[0]["IN_TIME"] = String.Empty;
                rows[0]["OUT_FLAG"] = String.Empty;
                rows[0]["DURATION"] = String.Empty;
                rows[0]["OUT_PLAN_TIME"] = String.Empty;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
                return;
            }
        }
    }
}
