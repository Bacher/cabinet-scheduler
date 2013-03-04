using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
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

            Directory.CreateDirectory(TASKS_FOLDER_NAME);

            string id = getRandomFileName();
            string xmlFileName = Path.Combine(TASKS_FOLDER_NAME, id + ".xml");
            string metaFileName = Path.Combine(TASKS_FOLDER_NAME, id + ".meta");

            File.Copy(txtXmlFilePath.Text, xmlFileName);

            var taskInfo = new TaskInfo();
            taskInfo.Id = id;
            taskInfo.Interval = (int)numInterval.Value;
            taskInfo.Type = (TaskType)comboType.SelectedIndex;
            taskInfo.Start = DateTime.Now.AddDays((int)numDelay.Value).Date;
            taskInfo.Count = getXMLCountOfRows(xmlFileName);
            taskInfo.Added = DateTime.Now;

            taskInfo.Save(metaFileName);

            this.ResultTaskInfo = taskInfo;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void AddTaskForm_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < 5; ++i)
            {
                comboType.Items.Add((TaskType)i);
            }
            comboType.SelectedIndex = 0;
        }


        private string getRandomFileName()
        {
            string path = string.Empty;
            while (path == string.Empty || File.Exists(path))
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
    }
}
