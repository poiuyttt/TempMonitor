namespace TempMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelTemp = new System.Windows.Forms.Panel();
            this.lblTempLabel = new System.Windows.Forms.Label();
            this.lblTempValue = new System.Windows.Forms.Label();
            this.panelHumid = new System.Windows.Forms.Panel();
            this.lblHumidLabel = new System.Windows.Forms.Label();
            this.lblHumidValue = new System.Windows.Forms.Label();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.chartTemp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnHistory = new System.Windows.Forms.Button();
            this.panelTemp.SuspendLayout();
            this.panelHumid.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTemp
            // 
            this.panelTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTemp.Controls.Add(this.lblTempLabel);
            this.panelTemp.Controls.Add(this.lblTempValue);
            this.panelTemp.Location = new System.Drawing.Point(32, 20);
            this.panelTemp.Name = "panelTemp";
            this.panelTemp.Size = new System.Drawing.Size(147, 100);
            this.panelTemp.TabIndex = 0;
            // 
            // lblTempLabel
            // 
            this.lblTempLabel.AutoSize = true;
            this.lblTempLabel.Location = new System.Drawing.Point(52, 63);
            this.lblTempLabel.Name = "lblTempLabel";
            this.lblTempLabel.Size = new System.Drawing.Size(47, 12);
            this.lblTempLabel.TabIndex = 1;
            this.lblTempLabel.Text = "温度 ℃";
            // 
            // lblTempValue
            // 
            this.lblTempValue.AutoSize = true;
            this.lblTempValue.Font = new System.Drawing.Font("宋体", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTempValue.Location = new System.Drawing.Point(11, 12);
            this.lblTempValue.Name = "lblTempValue";
            this.lblTempValue.Size = new System.Drawing.Size(131, 38);
            this.lblTempValue.TabIndex = 0;
            this.lblTempValue.Text = "label1";
            // 
            // panelHumid
            // 
            this.panelHumid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHumid.Controls.Add(this.lblHumidLabel);
            this.panelHumid.Controls.Add(this.lblHumidValue);
            this.panelHumid.Location = new System.Drawing.Point(252, 20);
            this.panelHumid.Name = "panelHumid";
            this.panelHumid.Size = new System.Drawing.Size(147, 100);
            this.panelHumid.TabIndex = 1;
            // 
            // lblHumidLabel
            // 
            this.lblHumidLabel.AutoSize = true;
            this.lblHumidLabel.Location = new System.Drawing.Point(49, 63);
            this.lblHumidLabel.Name = "lblHumidLabel";
            this.lblHumidLabel.Size = new System.Drawing.Size(41, 12);
            this.lblHumidLabel.TabIndex = 1;
            this.lblHumidLabel.Text = "湿度 %";
            // 
            // lblHumidValue
            // 
            this.lblHumidValue.AutoSize = true;
            this.lblHumidValue.Font = new System.Drawing.Font("宋体", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHumidValue.Location = new System.Drawing.Point(11, 12);
            this.lblHumidValue.Name = "lblHumidValue";
            this.lblHumidValue.Size = new System.Drawing.Size(131, 38);
            this.lblHumidValue.TabIndex = 0;
            this.lblHumidValue.Text = "label3";
            // 
            // panelStatus
            // 
            this.panelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatus.Controls.Add(this.lblStatusLabel);
            this.panelStatus.Controls.Add(this.lblStatusValue);
            this.panelStatus.Location = new System.Drawing.Point(470, 20);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(147, 100);
            this.panelStatus.TabIndex = 2;
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Location = new System.Drawing.Point(52, 63);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(53, 12);
            this.lblStatusLabel.TabIndex = 1;
            this.lblStatusLabel.Text = "运行状态";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatusValue.Location = new System.Drawing.Point(34, 26);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(76, 22);
            this.lblStatusValue.TabIndex = 0;
            this.lblStatusValue.Text = "label5";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.panelTemp);
            this.panelTop.Controls.Add(this.panelHumid);
            this.panelTop.Controls.Add(this.panelStatus);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(645, 120);
            this.panelTop.TabIndex = 4;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnHistory);
            this.panelBottom.Controls.Add(this.statusStrip1);
            this.panelBottom.Controls.Add(this.txtLog);
            this.panelBottom.Controls.Add(this.btnSettings);
            this.panelBottom.Controls.Add(this.btnExport);
            this.panelBottom.Controls.Add(this.btnStop);
            this.panelBottom.Controls.Add(this.btnStart);
            this.panelBottom.Controls.Add(this.cmbBaudRate);
            this.panelBottom.Controls.Add(this.cmbPortName);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 494);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(645, 140);
            this.panelBottom.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblConnection,
            this.lblTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 118);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(645, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblConnection
            // 
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(44, 17);
            this.lblConnection.Text = "未连接";
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(586, 17);
            this.lblTime.Spring = true;
            this.lblTime.Text = "00:00:00";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(0, 35);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(500, 80);
            this.txtLog.TabIndex = 6;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(558, 55);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "设置";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(470, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(380, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止采集";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(287, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始采集";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(142, 6);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(121, 20);
            this.cmbBaudRate.TabIndex = 1;
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Location = new System.Drawing.Point(12, 6);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(121, 20);
            this.cmbPortName.TabIndex = 0;
            // 
            // chartTemp
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTemp.ChartAreas.Add(chartArea2);
            this.chartTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartTemp.Legends.Add(legend2);
            this.chartTemp.Location = new System.Drawing.Point(0, 120);
            this.chartTemp.Name = "chartTemp";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartTemp.Series.Add(series2);
            this.chartTemp.Size = new System.Drawing.Size(645, 374);
            this.chartTemp.TabIndex = 6;
            this.chartTemp.Text = "chart1";
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(558, 4);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(75, 23);
            this.btnHistory.TabIndex = 8;
            this.btnHistory.Text = "历史数据";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 634);
            this.Controls.Add(this.chartTemp);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "MainForm";
            this.Text = "TempMonitor — 温湿度监控系统";
            this.panelTemp.ResumeLayout(false);
            this.panelTemp.PerformLayout();
            this.panelHumid.ResumeLayout(false);
            this.panelHumid.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTemp;
        private System.Windows.Forms.Panel panelHumid;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblTempLabel;
        private System.Windows.Forms.Label lblTempValue;
        private System.Windows.Forms.Label lblHumidLabel;
        private System.Windows.Forms.Label lblHumidValue;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemp;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblConnection;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.Button btnHistory;
    }
}

