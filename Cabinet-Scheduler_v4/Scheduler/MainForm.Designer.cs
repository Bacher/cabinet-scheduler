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
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.timerTableRefresher = new System.Windows.Forms.Timer(this.components);
            this.richLog = new System.Windows.Forms.RichTextBox();
            this.btnToogleLog = new System.Windows.Forms.Button();
            this.timerAlert = new System.Windows.Forms.Timer(this.components);
            this.btnPause = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numMaxHour = new System.Windows.Forms.NumericUpDown();
            this.numMinHour = new System.Windows.Forms.NumericUpDown();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowPublicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasks)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinHour)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnShowAddForm
            // 
            this.btnShowAddForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAddForm.Location = new System.Drawing.Point(649, 190);
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
            this.gridViewTasks.Location = new System.Drawing.Point(12, 12);
            this.gridViewTasks.Name = "gridViewTasks";
            this.gridViewTasks.ReadOnly = true;
            this.gridViewTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewTasks.Size = new System.Drawing.Size(877, 171);
            this.gridViewTasks.TabIndex = 1;
            // 
            // timerRun
            // 
            this.timerRun.Enabled = true;
            this.timerRun.Interval = 30000;
            this.timerRun.Tick += new System.EventHandler(this.timerRun_Tick);
            // 
            // timerTableRefresher
            // 
            this.timerTableRefresher.Enabled = true;
            this.timerTableRefresher.Interval = 6000;
            this.timerTableRefresher.Tick += new System.EventHandler(this.timerTableRefresher_Tick);
            // 
            // richLog
            // 
            this.richLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richLog.Location = new System.Drawing.Point(12, 218);
            this.richLog.Name = "richLog";
            this.richLog.ReadOnly = true;
            this.richLog.Size = new System.Drawing.Size(877, 141);
            this.richLog.TabIndex = 2;
            this.richLog.Text = "";
            // 
            // btnToogleLog
            // 
            this.btnToogleLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnToogleLog.BackColor = System.Drawing.SystemColors.Control;
            this.btnToogleLog.Location = new System.Drawing.Point(12, 189);
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
            this.btnPause.Location = new System.Drawing.Point(772, 190);
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
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numMaxHour);
            this.groupBox1.Controls.Add(this.numMinHour);
            this.groupBox1.Location = new System.Drawing.Point(261, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 34);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "(часы)";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "до:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Время работы с:";
            // 
            // numMaxHour
            // 
            this.numMaxHour.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numMaxHour.Location = new System.Drawing.Point(182, 11);
            this.numMaxHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numMaxHour.Name = "numMaxHour";
            this.numMaxHour.Size = new System.Drawing.Size(44, 20);
            this.numMaxHour.TabIndex = 17;
            this.numMaxHour.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // numMinHour
            // 
            this.numMinHour.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numMinHour.Location = new System.Drawing.Point(104, 11);
            this.numMinHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numMinHour.Name = "numMinHour";
            this.numMinHour.Size = new System.Drawing.Size(47, 20);
            this.numMinHour.TabIndex = 16;
            this.numMinHour.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowPublicMenuItem,
            this.RemoveMenuItem});
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 370);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinHour)).EndInit();
            this.contextMenu.ResumeLayout(false);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMaxHour;
        private System.Windows.Forms.NumericUpDown numMinHour;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowPublicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveMenuItem;
    }
}

