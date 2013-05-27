using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Scheduler
{
    public partial class AddTaskForm : Form
    {
        private static string TASKS_FOLDER_NAME = "tasks";

        public TaskInfo ResultTaskInfo;

        public AddTaskForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dialogOpenXmlFile.FileName = txtXmlFilePath.Text;

            if (dialogOpenXmlFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtXmlFilePath.Text = dialogOpenXmlFile.FileName;
                txtXmlFilePath_Validating(sender, null);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtXmlFilePath.Text))
            {
                MessageBox.Show("Файл не найден.");
                txtXmlFilePath.Focus();
                return;
            }

            if (DateTime.Now >= datetimeEnd.Value.AddMinutes(10))
            {
                MessageBox.Show("Дата выполнения должна быть в будущем");
                datetimeEnd.Focus();
                return;
            }

            TaskType type;
            if (radioApartmentAdd.Checked) type = TaskType.ApartmentAdding;
            else if (radioApartmentDelete.Checked) type = TaskType.ApartmentDeleting;
            else if (radioOther.Checked) type = TaskType.Other;
            else {
                MessageBox.Show("Тип не указан.");
                return;
            }

            Directory.CreateDirectory(TASKS_FOLDER_NAME);

            string id = getRandomFileName();
            string xmlFileName = Path.Combine(TASKS_FOLDER_NAME, id + ".xml");
            string infoFileName = Path.Combine(TASKS_FOLDER_NAME, id + ".info.data");

            File.Copy(txtXmlFilePath.Text, xmlFileName);

            var taskInfo = new TaskInfo();
            taskInfo.id = id;
            taskInfo.type = type;
            taskInfo.creation = DateTime.Now;
            taskInfo.end = datetimeEnd.Value;
            taskInfo.count = getXMLCountOfRows(xmlFileName);

            taskInfo.Serialize(infoFileName);
            
            this.ResultTaskInfo = taskInfo;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private string getRandomFileName()
        {
            string path = string.Empty;
            while (path == string.Empty || File.Exists(Path.Combine(TASKS_FOLDER_NAME, path)))
                path = Path.GetRandomFileName().Substring(0, 8);

            return path;
        }

        private int getXMLCountOfRows(string fileName)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rowDataElement = xmlDoc.DocumentElement.GetElementsByTagName("ROWDATA");

            if (rowDataElement.Count != 1)
            {
                throw new XmlException("Несоответствующий формат данных.");
            }

            return rowDataElement[0].ChildNodes.Count;
        }

        private void txtXmlFilePath_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                int count = getXMLCountOfRows(dialogOpenXmlFile.FileName);
                labelCountOfRows.Text = string.Format("В файле {0} записи(ей)", count.ToString());
            }
            catch {
                labelCountOfRows.Text = "Файл не подходит";
            }
        }

        private void changeFormScheme(object sender, EventArgs e)
        {
            if (radioApartmentAdd.Checked) {
                label2.Visible = datetimeEnd.Visible = false;
            }
            else if (radioApartmentDelete.Checked) {
                label2.Visible = datetimeEnd.Visible = true;
            }
            else {
                label2.Visible = datetimeEnd.Visible = true;
            }
        }

        private void AddTaskForm_Load(object sender, EventArgs e)
        {
            changeFormScheme(sender, null);
        }
    }
}
