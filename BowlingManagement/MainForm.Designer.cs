namespace BowlingManagement
{
    partial class MainForm
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
      this.txtConnectionString = new System.Windows.Forms.TextBox();
      this.lblDatabase = new System.Windows.Forms.Label();
      this.tabMain = new System.Windows.Forms.TabControl();
      this.tpagControl = new System.Windows.Forms.TabPage();
      this.btnLoadSeason = new System.Windows.Forms.Button();
      this.txtSQLOutputPath = new System.Windows.Forms.TextBox();
      this.lblSQLlog = new System.Windows.Forms.Label();
      this.lblLogPath = new System.Windows.Forms.Label();
      this.txtLogPath = new System.Windows.Forms.TextBox();
      this.chkClearLogs = new System.Windows.Forms.CheckBox();
      this.btnValidateSeason = new System.Windows.Forms.Button();
      this.chkCurrentSeason = new System.Windows.Forms.CheckBox();
      this.lblSeasonYear = new System.Windows.Forms.Label();
      this.txtSeasonYear = new System.Windows.Forms.TextBox();
      this.tpagStatus = new System.Windows.Forms.TabPage();
      this.lvwStatus = new System.Windows.Forms.ListView();
      this.colLogMessages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tpagFixSQL = new System.Windows.Forms.TabPage();
      this.btnOpen = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.bgwLoad = new System.ComponentModel.BackgroundWorker();
      this.bgwValidate = new System.ComponentModel.BackgroundWorker();
      this.lvwFixSQL = new System.Windows.Forms.ListView();
      this.colSQL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.tabMain.SuspendLayout();
      this.tpagControl.SuspendLayout();
      this.tpagStatus.SuspendLayout();
      this.tpagFixSQL.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtConnectionString
      // 
      this.txtConnectionString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
      this.txtConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtConnectionString.Location = new System.Drawing.Point(49, 116);
      this.txtConnectionString.Margin = new System.Windows.Forms.Padding(2);
      this.txtConnectionString.Name = "txtConnectionString";
      this.txtConnectionString.Size = new System.Drawing.Size(1666, 48);
      this.txtConnectionString.TabIndex = 4;
      this.txtConnectionString.TextChanged += new System.EventHandler(this.txtConnectionString_TextChanged);
      // 
      // lblDatabase
      // 
      this.lblDatabase.AutoSize = true;
      this.lblDatabase.Location = new System.Drawing.Point(61, 38);
      this.lblDatabase.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblDatabase.Name = "lblDatabase";
      this.lblDatabase.Size = new System.Drawing.Size(272, 29);
      this.lblDatabase.TabIndex = 5;
      this.lblDatabase.Text = "Connect to database at:-";
      // 
      // tabMain
      // 
      this.tabMain.Controls.Add(this.tpagControl);
      this.tabMain.Controls.Add(this.tpagStatus);
      this.tabMain.Controls.Add(this.tpagFixSQL);
      this.tabMain.Location = new System.Drawing.Point(49, 192);
      this.tabMain.Margin = new System.Windows.Forms.Padding(2);
      this.tabMain.Name = "tabMain";
      this.tabMain.SelectedIndex = 0;
      this.tabMain.Size = new System.Drawing.Size(1666, 796);
      this.tabMain.TabIndex = 9;
      // 
      // tpagControl
      // 
      this.tpagControl.Controls.Add(this.btnLoadSeason);
      this.tpagControl.Controls.Add(this.txtSQLOutputPath);
      this.tpagControl.Controls.Add(this.lblSQLlog);
      this.tpagControl.Controls.Add(this.lblLogPath);
      this.tpagControl.Controls.Add(this.txtLogPath);
      this.tpagControl.Controls.Add(this.chkClearLogs);
      this.tpagControl.Controls.Add(this.btnValidateSeason);
      this.tpagControl.Controls.Add(this.chkCurrentSeason);
      this.tpagControl.Controls.Add(this.lblSeasonYear);
      this.tpagControl.Controls.Add(this.txtSeasonYear);
      this.tpagControl.Location = new System.Drawing.Point(10, 47);
      this.tpagControl.Margin = new System.Windows.Forms.Padding(2);
      this.tpagControl.Name = "tpagControl";
      this.tpagControl.Padding = new System.Windows.Forms.Padding(2);
      this.tpagControl.Size = new System.Drawing.Size(1646, 739);
      this.tpagControl.TabIndex = 0;
      this.tpagControl.Text = "Control";
      this.tpagControl.UseVisualStyleBackColor = true;
      // 
      // btnLoadSeason
      // 
      this.btnLoadSeason.Enabled = false;
      this.btnLoadSeason.Location = new System.Drawing.Point(47, 560);
      this.btnLoadSeason.Margin = new System.Windows.Forms.Padding(2);
      this.btnLoadSeason.Name = "btnLoadSeason";
      this.btnLoadSeason.Size = new System.Drawing.Size(301, 129);
      this.btnLoadSeason.TabIndex = 18;
      this.btnLoadSeason.Text = "Load Season";
      this.btnLoadSeason.UseVisualStyleBackColor = true;
      this.btnLoadSeason.Click += new System.EventHandler(this.btnLoadSeason_Click);
      // 
      // txtSQLOutputPath
      // 
      this.txtSQLOutputPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtSQLOutputPath.Location = new System.Drawing.Point(47, 261);
      this.txtSQLOutputPath.Margin = new System.Windows.Forms.Padding(2);
      this.txtSQLOutputPath.Name = "txtSQLOutputPath";
      this.txtSQLOutputPath.Size = new System.Drawing.Size(1541, 48);
      this.txtSQLOutputPath.TabIndex = 17;
      this.txtSQLOutputPath.TextChanged += new System.EventHandler(this.txtSQLOutputPath_TextChanged);
      // 
      // lblSQLlog
      // 
      this.lblSQLlog.AutoSize = true;
      this.lblSQLlog.Location = new System.Drawing.Point(42, 196);
      this.lblSQLlog.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblSQLlog.Name = "lblSQLlog";
      this.lblSQLlog.Size = new System.Drawing.Size(209, 29);
      this.lblSQLlog.TabIndex = 16;
      this.lblSQLlog.Text = "Fix SQL output file";
      // 
      // lblLogPath
      // 
      this.lblLogPath.AutoSize = true;
      this.lblLogPath.Location = new System.Drawing.Point(42, 45);
      this.lblLogPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblLogPath.Name = "lblLogPath";
      this.lblLogPath.Size = new System.Drawing.Size(108, 29);
      this.lblLogPath.TabIndex = 15;
      this.lblLogPath.Text = "Log Path";
      // 
      // txtLogPath
      // 
      this.txtLogPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtLogPath.Location = new System.Drawing.Point(47, 100);
      this.txtLogPath.Margin = new System.Windows.Forms.Padding(2);
      this.txtLogPath.Name = "txtLogPath";
      this.txtLogPath.Size = new System.Drawing.Size(1541, 48);
      this.txtLogPath.TabIndex = 14;
      this.txtLogPath.TextChanged += new System.EventHandler(this.txtLogPath_TextChanged);
      // 
      // chkClearLogs
      // 
      this.chkClearLogs.AutoSize = true;
      this.chkClearLogs.Location = new System.Drawing.Point(782, 660);
      this.chkClearLogs.Margin = new System.Windows.Forms.Padding(2);
      this.chkClearLogs.Name = "chkClearLogs";
      this.chkClearLogs.Size = new System.Drawing.Size(396, 33);
      this.chkClearLogs.TabIndex = 13;
      this.chkClearLogs.Text = "Clear log and fix SQL on new run";
      this.chkClearLogs.UseVisualStyleBackColor = true;
      // 
      // btnValidateSeason
      // 
      this.btnValidateSeason.Enabled = false;
      this.btnValidateSeason.Location = new System.Drawing.Point(392, 560);
      this.btnValidateSeason.Margin = new System.Windows.Forms.Padding(2);
      this.btnValidateSeason.Name = "btnValidateSeason";
      this.btnValidateSeason.Size = new System.Drawing.Size(294, 134);
      this.btnValidateSeason.TabIndex = 12;
      this.btnValidateSeason.Text = "Validate Season";
      this.btnValidateSeason.UseVisualStyleBackColor = true;
      this.btnValidateSeason.Click += new System.EventHandler(this.btnValidateSeason_Click);
      // 
      // chkCurrentSeason
      // 
      this.chkCurrentSeason.AutoSize = true;
      this.chkCurrentSeason.Location = new System.Drawing.Point(392, 448);
      this.chkCurrentSeason.Margin = new System.Windows.Forms.Padding(2);
      this.chkCurrentSeason.Name = "chkCurrentSeason";
      this.chkCurrentSeason.Size = new System.Drawing.Size(252, 33);
      this.chkCurrentSeason.TabIndex = 11;
      this.chkCurrentSeason.Text = "Use current season";
      this.chkCurrentSeason.UseVisualStyleBackColor = true;
      this.chkCurrentSeason.CheckedChanged += new System.EventHandler(this.chkCurrentSeason_CheckedChanged);
      // 
      // lblSeasonYear
      // 
      this.lblSeasonYear.AutoSize = true;
      this.lblSeasonYear.Location = new System.Drawing.Point(42, 384);
      this.lblSeasonYear.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.lblSeasonYear.Name = "lblSeasonYear";
      this.lblSeasonYear.Size = new System.Drawing.Size(152, 29);
      this.lblSeasonYear.TabIndex = 10;
      this.lblSeasonYear.Text = "Season Year";
      // 
      // txtSeasonYear
      // 
      this.txtSeasonYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtSeasonYear.Location = new System.Drawing.Point(47, 435);
      this.txtSeasonYear.Margin = new System.Windows.Forms.Padding(2);
      this.txtSeasonYear.Name = "txtSeasonYear";
      this.txtSeasonYear.Size = new System.Drawing.Size(298, 48);
      this.txtSeasonYear.TabIndex = 9;
      // 
      // tpagStatus
      // 
      this.tpagStatus.Controls.Add(this.lvwStatus);
      this.tpagStatus.Location = new System.Drawing.Point(10, 47);
      this.tpagStatus.Margin = new System.Windows.Forms.Padding(2);
      this.tpagStatus.Name = "tpagStatus";
      this.tpagStatus.Padding = new System.Windows.Forms.Padding(2);
      this.tpagStatus.Size = new System.Drawing.Size(1646, 739);
      this.tpagStatus.TabIndex = 1;
      this.tpagStatus.Text = "Status";
      this.tpagStatus.UseVisualStyleBackColor = true;
      // 
      // lvwStatus
      // 
      this.lvwStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLogMessages});
      this.lvwStatus.Location = new System.Drawing.Point(28, 16);
      this.lvwStatus.Margin = new System.Windows.Forms.Padding(2);
      this.lvwStatus.Name = "lvwStatus";
      this.lvwStatus.Size = new System.Drawing.Size(1593, 682);
      this.lvwStatus.TabIndex = 0;
      this.lvwStatus.UseCompatibleStateImageBehavior = false;
      this.lvwStatus.View = System.Windows.Forms.View.Details;
      // 
      // colLogMessages
      // 
      this.colLogMessages.Text = "Log Messages";
      this.colLogMessages.Width = 3000;
      // 
      // tpagFixSQL
      // 
      this.tpagFixSQL.Controls.Add(this.lvwFixSQL);
      this.tpagFixSQL.Location = new System.Drawing.Point(10, 47);
      this.tpagFixSQL.Margin = new System.Windows.Forms.Padding(2);
      this.tpagFixSQL.Name = "tpagFixSQL";
      this.tpagFixSQL.Size = new System.Drawing.Size(1646, 739);
      this.tpagFixSQL.TabIndex = 2;
      this.tpagFixSQL.Text = "Fix SQL";
      this.tpagFixSQL.UseVisualStyleBackColor = true;
      // 
      // btnOpen
      // 
      this.btnOpen.Location = new System.Drawing.Point(1489, 24);
      this.btnOpen.Margin = new System.Windows.Forms.Padding(2);
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(107, 56);
      this.btnOpen.TabIndex = 10;
      this.btnOpen.Text = "Open";
      this.btnOpen.UseVisualStyleBackColor = true;
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(1601, 24);
      this.btnClose.Margin = new System.Windows.Forms.Padding(2);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(114, 56);
      this.btnClose.TabIndex = 11;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // bgwLoad
      // 
      this.bgwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoad_DoWork);
      this.bgwLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoad_RunWorkerCompleted);
      // 
      // bgwValidate
      // 
      this.bgwValidate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwValidate_DoWork);
      this.bgwValidate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwValidate_RunWorkerCompleted);
      // 
      // lvwFixSQL
      // 
      this.lvwFixSQL.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSQL});
      this.lvwFixSQL.Location = new System.Drawing.Point(22, 17);
      this.lvwFixSQL.Margin = new System.Windows.Forms.Padding(2);
      this.lvwFixSQL.Name = "lvwFixSQL";
      this.lvwFixSQL.Size = new System.Drawing.Size(1597, 682);
      this.lvwFixSQL.TabIndex = 1;
      this.lvwFixSQL.UseCompatibleStateImageBehavior = false;
      this.lvwFixSQL.View = System.Windows.Forms.View.Details;
      // 
      // colSQL
      // 
      this.colSQL.Text = "Log Fix SQL";
      this.colSQL.Width = 3000;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(216F, 216F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(1740, 1024);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnOpen);
      this.Controls.Add(this.tabMain);
      this.Controls.Add(this.lblDatabase);
      this.Controls.Add(this.txtConnectionString);
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "MainForm";
      this.Text = "Bowling Management";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.tabMain.ResumeLayout(false);
      this.tpagControl.ResumeLayout(false);
      this.tpagControl.PerformLayout();
      this.tpagStatus.ResumeLayout(false);
      this.tpagFixSQL.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpagControl;
        private System.Windows.Forms.TextBox txtSQLOutputPath;
        private System.Windows.Forms.Label lblSQLlog;
        private System.Windows.Forms.Label lblLogPath;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.CheckBox chkClearLogs;
        private System.Windows.Forms.Button btnValidateSeason;
        private System.Windows.Forms.CheckBox chkCurrentSeason;
        private System.Windows.Forms.Label lblSeasonYear;
        private System.Windows.Forms.TextBox txtSeasonYear;
        private System.Windows.Forms.TabPage tpagStatus;
        private System.Windows.Forms.ListView lvwStatus;
        private System.Windows.Forms.TabPage tpagFixSQL;
        private System.Windows.Forms.Button btnOpen;
    private System.Windows.Forms.Button btnLoadSeason;
    private System.Windows.Forms.Button btnClose;
    private System.ComponentModel.BackgroundWorker bgwLoad;
    private System.ComponentModel.BackgroundWorker bgwValidate;
    private System.Windows.Forms.ColumnHeader colLogMessages;
    private System.Windows.Forms.ListView lvwFixSQL;
    private System.Windows.Forms.ColumnHeader colSQL;
  }
}

