namespace VirtualMES
{
    partial class frmConfigure
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabConfigure = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtDeviceId = new System.Windows.Forms.TextBox();
            this.cboProtocolType = new System.Windows.Forms.ComboBox();
            this.cboIdentity = new System.Windows.Forms.ComboBox();
            this.cboSEComID = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageHSMS = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cboConnectMode = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblmsec = new System.Windows.Forms.Label();
            this.txtRemoteIP4 = new System.Windows.Forms.TextBox();
            this.txtRemoteIP3 = new System.Windows.Forms.TextBox();
            this.txtRemoteIP2 = new System.Windows.Forms.TextBox();
            this.txtRemoteIP1 = new System.Windows.Forms.TextBox();
            this.txtLinkTestInterval = new System.Windows.Forms.TextBox();
            this.lblLinkTestInterval = new System.Windows.Forms.Label();
            this.txtLocalPort = new System.Windows.Forms.TextBox();
            this.lblLocalPort = new System.Windows.Forms.Label();
            this.txtRemotePort = new System.Windows.Forms.TextBox();
            this.lblRemotePort = new System.Windows.Forms.Label();
            this.lblRemoteIP = new System.Windows.Forms.Label();
            this.txtT8 = new System.Windows.Forms.TextBox();
            this.lblT8 = new System.Windows.Forms.Label();
            this.txtT7 = new System.Windows.Forms.TextBox();
            this.lblT7 = new System.Windows.Forms.Label();
            this.txtT6 = new System.Windows.Forms.TextBox();
            this.lblT6 = new System.Windows.Forms.Label();
            this.txtT5 = new System.Windows.Forms.TextBox();
            this.lblT5 = new System.Windows.Forms.Label();
            this.txtT3 = new System.Windows.Forms.TextBox();
            this.lblT3 = new System.Windows.Forms.Label();
            this.lblConnectMode = new System.Windows.Forms.Label();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.cboLogBackup = new System.Windows.Forms.ComboBox();
            this.cboSECS2LogMode = new System.Windows.Forms.ComboBox();
            this.cboSEComLogLevel = new System.Windows.Forms.ComboBox();
            this.cboSECS1LogMode = new System.Windows.Forms.ComboBox();
            this.cboSEComLogMode = new System.Windows.Forms.ComboBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtLogDir = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblLogBackup = new System.Windows.Forms.Label();
            this.lblSEComLogMode = new System.Windows.Forms.Label();
            this.lblLogLevel = new System.Windows.Forms.Label();
            this.lblSECS2LogMode = new System.Windows.Forms.Label();
            this.lblSECS1LogMode = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabConfigure.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPageHSMS.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 394);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 56);
            this.panel1.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Location = new System.Drawing.Point(394, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 56);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Location = new System.Drawing.Point(480, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 56);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabConfigure
            // 
            this.tabConfigure.Controls.Add(this.tabPageGeneral);
            this.tabConfigure.Controls.Add(this.tabPageHSMS);
            this.tabConfigure.Controls.Add(this.tabPageLog);
            this.tabConfigure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabConfigure.Location = new System.Drawing.Point(0, 0);
            this.tabConfigure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabConfigure.Name = "tabConfigure";
            this.tabConfigure.SelectedIndex = 0;
            this.tabConfigure.Size = new System.Drawing.Size(566, 394);
            this.tabConfigure.TabIndex = 2;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.panel2);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageGeneral.Size = new System.Drawing.Size(558, 365);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtDeviceId);
            this.panel2.Controls.Add(this.cboProtocolType);
            this.panel2.Controls.Add(this.cboIdentity);
            this.panel2.Controls.Add(this.cboSEComID);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(552, 357);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // txtDeviceId
            // 
            this.txtDeviceId.Location = new System.Drawing.Point(154, 118);
            this.txtDeviceId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDeviceId.Name = "txtDeviceId";
            this.txtDeviceId.Size = new System.Drawing.Size(139, 25);
            this.txtDeviceId.TabIndex = 8;
            this.txtDeviceId.TextChanged += new System.EventHandler(this.txtDeviceId_TextChanged);
            // 
            // cboProtocolType
            // 
            this.cboProtocolType.FormattingEnabled = true;
            this.cboProtocolType.Items.AddRange(new object[] {
            "HSMS"});
            this.cboProtocolType.Location = new System.Drawing.Point(155, 161);
            this.cboProtocolType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboProtocolType.Name = "cboProtocolType";
            this.cboProtocolType.Size = new System.Drawing.Size(138, 23);
            this.cboProtocolType.TabIndex = 7;
            this.cboProtocolType.SelectedIndexChanged += new System.EventHandler(this.cboProtocolType_SelectedIndexChanged);
            // 
            // cboIdentity
            // 
            this.cboIdentity.FormattingEnabled = true;
            this.cboIdentity.Items.AddRange(new object[] {
            "EQP",
            "Host"});
            this.cboIdentity.Location = new System.Drawing.Point(155, 74);
            this.cboIdentity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboIdentity.Name = "cboIdentity";
            this.cboIdentity.Size = new System.Drawing.Size(138, 23);
            this.cboIdentity.TabIndex = 5;
            this.cboIdentity.SelectedIndexChanged += new System.EventHandler(this.cboIdentity_SelectedIndexChanged);
            // 
            // cboSEComID
            // 
            this.cboSEComID.FormattingEnabled = true;
            this.cboSEComID.Location = new System.Drawing.Point(155, 25);
            this.cboSEComID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSEComID.Name = "cboSEComID";
            this.cboSEComID.Size = new System.Drawing.Size(138, 23);
            this.cboSEComID.TabIndex = 4;
            this.cboSEComID.SelectedIndexChanged += new System.EventHandler(this.cboSEComID_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Protocol Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Device ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "EQP or HOST";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "SEComID";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabPageHSMS
            // 
            this.tabPageHSMS.Controls.Add(this.panel3);
            this.tabPageHSMS.Location = new System.Drawing.Point(4, 25);
            this.tabPageHSMS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageHSMS.Name = "tabPageHSMS";
            this.tabPageHSMS.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageHSMS.Size = new System.Drawing.Size(558, 365);
            this.tabPageHSMS.TabIndex = 1;
            this.tabPageHSMS.Text = "HSMS";
            this.tabPageHSMS.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cboConnectMode);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.lblmsec);
            this.panel3.Controls.Add(this.txtRemoteIP4);
            this.panel3.Controls.Add(this.txtRemoteIP3);
            this.panel3.Controls.Add(this.txtRemoteIP2);
            this.panel3.Controls.Add(this.txtRemoteIP1);
            this.panel3.Controls.Add(this.txtLinkTestInterval);
            this.panel3.Controls.Add(this.lblLinkTestInterval);
            this.panel3.Controls.Add(this.txtLocalPort);
            this.panel3.Controls.Add(this.lblLocalPort);
            this.panel3.Controls.Add(this.txtRemotePort);
            this.panel3.Controls.Add(this.lblRemotePort);
            this.panel3.Controls.Add(this.lblRemoteIP);
            this.panel3.Controls.Add(this.txtT8);
            this.panel3.Controls.Add(this.lblT8);
            this.panel3.Controls.Add(this.txtT7);
            this.panel3.Controls.Add(this.lblT7);
            this.panel3.Controls.Add(this.txtT6);
            this.panel3.Controls.Add(this.lblT6);
            this.panel3.Controls.Add(this.txtT5);
            this.panel3.Controls.Add(this.lblT5);
            this.panel3.Controls.Add(this.txtT3);
            this.panel3.Controls.Add(this.lblT3);
            this.panel3.Controls.Add(this.lblConnectMode);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(552, 357);
            this.panel3.TabIndex = 0;
            // 
            // cboConnectMode
            // 
            this.cboConnectMode.FormattingEnabled = true;
            this.cboConnectMode.Items.AddRange(new object[] {
            "Active",
            "Passive"});
            this.cboConnectMode.Location = new System.Drawing.Point(375, 79);
            this.cboConnectMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboConnectMode.Name = "cboConnectMode";
            this.cboConnectMode.Size = new System.Drawing.Size(107, 23);
            this.cboConnectMode.TabIndex = 76;
            this.cboConnectMode.SelectedIndexChanged += new System.EventHandler(this.cboConnectMode_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(433, 251);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 20);
            this.label10.TabIndex = 75;
            this.label10.Text = "(m.sec)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(207, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 26);
            this.label5.TabIndex = 74;
            this.label5.Text = "(sec)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(207, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 26);
            this.label6.TabIndex = 73;
            this.label6.Text = "(sec)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(207, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 26);
            this.label7.TabIndex = 72;
            this.label7.Text = "(sec)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(207, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 26);
            this.label8.TabIndex = 71;
            this.label8.Text = "(sec)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblmsec
            // 
            this.lblmsec.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblmsec.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblmsec.Location = new System.Drawing.Point(207, 79);
            this.lblmsec.Name = "lblmsec";
            this.lblmsec.Size = new System.Drawing.Size(48, 26);
            this.lblmsec.TabIndex = 70;
            this.lblmsec.Text = "(sec)";
            this.lblmsec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRemoteIP4
            // 
            this.txtRemoteIP4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteIP4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtRemoteIP4.Location = new System.Drawing.Point(457, 121);
            this.txtRemoteIP4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemoteIP4.MaxLength = 3;
            this.txtRemoteIP4.Name = "txtRemoteIP4";
            this.txtRemoteIP4.Size = new System.Drawing.Size(25, 24);
            this.txtRemoteIP4.TabIndex = 57;
            // 
            // txtRemoteIP3
            // 
            this.txtRemoteIP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteIP3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtRemoteIP3.Location = new System.Drawing.Point(430, 121);
            this.txtRemoteIP3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemoteIP3.MaxLength = 3;
            this.txtRemoteIP3.Name = "txtRemoteIP3";
            this.txtRemoteIP3.Size = new System.Drawing.Size(25, 24);
            this.txtRemoteIP3.TabIndex = 56;
            // 
            // txtRemoteIP2
            // 
            this.txtRemoteIP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteIP2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtRemoteIP2.Location = new System.Drawing.Point(402, 121);
            this.txtRemoteIP2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemoteIP2.MaxLength = 3;
            this.txtRemoteIP2.Name = "txtRemoteIP2";
            this.txtRemoteIP2.Size = new System.Drawing.Size(25, 24);
            this.txtRemoteIP2.TabIndex = 55;
            // 
            // txtRemoteIP1
            // 
            this.txtRemoteIP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteIP1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtRemoteIP1.Location = new System.Drawing.Point(375, 121);
            this.txtRemoteIP1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemoteIP1.MaxLength = 3;
            this.txtRemoteIP1.Name = "txtRemoteIP1";
            this.txtRemoteIP1.Size = new System.Drawing.Size(25, 24);
            this.txtRemoteIP1.TabIndex = 54;
            this.txtRemoteIP1.TextChanged += new System.EventHandler(this.txtRemoteIP1_TextChanged);
            // 
            // txtLinkTestInterval
            // 
            this.txtLinkTestInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLinkTestInterval.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtLinkTestInterval.Location = new System.Drawing.Point(375, 249);
            this.txtLinkTestInterval.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLinkTestInterval.MaxLength = 9;
            this.txtLinkTestInterval.Name = "txtLinkTestInterval";
            this.txtLinkTestInterval.Size = new System.Drawing.Size(52, 24);
            this.txtLinkTestInterval.TabIndex = 61;
            this.txtLinkTestInterval.TextChanged += new System.EventHandler(this.txtLinkTestInterval_TextChanged);
            // 
            // lblLinkTestInterval
            // 
            this.lblLinkTestInterval.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblLinkTestInterval.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLinkTestInterval.Location = new System.Drawing.Point(274, 249);
            this.lblLinkTestInterval.Name = "lblLinkTestInterval";
            this.lblLinkTestInterval.Size = new System.Drawing.Size(82, 26);
            this.lblLinkTestInterval.TabIndex = 69;
            this.lblLinkTestInterval.Text = "Linktest Time";
            this.lblLinkTestInterval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocalPort.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtLocalPort.Location = new System.Drawing.Point(375, 206);
            this.txtLocalPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLocalPort.MaxLength = 5;
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(107, 24);
            this.txtLocalPort.TabIndex = 60;
            // 
            // lblLocalPort
            // 
            this.lblLocalPort.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblLocalPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLocalPort.Location = new System.Drawing.Point(274, 206);
            this.lblLocalPort.Name = "lblLocalPort";
            this.lblLocalPort.Size = new System.Drawing.Size(64, 26);
            this.lblLocalPort.TabIndex = 68;
            this.lblLocalPort.Text = "Local Port";
            this.lblLocalPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRemotePort
            // 
            this.txtRemotePort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemotePort.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtRemotePort.Location = new System.Drawing.Point(375, 164);
            this.txtRemotePort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemotePort.MaxLength = 5;
            this.txtRemotePort.Name = "txtRemotePort";
            this.txtRemotePort.Size = new System.Drawing.Size(107, 24);
            this.txtRemotePort.TabIndex = 59;
            // 
            // lblRemotePort
            // 
            this.lblRemotePort.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblRemotePort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRemotePort.Location = new System.Drawing.Point(274, 164);
            this.lblRemotePort.Name = "lblRemotePort";
            this.lblRemotePort.Size = new System.Drawing.Size(82, 26);
            this.lblRemotePort.TabIndex = 67;
            this.lblRemotePort.Text = "Remote Port";
            this.lblRemotePort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRemoteIP
            // 
            this.lblRemoteIP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblRemoteIP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRemoteIP.Location = new System.Drawing.Point(274, 121);
            this.lblRemoteIP.Name = "lblRemoteIP";
            this.lblRemoteIP.Size = new System.Drawing.Size(73, 26);
            this.lblRemoteIP.TabIndex = 66;
            this.lblRemoteIP.Text = "Remote IP";
            this.lblRemoteIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtT8
            // 
            this.txtT8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtT8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtT8.Location = new System.Drawing.Point(152, 249);
            this.txtT8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtT8.MaxLength = 5;
            this.txtT8.Name = "txtT8";
            this.txtT8.Size = new System.Drawing.Size(52, 24);
            this.txtT8.TabIndex = 53;
            // 
            // lblT8
            // 
            this.lblT8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblT8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblT8.Location = new System.Drawing.Point(66, 249);
            this.lblT8.Name = "lblT8";
            this.lblT8.Size = new System.Drawing.Size(80, 26);
            this.lblT8.TabIndex = 65;
            this.lblT8.Text = "T8 Time Out";
            this.lblT8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtT7
            // 
            this.txtT7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtT7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtT7.Location = new System.Drawing.Point(152, 206);
            this.txtT7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtT7.MaxLength = 5;
            this.txtT7.Name = "txtT7";
            this.txtT7.Size = new System.Drawing.Size(52, 24);
            this.txtT7.TabIndex = 52;
            // 
            // lblT7
            // 
            this.lblT7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblT7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblT7.Location = new System.Drawing.Point(66, 206);
            this.lblT7.Name = "lblT7";
            this.lblT7.Size = new System.Drawing.Size(80, 26);
            this.lblT7.TabIndex = 64;
            this.lblT7.Text = "T7 Time Out";
            this.lblT7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtT6
            // 
            this.txtT6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtT6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtT6.Location = new System.Drawing.Point(152, 164);
            this.txtT6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtT6.MaxLength = 5;
            this.txtT6.Name = "txtT6";
            this.txtT6.Size = new System.Drawing.Size(52, 24);
            this.txtT6.TabIndex = 51;
            // 
            // lblT6
            // 
            this.lblT6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblT6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblT6.Location = new System.Drawing.Point(66, 164);
            this.lblT6.Name = "lblT6";
            this.lblT6.Size = new System.Drawing.Size(80, 26);
            this.lblT6.TabIndex = 62;
            this.lblT6.Text = "T6 Time Out";
            this.lblT6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtT5
            // 
            this.txtT5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtT5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtT5.Location = new System.Drawing.Point(152, 121);
            this.txtT5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtT5.MaxLength = 5;
            this.txtT5.Name = "txtT5";
            this.txtT5.Size = new System.Drawing.Size(52, 24);
            this.txtT5.TabIndex = 49;
            // 
            // lblT5
            // 
            this.lblT5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblT5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblT5.Location = new System.Drawing.Point(66, 121);
            this.lblT5.Name = "lblT5";
            this.lblT5.Size = new System.Drawing.Size(80, 26);
            this.lblT5.TabIndex = 58;
            this.lblT5.Text = "T5 Time Out";
            this.lblT5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtT3
            // 
            this.txtT3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtT3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtT3.Location = new System.Drawing.Point(152, 79);
            this.txtT3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtT3.MaxLength = 5;
            this.txtT3.Name = "txtT3";
            this.txtT3.Size = new System.Drawing.Size(52, 24);
            this.txtT3.TabIndex = 48;
            this.txtT3.TextChanged += new System.EventHandler(this.txtT3_TextChanged);
            // 
            // lblT3
            // 
            this.lblT3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblT3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblT3.Location = new System.Drawing.Point(66, 79);
            this.lblT3.Name = "lblT3";
            this.lblT3.Size = new System.Drawing.Size(80, 26);
            this.lblT3.TabIndex = 63;
            this.lblT3.Text = "T3 Time Out";
            this.lblT3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblConnectMode
            // 
            this.lblConnectMode.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblConnectMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblConnectMode.Location = new System.Drawing.Point(274, 79);
            this.lblConnectMode.Name = "lblConnectMode";
            this.lblConnectMode.Size = new System.Drawing.Size(91, 26);
            this.lblConnectMode.TabIndex = 50;
            this.lblConnectMode.Text = "Connect Mode";
            this.lblConnectMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.cboLogBackup);
            this.tabPageLog.Controls.Add(this.cboSECS2LogMode);
            this.tabPageLog.Controls.Add(this.cboSEComLogLevel);
            this.tabPageLog.Controls.Add(this.cboSECS1LogMode);
            this.tabPageLog.Controls.Add(this.cboSEComLogMode);
            this.tabPageLog.Controls.Add(this.btnBrowse);
            this.tabPageLog.Controls.Add(this.txtLogDir);
            this.tabPageLog.Controls.Add(this.label9);
            this.tabPageLog.Controls.Add(this.lblLogBackup);
            this.tabPageLog.Controls.Add(this.lblSEComLogMode);
            this.tabPageLog.Controls.Add(this.lblLogLevel);
            this.tabPageLog.Controls.Add(this.lblSECS2LogMode);
            this.tabPageLog.Controls.Add(this.lblSECS1LogMode);
            this.tabPageLog.Location = new System.Drawing.Point(4, 25);
            this.tabPageLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(558, 365);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // cboLogBackup
            // 
            this.cboLogBackup.FormattingEnabled = true;
            this.cboLogBackup.Items.AddRange(new object[] {
            "1 Day",
            "2 Days",
            "3 Days",
            "4 Days",
            "5 Days",
            "6 Days",
            "1 Week",
            "2 Weeks",
            "3 Weeks",
            "4 Weeks"});
            this.cboLogBackup.Location = new System.Drawing.Point(150, 212);
            this.cboLogBackup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboLogBackup.Name = "cboLogBackup";
            this.cboLogBackup.Size = new System.Drawing.Size(87, 23);
            this.cboLogBackup.TabIndex = 52;
            this.cboLogBackup.SelectedIndexChanged += new System.EventHandler(this.cboLogBackup_SelectedIndexChanged);
            // 
            // cboSECS2LogMode
            // 
            this.cboSECS2LogMode.FormattingEnabled = true;
            this.cboSECS2LogMode.Items.AddRange(new object[] {
            "Hour",
            "Day",
            "None"});
            this.cboSECS2LogMode.Location = new System.Drawing.Point(362, 154);
            this.cboSECS2LogMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSECS2LogMode.Name = "cboSECS2LogMode";
            this.cboSECS2LogMode.Size = new System.Drawing.Size(87, 23);
            this.cboSECS2LogMode.TabIndex = 51;
            this.cboSECS2LogMode.SelectedIndexChanged += new System.EventHandler(this.cboSECS2LogMode_SelectedIndexChanged);
            // 
            // cboSEComLogLevel
            // 
            this.cboSEComLogLevel.FormattingEnabled = true;
            this.cboSEComLogLevel.Items.AddRange(new object[] {
            "Level 1",
            "Level 2",
            "Level 3",
            "Level 4",
            "Level 5"});
            this.cboSEComLogLevel.Location = new System.Drawing.Point(150, 154);
            this.cboSEComLogLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSEComLogLevel.Name = "cboSEComLogLevel";
            this.cboSEComLogLevel.Size = new System.Drawing.Size(87, 23);
            this.cboSEComLogLevel.TabIndex = 50;
            // 
            // cboSECS1LogMode
            // 
            this.cboSECS1LogMode.FormattingEnabled = true;
            this.cboSECS1LogMode.Items.AddRange(new object[] {
            "Hour",
            "Day",
            "None"});
            this.cboSECS1LogMode.Location = new System.Drawing.Point(362, 94);
            this.cboSECS1LogMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSECS1LogMode.Name = "cboSECS1LogMode";
            this.cboSECS1LogMode.Size = new System.Drawing.Size(87, 23);
            this.cboSECS1LogMode.TabIndex = 49;
            this.cboSECS1LogMode.SelectedIndexChanged += new System.EventHandler(this.cboSECS1LogMode_SelectedIndexChanged);
            // 
            // cboSEComLogMode
            // 
            this.cboSEComLogMode.FormattingEnabled = true;
            this.cboSEComLogMode.Items.AddRange(new object[] {
            "Hour",
            "Day",
            "None"});
            this.cboSEComLogMode.Location = new System.Drawing.Point(150, 94);
            this.cboSEComLogMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboSEComLogMode.Name = "cboSEComLogMode";
            this.cboSEComLogMode.Size = new System.Drawing.Size(87, 23);
            this.cboSEComLogMode.TabIndex = 48;
            this.cboSEComLogMode.SelectedIndexChanged += new System.EventHandler(this.cboSEComLogMode_SelectedIndexChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(376, 26);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(74, 29);
            this.btnBrowse.TabIndex = 47;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtLogDir
            // 
            this.txtLogDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogDir.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtLogDir.Location = new System.Drawing.Point(150, 29);
            this.txtLogDir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLogDir.Name = "txtLogDir";
            this.txtLogDir.ReadOnly = true;
            this.txtLogDir.Size = new System.Drawing.Size(219, 24);
            this.txtLogDir.TabIndex = 40;
            this.txtLogDir.TextChanged += new System.EventHandler(this.txtLogDir_TextChanged);
            this.txtLogDir.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtLogDir_MouseDown);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(30, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 32);
            this.label9.TabIndex = 46;
            this.label9.Text = "SECom Log Folder";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLogBackup
            // 
            this.lblLogBackup.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblLogBackup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLogBackup.Location = new System.Drawing.Point(30, 206);
            this.lblLogBackup.Name = "lblLogBackup";
            this.lblLogBackup.Size = new System.Drawing.Size(73, 32);
            this.lblLogBackup.TabIndex = 45;
            this.lblLogBackup.Text = "Log Backup";
            this.lblLogBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSEComLogMode
            // 
            this.lblSEComLogMode.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSEComLogMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSEComLogMode.Location = new System.Drawing.Point(30, 88);
            this.lblSEComLogMode.Name = "lblSEComLogMode";
            this.lblSEComLogMode.Size = new System.Drawing.Size(110, 32);
            this.lblSEComLogMode.TabIndex = 44;
            this.lblSEComLogMode.Text = "SECom Log Mode";
            this.lblSEComLogMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLogLevel
            // 
            this.lblLogLevel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblLogLevel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLogLevel.Location = new System.Drawing.Point(30, 148);
            this.lblLogLevel.Name = "lblLogLevel";
            this.lblLogLevel.Size = new System.Drawing.Size(110, 32);
            this.lblLogLevel.TabIndex = 43;
            this.lblLogLevel.Text = "SECom Log Level";
            this.lblLogLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSECS2LogMode
            // 
            this.lblSECS2LogMode.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSECS2LogMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSECS2LogMode.Location = new System.Drawing.Point(250, 149);
            this.lblSECS2LogMode.Name = "lblSECS2LogMode";
            this.lblSECS2LogMode.Size = new System.Drawing.Size(112, 32);
            this.lblSECS2LogMode.TabIndex = 42;
            this.lblSECS2LogMode.Text = "SECS-II Log Mode";
            this.lblSECS2LogMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSECS1LogMode
            // 
            this.lblSECS1LogMode.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblSECS1LogMode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSECS1LogMode.Location = new System.Drawing.Point(250, 89);
            this.lblSECS1LogMode.Name = "lblSECS1LogMode";
            this.lblSECS1LogMode.Size = new System.Drawing.Size(105, 32);
            this.lblSECS1LogMode.TabIndex = 41;
            this.lblSECS1LogMode.Text = "SECS-I Log Mode";
            this.lblSECS1LogMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmConfigure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 450);
            this.Controls.Add(this.tabConfigure);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmConfigure";
            this.Text = "frmConfigure";
            this.Load += new System.EventHandler(this.frmConfigure_Load);
            this.panel1.ResumeLayout(false);
            this.tabConfigure.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPageHSMS.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabConfigure;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cboProtocolType;
        private System.Windows.Forms.ComboBox cboIdentity;
        private System.Windows.Forms.ComboBox cboSEComID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageHSMS;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TextBox txtDeviceId;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cboConnectMode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblmsec;
        private System.Windows.Forms.TextBox txtRemoteIP4;
        private System.Windows.Forms.TextBox txtRemoteIP3;
        private System.Windows.Forms.TextBox txtRemoteIP2;
        private System.Windows.Forms.TextBox txtRemoteIP1;
        private System.Windows.Forms.TextBox txtLinkTestInterval;
        private System.Windows.Forms.Label lblLinkTestInterval;
        private System.Windows.Forms.TextBox txtLocalPort;
        private System.Windows.Forms.Label lblLocalPort;
        private System.Windows.Forms.TextBox txtRemotePort;
        private System.Windows.Forms.Label lblRemotePort;
        private System.Windows.Forms.Label lblRemoteIP;
        private System.Windows.Forms.TextBox txtT8;
        private System.Windows.Forms.Label lblT8;
        private System.Windows.Forms.TextBox txtT7;
        private System.Windows.Forms.Label lblT7;
        private System.Windows.Forms.TextBox txtT6;
        private System.Windows.Forms.Label lblT6;
        private System.Windows.Forms.TextBox txtT5;
        private System.Windows.Forms.Label lblT5;
        private System.Windows.Forms.TextBox txtT3;
        private System.Windows.Forms.Label lblT3;
        private System.Windows.Forms.Label lblConnectMode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cboLogBackup;
        private System.Windows.Forms.ComboBox cboSECS2LogMode;
        private System.Windows.Forms.ComboBox cboSEComLogLevel;
        private System.Windows.Forms.ComboBox cboSECS1LogMode;
        private System.Windows.Forms.ComboBox cboSEComLogMode;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtLogDir;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblLogBackup;
        private System.Windows.Forms.Label lblSEComLogMode;
        private System.Windows.Forms.Label lblLogLevel;
        private System.Windows.Forms.Label lblSECS2LogMode;
        private System.Windows.Forms.Label lblSECS1LogMode;
    }
}