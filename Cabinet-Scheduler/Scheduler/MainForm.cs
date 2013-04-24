using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using Medium;

namespace Scheduler
{
    public partial class MainForm : Form
    {
        private static string KALUGA_HOUSE_URL = @"http://www.kalugahouse.ru/cabinet/";
        private static string AGENCY40_URL = @"http://www.agency40.ru/mysystem/";
        private static string SETTINGS_FILE_NAME = @"Scheduler.settings";
        private static string ACCOUNTS_FILE_NAME = @"Scheduler.accounts.txt";
        private TimeSpan startOfDay = new DateTime(2000, 1, 1, 8, 0, 0).TimeOfDay;
        private TimeSpan endOfDay = new DateTime(2000, 1, 1, 19, 59, 0).TimeOfDay;
        private TimeSpan startOfDayApartments = new DateTime(2000, 1, 1, 8, 0, 0).TimeOfDay;
        private TimeSpan endOfDayApartments = new DateTime(2000, 1, 1, 10, 59, 0).TimeOfDay;
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

            KHRemover.DoWork += KHRemover_DoWork;
            KHRemover.RunWorkerCompleted += KHRemover_RunWorkerCompleted;

            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("Index", typeof(int));
            table.Columns.Add("Count", typeof(int));
            table.Columns.Add("Added", typeof(DateTime));
            table.Columns.Add("Start", typeof(DateTime));
            gridViewTasks.DataSource = table;

            gridViewTasks.Columns[0].HeaderText = "Идентификатор";
            gridViewTasks.Columns[1].HeaderText = "Статус";
            gridViewTasks.Columns[1].Width *= 3;
            gridViewTasks.Columns[2].HeaderText = "Отправлено";
            gridViewTasks.Columns[3].HeaderText = "Всего";
            gridViewTasks.Columns[4].HeaderText = "Дата добавления";
            gridViewTasks.Columns[5].HeaderText = "Дата завершения";

            RefreshTable(sender, null);
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
            var task = e.Argument as Task;
            e.Result = task;

            //

            var count = task.info.count;
            var remainingMinutes = task.info.calcRemainingTimeMinutes();

            var THREEDAYDELAYMINUTES = 3 * 12 * 60;
            remainingMinutes -= THREEDAYDELAYMINUTES;
            var deleteIndex = 0;

            var deletedChunks = task.state.deletedChunks;
            if (deletedChunks.Count > 0)
                deleteIndex = deletedChunks[deletedChunks.Count - 1].Value;

            count -= deleteIndex;

            var countToDelete = (int)Math.Ceiling((double)(count * 60) / remainingMinutes);

            if (countToDelete > count) countToDelete = count;

            // Удаление
            if(!RemoveFromKH(task, deleteIndex, countToDelete))
                throw new ApplicationException("Удаление прошло с ошибкой");

            task.state.deletedChunks.Add(new KeyValuePair<DateTime, int>(DateTime.Now, deleteIndex + countToDelete));
        }

        private bool RemoveFromKH(Task task, int deleteIndex, int countToDelete)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(Path.Combine("tasks", task.info.id + ".xml"));

            var rowDataElement = xmlDoc.DocumentElement.GetElementsByTagName("ROWDATA");

            if (rowDataElement.Count != 1)
            {
                Log("Неправильный формат файла.");
                return false;
            }

            var rows = rowDataElement[0].ChildNodes;

            var khMedium = new KalugaHouseMedium(KALUGA_HOUSE_URL);
            try
            {
                khMedium.Login(KHAccount.Key, KHAccount.Value);
            }
            catch (NetMediumException ex)
            {
                Log("KalugaHouse.ru не отвечает.");
                return false;
            }
            catch (LoginMediumException)
            {
                Log("KalugaHouse.ru логин или пароль не подходят.");
                return false;
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }

            for (int i = deleteIndex; i < task.info.count && i < deleteIndex + countToDelete; ++i)
            {
                var secId = rows[i].Attributes["RLT_MAIN_ID"].Value;
                var nodeId = rows[i].Attributes["RLT_MAIN_NODEID"].Value.Trim();
                secId = secId.Substring(0, secId.Length - (1 + nodeId.Length));
                khMedium.RemoveItemBySecondId(secId);
            }
            return true;
        }

        private void CheckEntryAndLog(Task task, int index)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(Path.Combine("tasks", task.info.id + ".xml"));

            var rowDataElement = xmlDoc.DocumentElement.GetElementsByTagName("ROWDATA");

            if (rowDataElement.Count != 1)
            {
                throw new FormatException("Неправильный формат файла.");
            }

            var rows = rowDataElement[0].ChildNodes;

            var khMedium = new KalugaHouseMedium(KALUGA_HOUSE_URL);
            try
            {
                khMedium.Login(KHAccount.Key, KHAccount.Value);
            }
            catch (NetMediumException ex)
            {
                Log("KalugaHouse.ru не отвечает.");
            }
            catch (LoginMediumException)
            {
                Log("KalugaHouse.ru логин или пароль не подходят.");
            }
            catch (Exception ex)
            {
                Log(ex);
            }

            var secId = rows[index].Attributes["RLT_MAIN_ID"].Value;
            var nodeId = rows[index].Attributes["RLT_MAIN_NODEID"].Value.Trim();
            secId = secId.Substring(0, secId.Length - (1 + nodeId.Length));

            try
            {
                if (!khMedium.CheckItemBySecondId(secId))
                {
                    Log(string.Format("Не получилось добавить запись с ID: {0} в базу KalugaHouse.", secId));
                }
            }
            catch (NotLoggedMediumException)
            {
                Log("KalugaHouse.ru логин или пароль не подходят.");
            }
            catch (NetMediumException)
            {
                Log("KalugaHouse.ru не отвечает.");
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void KHRemover_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var task = e.Result as Task;

            task.state.backgroundDeleting = false;

            if (e.Error != null)
            {
                task.deletingErrorTimeout = DateTime.Now.AddMinutes(5);
            }
            //Log("BW Removing: " + DateTime.Now.ToString());
            task.Save();
        }

        private void btnShowAddForm_Click(object sender, EventArgs e)
        {
            var form = new AddTaskForm();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var taskInfo = form.ResultTaskInfo;

                var taskState = new TaskState();
                taskState.index = 0;
                if (taskInfo.apartment)
                {
                    var THREE = 3;
                    taskState.addTimeout = DateTime.Now.AddDays(THREE);
                }
                else
                    taskState.addTimeout = DateTime.Now;
                taskState.Serialize(Path.Combine("tasks", taskInfo.id + ".state.data"));

                var task = new Task(taskInfo.id);
                tasksManager.tasks.Add(task);
                tasksManager.Save();

                RefreshTable(sender, null);

                Log("Задача успешно добавлена. Количество добавленых записей: " + task.info.count + ".");
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

        private void RefreshTable(object sender, EventArgs e)
        {
            table.Clear();
            foreach (var task in tasksManager.tasks)
            {
                string state = "";
                if (task.state.index == task.info.count)
                    state = "Завершено";
                if (task.state.paused)
                    state = "Приостановлено";
                else 
                    state = "В обработке";

                table.Rows.Add(
                    task.info.id,
                    string.Format("{0}{1}", task.info.apartment ? "[Квартиры] " : "", state),
                    task.state.index,
                    task.info.count,
                    task.info.creation,
                    task.info.end);
            }
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            if (now.TimeOfDay >= startOfDay && now.TimeOfDay < endOfDay)
            {
                CheckWorkForRemoving();
                CheckSend();
            }
        }

        private void CheckWorkForRemoving()
        {
            var now = DateTime.Now;
            if (now.TimeOfDay < startOfDayApartments || now.TimeOfDay >= endOfDayApartments) {
                return;
            }

            if (KHRemover.IsBusy)
                return;

            for (int i = 0; i < tasksManager.tasks.Count; ++i)
            {
                var task = tasksManager.tasks[i];

                if (task.state.paused == true || task.deletingErrorTimeout > DateTime.Now)
                    continue;

                if (task.info.apartment && task.state.GetDeletingIndex() < task.info.count)
                {       
                    var newTimestamp = task.state.GetLastDeletingDate().AddHours(1);
                    if(newTimestamp.TimeOfDay > endOfDay)
                        newTimestamp.AddHours(12);

                    if (newTimestamp <= DateTime.Now)
                    {
                        if (!task.state.backgroundDeleting)
                        {
                            task.state.backgroundDeleting = true;
                            if(!KHRemover.IsBusy)
                                KHRemover.RunWorkerAsync(task);
                        }
                    }
                }
            }
        }

        private void CheckSend()
        {
            DateTime now = DateTime.Now;

            Agency40Medium ag40 = new Agency40Medium(AGENCY40_URL);
            bool logged = false;

            for (int i = 0, taskCount = tasksManager.tasks.Count; i < taskCount; ++i)
            {
                var task = tasksManager.tasks[i];

                if (task.info.apartment && (now.TimeOfDay < startOfDayApartments || now.TimeOfDay >= endOfDayApartments))
                    continue;                    

                if (task.state.paused == true || task.addingErrorTimeout > now)
                    continue;

                if (task.state.addTimeout > now || task.state.index >= task.info.count)
                    continue;

                if (task.info.apartment && !task.state.CheckAvailableDeleteIndex(now, task.state.index))
                    continue;

                if (!logged)
                {
                    try
                    {
                        ag40.Login(Ag40Account.Key, Ag40Account.Value);
                        logged = true;
                    }
                    catch (NetMediumException)
                    {
                        task.addingErrorTimeout = now.AddMinutes(5);
                        Log("Agency40.ru не отвечает.", true);
                        return;
                    }
                    catch (LoginMediumException)
                    {
                        task.addingErrorTimeout = now.AddMinutes(5);
                        Log("Agency40.ru логин или пароль не подходят.", true);
                        return;
                    }
                    catch (Exception ex)
                    {
                        task.addingErrorTimeout = now.AddMinutes(5);
                        Log(ex);
                        continue;
                    }
                }

                if (!task.info.apartment)
                {
                    // Удаление записи
                    if (!RemoveFromKH(task, task.state.index, 1))
                    {
                        task.addingErrorTimeout = now.AddMinutes(5);
                        continue;
                    }
                    Thread.Sleep(100);
                }

                XmlDocument xmlDoc;
                try
                {
                    xmlDoc = Agency40Medium.GetPartOfXml(Path.Combine("tasks", task.info.id + ".xml"), task.state.index);
                }
                catch (Exception ex)
                {
                    task.addingErrorTimeout = now.AddMinutes(5);
                    Log(ex);
                    continue;
                }

                try
                {
                    //Log("Send " + DateTime.Now.ToString());
                    ag40.UploadXML(xmlDoc);
                    task.state.index++;
                    if (task.state.index < task.info.count)
                        task.state.addTimeout = CalculateNextAddTimeout(task);
                    task.Save();
                }
                catch (NetMediumException)
                {
                    task.addingErrorTimeout = now.AddMinutes(5);
                    Log("Agency40.ru не отвечает.", true);
                    continue;
                }
                catch (Exception ex)
                {
                    task.addingErrorTimeout = now.AddMinutes(5);
                    Log(ex);
                    continue;
                }

                // Проверка добавления
                Thread.Sleep(300);
                CheckEntryAndLog(task, task.state.index - 1);
            }
        }

        private DateTime CalculateNextAddTimeout(Task task)
        {
            var remainingMinutes = task.info.calcRemainingTimeMinutes();
            var remainingCount = task.info.count - task.state.index;

            return DateTime.Now.AddSeconds(remainingMinutes * 60 / remainingCount);
        }

        private void Log(string message, bool alert = false)
        {
            string msg = string.Format("[{0}]: {2}{1}\r\n", DateTime.Now.ToLongTimeString(), message, alert ? "[!]" : "");

            richLog.AppendText(msg);
            File.AppendAllText("Scheduler.log", msg);

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
                RefreshTable(sender, null);
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

            var writer = new StreamWriter(SETTINGS_FILE_NAME);
            writer.Write(text);
            writer.Close();
        }

        private void ShowPublicMenuItem_Click(object sender, EventArgs e)
        {
            if (gridViewTasks.SelectedRows.Count != 0)
            {
                string id = (string)gridViewTasks.SelectedRows[0].Cells["Id"].Value;
                int index = 0;
                for (int i = 0; i < table.Rows.Count; ++i)
                {
                    if (table.Rows[i]["Id"] == id)
                    {
                        index = (int)table.Rows[i]["Index"];
                        break;
                    }
                }

                var list = Agency40Medium.GetPublicItems(Path.Combine("tasks", id + ".xml"), 0, index);

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
                string id = (string)gridViewTasks.SelectedRows[0].Cells["Id"].Value;

                for (int i = 0; i < tasksManager.tasks.Count; ++i)
                {
                    var task = tasksManager.tasks[i];
                    if (task.info.id == id)
                    {
                        tasksManager.tasks.Remove(task);
                        tasksManager.Save();

                        RefreshTable(sender, null);
                        return;
                    }
                }
            }
        }

        private void pauseResumeMenuItem_Click(object sender, EventArgs e)
        {
            string id = (string)gridViewTasks.SelectedRows[0].Cells["Id"].Value;

            for (int i = 0; i < tasksManager.tasks.Count; ++i) {
                var task = tasksManager.tasks[i];
                if (task.info.id == id) {
                    task.state.paused = !task.state.paused;
                    tasksManager.Save();

                    RefreshTable(sender, null);
                    return;
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
