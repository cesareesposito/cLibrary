namespace cLibrary.WinCustomControl.LogProcess
{
    partial class cFrmLogProgress
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlProgress = new cLibrary.WinCustomControl.cPanel(this.components);
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.pnlLogSelection = new cLibrary.WinCustomControl.cPanel(this.components);
            this.cbxActivity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxSeverity = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTop = new cLibrary.WinCustomControl.cPanel(this.components);
            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.btnChiudi = new cLibrary.WinCustomControl.cButton();
            this.bgw_ExecuteTask = new System.ComponentModel.BackgroundWorker();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnStampa = new cLibrary.WinCustomControl.cButton();
            this.tlpMain.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.pnlLogSelection.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Silver;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlProgress, 0, 1);
            this.tlpMain.Controls.Add(this.pnlLogSelection, 0, 2);
            this.tlpMain.Controls.Add(this.pnlTop, 0, 0);
            this.tlpMain.Controls.Add(this.dgvLog, 0, 3);
            this.tlpMain.Controls.Add(this.pnlFooter, 0, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(956, 495);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlProgress
            // 
            this.pnlProgress.BackColor = System.Drawing.Color.Silver;
            this.pnlProgress.BackColor2 = System.Drawing.Color.Gainsboro;
            this.pnlProgress.Controls.Add(this.pBar);
            this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProgress.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.pnlProgress.Location = new System.Drawing.Point(3, 33);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(950, 21);
            this.pnlProgress.TabIndex = 10;
            // 
            // pBar
            // 
            this.pBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pBar.Location = new System.Drawing.Point(3, 0);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(944, 21);
            this.pBar.TabIndex = 9;
            // 
            // pnlLogSelection
            // 
            this.pnlLogSelection.BackColor = System.Drawing.Color.Silver;
            this.pnlLogSelection.BackColor2 = System.Drawing.Color.Gainsboro;
            this.pnlLogSelection.Controls.Add(this.cbxActivity);
            this.pnlLogSelection.Controls.Add(this.label3);
            this.pnlLogSelection.Controls.Add(this.label2);
            this.pnlLogSelection.Controls.Add(this.cbxSeverity);
            this.pnlLogSelection.Controls.Add(this.label1);
            this.pnlLogSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogSelection.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.pnlLogSelection.Location = new System.Drawing.Point(3, 60);
            this.pnlLogSelection.Name = "pnlLogSelection";
            this.pnlLogSelection.Size = new System.Drawing.Size(950, 28);
            this.pnlLogSelection.TabIndex = 2;
            // 
            // cbxActivity
            // 
            this.cbxActivity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActivity.FormattingEnabled = true;
            this.cbxActivity.Items.AddRange(new object[] {
            "(tutto)"});
            this.cbxActivity.Location = new System.Drawing.Point(173, 6);
            this.cbxActivity.Name = "cbxActivity";
            this.cbxActivity.Size = new System.Drawing.Size(121, 21);
            this.cbxActivity.Sorted = true;
            this.cbxActivity.TabIndex = 4;
            this.cbxActivity.SelectedIndexChanged += new System.EventHandler(this.cbxActivity_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(125, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Activity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(317, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Severity";
            // 
            // cbxSeverity
            // 
            this.cbxSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSeverity.FormattingEnabled = true;
            this.cbxSeverity.Items.AddRange(new object[] {
            "(tutto)",
            "Error",
            "Warning",
            "Message"});
            this.cbxSeverity.Location = new System.Drawing.Point(372, 6);
            this.cbxSeverity.Name = "cbxSeverity";
            this.cbxSeverity.Size = new System.Drawing.Size(121, 21);
            this.cbxSeverity.TabIndex = 1;
            this.cbxSeverity.SelectedIndexChanged += new System.EventHandler(this.cbxSeverity_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Criteri di ricerca";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Silver;
            this.pnlTop.BackColor2 = System.Drawing.Color.Gainsboro;
            this.pnlTop.Controls.Add(this.txtTaskTitle);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTop.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(950, 24);
            this.pnlTop.TabIndex = 0;
            // 
            // txtTaskTitle
            // 
            this.txtTaskTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTaskTitle.BackColor = System.Drawing.Color.LightGray;
            this.txtTaskTitle.Enabled = false;
            this.txtTaskTitle.Location = new System.Drawing.Point(151, 2);
            this.txtTaskTitle.Name = "txtTaskTitle";
            this.txtTaskTitle.Size = new System.Drawing.Size(790, 20);
            this.txtTaskTitle.TabIndex = 18;
            this.txtTaskTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTaskTitle.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(142, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Task in esecuzione";
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLog.Location = new System.Drawing.Point(3, 94);
            this.dgvLog.MultiSelect = false;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            this.dgvLog.RowHeadersVisible = false;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(950, 361);
            this.dgvLog.TabIndex = 3;
            // 
            // btnChiudi
            // 
            this.btnChiudi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChiudi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnChiudi.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChiudi.Enabled = false;
            this.btnChiudi.FocusRectangleEnabled = true;
            this.btnChiudi.Image = null;
            this.btnChiudi.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.btnChiudi.ImageBorderEnabled = true;
            this.btnChiudi.ImageDropShadow = true;
            this.btnChiudi.ImageFocused = null;
            this.btnChiudi.ImageInactive = null;
            this.btnChiudi.ImageMouseOver = null;
            this.btnChiudi.ImageNormal = null;
            this.btnChiudi.ImagePressed = null;
            this.btnChiudi.InnerBorderColor = System.Drawing.Color.LightGray;
            this.btnChiudi.InnerBorderColor_Focus = System.Drawing.Color.LightBlue;
            this.btnChiudi.InnerBorderColor_MouseOver = System.Drawing.Color.Gold;
            this.btnChiudi.Location = new System.Drawing.Point(866, 4);
            this.btnChiudi.Name = "btnChiudi";
            this.btnChiudi.OffsetPressedContent = true;
            this.btnChiudi.Size = new System.Drawing.Size(75, 23);
            this.btnChiudi.SizeToLabel = false;
            this.btnChiudi.StretchImage = false;
            this.btnChiudi.TabIndex = 4;
            this.btnChiudi.Text = "CHIUDI";
            this.btnChiudi.TextDropShadow = true;
            this.btnChiudi.UseVisualStyleBackColor = false;
            this.btnChiudi.Click += new System.EventHandler(this.btnChiudi_Click);
            // 
            // bgw_ExecuteTask
            // 
            this.bgw_ExecuteTask.WorkerReportsProgress = true;
            this.bgw_ExecuteTask.WorkerSupportsCancellation = true;
            this.bgw_ExecuteTask.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_ExecuteTask_DoWork);
            this.bgw_ExecuteTask.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_ExecuteTask_ProgressChanged);
            this.bgw_ExecuteTask.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_ExecuteTask_RunWorkerCompleted);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnStampa);
            this.pnlFooter.Controls.Add(this.btnChiudi);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFooter.Location = new System.Drawing.Point(3, 461);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(950, 31);
            this.pnlFooter.TabIndex = 11;
            // 
            // btnStampa
            // 
            this.btnStampa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStampa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnStampa.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStampa.Enabled = false;
            this.btnStampa.FocusRectangleEnabled = true;
            this.btnStampa.Image = null;
            this.btnStampa.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.btnStampa.ImageBorderEnabled = true;
            this.btnStampa.ImageDropShadow = true;
            this.btnStampa.ImageFocused = null;
            this.btnStampa.ImageInactive = null;
            this.btnStampa.ImageMouseOver = null;
            this.btnStampa.ImageNormal = null;
            this.btnStampa.ImagePressed = null;
            this.btnStampa.InnerBorderColor = System.Drawing.Color.LightGray;
            this.btnStampa.InnerBorderColor_Focus = System.Drawing.Color.LightBlue;
            this.btnStampa.InnerBorderColor_MouseOver = System.Drawing.Color.Gold;
            this.btnStampa.Location = new System.Drawing.Point(392, 4);
            this.btnStampa.Name = "btnStampa";
            this.btnStampa.OffsetPressedContent = true;
            this.btnStampa.Size = new System.Drawing.Size(166, 23);
            this.btnStampa.SizeToLabel = false;
            this.btnStampa.StretchImage = false;
            this.btnStampa.TabIndex = 5;
            this.btnStampa.Text = "STAMPA";
            this.btnStampa.TextDropShadow = true;
            this.btnStampa.UseVisualStyleBackColor = false;
            this.btnStampa.Click += new System.EventHandler(this.btnStampa_Click);
            // 
            // cFrmLogProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 495);
            this.Controls.Add(this.tlpMain);
            this.MinimizeBox = false;
            this.Name = "cFrmLogProgress";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Log delle operazioni in corso";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cFrmLogProgress_FormClosing);
            this.Shown += new System.EventHandler(this.cFrmLogProgress_Shown);
            this.tlpMain.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlLogSelection.ResumeLayout(false);
            this.pnlLogSelection.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.ComponentModel.BackgroundWorker bgw_ExecuteTask;
        private cPanel pnlLogSelection;
        private System.Windows.Forms.Label label1;
        private cPanel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvLog;
        private System.Windows.Forms.ComboBox cbxSeverity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxActivity;
        private System.Windows.Forms.Label label3;
        private cButton btnChiudi;
        private cPanel pnlProgress;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.TextBox txtTaskTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private cButton btnStampa;
    }
}