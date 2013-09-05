namespace LED
{
    partial class SettingForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            TextImage.releaseAll();
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Setting = new System.Windows.Forms.TabControl();
            this.LEDScreenSetting = new System.Windows.Forms.TabPage();
            this.groupBox_net = new System.Windows.Forms.GroupBox();
            this.label_help = new System.Windows.Forms.Label();
            this.button_ping = new System.Windows.Forms.Button();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.label_timeout = new System.Windows.Forms.Label();
            this.label_IP = new System.Windows.Forms.Label();
            this.textBox_timeout = new System.Windows.Forms.TextBox();
            this.label_idcode = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_idcode = new System.Windows.Forms.TextBox();
            this.label_port = new System.Windows.Forms.Label();
            this.AlarmSetting = new System.Windows.Forms.TabPage();
            this.IMSetting = new System.Windows.Forms.TabPage();
            this.inputButton = new System.Windows.Forms.Button();
            this.dataGridView_IM = new System.Windows.Forms.DataGridView();
            this.IMData_string = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMData_tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMData_format = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMData_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMData_color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviewResult = new System.Windows.Forms.TextBox();
            this.im_colorButton = new System.Windows.Forms.Button();
            this.im_unit = new System.Windows.Forms.TextBox();
            this.label_unit = new System.Windows.Forms.Label();
            this.label_format = new System.Windows.Forms.Label();
            this.im_format = new System.Windows.Forms.ComboBox();
            this.label_tag = new System.Windows.Forms.Label();
            this.im_tag = new System.Windows.Forms.TextBox();
            this.label_string = new System.Windows.Forms.Label();
            this.im_string = new System.Windows.Forms.TextBox();
            this.im_colorDialog = new System.Windows.Forms.ColorDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.EDABackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.Setting.SuspendLayout();
            this.LEDScreenSetting.SuspendLayout();
            this.groupBox_net.SuspendLayout();
            this.IMSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_IM)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Setting
            // 
            this.Setting.Controls.Add(this.LEDScreenSetting);
            this.Setting.Controls.Add(this.AlarmSetting);
            this.Setting.Controls.Add(this.IMSetting);
            this.Setting.Location = new System.Drawing.Point(13, 13);
            this.Setting.Name = "Setting";
            this.Setting.SelectedIndex = 0;
            this.Setting.Size = new System.Drawing.Size(983, 623);
            this.Setting.TabIndex = 0;
            // 
            // LEDScreenSetting
            // 
            this.LEDScreenSetting.Controls.Add(this.groupBox_net);
            this.LEDScreenSetting.Location = new System.Drawing.Point(4, 22);
            this.LEDScreenSetting.Name = "LEDScreenSetting";
            this.LEDScreenSetting.Padding = new System.Windows.Forms.Padding(3);
            this.LEDScreenSetting.Size = new System.Drawing.Size(975, 597);
            this.LEDScreenSetting.TabIndex = 0;
            this.LEDScreenSetting.Text = "字幕機設定";
            this.LEDScreenSetting.UseVisualStyleBackColor = true;
            // 
            // groupBox_net
            // 
            this.groupBox_net.Controls.Add(this.label_help);
            this.groupBox_net.Controls.Add(this.button_ping);
            this.groupBox_net.Controls.Add(this.textBox_IP);
            this.groupBox_net.Controls.Add(this.button_save);
            this.groupBox_net.Controls.Add(this.label_timeout);
            this.groupBox_net.Controls.Add(this.label_IP);
            this.groupBox_net.Controls.Add(this.textBox_timeout);
            this.groupBox_net.Controls.Add(this.label_idcode);
            this.groupBox_net.Controls.Add(this.textBox_port);
            this.groupBox_net.Controls.Add(this.textBox_idcode);
            this.groupBox_net.Controls.Add(this.label_port);
            this.groupBox_net.Location = new System.Drawing.Point(6, 6);
            this.groupBox_net.Name = "groupBox_net";
            this.groupBox_net.Size = new System.Drawing.Size(417, 91);
            this.groupBox_net.TabIndex = 14;
            this.groupBox_net.TabStop = false;
            this.groupBox_net.Text = "網路設定";
            // 
            // label_help
            // 
            this.label_help.AutoSize = true;
            this.label_help.ForeColor = System.Drawing.Color.Red;
            this.label_help.Location = new System.Drawing.Point(13, 70);
            this.label_help.Name = "label_help";
            this.label_help.Size = new System.Drawing.Size(53, 12);
            this.label_help.TabIndex = 15;
            this.label_help.Text = "label_help";
            // 
            // button_ping
            // 
            this.button_ping.Location = new System.Drawing.Point(214, 59);
            this.button_ping.Name = "button_ping";
            this.button_ping.Size = new System.Drawing.Size(75, 23);
            this.button_ping.TabIndex = 14;
            this.button_ping.Text = "Ping";
            this.button_ping.UseVisualStyleBackColor = true;
            this.button_ping.Click += new System.EventHandler(this.button_ping_Click);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(15, 31);
            this.textBox_IP.MaxLength = 15;
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(100, 22);
            this.textBox_IP.TabIndex = 5;
            this.textBox_IP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_IpPress);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(336, 59);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 7;
            this.button_save.Text = "儲存設定";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label_timeout
            // 
            this.label_timeout.AutoSize = true;
            this.label_timeout.Location = new System.Drawing.Point(309, 16);
            this.label_timeout.Name = "label_timeout";
            this.label_timeout.Size = new System.Drawing.Size(40, 12);
            this.label_timeout.TabIndex = 13;
            this.label_timeout.Text = "timeout";
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(15, 16);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(15, 12);
            this.label_IP.TabIndex = 6;
            this.label_IP.Text = "IP";
            // 
            // textBox_timeout
            // 
            this.textBox_timeout.Location = new System.Drawing.Point(309, 31);
            this.textBox_timeout.Name = "textBox_timeout";
            this.textBox_timeout.Size = new System.Drawing.Size(100, 22);
            this.textBox_timeout.TabIndex = 12;
            this.textBox_timeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_DigitPress);
            // 
            // label_idcode
            // 
            this.label_idcode.AutoSize = true;
            this.label_idcode.Location = new System.Drawing.Point(185, 16);
            this.label_idcode.Name = "label_idcode";
            this.label_idcode.Size = new System.Drawing.Size(42, 12);
            this.label_idcode.TabIndex = 11;
            this.label_idcode.Text = "IDCode";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(132, 31);
            this.textBox_port.MaxLength = 5;
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.ShortcutsEnabled = false;
            this.textBox_port.Size = new System.Drawing.Size(39, 22);
            this.textBox_port.TabIndex = 8;
            this.textBox_port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_DigitPress);
            // 
            // textBox_idcode
            // 
            this.textBox_idcode.Location = new System.Drawing.Point(187, 31);
            this.textBox_idcode.MaxLength = 15;
            this.textBox_idcode.Name = "textBox_idcode";
            this.textBox_idcode.Size = new System.Drawing.Size(100, 22);
            this.textBox_idcode.TabIndex = 10;
            this.textBox_idcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_IpPress);
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(132, 16);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(24, 12);
            this.label_port.TabIndex = 9;
            this.label_port.Text = "Port";
            // 
            // AlarmSetting
            // 
            this.AlarmSetting.Location = new System.Drawing.Point(4, 22);
            this.AlarmSetting.Name = "AlarmSetting";
            this.AlarmSetting.Padding = new System.Windows.Forms.Padding(3);
            this.AlarmSetting.Size = new System.Drawing.Size(975, 597);
            this.AlarmSetting.TabIndex = 1;
            this.AlarmSetting.Text = "警報訊息設定";
            this.AlarmSetting.UseVisualStyleBackColor = true;
            // 
            // IMSetting
            // 
            this.IMSetting.Controls.Add(this.inputButton);
            this.IMSetting.Controls.Add(this.dataGridView_IM);
            this.IMSetting.Controls.Add(this.PreviewResult);
            this.IMSetting.Controls.Add(this.im_colorButton);
            this.IMSetting.Controls.Add(this.im_unit);
            this.IMSetting.Controls.Add(this.label_unit);
            this.IMSetting.Controls.Add(this.label_format);
            this.IMSetting.Controls.Add(this.im_format);
            this.IMSetting.Controls.Add(this.label_tag);
            this.IMSetting.Controls.Add(this.im_tag);
            this.IMSetting.Controls.Add(this.label_string);
            this.IMSetting.Controls.Add(this.im_string);
            this.IMSetting.Location = new System.Drawing.Point(4, 22);
            this.IMSetting.Name = "IMSetting";
            this.IMSetting.Padding = new System.Windows.Forms.Padding(3);
            this.IMSetting.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.IMSetting.Size = new System.Drawing.Size(975, 597);
            this.IMSetting.TabIndex = 2;
            this.IMSetting.Text = "即時資訊設定";
            this.IMSetting.UseVisualStyleBackColor = true;
            // 
            // inputButton
            // 
            this.inputButton.Location = new System.Drawing.Point(724, 41);
            this.inputButton.Name = "inputButton";
            this.inputButton.Size = new System.Drawing.Size(75, 23);
            this.inputButton.TabIndex = 13;
            this.inputButton.Text = "Add";
            this.inputButton.UseVisualStyleBackColor = true;
            this.inputButton.Click += new System.EventHandler(this.inputButton_Click);
            // 
            // dataGridView_IM
            // 
            this.dataGridView_IM.AllowUserToAddRows = false;
            this.dataGridView_IM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_IM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_IM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_IM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IMData_string,
            this.IMData_tag,
            this.IMData_format,
            this.IMData_unit,
            this.IMData_color});
            this.dataGridView_IM.Location = new System.Drawing.Point(6, 143);
            this.dataGridView_IM.Name = "dataGridView_IM";
            this.dataGridView_IM.ReadOnly = true;
            this.dataGridView_IM.RowHeadersWidth = 61;
            this.dataGridView_IM.RowTemplate.Height = 24;
            this.dataGridView_IM.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_IM.Size = new System.Drawing.Size(963, 438);
            this.dataGridView_IM.TabIndex = 12;
            this.dataGridView_IM.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_IM_RowsRemoved);
            // 
            // IMData_string
            // 
            this.IMData_string.HeaderText = "字串";
            this.IMData_string.Name = "IMData_string";
            this.IMData_string.ReadOnly = true;
            // 
            // IMData_tag
            // 
            this.IMData_tag.HeaderText = "Node Name";
            this.IMData_tag.Name = "IMData_tag";
            this.IMData_tag.ReadOnly = true;
            // 
            // IMData_format
            // 
            this.IMData_format.HeaderText = "格式";
            this.IMData_format.Name = "IMData_format";
            this.IMData_format.ReadOnly = true;
            // 
            // IMData_unit
            // 
            this.IMData_unit.HeaderText = "單位";
            this.IMData_unit.Name = "IMData_unit";
            this.IMData_unit.ReadOnly = true;
            // 
            // IMData_color
            // 
            this.IMData_color.HeaderText = "顏色";
            this.IMData_color.Name = "IMData_color";
            this.IMData_color.ReadOnly = true;
            // 
            // PreviewResult
            // 
            this.PreviewResult.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PreviewResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviewResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.PreviewResult.Font = new System.Drawing.Font("細明體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.PreviewResult.ForeColor = System.Drawing.SystemColors.Window;
            this.PreviewResult.Location = new System.Drawing.Point(22, 88);
            this.PreviewResult.MaxLength = 30;
            this.PreviewResult.Name = "PreviewResult";
            this.PreviewResult.ReadOnly = true;
            this.PreviewResult.Size = new System.Drawing.Size(380, 36);
            this.PreviewResult.TabIndex = 11;
            // 
            // im_colorButton
            // 
            this.im_colorButton.BackColor = System.Drawing.Color.Black;
            this.im_colorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.im_colorButton.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.im_colorButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.im_colorButton.Location = new System.Drawing.Point(603, 41);
            this.im_colorButton.Name = "im_colorButton";
            this.im_colorButton.Size = new System.Drawing.Size(75, 23);
            this.im_colorButton.TabIndex = 10;
            this.im_colorButton.Text = "Color";
            this.im_colorButton.UseVisualStyleBackColor = false;
            this.im_colorButton.ForeColorChanged += new System.EventHandler(this.im_colorButton_ForeColorChanged);
            this.im_colorButton.Click += new System.EventHandler(this.im_colorButton_Click);
            // 
            // im_unit
            // 
            this.im_unit.Location = new System.Drawing.Point(485, 43);
            this.im_unit.Name = "im_unit";
            this.im_unit.Size = new System.Drawing.Size(100, 22);
            this.im_unit.TabIndex = 9;
            this.im_unit.TextChanged += new System.EventHandler(this.im_unit_TextChanged);
            // 
            // label_unit
            // 
            this.label_unit.AutoSize = true;
            this.label_unit.Location = new System.Drawing.Point(481, 28);
            this.label_unit.Name = "label_unit";
            this.label_unit.Size = new System.Drawing.Size(29, 12);
            this.label_unit.TabIndex = 8;
            this.label_unit.Text = "單位";
            // 
            // label_format
            // 
            this.label_format.AutoSize = true;
            this.label_format.Location = new System.Drawing.Point(346, 30);
            this.label_format.Name = "label_format";
            this.label_format.Size = new System.Drawing.Size(29, 12);
            this.label_format.TabIndex = 6;
            this.label_format.Text = "格式";
            // 
            // im_format
            // 
            this.im_format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.im_format.FormattingEnabled = true;
            this.im_format.Items.AddRange(new object[] {
            "####",
            "###.#",
            "##.##",
            "#.###"});
            this.im_format.Location = new System.Drawing.Point(346, 45);
            this.im_format.Name = "im_format";
            this.im_format.Size = new System.Drawing.Size(121, 20);
            this.im_format.TabIndex = 5;
            this.im_format.SelectedIndexChanged += new System.EventHandler(this.im_format_SelectedIndexChanged);
            // 
            // label_tag
            // 
            this.label_tag.AutoSize = true;
            this.label_tag.Location = new System.Drawing.Point(228, 30);
            this.label_tag.Name = "label_tag";
            this.label_tag.Size = new System.Drawing.Size(60, 12);
            this.label_tag.TabIndex = 4;
            this.label_tag.Text = "Node Name";
            // 
            // im_tag
            // 
            this.im_tag.Location = new System.Drawing.Point(228, 45);
            this.im_tag.Name = "im_tag";
            this.im_tag.Size = new System.Drawing.Size(100, 22);
            this.im_tag.TabIndex = 3;
            this.im_tag.TextChanged += new System.EventHandler(this.im_tag_TextChanged);
            // 
            // label_string
            // 
            this.label_string.AutoSize = true;
            this.label_string.Location = new System.Drawing.Point(22, 30);
            this.label_string.Name = "label_string";
            this.label_string.Size = new System.Drawing.Size(29, 12);
            this.label_string.TabIndex = 2;
            this.label_string.Text = "字串";
            // 
            // im_string
            // 
            this.im_string.Location = new System.Drawing.Point(22, 45);
            this.im_string.MaxLength = 30;
            this.im_string.Name = "im_string";
            this.im_string.Size = new System.Drawing.Size(188, 22);
            this.im_string.TabIndex = 1;
            this.im_string.TextChanged += new System.EventHandler(this.im_string_TextChanged);
            // 
            // im_colorDialog
            // 
            this.im_colorDialog.AllowFullOpen = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 708);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.Setting);
            this.Name = "SettingForm";
            this.Text = "字幕機設定";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.Setting.ResumeLayout(false);
            this.LEDScreenSetting.ResumeLayout(false);
            this.groupBox_net.ResumeLayout(false);
            this.groupBox_net.PerformLayout();
            this.IMSetting.ResumeLayout(false);
            this.IMSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_IM)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Setting;
        private System.Windows.Forms.TabPage LEDScreenSetting;
        private System.Windows.Forms.TabPage AlarmSetting;
        private System.Windows.Forms.TabPage IMSetting;
        private System.Windows.Forms.TextBox im_unit;
        private System.Windows.Forms.Label label_unit;
        private System.Windows.Forms.Label label_format;
        private System.Windows.Forms.ComboBox im_format;
        private System.Windows.Forms.Label label_tag;
        private System.Windows.Forms.TextBox im_tag;
        private System.Windows.Forms.Label label_string;
        private System.Windows.Forms.TextBox im_string;
        private System.Windows.Forms.ColorDialog im_colorDialog;
        private System.Windows.Forms.Button im_colorButton;
        private System.Windows.Forms.TextBox PreviewResult;
        private System.Windows.Forms.DataGridView dataGridView_IM;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button inputButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMData_string;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMData_tag;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMData_format;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMData_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMData_color;
        private System.Windows.Forms.Button button_save;
        private System.ComponentModel.BackgroundWorker EDABackgroundWorker;
        private System.Windows.Forms.Label label_idcode;
        private System.Windows.Forms.TextBox textBox_idcode;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label_timeout;
        private System.Windows.Forms.TextBox textBox_timeout;
        private System.Windows.Forms.GroupBox groupBox_net;
        private System.Windows.Forms.Button button_ping;
        private System.Windows.Forms.Label label_help;

    }
}

