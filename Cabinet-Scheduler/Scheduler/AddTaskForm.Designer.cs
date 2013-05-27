namespace Scheduler
{
    partial class AddTaskForm
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
            this.txtXmlFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dialogOpenXmlFile = new System.Windows.Forms.OpenFileDialog();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.datetimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCountOfRows = new System.Windows.Forms.Label();
            this.groupType = new System.Windows.Forms.GroupBox();
            this.radioOther = new System.Windows.Forms.RadioButton();
            this.radioApartmentDelete = new System.Windows.Forms.RadioButton();
            this.radioApartmentAdd = new System.Windows.Forms.RadioButton();
            this.groupType.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtXmlFilePath
            // 
            this.txtXmlFilePath.Location = new System.Drawing.Point(12, 25);
            this.txtXmlFilePath.Name = "txtXmlFilePath";
            this.txtXmlFilePath.Size = new System.Drawing.Size(353, 20);
            this.txtXmlFilePath.TabIndex = 0;
            this.txtXmlFilePath.Validating += new System.ComponentModel.CancelEventHandler(this.txtXmlFilePath_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "XML файл:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(371, 23);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Обзор...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dialogOpenXmlFile
            // 
            this.dialogOpenXmlFile.Filter = "All Files|*|Xml Files|*.xml";
            this.dialogOpenXmlFile.FilterIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(291, 180);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(372, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // datetimeEnd
            // 
            this.datetimeEnd.CustomFormat = "dd/MM/yyyy";
            this.datetimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetimeEnd.Location = new System.Drawing.Point(119, 153);
            this.datetimeEnd.Name = "datetimeEnd";
            this.datetimeEnd.Size = new System.Drawing.Size(98, 20);
            this.datetimeEnd.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Дата завершения:";
            // 
            // labelCountOfRows
            // 
            this.labelCountOfRows.AutoSize = true;
            this.labelCountOfRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCountOfRows.Location = new System.Drawing.Point(12, 191);
            this.labelCountOfRows.Name = "labelCountOfRows";
            this.labelCountOfRows.Size = new System.Drawing.Size(0, 13);
            this.labelCountOfRows.TabIndex = 14;
            // 
            // groupType
            // 
            this.groupType.Controls.Add(this.radioOther);
            this.groupType.Controls.Add(this.radioApartmentDelete);
            this.groupType.Controls.Add(this.radioApartmentAdd);
            this.groupType.Location = new System.Drawing.Point(12, 51);
            this.groupType.Name = "groupType";
            this.groupType.Size = new System.Drawing.Size(205, 95);
            this.groupType.TabIndex = 15;
            this.groupType.TabStop = false;
            this.groupType.Text = "Тип";
            // 
            // radioOther
            // 
            this.radioOther.AutoSize = true;
            this.radioOther.Checked = true;
            this.radioOther.Location = new System.Drawing.Point(9, 65);
            this.radioOther.Name = "radioOther";
            this.radioOther.Size = new System.Drawing.Size(182, 17);
            this.radioOther.TabIndex = 2;
            this.radioOther.TabStop = true;
            this.radioOther.Text = "Остальные типы - Добавление";
            this.radioOther.UseVisualStyleBackColor = true;
            this.radioOther.CheckedChanged += new System.EventHandler(this.changeFormScheme);
            // 
            // radioApartmentDelete
            // 
            this.radioApartmentDelete.AutoSize = true;
            this.radioApartmentDelete.Location = new System.Drawing.Point(9, 42);
            this.radioApartmentDelete.Name = "radioApartmentDelete";
            this.radioApartmentDelete.Size = new System.Drawing.Size(134, 17);
            this.radioApartmentDelete.TabIndex = 1;
            this.radioApartmentDelete.Text = "Квартиры - Удаление";
            this.radioApartmentDelete.UseVisualStyleBackColor = true;
            this.radioApartmentDelete.CheckedChanged += new System.EventHandler(this.changeFormScheme);
            // 
            // radioApartmentAdd
            // 
            this.radioApartmentAdd.AutoSize = true;
            this.radioApartmentAdd.Location = new System.Drawing.Point(9, 19);
            this.radioApartmentAdd.Name = "radioApartmentAdd";
            this.radioApartmentAdd.Size = new System.Drawing.Size(147, 17);
            this.radioApartmentAdd.TabIndex = 0;
            this.radioApartmentAdd.Text = "Квартиры - Добавление";
            this.radioApartmentAdd.UseVisualStyleBackColor = true;
            this.radioApartmentAdd.CheckedChanged += new System.EventHandler(this.changeFormScheme);
            // 
            // AddTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 214);
            this.Controls.Add(this.groupType);
            this.Controls.Add(this.labelCountOfRows);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datetimeEnd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtXmlFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddTaskForm";
            this.Text = "Добавить задачу";
            this.Load += new System.EventHandler(this.AddTaskForm_Load);
            this.groupType.ResumeLayout(false);
            this.groupType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtXmlFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog dialogOpenXmlFile;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker datetimeEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCountOfRows;
        private System.Windows.Forms.GroupBox groupType;
        private System.Windows.Forms.RadioButton radioOther;
        private System.Windows.Forms.RadioButton radioApartmentDelete;
        private System.Windows.Forms.RadioButton radioApartmentAdd;
    }
}