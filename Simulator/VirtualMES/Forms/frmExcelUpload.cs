using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace VirtualMES
{
    public partial class frmExcelUpload : Form
    {
        public frmExcelUpload()
        {
            InitializeComponent();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtExcelFileName.Text = openFileDialog.FileName;

                // Set Sheet List
                Cursor.Current = Cursors.WaitCursor;

                ArrayList arrSheetNames = VirtualMES.Util.ExcelUtil.getExcelSheetList(this.txtExcelFileName.Text.Trim());
                this.cboSheetName.Items.AddRange(arrSheetNames.ToArray());

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnReadExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (!String.IsNullOrEmpty(this.txtExcelFileName.Text))
            {
                DataTable dtExcel = VirtualMES.Util.ExcelUtil.getExcelFile(this.txtExcelFileName.Text.Trim());

                this.grdExcelUpload.DataSource = dtExcel;
            }

            Cursor.Current = Cursors.Default;
        }

        // Grid Row Hader Diaplyed
        private void grdExcelUpload_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.grdExcelUpload.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
