namespace VirtualMES
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendReplyMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mESFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.s1F13CRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s1F101ESSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s1F103STRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s1F105STRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.s2F13ECRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s2F15ECSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s5F101MARSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s6F12EventReportAckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grbTree = new System.Windows.Forms.GroupBox();
            this.tvwSECSMessage = new System.Windows.Forms.TreeView();
            this.grbLog = new System.Windows.Forms.GroupBox();
            this.rtxtSECS2Log = new System.Windows.Forms.RichTextBox();
            this.mnuPopupEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sendMessageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sendReplyMessageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.insertTransactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertChildItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.actionOpen = new System.Windows.Forms.ToolStripButton();
            this.actionSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.actionConnect = new System.Windows.Forms.ToolStripButton();
            this.actionDisconnect = new System.Windows.Forms.ToolStripButton();
            this.actionConfiguration = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.actionSendMessage = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grbTree.SuspendLayout();
            this.grbLog.SuspendLayout();
            this.mnuPopupEdit.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 981);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1445, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(400, 18);
            this.toolStripStatusLabel1.Text = "File Name : ";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(828, 18);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "HSMS Status : ";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(200, 18);
            this.toolStripStatusLabel3.Text = "DB Status : ";
            this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel3.Click += new System.EventHandler(this.toolStripStatusLabel3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.commandToolStripMenuItem,
            this.mESFormToolStripMenuItem,
            this.dataToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1445, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConnectionToolStripMenuItem,
            this.closeConnectionToolStripMenuItem,
            this.configurationConnectionToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.settingToolStripMenuItem.Text = "Settings";
            // 
            // openConnectionToolStripMenuItem
            // 
            this.openConnectionToolStripMenuItem.Name = "openConnectionToolStripMenuItem";
            this.openConnectionToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.openConnectionToolStripMenuItem.Text = "Open Connection";
            this.openConnectionToolStripMenuItem.Click += new System.EventHandler(this.openConnectionToolStripMenuItem_Click);
            // 
            // closeConnectionToolStripMenuItem
            // 
            this.closeConnectionToolStripMenuItem.Name = "closeConnectionToolStripMenuItem";
            this.closeConnectionToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.closeConnectionToolStripMenuItem.Text = "Close Connection";
            this.closeConnectionToolStripMenuItem.Click += new System.EventHandler(this.closeConnectionToolStripMenuItem_Click);
            // 
            // configurationConnectionToolStripMenuItem
            // 
            this.configurationConnectionToolStripMenuItem.Name = "configurationConnectionToolStripMenuItem";
            this.configurationConnectionToolStripMenuItem.Size = new System.Drawing.Size(271, 26);
            this.configurationConnectionToolStripMenuItem.Text = "Configuration Connection";
            this.configurationConnectionToolStripMenuItem.Click += new System.EventHandler(this.configurationConnectionToolStripMenuItem_Click);
            // 
            // commandToolStripMenuItem
            // 
            this.commandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendMessageToolStripMenuItem,
            this.sendReplyMessageToolStripMenuItem});
            this.commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            this.commandToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.commandToolStripMenuItem.Text = "Command";
            // 
            // sendMessageToolStripMenuItem
            // 
            this.sendMessageToolStripMenuItem.Name = "sendMessageToolStripMenuItem";
            this.sendMessageToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.sendMessageToolStripMenuItem.Text = "Send Message";
            this.sendMessageToolStripMenuItem.Click += new System.EventHandler(this.sendMessageToolStripMenuItem_Click);
            // 
            // sendReplyMessageToolStripMenuItem
            // 
            this.sendReplyMessageToolStripMenuItem.Name = "sendReplyMessageToolStripMenuItem";
            this.sendReplyMessageToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.sendReplyMessageToolStripMenuItem.Text = "Send Reply Message";
            this.sendReplyMessageToolStripMenuItem.Click += new System.EventHandler(this.sendReplyMessageToolStripMenuItem_Click);
            // 
            // mESFormToolStripMenuItem
            // 
            this.mESFormToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainFormToolStripMenuItem,
            this.toolStripSeparator3,
            this.s1F13CRToolStripMenuItem,
            this.s1F101ESSToolStripMenuItem,
            this.s1F103STRToolStripMenuItem,
            this.s1F105STRToolStripMenuItem,
            this.toolStripSeparator4,
            this.s2F13ECRToolStripMenuItem,
            this.s2F15ECSToolStripMenuItem,
            this.s5F101MARSToolStripMenuItem,
            this.s6F12EventReportAckToolStripMenuItem,
            this.toolStripSeparator5,
            this.toolStripSeparator6});
            this.mESFormToolStripMenuItem.Name = "mESFormToolStripMenuItem";
            this.mESFormToolStripMenuItem.Size = new System.Drawing.Size(92, 24);
            this.mESFormToolStripMenuItem.Text = "MES Form";
            // 
            // mainFormToolStripMenuItem
            // 
            this.mainFormToolStripMenuItem.Name = "mainFormToolStripMenuItem";
            this.mainFormToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.mainFormToolStripMenuItem.Text = "MainForm";
            this.mainFormToolStripMenuItem.Click += new System.EventHandler(this.mainFormToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(273, 6);
            // 
            // s1F13CRToolStripMenuItem
            // 
            this.s1F13CRToolStripMenuItem.Name = "s1F13CRToolStripMenuItem";
            this.s1F13CRToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.s1F13CRToolStripMenuItem.Text = "S1F13(CommunicationReq)";
            this.s1F13CRToolStripMenuItem.Click += new System.EventHandler(this.s1F13CRToolStripMenuItem_Click);
            // 
            // s1F101ESSToolStripMenuItem
            // 
            this.s1F101ESSToolStripMenuItem.Name = "s1F101ESSToolStripMenuItem";
            this.s1F101ESSToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.s1F101ESSToolStripMenuItem.Text = "S1F101(EqpStatusSend)";
            this.s1F101ESSToolStripMenuItem.Click += new System.EventHandler(this.s1F101ESSToolStripMenuItem_Click);
            // 
            // s1F103STRToolStripMenuItem
            // 
            this.s1F103STRToolStripMenuItem.Name = "s1F103STRToolStripMenuItem";
            this.s1F103STRToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.s1F103STRToolStripMenuItem.Text = "S1F103(TrackStatusReq)";
            this.s1F103STRToolStripMenuItem.Click += new System.EventHandler(this.s1F103STRToolStripMenuItem_Click);
            // 
            // s1F105STRToolStripMenuItem
            // 
            this.s1F105STRToolStripMenuItem.Name = "s1F105STRToolStripMenuItem";
            this.s1F105STRToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.s1F105STRToolStripMenuItem.Text = "S1F105(TrayStatusReq)";
            this.s1F105STRToolStripMenuItem.Click += new System.EventHandler(this.s1F105STRToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(273, 6);
            // 
            // s2F13ECRToolStripMenuItem
            // 
            this.s2F13ECRToolStripMenuItem.Name = "s2F13ECRToolStripMenuItem";
            this.s2F13ECRToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.s2F13ECRToolStripMenuItem.Text = "S2F13(EqpConstReq)";
            this.s2F13ECRToolStripMenuItem.Click += new System.EventHandler(this.s2F13ECRToolStripMenuItem_Click);
            // 
            // s2F15ECSToolStripMenuItem
            // 
            this.s2F15ECSToolStripMenuItem.Name = "s2F15ECSToolStripMenuItem";
            this.s2F15ECSToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.s2F15ECSToolStripMenuItem.Text = "S2F15(EqpConstSend)";
            this.s2F15ECSToolStripMenuItem.Click += new System.EventHandler(this.s2F15ECSToolStripMenuItem_Click);
            // 
            // s5F101MARSToolStripMenuItem
            // 
            this.s5F101MARSToolStripMenuItem.Name = "s5F101MARSToolStripMenuItem";
            this.s5F101MARSToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.s5F101MARSToolStripMenuItem.Text = "S5F101(MESAlarmReport)";
            this.s5F101MARSToolStripMenuItem.Click += new System.EventHandler(this.s5F101MARSToolStripMenuItem_Click);
            // 
            // s6F12EventReportAckToolStripMenuItem
            // 
            this.s6F12EventReportAckToolStripMenuItem.Name = "s6F12EventReportAckToolStripMenuItem";
            this.s6F12EventReportAckToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.s6F12EventReportAckToolStripMenuItem.Text = "S6F12(EventReportAck)";
            this.s6F12EventReportAckToolStripMenuItem.Click += new System.EventHandler(this.s6F12EventReportAckToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(273, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(273, 6);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelUploadToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // excelUploadToolStripMenuItem
            // 
            this.excelUploadToolStripMenuItem.Name = "excelUploadToolStripMenuItem";
            this.excelUploadToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.excelUploadToolStripMenuItem.Text = "Excel Upload";
            this.excelUploadToolStripMenuItem.Click += new System.EventHandler(this.excelUploadToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbLog);
            this.splitContainer1.Size = new System.Drawing.Size(1445, 926);
            this.splitContainer1.SplitterDistance = 481;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // grbTree
            // 
            this.grbTree.Controls.Add(this.tvwSECSMessage);
            this.grbTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbTree.Location = new System.Drawing.Point(0, 0);
            this.grbTree.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbTree.Name = "grbTree";
            this.grbTree.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbTree.Size = new System.Drawing.Size(481, 926);
            this.grbTree.TabIndex = 0;
            this.grbTree.TabStop = false;
            this.grbTree.Text = "Message";
            // 
            // tvwSECSMessage
            // 
            this.tvwSECSMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwSECSMessage.Location = new System.Drawing.Point(3, 22);
            this.tvwSECSMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvwSECSMessage.Name = "tvwSECSMessage";
            this.tvwSECSMessage.Size = new System.Drawing.Size(475, 900);
            this.tvwSECSMessage.TabIndex = 0;
            this.tvwSECSMessage.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSECSMessage_AfterSelect);
            this.tvwSECSMessage.Click += new System.EventHandler(this.tvwSECSMessage_Click);
            this.tvwSECSMessage.DoubleClick += new System.EventHandler(this.tvwSECSMessage_DoubleClick);
            this.tvwSECSMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvwSECSMessage_KeyDown);
            this.tvwSECSMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwSECSMessage_MouseDown);
            this.tvwSECSMessage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwSECSMessage_MouseUp);
            // 
            // grbLog
            // 
            this.grbLog.Controls.Add(this.rtxtSECS2Log);
            this.grbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbLog.Location = new System.Drawing.Point(0, 0);
            this.grbLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbLog.Name = "grbLog";
            this.grbLog.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grbLog.Size = new System.Drawing.Size(959, 926);
            this.grbLog.TabIndex = 0;
            this.grbLog.TabStop = false;
            this.grbLog.Text = "Log";
            // 
            // rtxtSECS2Log
            // 
            this.rtxtSECS2Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtSECS2Log.Location = new System.Drawing.Point(3, 22);
            this.rtxtSECS2Log.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtxtSECS2Log.Name = "rtxtSECS2Log";
            this.rtxtSECS2Log.Size = new System.Drawing.Size(953, 900);
            this.rtxtSECS2Log.TabIndex = 0;
            this.rtxtSECS2Log.Text = "";
            this.rtxtSECS2Log.TextChanged += new System.EventHandler(this.rtxtSECS2Log_TextChanged);
            this.rtxtSECS2Log.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtxtSECS2Log_MouseDown);
            // 
            // mnuPopupEdit
            // 
            this.mnuPopupEdit.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuPopupEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1,
            this.sendMessageToolStripMenuItem1,
            this.sendReplyMessageToolStripMenuItem1,
            this.insertTransactionToolStripMenuItem,
            this.insertChildItemToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.mnuPopupEdit.Name = "mnuPopupEdit";
            this.mnuPopupEdit.Size = new System.Drawing.Size(219, 148);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(218, 24);
            this.editToolStripMenuItem1.Text = "Edit SECS Message";
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.editToolStripMenuItem1_Click);
            // 
            // sendMessageToolStripMenuItem1
            // 
            this.sendMessageToolStripMenuItem1.Name = "sendMessageToolStripMenuItem1";
            this.sendMessageToolStripMenuItem1.Size = new System.Drawing.Size(218, 24);
            this.sendMessageToolStripMenuItem1.Text = "Send Message";
            this.sendMessageToolStripMenuItem1.Click += new System.EventHandler(this.sendMessageToolStripMenuItem1_Click);
            // 
            // sendReplyMessageToolStripMenuItem1
            // 
            this.sendReplyMessageToolStripMenuItem1.Name = "sendReplyMessageToolStripMenuItem1";
            this.sendReplyMessageToolStripMenuItem1.Size = new System.Drawing.Size(218, 24);
            this.sendReplyMessageToolStripMenuItem1.Text = "Send Reply Message";
            this.sendReplyMessageToolStripMenuItem1.Click += new System.EventHandler(this.sendReplyMessageToolStripMenuItem1_Click);
            // 
            // insertTransactionToolStripMenuItem
            // 
            this.insertTransactionToolStripMenuItem.Name = "insertTransactionToolStripMenuItem";
            this.insertTransactionToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.insertTransactionToolStripMenuItem.Text = "Insert Transaction";
            this.insertTransactionToolStripMenuItem.Click += new System.EventHandler(this.insertTransactionToolStripMenuItem_Click);
            // 
            // insertChildItemToolStripMenuItem
            // 
            this.insertChildItemToolStripMenuItem.Name = "insertChildItemToolStripMenuItem";
            this.insertChildItemToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.insertChildItemToolStripMenuItem.Text = "Insert Child Item";
            this.insertChildItemToolStripMenuItem.Click += new System.EventHandler(this.insertChildItemToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionOpen,
            this.actionSave,
            this.toolStripSeparator1,
            this.actionConnect,
            this.actionDisconnect,
            this.actionConfiguration,
            this.toolStripSeparator2,
            this.actionSendMessage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1445, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // actionOpen
            // 
            this.actionOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionOpen.Image = ((System.Drawing.Image)(resources.GetObject("actionOpen.Image")));
            this.actionOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionOpen.Name = "actionOpen";
            this.actionOpen.Size = new System.Drawing.Size(29, 24);
            this.actionOpen.Text = "Open";
            this.actionOpen.ToolTipText = "Open";
            this.actionOpen.Click += new System.EventHandler(this.actionOpen_Click);
            // 
            // actionSave
            // 
            this.actionSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionSave.Image = ((System.Drawing.Image)(resources.GetObject("actionSave.Image")));
            this.actionSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionSave.Name = "actionSave";
            this.actionSave.Size = new System.Drawing.Size(29, 24);
            this.actionSave.Text = "Save";
            this.actionSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.actionSave.Click += new System.EventHandler(this.actionSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // actionConnect
            // 
            this.actionConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionConnect.Image = ((System.Drawing.Image)(resources.GetObject("actionConnect.Image")));
            this.actionConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionConnect.Name = "actionConnect";
            this.actionConnect.Size = new System.Drawing.Size(29, 24);
            this.actionConnect.Text = "Open Connection";
            this.actionConnect.Click += new System.EventHandler(this.actionConnect_Click);
            // 
            // actionDisconnect
            // 
            this.actionDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("actionDisconnect.Image")));
            this.actionDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionDisconnect.Name = "actionDisconnect";
            this.actionDisconnect.Size = new System.Drawing.Size(29, 24);
            this.actionDisconnect.Text = "Close Connection";
            this.actionDisconnect.Click += new System.EventHandler(this.actionDisconnect_Click);
            // 
            // actionConfiguration
            // 
            this.actionConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionConfiguration.Image = ((System.Drawing.Image)(resources.GetObject("actionConfiguration.Image")));
            this.actionConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionConfiguration.Name = "actionConfiguration";
            this.actionConfiguration.Size = new System.Drawing.Size(29, 24);
            this.actionConfiguration.Text = "Configuration Connection";
            this.actionConfiguration.Click += new System.EventHandler(this.actionConfiguration_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // actionSendMessage
            // 
            this.actionSendMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.actionSendMessage.Image = ((System.Drawing.Image)(resources.GetObject("actionSendMessage.Image")));
            this.actionSendMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.actionSendMessage.Name = "actionSendMessage";
            this.actionSendMessage.Size = new System.Drawing.Size(29, 24);
            this.actionSendMessage.Text = "Send Message";
            this.actionSendMessage.Click += new System.EventHandler(this.actionSendMessage_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1445, 1005);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.Text = "VitualMES_EQP";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grbTree.ResumeLayout(false);
            this.grbLog.ResumeLayout(false);
            this.mnuPopupEdit.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationConnectionToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grbTree;
        private System.Windows.Forms.GroupBox grbLog;
        private System.Windows.Forms.RichTextBox rtxtSECS2Log;
        private System.Windows.Forms.TreeView tvwSECSMessage;
        private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendReplyMessageToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnuPopupEdit;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sendMessageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sendReplyMessageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem insertTransactionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertChildItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mESFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s5F101MARSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s1F13CRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s2F13ECRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s2F15ECSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s1F101ESSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s1F103STRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s1F105STRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem s6F12EventReportAckToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem mainFormToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton actionSave;
        private System.Windows.Forms.ToolStripButton actionOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton actionConnect;
        private System.Windows.Forms.ToolStripButton actionDisconnect;
        private System.Windows.Forms.ToolStripButton actionConfiguration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton actionSendMessage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    }
}

