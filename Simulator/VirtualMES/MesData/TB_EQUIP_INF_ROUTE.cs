using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;

namespace VirtualMES.MesData
{
    public class TB_EQUIP_INF_ROUTE
    {
        public DataTable dtEquipInfRoute { get; set; }

        public TB_EQUIP_INF_ROUTE()
        {

        }

        public TB_EQUIP_INF_ROUTE(string fileName)
        {
            LoadConf(fileName);
        }

        private void LoadConf(string fileName)
        {
            dtEquipInfRoute = null;
            dtEquipInfRoute = VirtualMES.Util.ExcelUtil.getExcelFile(fileName);
        }
    }
}
