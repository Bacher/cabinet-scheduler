using Medium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Scheduler
{
    public partial class MainForm : Form
    {
        private static string KALUGA_HOUSE_URL = @"http://www.kalugahouse.ru/cabinet/";
        private static string AGENCY40_URL = @"http://www.agency40.ru/mysystem/";
        private static string STATE_FILE_NAME = @"Scheduler.state";
        private static string SETTINGS_FILE_NAME = @"Scheduler.settings";
        private static string ACCOUNTS_FILE_NAME = @"Scheduler.accounts.txt";
        private KeyValuePair<string, string> Ag40Account;
        private KeyValuePair<string, string> KHAccount;
        private TasksManager tasksManager = new TasksManager();
        private DataTable table = new DataTable();
        private BackgroundWorker KHRemover = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadSettings()
        {
            if (File.Exists(SETTINGS_FILE_NAME))
            {
                var lines = File.ReadAllLines(SETTINGS_FILE_NAME);

                Width = int.Parse(lines[1].Split('=')[1]);
                Height = int.Parse(lines[2].Split('=')[1]);
                numMinHour.Value = int.Parse(lines[3].Split('=')[1]);
                numMaxHour.Value = int.Parse(lines[4].Split('=')[1]);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LockThisInstance();

            LoadSettings();

            if (!LoadAccounts())
            {
                this.Close();
                return;
            }

            KHRemover.WorkerReportsProgress = true;
            KHRemover.DoWork += KHRemover_DoWork;
            KHRemover.RunWorkerCompleted += KHRemover_RunWorkerCompleted;
            KHRemover.ProgressChanged += KHRemover_ProgressChanged;

            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Type", typeof(TaskType));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("Index", typeof(int));
            table.Columns.Add("Count", typeof(int));
            table.Columns.Add("Added", typeof(DateTime));
            table.Columns.Add("Start", typeof(DateTime));
            gridViewTasks.DataSource = table;

            gridViewTasks.Columns[0].HeaderText = "Идентификатор";
            gridViewTasks.Columns[1].HeaderText = "Тип";
            gridViewTasks.Columns[2].HeaderText = "Статус";
            gridViewTasks.Columns[2].Width *= 3;
            gridViewTasks.Columns[3].HeaderText = "Отправлено";
            gridViewTasks.Columns[4].HeaderText = "Всего";
            gridViewTasks.Columns[5].HeaderText = "Дата добавления";
            gridViewTasks.Columns[6].HeaderText = "Начало обработки";

            tasksManager.Load(STATE_FILE_NAME, "tasks");

            RefreshTable();
        }

        private bool LoadAccounts()
        {
            string[] lines = new string[0];
            try
            {
                lines = File.ReadAllLines(ACCOUNTS_FILE_NAME);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Файл с паролями не найден.");
                return false;
            }

            foreach (var line in lines)
            {
                if (line.StartsWith("[")) continue;

                var parms = line.Split('=');
                if (parms.Length == 3)
                {
                    if (parms[0] == "Agency40")
                    {
                        Ag40Account = new KeyValuePair<string, string>(parms[1], parms[2]);
                    }
                    else if (parms[0] == "KalugaHouse")
                    {
                        KHAccount = new KeyValuePair<string, string>(parms[1], parms[2]);
                    }
                }
            }
            return true;
        }

        private void KHRemover_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw = sender as BackgroundWorker;
            var task = e.Argument as TaskState;

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(Path.Combine("tasks", task.Info.Id + ".xml"));

            var rowDataElement = xmlDoc.DocumentElement.GetElementsByTagName("ROWDATA");

            if (rowDataElement.Count != 1)
            {
                throw new FormatException("Неправильный формат файла.");
            }

            var rows = rowDataElement[0].ChildNodes;
            var rowCount = rows.Count;

            var khMedium = new KalugaHouseMedium(KALUGA_HOUSE_URL);
            try
            {
                khMedium.Login(KHAccount.Key, KHAccount.Value);
            }
            catch (NetMediumException ex)
            {
                Log("KalugaHouse.ru не отвечает.");
                return;
            }
            catch (LoginMediumException)
            {
                Log("KalugaHouse.ru логин или пароль не подходят.");
                return;
            }
            catch (Exception ex)
            {
                Log(ex);
                return;
            }

            for (int i = 0; i < rowCount; ++i)
            {
                var secId = rows[i].Attributes["RLT_MAIN_ID"].Value;

                secId = secId.Substring(0, secId.Length - 5);

                khMedium.RemoveItemBySecondId(secId);

                bw.ReportProgress(i * 100 / rowCount, task);
            }

            e.Result = task;
        }

        private void KHRemover_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var task = e.UserState as TaskState;
            task.workingPercent = e.ProgressPercentage;
        }

        private void KHRemover_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var task = e.Result as TaskState;
            if (e.Error != null)
            {
                task.error = true;
            }
            if (e.Result != null)
            {
                task.khEmpty = true;
                tasksManager.Save(STATE_FILE_NAME);
            }
        }

        private void btnShowAddForm_Click(object sender, EventArgs e)
        {
            var form = new AddTaskForm();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var taskState = new TaskState();
                taskState.Info = form.ResultTaskInfo;
                taskState.Index = 0;
                taskState.khEmpty = false;

                tasksManager.tasks.Add(taskState);
                tasksManager.Save(STATE_FILE_NAME);

                RefreshTable();

                Log("Задача успешно добавлена. Количество добавленых записей: " + taskState.Info.Count + ".");
            }

        }

        private void LockThisInstance()
        {
            try
            {
                File.Create(".lock", 1, FileOptions.DeleteOnClose);
            }
            catch (Exception)
            {
                MessageBox.Show("Application can't be access to file '.lock' and will be closed.");
                this.Close();
            }
        }

        private void RefreshTable()
        {
            table.Clear();
            foreach (var task in tasksManager.tasks)
            {
                string status = "";
                if (!task.khEmpty)
                    status = task.working ? "Удаление из KalugaHouse: " + task.workingPercent + "%" : "Ожидание удаления из базы KalugaHouse";
                else
                {
                    if (task.Index == task.Info.Count)
                        status = "Обработка завершена";
                    else if (DateTime.Now < task.Info.Start)
                        status = "Обработка отложена";
                    else
                        status = "В обработке: " + (100 * task.Index / task.Info.Count) + "%";
                }

                if (task.error)
                {
                    status += ". Операция завершилась с ошибкой";
                }

                table.Rows.Add(task.Info.Id, task.Info.Type, status, task.Index, task.Info.Count, task.Info.Added, task.Info.Start);
            }
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            CheckWorkForRemoving();

            DateTime now = DateTime.Now.AddSeconds(15);

            if (now.Hour < numMinHour.Value || now.Hour >= numMaxHour.Value)
                return;

            bool[] flags = { false, false, false, false, false };

            Agency40Medium ag40 = new Agency40Medium(AGENCY40_URL);
            bool logged = false;

            for (int i = 0; i < tasksManager.tasks.Count; ++i)
            {
                var task = tasksManager.tasks[i];

                if (task.khEmpty && now.Date >= task.Info.Start && flags[(int)task.Info.Type] == false && task.Index < task.Info.Count)
                {
                    if (now >= task.lastSend.AddMinutes(task.Info.Interval))
                    {
                        flags[(int)task.Info.Type] = true;

                        if (!logged)
                        {
                            try
                            {
                                ag40.Login(Ag40Account.Key, Ag40Account.Value);
                            }
                            catch (NetMediumException ex)
                            {
                                Log("Agency40.ru не отвечает.", true);
                                return;
                            }
                            catch (LoginMediumException)
                            {
                                Log("Agency40.ru логин или пароль не подходят.", true);
                                return;
                            }
                            catch (Exception ex)
                            {
                                Log("Неопознаная ошибка. Прозьба обратиться к разработчику. [" + ex.Message + "]", true);
                                continue;
                            }
                        }

                        XmlDocument xmlDoc;
                        try
                        {
                            xmlDoc = Agency40Medium.GetPartOfXml(Path.Combine("tasks", task.Info.Id + ".xml"), task.Index);
                        }
                        catch (Exception ex)
                        {
                            Log(ex);
                            continue;
                        }

                        try
                        {
                            ag40.UploadXML(xmlDoc);
                            task.error = false;
                            task.Index++;
                            task.lastSend = now;
                            tasksManager.Save(STATE_FILE_NAME);
                        }
                        catch (NetMediumException ex)
                        {
                            Log("Agency40.ru не отвечает.", true);
                            task.error = true;
                        }
                        catch (Exception ex)
                        {
                            Log(ex);
                        }
                    }
                }
            }
        }

        private void CheckWorkForRemoving()
        {
            for (int i = 0; i < tasksManager.tasks.Count; ++i)
            {
                var task = tasksManager.tasks[i];
                lock (task.locked)
                {
                    if (!task.working)
                    {
                        if (!KHRemover.IsBusy)
                        {
                            task.working = true;
                            KHRemover.RunWorkerAsync(task);
                        }
                    }
                }
            }
        }

        private void timerTableRefresher_Tick(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void Log(string message, bool alert = false)
        {
            string msg = string.Format("{0}: {2}{1}\n", DateTime.Now.ToLongTimeString(), message, alert ? "[!]" : "");

            richLog.AppendText(msg);
            File.AppendAllText("Scheduler.log", msg + "\n");

            if (alert && richLog.Visible == false)
            {
                timerAlert.Enabled = true;
            }
        }

        private void Log(Exception ex)
        {
            Log(string.Format("Неопознаная ошибка. Прозьба обратиться к разработчику. [ {0}, Source: {1}, StackTrace: {2} ]", ex.Message, ex.Source, ex.StackTrace), true);
        }

        private void btnToogleLog_Click(object sender, EventArgs e)
        {
            if (richLog.Visible)
            {
                richLog.Visible = false;
                btnShowAddForm.Anchor = AnchorStyles.Top;
                btnToogleLog.Anchor = AnchorStyles.Top;
                gridViewTasks.Anchor = AnchorStyles.Top;
                btnPause.Anchor = AnchorStyles.Top;
                groupBox1.Anchor = AnchorStyles.Top;
                MainForm.ActiveForm.Height -= 154;
                btnShowAddForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                btnToogleLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                gridViewTasks.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                btnPause.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                groupBox1.Anchor = AnchorStyles.Bottom;
                btnToogleLog.Text = "v";
            }
            else
            {
                timerAlert.Enabled = false;
                richLog.Visible = true;
                btnShowAddForm.Anchor = AnchorStyles.Top;
                btnToogleLog.Anchor = AnchorStyles.Top;
                gridViewTasks.Anchor = AnchorStyles.Top;
                btnPause.Anchor = AnchorStyles.Top;
                groupBox1.Anchor = AnchorStyles.Top;
                MainForm.ActiveForm.Height += 154;
                btnShowAddForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                btnToogleLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                gridViewTasks.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                btnPause.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                groupBox1.Anchor = AnchorStyles.Bottom;
                btnToogleLog.Text = "^";
            }
        }

        private int alertPulse = 0;

        private void timerAlert_Tick(object sender, EventArgs e)
        {
            alertPulse++;
            alertPulse %= 3;

            if (alertPulse == 2)
                btnToogleLog.BackColor = Color.Red;
            else
                btnToogleLog.BackColor = Color.FromName("Control");

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Приостановить всё")
            {
                btnPause.Text = "Возобновить";
                timerRun.Enabled = false;
                timerTableRefresher.Enabled = false;
                RefreshTable();
                gridViewTasks.Enabled = false;
                Log("Работа приостановлена.");
            }
            else
            {
                btnPause.Text = "Приостановить всё";
                timerRun.Enabled = true;
                timerTableRefresher.Enabled = true;
                gridViewTasks.Enabled = true;
                Log("Работа возобновлена.");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var text = new StringBuilder();

            text.AppendLine("[SETTINGS]");
            text.AppendFormat("{0}={1}\n", "width", Width);
            text.AppendFormat("{0}={1}\n", "height", Height);
            text.AppendFormat("{0}={1}\n", "minhour", numMinHour.Value);
            text.AppendFormat("{0}={1}\n", "maxhour", numMaxHour.Value);

            var writer = new StreamWriter(SETTINGS_FILE_NAME);
            writer.Write(text);
            writer.Close();
        }

        private void ShowPublicMenuItem_Click(object sender, EventArgs e)
        {
            var rowIndex = gridViewTasks.SelectedRows[0].Index;

            var id = table.Rows[rowIndex]["Id"];

            var list = Agency40Medium.GetPublicItems(Path.Combine("tasks", id + ".xml"));

            string text;

            if (list.Count != 0)
            {
                text = "Записи с публичной пометкой:\n";
                for (int i = 0; i < list.Count; ++i)
                {
                    text += list[i] + "\n";
                }
            }
            else
                text = "Записи с пометкой отсутствуют.";

            var form = new TextForm();
            form.SetText(text);
            form.Show();
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            timerTableRefresher.Enabled = false;
        }

        private void contextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            timerTableRefresher.Enabled = true;
        }

        private void RemoveMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить задачу?", "Удаление задачи", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {

                var rowIndex = gridViewTasks.SelectedRows[0].Index;

                var id = table.Rows[rowIndex]["Id"];

                for (int i = 0; i < tasksManager.tasks.Count; ++i)
                {
                    var task = tasksManager.tasks[i];
                    if (task.Info.Id == id)
                    {
                        tasksManager.tasks.Remove(task);
                        tasksManager.Save(STATE_FILE_NAME);
                        return;
                    }
                }

                RefreshTable();
            }
        }
    }
}
