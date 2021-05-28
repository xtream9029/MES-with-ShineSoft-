namespace VirtualMES
{
    partial class frmMES_S1F103
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
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlClose = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTrackNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRStatus = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRTopTrayId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRBottomTrayId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRTrackNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlClose.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Location = new System.Drawing.Point(0, 0);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(354, 68);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "SendMessage(S1F103)";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Location = new System.Drawing.Point(0, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 68);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(337, 44);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 15);
            this.label16.TabIndex = 21;
            this.label16.Text = "A5";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pnlClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 306);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 68);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 68);
            this.panel2.TabIndex = 3;
            // 
            // pnlClose
            // 
            this.pnlClose.Controls.Add(this.btnClose);
            this.pnlClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlClose.Location = new System.Drawing.Point(354, 0);
            this.pnlClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlClose.Name = "pnlClose";
            this.pnlClose.Size = new System.Drawing.Size(86, 68);
            this.pnlClose.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtTrackNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(440, 110);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Data";
            // 
            // txtTrackNo
            // 
            this.txtTrackNo.Location = new System.Drawing.Point(162, 40);
            this.txtTrackNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTrackNo.MaxLength = 5;
            this.txtTrackNo.Name = "txtTrackNo";
            this.txtTrackNo.Size = new System.Drawing.Size(167, 25);
            this.txtTrackNo.TabIndex = 9;
            this.txtTrackNo.TextChanged += new System.EventHandler(this.txtTrackNo_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "TRACK_NO";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtRStatus);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtRTopTrayId);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtRBottomTrayId);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtRTrackNo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 110);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(440, 196);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Receive Data";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(337, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 15);
            this.label8.TabIndex = 30;
            this.label8.Text = "1";
            // 
            // txtRStatus
            // 
            this.txtRStatus.Location = new System.Drawing.Point(162, 141);
            this.txtRStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRStatus.MaxLength = 5;
            this.txtRStatus.Name = "txtRStatus";
            this.txtRStatus.Size = new System.Drawing.Size(167, 25);
            this.txtRStatus.TabIndex = 29;
            this.txtRStatus.TextChanged += new System.EventHandler(this.txtRStatus_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 15);
            this.label9.TabIndex = 28;
            this.label9.Text = "TRACK_STATUS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(337, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 15);
            this.label6.TabIndex = 27;
            this.label6.Text = "A6";
            // 
            // txtRTopTrayId
            // 
            this.txtRTopTrayId.Location = new System.Drawing.Point(162, 108);
            this.txtRTopTrayId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRTopTrayId.MaxLength = 5;
            this.txtRTopTrayId.Name = "txtRTopTrayId";
            this.txtRTopTrayId.Size = new System.Drawing.Size(167, 25);
            this.txtRTopTrayId.TabIndex = 26;
            this.txtRTopTrayId.TextChanged += new System.EventHandler(this.txtRTopTrayId_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "TRAY_ID_TOP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(337, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "A6";
            // 
            // txtRBottomTrayId
            // 
            this.txtRBottomTrayId.Location = new System.Drawing.Point(162, 74);
            this.txtRBottomTrayId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRBottomTrayId.MaxLength = 5;
            this.txtRBottomTrayId.Name = "txtRBottomTrayId";
            this.txtRBottomTrayId.Size = new System.Drawing.Size(167, 25);
            this.txtRBottomTrayId.TabIndex = 23;
            this.txtRBottomTrayId.TextChanged += new System.EventHandler(this.txtRBottomTrayId_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "TRAY_ID_BOTTOM";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(337, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "A5";
            // 
            // txtRTrackNo
            // 
            this.txtRTrackNo.Location = new System.Drawing.Point(162, 40);
            this.txtRTrackNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRTrackNo.MaxLength = 5;
            this.txtRTrackNo.Name = "txtRTrackNo";
            this.txtRTrackNo.Size = new System.Drawing.Size(167, 25);
            this.txtRTrackNo.TabIndex = 9;
            this.txtRTrackNo.TextChanged += new System.EventHandler(this.txtRTrackNo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "TRACK_NO";
            // 
            // frmMES_S1F103
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 374);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMES_S1F103";
            this.Text = "frmMES_S1F101";
            this.Load += new System.EventHandler(this.frmMES_S1F103_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlClose.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTrackNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRTopTrayId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRBottomTrayId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRTrackNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlClose;
    }
}