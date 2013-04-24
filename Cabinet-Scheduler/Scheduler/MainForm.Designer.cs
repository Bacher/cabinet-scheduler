namespace Scheduler
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
            this.components = new System.ComponentModel.Container();
            this.btnShowAddForm = new System.Windows.Forms.Button();
            this.gridViewTasks = new System.Windows.Forms.DataGridView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowPublicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseResumeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.timerTableRefresher = new System.Windows.Forms.Timer(this.components);
            this.richLog = new System.Windows.Forms.RichTextBox();
            this.btnToogleLog = new System.Windows.Forms.Button();
            this.timerAlert = new System.Windows.Forms.Timer(this.components);
            this.btnPause = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numMaxHour = new System.Windows.Forms.NumericUpDown();
            this.numMinHour = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasks)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShowAddForm
            // 
            this.btnShowAddForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAddForm.Location = new System.Drawing.Point(872, 360);
            this.btnShowAddForm.Name = "btnShowAddForm";
            this.btnShowAddForm.Size = new System.Drawing.Size(117, 23);
            this.btnShowAddForm.TabIndex = 0;
            this.btnShowAddForm.Text = "Добавить задачу";
            this.btnShowAddForm.UseVisualStyleBackColor = true;
            this.btnShowAddForm.Click += new System.EventHandler(this.btnShowAddForm_Click);
            // 
            // gridViewTasks
            // 
            this.gridViewTasks.AllowUserToAddRows = false;
            this.gridViewTasks.AllowUserToDeleteRows = false;
            this.gridViewTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridViewTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewTasks.ContextMenuStrip = this.contextMenu;
            this.gridViewTasks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridViewTasks.Location = new System.Drawing.Point(12, 12);
            this.gridViewTasks.MultiSelect = false;
            this.gridViewTasks.Name = "gridViewTasks";
            this.gridViewTasks.ReadOnly = true;
            this.gridViewTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewTasks.Size = new System.Drawing.Size(1100, 341);
            this.gridViewTasks.TabIndex = 1;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowPublicMenuItem,
            this.RemoveMenuItem,
            this.pauseResumeMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(260, 70);
            this.contextMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenu_Closed);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // ShowPublicMenuItem
            // 
            this.ShowPublicMenuItem.Name = "ShowPublicMenuItem";
            this.ShowPublicMenuItem.Size = new System.Drawing.Size(259, 22);
            this.ShowPublicMenuItem.Text = "Показать записи с публикациями";
            this.ShowPublicMenuItem.Click += new System.EventHandler(this.ShowPublicMenuItem_Click);
            // 
            // RemoveMenuItem
            // 
            this.RemoveMenuItem.Name = "RemoveMenuItem";
            this.RemoveMenuItem.Size = new System.Drawing.Size(259, 22);
            this.RemoveMenuItem.Text = "Удалить задачу";
            this.RemoveMenuItem.Click += new System.EventHandler(this.RemoveMenuItem_Click);
            // 
            // pauseResumeMenuItem
            // 
            this.pauseResumeMenuItem.Name = "pauseResumeMenuItem";
            this.pauseResumeMenuItem.Size = new System.Drawing.Size(259, 22);
            this.pauseResumeMenuItem.Text = "Приостановить/Возобновить";
            this.pauseResumeMenuItem.Click += new System.EventHandler(this.pauseResumeMenuItem_Click);
            // 
            // timerRun
            // 
            this.timerRun.Enabled = true;
            this.timerRun.Interval = 5000;
            this.timerRun.Tick += new System.EventHandler(this.timerRun_Tick);
            // 
            // timerTableRefresher
            // 
            this.timerTableRefresher.Enabled = true;
            this.timerTableRefresher.Interval = 6000;
            this.timerTableRefresher.Tick += new System.EventHandler(this.RefreshTable);
            // 
            // richLog
            // 
            this.richLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richLog.Location = new System.Drawing.Point(12, 388);
            this.richLog.Name = "richLog";
            this.richLog.ReadOnly = true;
            this.richLog.Size = new System.Drawing.Size(1100, 141);
            this.richLog.TabIndex = 2;
            this.richLog.Text = "";
            // 
            // btnToogleLog
            // 
            this.btnToogleLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnToogleLog.BackColor = System.Drawing.SystemColors.Control;
            this.btnToogleLog.Location = new System.Drawing.Point(12, 359);
            this.btnToogleLog.Name = "btnToogleLog";
            this.btnToogleLog.Size = new System.Drawing.Size(21, 23);
            this.btnToogleLog.TabIndex = 3;
            this.btnToogleLog.Text = "^";
            this.btnToogleLog.UseVisualStyleBackColor = false;
            this.btnToogleLog.Click += new System.EventHandler(this.btnToogleLog_Click);
            // 
            // timerAlert
            // 
            this.timerAlert.Interval = 400;
            this.timerAlert.Tick += new System.EventHandler(this.timerAlert_Tick);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Location = new System.Drawing.Point(995, 360);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(117, 23);
            this.btnPause.TabIndex = 9;
            this.btnPause.Text = "Приостановить всё";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.numMaxHour);
            this.groupBox1.Controls.Add(this.numMinHour);
            this.groupBox1.Location = new System.Drawing.Point(261, 351);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 34);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "до:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Время работы с:";
            // 
            // numMaxHour
            // 
            this.numMaxHour.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numMaxHour.Enabled = false;
            this.numMaxHour.Location = new System.Drawing.Point(201, 9);
            this.numMaxHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numMaxHour.Name = "numMaxHour";
            this.numMaxHour.Size = new System.Drawing.Size(44, 20);
            this.numMaxHour.TabIndex = 17;
            this.numMaxHour.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numMinHour
            // 
            this.numMinHour.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numMinHour.Enabled = false;
            this.numMinHour.Location = new System.Drawing.Point(123, 9);
            this.numMinHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numMinHour.Name = "numMinHour";
            this.numMinHour.Size = new System.Drawing.Size(47, 20);
            this.numMinHour.TabIndex = 16;
            this.numMinHour.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(354, 11);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown1.TabIndex = 16;
            this.numericUpDown1.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDown2.Enabled = false;
            this.numericUpDown2.Location = new System.Drawing.Point(432, 11);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown2.TabIndex = 17;
            this.numericUpDown2.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Квартиры с:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(407, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "до:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 540);
            this.Controls.Add(this.gridViewTasks);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnToogleLog);
            this.Controls.Add(this.richLog);
            this.Controls.Add(this.btnShowAddForm);
            this.Name = "MainForm";
            this.Text = "Новый адрес :: Планировщик";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasks)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowAddForm;
        private System.Windows.Forms.DataGridView gridViewTasks;
        private System.Windows.Forms.Timer timerRun;
        private System.Windows.Forms.Timer timerTableRefresher;
        private System.Windows.Forms.RichTextBox richLog;
        private System.Windows.Forms.Button btnToogleLog;
        private System.Windows.Forms.Timer timerAlert;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMaxHour;
        private System.Windows.Forms.NumericUpDown numMinHour;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowPublicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseResumeMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

