using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;




namespace CPower_CSharp
{
	public class Form1 : System.Windows.Forms.Form
	{
		//========================================
		//自定義變數	
		private byte m_nCommType = 1;
		private int  m_nTimeout = 600 ;
		private long[] m_lBaudtbl = new long[7] { 115200, 57600, 38400, 19200, 9600, 4800 , 2400  };
        //========================================

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.RadioButton m_btnRS232;
		private System.Windows.Forms.RadioButton m_btnNetWork;
		private System.Windows.Forms.ComboBox m_cmbPort;
		private System.Windows.Forms.ComboBox m_cmbBaudrate;
		private System.Windows.Forms.ComboBox m_cmbCardID;
		private System.Windows.Forms.TextBox m_txtIPAddr;
		private System.Windows.Forms.TextBox m_txtIPPort;
		private System.Windows.Forms.TextBox m_txtIDCode;
		private System.Windows.Forms.Button btnSplitWnd;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox m_txtWidth;
		private System.Windows.Forms.TextBox m_txtHeight;
		private System.Windows.Forms.ComboBox m_cmbWndNo;
		private System.Windows.Forms.Button btnSendText;
		private System.Windows.Forms.TextBox m_txtText;
		private System.Windows.Forms.Button btnSendPict;
		private System.Windows.Forms.TextBox m_txtPict;
		private System.Windows.Forms.TextBox m_txtStaticText;
		private System.Windows.Forms.Button btnSendStaticText;
		private System.Windows.Forms.Button btnSendClock;
		private System.Windows.Forms.Button btnSetTime;
		private System.Windows.Forms.Button btnPlayProgram;
		private System.Windows.Forms.TextBox m_txtProNo;
		private System.Windows.Forms.Button btnMakProgram;
		private System.Windows.Forms.Button btnMakePlaybill;
        private System.Windows.Forms.Button btnUpload;
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
		}

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        #region Windows Form 設計工具產生的程式碼
        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtIPAddr = new System.Windows.Forms.TextBox();
            this.m_cmbPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnNetWork = new System.Windows.Forms.RadioButton();
            this.m_btnRS232 = new System.Windows.Forms.RadioButton();
            this.m_cmbBaudrate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_cmbCardID = new System.Windows.Forms.ComboBox();
            this.m_txtIPPort = new System.Windows.Forms.TextBox();
            this.m_txtIDCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSplitWnd = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.m_txtWidth = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_txtHeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_cmbWndNo = new System.Windows.Forms.ComboBox();
            this.btnSendText = new System.Windows.Forms.Button();
            this.m_txtText = new System.Windows.Forms.TextBox();
            this.btnSendPict = new System.Windows.Forms.Button();
            this.m_txtPict = new System.Windows.Forms.TextBox();
            this.m_txtStaticText = new System.Windows.Forms.TextBox();
            this.btnSendStaticText = new System.Windows.Forms.Button();
            this.btnSendClock = new System.Windows.Forms.Button();
            this.btnSetTime = new System.Windows.Forms.Button();
            this.btnPlayProgram = new System.Windows.Forms.Button();
            this.m_txtProNo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnMakProgram = new System.Windows.Forms.Button();
            this.btnMakePlaybill = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_txtIPAddr);
            this.groupBox1.Controls.Add(this.m_cmbPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.m_btnNetWork);
            this.groupBox1.Controls.Add(this.m_btnRS232);
            this.groupBox1.Controls.Add(this.m_cmbBaudrate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_cmbCardID);
            this.groupBox1.Controls.Add(this.m_txtIPPort);
            this.groupBox1.Controls.Add(this.m_txtIDCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(7, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // m_txtIPAddr
            // 
            this.m_txtIPAddr.Location = new System.Drawing.Point(133, 60);
            this.m_txtIPAddr.Name = "m_txtIPAddr";
            this.m_txtIPAddr.Size = new System.Drawing.Size(84, 22);
            this.m_txtIPAddr.TabIndex = 5;
            this.m_txtIPAddr.Text = "192.168.1.222";
            // 
            // m_cmbPort
            // 
            this.m_cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbPort.Location = new System.Drawing.Point(133, 17);
            this.m_cmbPort.Name = "m_cmbPort";
            this.m_cmbPort.Size = new System.Drawing.Size(84, 20);
            this.m_cmbPort.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(80, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP Addr";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(80, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "COM Port";
            // 
            // m_btnNetWork
            // 
            this.m_btnNetWork.Checked = true;
            this.m_btnNetWork.Location = new System.Drawing.Point(7, 60);
            this.m_btnNetWork.Name = "m_btnNetWork";
            this.m_btnNetWork.Size = new System.Drawing.Size(66, 17);
            this.m_btnNetWork.TabIndex = 1;
            this.m_btnNetWork.TabStop = true;
            this.m_btnNetWork.Text = "NetWork";
            this.m_btnNetWork.CheckedChanged += new System.EventHandler(this.m_btnNetWork_CheckedChanged);
            // 
            // m_btnRS232
            // 
            this.m_btnRS232.Location = new System.Drawing.Point(7, 9);
            this.m_btnRS232.Name = "m_btnRS232";
            this.m_btnRS232.Size = new System.Drawing.Size(66, 34);
            this.m_btnRS232.TabIndex = 0;
            this.m_btnRS232.Text = "RS232/485";
            this.m_btnRS232.CheckedChanged += new System.EventHandler(this.m_btnRS232_CheckedChanged);
            // 
            // m_cmbBaudrate
            // 
            this.m_cmbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbBaudrate.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "19200",
            "9600",
            "4800",
            "2400"});
            this.m_cmbBaudrate.Location = new System.Drawing.Point(267, 17);
            this.m_cmbBaudrate.Name = "m_cmbBaudrate";
            this.m_cmbBaudrate.Size = new System.Drawing.Size(83, 20);
            this.m_cmbBaudrate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(220, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Baudrate";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(360, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Controller ID";
            // 
            // m_cmbCardID
            // 
            this.m_cmbCardID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbCardID.Location = new System.Drawing.Point(433, 17);
            this.m_cmbCardID.Name = "m_cmbCardID";
            this.m_cmbCardID.Size = new System.Drawing.Size(84, 20);
            this.m_cmbCardID.TabIndex = 4;
            // 
            // m_txtIPPort
            // 
            this.m_txtIPPort.Location = new System.Drawing.Point(267, 60);
            this.m_txtIPPort.Name = "m_txtIPPort";
            this.m_txtIPPort.Size = new System.Drawing.Size(83, 22);
            this.m_txtIPPort.TabIndex = 5;
            this.m_txtIPPort.Text = "5200";
            // 
            // m_txtIDCode
            // 
            this.m_txtIDCode.Location = new System.Drawing.Point(433, 60);
            this.m_txtIDCode.Name = "m_txtIDCode";
            this.m_txtIDCode.Size = new System.Drawing.Size(84, 22);
            this.m_txtIDCode.TabIndex = 5;
            this.m_txtIDCode.Text = "255.255.255.255";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(220, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 22);
            this.label5.TabIndex = 2;
            this.label5.Text = "IP Port";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(360, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 22);
            this.label6.TabIndex = 2;
            this.label6.Text = "ID Code";
            // 
            // btnSplitWnd
            // 
            this.btnSplitWnd.Location = new System.Drawing.Point(13, 120);
            this.btnSplitWnd.Name = "btnSplitWnd";
            this.btnSplitWnd.Size = new System.Drawing.Size(94, 25);
            this.btnSplitWnd.TabIndex = 1;
            this.btnSplitWnd.Text = "Make two window";
            this.btnSplitWnd.Click += new System.EventHandler(this.btnSplitWnd_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(127, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "Width";
            // 
            // m_txtWidth
            // 
            this.m_txtWidth.Location = new System.Drawing.Point(167, 120);
            this.m_txtWidth.Name = "m_txtWidth";
            this.m_txtWidth.Size = new System.Drawing.Size(40, 22);
            this.m_txtWidth.TabIndex = 3;
            this.m_txtWidth.Text = "320";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(213, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 25);
            this.label8.TabIndex = 2;
            this.label8.Text = "Height";
            // 
            // m_txtHeight
            // 
            this.m_txtHeight.Location = new System.Drawing.Point(260, 120);
            this.m_txtHeight.Name = "m_txtHeight";
            this.m_txtHeight.Size = new System.Drawing.Size(40, 22);
            this.m_txtHeight.TabIndex = 3;
            this.m_txtHeight.Text = "32";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(327, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 25);
            this.label9.TabIndex = 2;
            this.label9.Text = "Select send Window";
            // 
            // m_cmbWndNo
            // 
            this.m_cmbWndNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbWndNo.Items.AddRange(new object[] {
            "0",
            "1"});
            this.m_cmbWndNo.Location = new System.Drawing.Point(433, 120);
            this.m_cmbWndNo.Name = "m_cmbWndNo";
            this.m_cmbWndNo.Size = new System.Drawing.Size(84, 20);
            this.m_cmbWndNo.TabIndex = 5;
            // 
            // btnSendText
            // 
            this.btnSendText.Location = new System.Drawing.Point(13, 163);
            this.btnSendText.Name = "btnSendText";
            this.btnSendText.Size = new System.Drawing.Size(94, 24);
            this.btnSendText.TabIndex = 1;
            this.btnSendText.Text = "Send Text";
            this.btnSendText.Click += new System.EventHandler(this.btnSendText_Click);
            // 
            // m_txtText
            // 
            this.m_txtText.AcceptsReturn = true;
            this.m_txtText.Location = new System.Drawing.Point(127, 154);
            this.m_txtText.Multiline = true;
            this.m_txtText.Name = "m_txtText";
            this.m_txtText.Size = new System.Drawing.Size(173, 52);
            this.m_txtText.TabIndex = 6;
            this.m_txtText.Text = "歡迎光臨流明電子！";
            // 
            // btnSendPict
            // 
            this.btnSendPict.Location = new System.Drawing.Point(13, 223);
            this.btnSendPict.Name = "btnSendPict";
            this.btnSendPict.Size = new System.Drawing.Size(94, 24);
            this.btnSendPict.TabIndex = 1;
            this.btnSendPict.Text = "Send Picture";
            this.btnSendPict.Click += new System.EventHandler(this.btnSendPict_Click);
            // 
            // m_txtPict
            // 
            this.m_txtPict.Location = new System.Drawing.Point(127, 223);
            this.m_txtPict.Name = "m_txtPict";
            this.m_txtPict.Size = new System.Drawing.Size(173, 22);
            this.m_txtPict.TabIndex = 7;
            this.m_txtPict.Text = "test.bmp";
            // 
            // m_txtStaticText
            // 
            this.m_txtStaticText.Location = new System.Drawing.Point(127, 266);
            this.m_txtStaticText.Name = "m_txtStaticText";
            this.m_txtStaticText.Size = new System.Drawing.Size(173, 22);
            this.m_txtStaticText.TabIndex = 7;
            this.m_txtStaticText.Text = "Welcome to Lumen!";
            // 
            // btnSendStaticText
            // 
            this.btnSendStaticText.Location = new System.Drawing.Point(13, 266);
            this.btnSendStaticText.Name = "btnSendStaticText";
            this.btnSendStaticText.Size = new System.Drawing.Size(94, 24);
            this.btnSendStaticText.TabIndex = 1;
            this.btnSendStaticText.Text = "Send Static Text";
            this.btnSendStaticText.Click += new System.EventHandler(this.btnSendStaticText_Click);
            // 
            // btnSendClock
            // 
            this.btnSendClock.Location = new System.Drawing.Point(13, 309);
            this.btnSendClock.Name = "btnSendClock";
            this.btnSendClock.Size = new System.Drawing.Size(94, 24);
            this.btnSendClock.TabIndex = 1;
            this.btnSendClock.Text = "Send Clock";
            this.btnSendClock.Click += new System.EventHandler(this.btnSendClock_Click);
            // 
            // btnSetTime
            // 
            this.btnSetTime.Location = new System.Drawing.Point(127, 309);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(93, 24);
            this.btnSetTime.TabIndex = 1;
            this.btnSetTime.Text = "Set Time";
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // btnPlayProgram
            // 
            this.btnPlayProgram.Location = new System.Drawing.Point(327, 154);
            this.btnPlayProgram.Name = "btnPlayProgram";
            this.btnPlayProgram.Size = new System.Drawing.Size(93, 25);
            this.btnPlayProgram.TabIndex = 1;
            this.btnPlayProgram.Text = "Play One Program";
            this.btnPlayProgram.Click += new System.EventHandler(this.btnPlayProgram_Click);
            // 
            // m_txtProNo
            // 
            this.m_txtProNo.Location = new System.Drawing.Point(433, 154);
            this.m_txtProNo.Name = "m_txtProNo";
            this.m_txtProNo.Size = new System.Drawing.Size(84, 22);
            this.m_txtProNo.TabIndex = 3;
            this.m_txtProNo.Text = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnMakProgram);
            this.groupBox2.Controls.Add(this.btnMakePlaybill);
            this.groupBox2.Controls.Add(this.btnUpload);
            this.groupBox2.Location = new System.Drawing.Point(327, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(206, 137);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Make program/playbill and upload";
            // 
            // btnMakProgram
            // 
            this.btnMakProgram.Location = new System.Drawing.Point(40, 26);
            this.btnMakProgram.Name = "btnMakProgram";
            this.btnMakProgram.Size = new System.Drawing.Size(127, 24);
            this.btnMakProgram.TabIndex = 1;
            this.btnMakProgram.Text = "Make program";
            this.btnMakProgram.Click += new System.EventHandler(this.btnMakProgram_Click);
            // 
            // btnMakePlaybill
            // 
            this.btnMakePlaybill.Location = new System.Drawing.Point(40, 64);
            this.btnMakePlaybill.Name = "btnMakePlaybill";
            this.btnMakePlaybill.Size = new System.Drawing.Size(127, 25);
            this.btnMakePlaybill.TabIndex = 1;
            this.btnMakePlaybill.Text = "Make playbill";
            this.btnMakePlaybill.Click += new System.EventHandler(this.btnMakePlaybill_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(40, 103);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(127, 24);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Upload and restart App";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.ClientSize = new System.Drawing.Size(648, 345);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.m_txtPict);
            this.Controls.Add(this.m_txtText);
            this.Controls.Add(this.m_cmbWndNo);
            this.Controls.Add(this.m_txtWidth);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSplitWnd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.m_txtHeight);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSendText);
            this.Controls.Add(this.btnSendPict);
            this.Controls.Add(this.m_txtStaticText);
            this.Controls.Add(this.btnSendStaticText);
            this.Controls.Add(this.btnSendClock);
            this.Controls.Add(this.btnSetTime);
            this.Controls.Add(this.btnPlayProgram);
            this.Controls.Add(this.m_txtProNo);
            this.Name = "Form1";
            this.Text = "CPower Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion		

		private void Form1_Load(object sender, System.EventArgs e)
		{
			for ( int i = 1; i <= 254; i++)
			{
				String  strTemp = i.ToString();
				m_cmbPort.Items.Add(strTemp);
				strTemp = "COM" + strTemp;
				m_cmbCardID.Items.Add(strTemp);
			}

			m_cmbCardID.SelectedIndex = 0;
			m_cmbPort.SelectedIndex = 0;
			m_cmbBaudrate.SelectedIndex = 0;
			m_cmbWndNo.SelectedIndex = 0;

			EnableCtrl();
		}

		//=======================================
		private void EnableCtrl()
		{
			m_cmbPort.Enabled = m_cmbBaudrate.Enabled = m_nCommType == 0 ;
			m_txtIPAddr.Enabled = m_txtIPPort.Enabled = m_txtIDCode.Enabled = m_nCommType == 1 ;
		}


		private uint GetIP(string strIp)
		{
			System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(strIp);
            uint lIp = BitConverter.ToUInt32(ipaddress.GetAddressBytes(), 0);
			//調整IP地址的字節序
			lIp = ((lIp & 0xFF000000) >> 24) + ((lIp & 0x00FF0000) >> 8) + ((lIp & 0x0000FF00) << 8) + ((lIp & 0x000000FF) << 24);
			return (lIp);
		}

		private void InitComm()
		{			
			if (0 == m_nCommType)
			{
				int nPort = Convert.ToByte(m_cmbPort.SelectedIndex + 1);
				String strPort = "COM" + nPort.ToString();
				int nBaudrate = Convert.ToInt32(m_lBaudtbl[m_cmbBaudrate.SelectedIndex]);
				CP5200.CP5200_RS232_InitEx( Marshal.StringToHGlobalAnsi(strPort), nBaudrate, m_nTimeout);
			}
			else
			{
				uint dwIPAddr = GetIP( m_txtIPAddr.Text );
				uint dwIDCode = GetIP( m_txtIDCode.Text );
				int  nIPPort  = Convert.ToInt32(m_txtIPPort.Text) ;
                if (dwIPAddr != 0 && dwIDCode != 0)
                {
                    CP5200.CP5200_Net_Init(dwIPAddr, nIPPort, dwIDCode, m_nTimeout);
                }

			}
		}

		void GetSplitWnd( int [] rcWins )
		{
			int nWidth = Convert.ToInt32(m_txtWidth.Text);
			int nHeight = Convert.ToInt32(m_txtHeight.Text);
			rcWins[0] = 0;
			rcWins[1] = 0;
			rcWins[2] = nWidth / 2;
			rcWins[3] = nHeight;
			rcWins[4] = nWidth / 2;
			rcWins[5] = 0;
			rcWins[6] = nWidth;
			rcWins[7] = nHeight;
		}
		//=======================================
		private void m_btnRS232_CheckedChanged(object sender, System.EventArgs e)
		{
			m_nCommType = 0;
			EnableCtrl();		
		}

		private void m_btnNetWork_CheckedChanged(object sender, System.EventArgs e)
		{
			m_nCommType = 1;
			EnableCtrl();		
		}

		private void btnSplitWnd_Click(object sender, System.EventArgs e)
		{
			int ret = 0;
			int[] nWndRect = new int[8];
			GetSplitWnd( nWndRect );	
			InitComm();	
			if (0 == m_nCommType) 
			{
				ret = CP5200.CP5200_RS232_SplitScreen( Convert.ToByte(m_cmbCardID.SelectedIndex + 1), Convert.ToInt32(m_txtWidth.Text), Convert.ToInt32(m_txtHeight.Text), 2,  nWndRect);
			}
			else 
			{
				ret = CP5200.CP5200_Net_SplitScreen( Convert.ToByte(m_cmbCardID.SelectedIndex + 1), Convert.ToInt32(m_txtWidth.Text), Convert.ToInt32(m_txtHeight.Text), 2,  nWndRect);
			}

			if( ret >= 0 )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		private void btnSendText_Click(object sender, System.EventArgs e)
		{
			
			InitComm();
			int nRet;
            // Network
	        if ( m_nCommType == 1)
			{
				nRet = CP5200.CP5200_Net_SendText(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), m_cmbWndNo.SelectedIndex, Marshal.StringToHGlobalAnsi(m_txtText.Text), 0xFF, 16, 3, 0, 3, 5);
            }
            // RS232/485
			else
			{
				nRet = CP5200.CP5200_RS232_SendText(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), m_cmbWndNo.SelectedIndex, Marshal.StringToHGlobalAnsi(m_txtText.Text), 0xFF, 16, 3, 0, 3, 5);
			}
	
			if( nRet >= 0 )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		private void btnSendPict_Click(object sender, System.EventArgs e)
		{
			
			int nRet;
			int[] nWndRect = new int[8];
			GetSplitWnd( nWndRect );	
			InitComm();
	
			if ( m_nCommType == 1) //網口
			{
				nRet = CP5200.CP5200_Net_SendPicture(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), m_cmbWndNo.SelectedIndex,0, 0, 
					nWndRect[2 + m_cmbWndNo.SelectedIndex * 4] - nWndRect[0 + m_cmbWndNo.SelectedIndex * 4], nWndRect[3 + m_cmbWndNo.SelectedIndex * 4] - nWndRect[1 + m_cmbWndNo.SelectedIndex * 4],
					Marshal.StringToHGlobalAnsi(m_txtPict.Text), 1, 0, 3, 0);
			}
			else //串口
			{
				nRet = CP5200.CP5200_RS232_SendPicture(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), m_cmbWndNo.SelectedIndex,0, 0, 
					nWndRect[2 + m_cmbWndNo.SelectedIndex * 4] - nWndRect[0 + m_cmbWndNo.SelectedIndex * 4], nWndRect[3 + m_cmbWndNo.SelectedIndex * 4] - nWndRect[1 + m_cmbWndNo.SelectedIndex * 4],
					Marshal.StringToHGlobalAnsi(m_txtPict.Text), 1, 0, 3, 0);
			}

			if( nRet >= 0 )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		private void btnSendStaticText_Click(object sender, System.EventArgs e)
		{
			int nRet;
			int[] nWndRect = new int[8];
			GetSplitWnd( nWndRect );	
			InitComm();
		
			if ( m_nCommType == 1) //網口
			{
				nRet = CP5200.CP5200_Net_SendStatic(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), m_cmbWndNo.SelectedIndex, Marshal.StringToHGlobalAnsi(m_txtStaticText.Text), 0xFF, 16, 0, 
					0, 0, nWndRect[2 + m_cmbWndNo.SelectedIndex * 4] - nWndRect[0 + m_cmbWndNo.SelectedIndex * 4], nWndRect[3 + m_cmbWndNo.SelectedIndex * 4] - nWndRect[1 + m_cmbWndNo.SelectedIndex * 4]);
			}
			else //串口
			{
				nRet = CP5200.CP5200_RS232_SendStatic(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), m_cmbWndNo.SelectedIndex, Marshal.StringToHGlobalAnsi(m_txtStaticText.Text), 0xFF, 16, 0, 
					0, 0, nWndRect[2 + m_cmbWndNo.SelectedIndex * 4] - nWndRect[0 + m_cmbWndNo.SelectedIndex * 4], nWndRect[3 + m_cmbWndNo.SelectedIndex * 4] - nWndRect[1 + m_cmbWndNo.SelectedIndex * 4]);
			}

			if( nRet >= 0 )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		private void btnSendClock_Click(object sender, System.EventArgs e)
		{
			InitComm();
			int nRet;
	
			if ( m_nCommType == 1) //網口
			{
				nRet = CP5200.CP5200_Net_SendClock( Convert.ToByte(m_cmbCardID.SelectedIndex + 1), m_cmbWndNo.SelectedIndex, 3, 0, 7, 7, 1, 255, 255, 255, Marshal.StringToHGlobalAnsi("Date"));
			}
			else //串口
			{
				nRet = CP5200.CP5200_RS232_SendClock( Convert.ToByte(m_cmbCardID.SelectedIndex + 1), m_cmbWndNo.SelectedIndex, 3, 0, 7, 7, 1, 255, 255, 255, Marshal.StringToHGlobalAnsi("Date"));
			}

			if( nRet >= 0 )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		private void btnSetTime_Click(object sender, System.EventArgs e)
		{
			InitComm();
			int nRet;
			byte[] byTimeInfo = new byte[7];
			DateTime curTime;
			curTime = DateTime.Now; 
			byTimeInfo[0] = Convert.ToByte(curTime.Second);
			byTimeInfo[1] = Convert.ToByte(curTime.Minute);
			byTimeInfo[2] = Convert.ToByte(curTime.Hour);
			byTimeInfo[3] = Convert.ToByte(curTime.DayOfWeek);
			byTimeInfo[4] = Convert.ToByte(curTime.Day);
			byTimeInfo[5] = Convert.ToByte(curTime.Month);
			byTimeInfo[6] = Convert.ToByte(curTime.Year - 2000);
	
			if ( m_nCommType == 1) //網口
			{
				nRet = CP5200.CP5200_Net_SetTime(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), byTimeInfo) ; 
			}
			else //串口
			{
				nRet = CP5200.CP5200_RS232_SetTime(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), byTimeInfo) ; 
			}

			if( nRet >= 0 )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		private void btnPlayProgram_Click(object sender, System.EventArgs e)
		{		
			InitComm();		
			int nRet = 0;
			int[] nProNo = new int[2];
			nProNo[0] = Convert.ToInt32( m_txtProNo.Text);
			nProNo[1] = 0;

			if ( m_nCommType == 1) //網口
			{
				nRet = CP5200.CP5200_Net_PlaySelectedPrg(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), nProNo, 1, 0);
			}
			else //串口
			{
				nRet = CP5200.CP5200_RS232_PlaySelectedPrg(Convert.ToByte(m_cmbCardID.SelectedIndex + 1), nProNo, 1, 0);
			}

			if( nRet >= 0 )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		
		//======================================
		IntPtr GetProgramFileName()
		{			
			String strName = String.Format( "{0:0000}0000.lpb" , m_cmbCardID.SelectedIndex+1 ) ;
			return Marshal.StringToHGlobalAnsi(strName);
		}

		IntPtr GetPlaybillFileName()
		{
			return Marshal.StringToHGlobalAnsi("playbill.lpp");
		}
		
		private void btnMakProgram_Click(object sender, System.EventArgs e)
		{	
			Boolean bRet = false;
			IntPtr hObj = CP5200.CP5200_Program_Create( Convert.ToInt32(m_txtWidth.Text), Convert.ToInt32(m_txtHeight.Text), 0x77 );
			if ( hObj != IntPtr.Zero)
			{
				//分左右兩個窗口，
				int[] nWndRect = new int[8];
				GetSplitWnd( nWndRect );
				if ( CP5200.CP5200_Program_SetProperty( hObj , 0xFFFF , 1 ) > 0 )
				{
					int nItemCnt = 0;
					//0號窗口放文字，
					int nWndNo = CP5200.CP5200_Program_AddPlayWindow( hObj , nWndRect[0], nWndRect[1], nWndRect[2] - nWndRect[0], nWndRect[3] - nWndRect[1] );
					if ( nWndNo >= 0)
					{
						CP5200.CP5200_Program_SetWindowProperty( hObj , nWndNo , 0x30 , 1 ); //設置窗口邊框
						//添加文本節目						
						if ( CP5200.CP5200_Program_AddText(hObj, nWndNo, Marshal.StringToHGlobalAnsi(m_txtText.Text), 16, 0xFF, 0xFFFF, 100, 3 ) >= 0)
							nItemCnt++;
					}
	
					//1號窗口放圖片
					nWndNo = CP5200.CP5200_Program_AddPlayWindow( hObj , nWndRect[4], nWndRect[5], nWndRect[6] - nWndRect[4], nWndRect[7] - nWndRect[5] );
					if ( nWndNo >= 0)
					{
						//添加圖片節目
						if ( CP5200.CP5200_Program_AddPicture(hObj,nWndNo,  Marshal.StringToHGlobalAnsi(m_txtPict.Text), 2, 0xFFFF, 100, 3, 0) >= 0)
							nItemCnt++;
					}

					if ( nItemCnt > 0 && CP5200.CP5200_Program_SaveToFile( hObj, GetProgramFileName()) >= 0 )
						bRet = true;
				}
				CP5200.CP5200_Program_Destroy( hObj);
			}
    
			if( bRet  )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		private void btnMakePlaybill_Click(object sender, System.EventArgs e)
		{
			Boolean bRet = false;
			IntPtr hObj = CP5200.CP5200_Playbill_Create( Convert.ToInt32(m_txtWidth.Text), Convert.ToInt32(m_txtHeight.Text), 0x77);
			if ( hObj != IntPtr.Zero)
			{		
				if ( CP5200.CP5200_Playbill_AddFile( hObj, GetProgramFileName()) >= 0)
				{
					if ( CP5200.CP5200_Playbill_SaveToFile( hObj, GetPlaybillFileName()) == 0)
						bRet = true;
				}	
				CP5200.CP5200_Playbill_Destroy(hObj);
			}

			if( bRet  )
			{
				MessageBox.Show("Successful");
			}
			else
			{
				MessageBox.Show("Fail");
			}
		}

		private void btnUpload_Click(object sender, System.EventArgs e)
		{
			InitComm();

 			int nUploadCnt = 0 ;
 			if ( m_nCommType == 1) //網口
 			{
 				if ( 0 == CP5200.CP5200_Net_UploadFile( Convert.ToByte(m_cmbCardID.SelectedIndex + 1), GetProgramFileName(), GetProgramFileName()) )
 					nUploadCnt++;
 
 				if ( 0 == CP5200.CP5200_Net_UploadFile( Convert.ToByte(m_cmbCardID.SelectedIndex + 1), GetPlaybillFileName(), GetPlaybillFileName()) )
 					nUploadCnt++;
 
 				if ( nUploadCnt > 0)
 					CP5200.CP5200_Net_RestartApp( Convert.ToByte(m_cmbCardID.SelectedIndex + 1) );
 			}
 			else //串口
 			{
 				if ( 0 == CP5200.CP5200_RS232_UploadFile( Convert.ToByte(m_cmbCardID.SelectedIndex + 1), GetProgramFileName(), GetProgramFileName()) )
 					nUploadCnt++;
 		
 				if ( 0 == CP5200.CP5200_RS232_UploadFile( Convert.ToByte(m_cmbCardID.SelectedIndex + 1), GetPlaybillFileName(), GetPlaybillFileName()) )
 					nUploadCnt++;
 
 				if ( nUploadCnt > 0)
 					CP5200.CP5200_RS232_RestartApp( Convert.ToByte(m_cmbCardID.SelectedIndex + 1) );
 			}

		
			String strName = String.Format( "Upload 2 files ,{0:D} successful ,{1:D}  failed!" , nUploadCnt , 2 - nUploadCnt ) ;	
			MessageBox.Show(strName);	
		}

	
	}
}
