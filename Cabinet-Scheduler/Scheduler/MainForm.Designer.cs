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
            this.btnPause = new System.Windows.Forms.Button();
            this.timeStartWork = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timeEndWork = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timeApartmentDelete = new System.Windows.Forms.MaskedTextBox();
            this.timeApartmentAdd = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasks)).BeginInit();
            this.contextMenu.SuspendLayout();
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
            // timeStartWork
            // 
            this.timeStartWork.Location = new System.Drawing.Point(105, 370);
            this.timeStartWork.Mask = "00:00";
            this.timeStartWork.Name = "timeStartWork";
            this.timeStartWork.Size = new System.Drawing.Size(42, 20);
            this.timeStartWork.TabIndex = 10;
            this.timeStartWork.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 373);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Начало работы:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Конец работы:";
            // 
            // timeEndWork
            // 
            this.timeEndWork.Location = new System.Drawing.Point(240, 370);
            this.timeEndWork.Mask = "00:00";
            this.timeEndWork.Name = "timeEndWork";
            this.timeEndWork.Size = new System.Drawing.Size(43, 20);
            this.timeEndWork.TabIndex = 13;
            this.timeEndWork.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(486, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Удалять квартиры в:";
            // 
            // timeApartmentDelete
            // 
            this.timeApartmentDelete.Location = new System.Drawing.Point(606, 370);
            this.timeApartmentDelete.Mask = "00:00";
            this.timeApartmentDelete.Name = "timeApartmentDelete";
            this.timeApartmentDelete.Size = new System.Drawing.Size(43, 20);
            this.timeApartmentDelete.TabIndex = 13;
            this.timeApartmentDelete.ValidatingType = typeof(System.DateTime);
            // 
            // timeApartmentAdd
            // 
            this.timeApartmentAdd.Location = new System.Drawing.Point(432, 370);
            this.timeApartmentAdd.Mask = "00:00";
            this.timeApartmentAdd.Name = "timeApartmentAdd";
            this.timeApartmentAdd.Size = new System.Drawing.Size(43, 20);
            this.timeApartmentAdd.TabIndex = 13;
            this.timeApartmentAdd.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 373);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Выгружать квартиры в:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 540);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.timeApartmentAdd);
            this.Controls.Add(this.timeApartmentDelete);
            this.Controls.Add(this.timeEndWork);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeStartWork);
            this.Controls.Add(this.gridViewTasks);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.richLog);
            this.Controls.Add(this.btnShowAddForm);
            this.Name = "MainForm";
            this.Text = "Новый адрес :: Планировщик";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTasks)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShowAddForm;
        private System.Windows.Forms.DataGridView gridViewTasks;
        private System.Windows.Forms.Timer timerRun;
        private System.Windows.Forms.Timer timerTableRefresher;
        private System.Windows.Forms.RichTextBox richLog;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowPublicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseResumeMenuItem;
        private System.Windows.Forms.MaskedTextBox timeStartWork;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox timeEndWork;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox timeApartmentDelete;
        private System.Windows.Forms.MaskedTextBox timeApartmentAdd;
        private System.Windows.Forms.Label label4;
    }
}

