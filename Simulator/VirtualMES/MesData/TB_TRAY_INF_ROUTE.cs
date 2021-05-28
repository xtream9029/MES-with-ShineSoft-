using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;

namespace VirtualMES.MesData
{
    public class TB_TRAY_INF_ROUTE
    {
        public DataTable dtTrayInfRoute { get; set; }

        public TB_TRAY_INF_ROUTE()
        {

        }

        public TB_TRAY_INF_ROUTE(string fileName)
        {
            LoadConf(fileName);
        }

        private void LoadConf(string fileName)
        {
            dtTrayInfRoute = null;
            dtTrayInfRoute = VirtualMES.Util.ExcelUtil.getExcelFile(fileName);
        }
    }
}
