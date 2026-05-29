namespace TempMonitor.Presentation
{
    partial class SettingsForm
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
            this.grpAlarm = new System.Windows.Forms.GroupBox();
            this.grpSerial = new System.Windows.Forms.GroupBox();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numTempHigh = new System.Windows.Forms.NumericUpDown();
            this.numTempLow = new System.Windows.Forms.NumericUpDown();
            this.numHumidHigh = new System.Windows.Forms.NumericUpDown();
            this.numHumidLow = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.listUsers = new System.Windows.Forms.ListBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.grpAlarm.SuspendLayout();
            this.grpSerial.SuspendLayout();
            this.grpUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTempLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHumidHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHumidLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAlarm
            // 
            this.grpAlarm.Controls.Add(this.numHumidLow);
            this.grpAlarm.Controls.Add(this.numHumidHigh);
            this.grpAlarm.Controls.Add(this.numTempLow);
            this.grpAlarm.Controls.Add(this.numTempHigh);
            this.grpAlarm.Controls.Add(this.label4);
            this.grpAlarm.Controls.Add(this.label3);
            this.grpAlarm.Controls.Add(this.label2);
            this.grpAlarm.Controls.Add(this.label1);
            this.grpAlarm.Location = new System.Drawing.Point(40, 81);
            this.grpAlarm.Name = "grpAlarm";
            this.grpAlarm.Size = new System.Drawing.Size(298, 96);
            this.grpAlarm.TabIndex = 0;
            this.grpAlarm.TabStop = false;
            this.grpAlarm.Text = "报警阈值";
            // 
            // grpSerial
            // 
            this.grpSerial.Controls.Add(this.numInterval);
            this.grpSerial.Controls.Add(this.cmbPortName);
            this.grpSerial.Controls.Add(this.label6);
            this.grpSerial.Controls.Add(this.label5);
            this.grpSerial.Location = new System.Drawing.Point(40, 203);
            this.grpSerial.Name = "grpSerial";
            this.grpSerial.Size = new System.Drawing.Size(298, 73);
            this.grpSerial.TabIndex = 1;
            this.grpSerial.TabStop = false;
            this.grpSerial.Text = "串口参数";
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.btnAddUser);
            this.grpUser.Controls.Add(this.listUsers);
            this.grpUser.Location = new System.Drawing.Point(40, 313);
            this.grpUser.Name = "grpUser";
            this.grpUser.Size = new System.Drawing.Size(298, 107);
            this.grpUser.TabIndex = 2;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "用户管理：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(86, 456);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(186, 455);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "温度上限：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "温度下限：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "湿度上限：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "湿度下限：";
            // 
            // numTempHigh
            // 
            this.numTempHigh.DecimalPlaces = 1;
            this.numTempHigh.Location = new System.Drawing.Point(89, 19);
            this.numTempHigh.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numTempHigh.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            this.numTempHigh.Name = "numTempHigh";
            this.numTempHigh.Size = new System.Drawing.Size(49, 21);
            this.numTempHigh.TabIndex = 4;
            // 
            // numTempLow
            // 
            this.numTempLow.DecimalPlaces = 1;
            this.numTempLow.Location = new System.Drawing.Point(220, 20);
            this.numTempLow.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numTempLow.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            this.numTempLow.Name = "numTempLow";
            this.numTempLow.Size = new System.Drawing.Size(49, 21);
            this.numTempLow.TabIndex = 5;
            // 
            // numHumidHigh
            // 
            this.numHumidHigh.DecimalPlaces = 1;
            this.numHumidHigh.Location = new System.Drawing.Point(89, 54);
            this.numHumidHigh.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numHumidHigh.Name = "numHumidHigh";
            this.numHumidHigh.Size = new System.Drawing.Size(49, 21);
            this.numHumidHigh.TabIndex = 6;
            // 
            // numHumidLow
            // 
            this.numHumidLow.DecimalPlaces = 1;
            this.numHumidLow.Location = new System.Drawing.Point(220, 54);
            this.numHumidLow.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numHumidLow.Name = "numHumidLow";
            this.numHumidLow.Size = new System.Drawing.Size(49, 21);
            this.numHumidLow.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "串口号：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(144, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "采集间隔：";
            // 
            // cmbPortName
            // 
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Location = new System.Drawing.Point(86, 26);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(52, 20);
            this.cmbPortName.TabIndex = 2;
            // 
            // numInterval
            // 
            this.numInterval.Location = new System.Drawing.Point(212, 27);
            this.numInterval.Maximum = new decimal(new int[] { 3600000, 0, 0, 0 });
            this.numInterval.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(57, 21);
            this.numInterval.TabIndex = 3;
            // 
            // listUsers
            // 
            this.listUsers.FormattingEnabled = true;
            this.listUsers.ItemHeight = 12;
            this.listUsers.Location = new System.Drawing.Point(86, 30);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(120, 40);
            this.listUsers.TabIndex = 0;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(108, 78);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 1;
            this.btnAddUser.Text = "添加用户";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 529);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.grpSerial);
            this.Controls.Add(this.grpAlarm);
            this.Name = "SettingsForm";
            this.Text = "设置";
            this.grpAlarm.ResumeLayout(false);
            this.grpAlarm.PerformLayout();
            this.grpSerial.ResumeLayout(false);
            this.grpSerial.PerformLayout();
            this.grpUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTempHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTempLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHumidHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHumidLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAlarm;
        private System.Windows.Forms.GroupBox grpSerial;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numTempHigh;
        private System.Windows.Forms.NumericUpDown numHumidLow;
        private System.Windows.Forms.NumericUpDown numHumidHigh;
        private System.Windows.Forms.NumericUpDown numTempLow;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ListBox listUsers;
    }
}