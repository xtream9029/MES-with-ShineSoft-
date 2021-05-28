using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace VirtualMES.Util
{
    public class ExcelUtil
    {
        private static string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                // 엑셀 2003 및 이하 버전
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
                //oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0\"";
            }
            else
            {
                // 엑셀 2007 버전 이상
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
                //oledbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0\"";
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }

        public static DataTable ReadExcelSheet(string FilePath)
        {
            DataTable dtResult = null;

            using (OleDbConnection conn = new OleDbConnection { ConnectionString = ConnectionString(FilePath, "No") })
            {
                conn.Open();
                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string sheetName = dt.Rows[0]["TABLE_NAME"].ToString(); // 엑셀 첫번째 시트명
                string sQuery = string.Format(" SELECT * FROM [{0}] ", sheetName); // 쿼리

                dtResult = new DataTable();

                using (OleDbDataAdapter adt = new OleDbDataAdapter(sQuery, conn))
                {
                    adt.Fill(dtResult);
                }
            }

            //if (dtResult.Rows.Count > 1)
            //{
            //    // remove header
            //    dtResult.Rows[0].Delete();
            //}

            //dtResult.AcceptChanges();

            return dtResult;
        }

        public static DataTable getExcelFile(string sFileName)
        {
            // Return Value
            DataTable dtResult = new DataTable();

            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;
            Excel._Worksheet xlWorksheet = null;
            Excel.Range xlRange = null;

            try
            {
                //Create COM Objects. Create a COM object for everything that is referenced
                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(sFileName);
                xlWorksheet = xlWorkbook.Sheets[1];
                xlRange = xlWorksheet.UsedRange;
                var usedRangeValue2 = xlWorksheet.UsedRange.Value2;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                // Definition Column
                for (int j = 1; j <= colCount; j++)
                {
                    var cell = usedRangeValue2[1, j];
                    if (cell != null)
                    {
                        DataColumn dc = new DataColumn(cell.ToString());
                        dtResult.Columns.Add(dc);
                    }
                }

                //iterate over the rows and columns and print to the console as it appears in the file
                //excel is not zero based!!
                for (int i = 2; i <= rowCount; i++)
                {
                    DataRow dr = dtResult.NewRow();

                    for (int j = 1; j <= dtResult.Columns.Count; j++)
                    {
                        //write the value to the console
                        var cell = usedRangeValue2[i, j];
                        if (cell != null)
                            dr[j - 1] = cell;
                    }

                    dtResult.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
            }
            finally
            {
                //cleanup
                if (xlApp != null)
                {
                    if (xlWorkbook != null)
                        xlWorkbook.Close(false, Type.Missing, Type.Missing);

                    xlApp.Application.Quit();
                }
                xlApp = null;
                xlWorkbook = null;
            }

            return dtResult;
        }

        public static ArrayList getExcelSheetList(string sFileName)
        {
            // Return Value
            ArrayList arrResult = new ArrayList();

            Excel.Application xlApp = null;
            Excel.Workbook xlWorkbook = null;

            try
            {
                //Create COM Objects. Create a COM object for everything that is referenced
                xlApp = new Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(sFileName);

                foreach (Excel._Worksheet sheet in xlWorkbook.Sheets)
                {
                    arrResult.Add(sheet.Name);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.GetBaseException().ToString());
            }
            finally
            {
                if (xlApp != null)
                {

                    if (xlWorkbook != null)
                        xlWorkbook.Close(false, Type.Missing, Type.Missing);
                    xlApp.Application.Quit();
                }
                xlApp = null;
                xlWorkbook = null;
            }

            return arrResult;
        }
    }
}
