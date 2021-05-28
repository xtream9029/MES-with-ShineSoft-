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
    public partial class frmMES_Main : Form
    {
        private frmMES_S1F13 frmS1F13 = null;
        private frmMES_S1F101 frmS1F101 = null;
        private frmMES_S1F103 frmS1F103 = null;
        private frmMES_S1F105 frmS1F105 = null;
        private frmMES_S2F13 frmS2F13 = null;
        private frmMES_S2F15 frmS2F15 = null;
        //private frmMES_S2F41 frmS2F41 = null;
        private frmMES_S5F101 frmS5F101 = null;
        private frmMES_S6F12 frmS6F12 = null;

        public frmMES_Main()
        {//생성자
            InitializeComponent();
        }

        private void frmMES_Main_Load(object sender, EventArgs e)
        {
            // S1F13
            frmS1F13 = new frmMES_S1F13();
            frmS1F13.mdiFlag = false;
            frmS1F13.TopLevel = false;
            frmS1F13.FormBorderStyle = FormBorderStyle.None;
            frmS1F13.Dock = DockStyle.Fill;
            frmS1F13.Parent = tpS1F13;
            tpS1F13.Controls.Add(frmS1F13);
            frmS1F13.Show();

            // S1F101
            frmS1F101 = new frmMES_S1F101();
            frmS1F101.mdiFlag = false;
            frmS1F101.TopLevel = false;
            frmS1F101.FormBorderStyle = FormBorderStyle.None;
            frmS1F101.Dock = DockStyle.Fill;
            frmS1F101.Parent = tpS1F101;
            tpS1F101.Controls.Add(frmS1F101);
            frmS1F101.Show();

            // S1F103
            frmS1F103 = new frmMES_S1F103();
            frmS1F103.mdiFlag = false;
            frmS1F103.TopLevel = false;
            frmS1F103.FormBorderStyle = FormBorderStyle.None;
            frmS1F103.Dock = DockStyle.Fill;
            frmS1F103.Parent = tpS1F103;
            tpS1F103.Controls.Add(frmS1F103);
            frmS1F103.Show();

            // S1F105
            frmS1F105 = new frmMES_S1F105();
            frmS1F105.mdiFlag = false;
            frmS1F105.TopLevel = false;
            frmS1F105.FormBorderStyle = FormBorderStyle.None;
            frmS1F105.Dock = DockStyle.Fill;
            frmS1F105.Parent = tpS1F105;
            tpS1F105.Controls.Add(frmS1F105);
            frmS1F105.Show();

            // S2F13
            frmS2F13 = new frmMES_S2F13();
            frmS2F13.mdiFlag = false;
            frmS2F13.TopLevel = false;
            frmS2F13.FormBorderStyle = FormBorderStyle.None;
            frmS2F13.Dock = DockStyle.Fill;
            frmS2F13.Parent = tpS2F13;
            tpS2F13.Controls.Add(frmS2F13);
            frmS2F13.Show();

            // S2F15
            frmS2F15 = new frmMES_S2F15();
            frmS2F15.mdiFlag = false;
            frmS2F15.TopLevel = false;
            frmS2F15.FormBorderStyle = FormBorderStyle.None;
            frmS2F15.Dock = DockStyle.Fill;
            frmS2F15.Parent = tpS2F15;
            tpS2F15.Controls.Add(frmS2F15);
            frmS2F15.Show();

            // S5F101
            frmS5F101 = new frmMES_S5F101();
            frmS5F101.mdiFlag = false;
            frmS5F101.TopLevel = false;
            frmS5F101.FormBorderStyle = FormBorderStyle.None;
            frmS5F101.Dock = DockStyle.Fill;
            frmS5F101.Parent = tpS5F101;
            tpS5F101.Controls.Add(frmS5F101);
            frmS5F101.Show();

            // S6F12
            frmS6F12 = new frmMES_S6F12();
            frmS6F12.mdiFlag = false;
            frmS6F12.TopLevel = false;
            frmS6F12.FormBorderStyle = FormBorderStyle.None;
            frmS6F12.Dock = DockStyle.Fill;
            frmS6F12.Parent = tpS6F12;
            tpS6F12.Controls.Add(frmS6F12);
            frmS6F12.Show();

            //frmMain.ReplayReceived += new frmMain.OnReplyEventHandler(OnSECSReceived);
            //frmMain.S6F11Received += new frmMain.OnS6F11EventHandler(OnSECSReceived);
        }

        // ***************************************************************************
        // OnMessage Event
        // ***************************************************************************
        private void OnSECSReceived(SEComData sd)
        {
            //try
            //{
            //    if (sd.HeaderItems.Stream == 1 && sd.HeaderItems.Function == 14)
            //        this.tabControl1.SelectedTab = tpS1F13;
            //    else if (sd.HeaderItems.Stream == 1 && sd.HeaderItems.Function == 102)
            //        this.tabControl1.SelectedTab = tpS1F101;
            //    else if (sd.HeaderItems.Stream == 1 && sd.HeaderItems.Function == 104)
            //        this.tabControl1.SelectedTab = tpS1F103;
            //    else if (sd.HeaderItems.Stream == 1 && sd.HeaderItems.Function == 106)
            //        this.tabControl1.SelectedTab = tpS1F105;
            //    else if (sd.HeaderItems.Stream == 2 && sd.HeaderItems.Function == 14)
            //        this.tabControl1.SelectedTab = tpS2F13;
            //    else if (sd.HeaderItems.Stream == 2 && sd.HeaderItems.Function == 16)
            //        this.tabControl1.SelectedTab = tpS2F15;
            //    else if (sd.HeaderItems.Stream == 2 && sd.HeaderItems.Function == 42)
            //        this.tabControl1.SelectedTab = tpS2F41;
            //    else if (sd.HeaderItems.Stream == 5 && sd.HeaderItems.Function == 102)
            //        this.tabControl1.SelectedTab = tpS5F101;
            //    else if (sd.HeaderItems.Stream == 6 && sd.HeaderItems.Function == 11)
            //        this.tabControl1.SelectedTab = tpS6F12;
            //}
            //catch
            //{
            //    return;
            //}
        }

        private void frmMES_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // EventHandler Remove
            frmMain.ReplayReceived -= frmS1F13.OnSECSReceived;
            frmMain.ReplayReceived -= frmS1F101.OnSECSReceived;
            frmMain.ReplayReceived -= frmS1F103.OnSECSReceived;
            frmMain.ReplayReceived -= frmS1F105.OnSECSReceived;
            frmMain.ReplayReceived -= frmS2F13.OnSECSReceived;
            frmMain.ReplayReceived -= frmS2F15.OnSECSReceived;
            //frmMain.ReplayReceived -= frmS2F41.OnSECSReceived;
            frmMain.ReplayReceived -= frmS5F101.OnSECSReceived;
            //frmMain.S6F11Received -= frmS6F12.OnSECSReceived;
        }

        private void tpS6F12_Click(object sender, EventArgs e)
        {

        }
    }
}
