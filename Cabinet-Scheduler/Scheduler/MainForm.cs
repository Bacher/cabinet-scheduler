using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
        private static string ACCOUNTS_FILE_NAME = @"Scheduler.accounts.txt";
        private KeyValuePair<string, string> Ag40Account;
        private KeyValuePair<string, string> KHAccount;
        private TasksManager tasksManager = new TasksManager();
        private DataTable table = new DataTable();

        private DateTime tsStartWork;
        private DateTime tsEndWork;
        private DateTime tsApartmentAdd;
        private DateTime tsApartmentDelete;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try {
                Utilities.LockThisInstance();
            } catch(ApplicationException ex) {
                MessageBox.Show(ex.Message);
                this.Close();
                return;
            }
            if (Properties.Settings.Default.MainFormWidth != 0) {
                Width = Properties.Settings.Default.MainFormWidth;
                Height = Properties.Settings.Default.MainFormHeight;

                timeStartWork.Text = Properties.Settings.Default.TimeStartWork;
                timeEndWork.Text = Properties.Settings.Default.TimeEndWork;
                timeApartmentAdd.Text = Properties.Settings.Default.TimeApartmentAdd;
                timeApartmentDelete.Text = Properties.Settings.Default.TimeApartmentDelete;
            }

            if (!LoadAccounts())
            {
                this.Close();
                return;
            }

            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("Index", typeof(string));
            table.Columns.Add("Count", typeof(int));
            table.Columns.Add("Added", typeof(DateTime));
            table.Columns.Add("Start", typeof(DateTime));
            gridViewTasks.DataSource = table;

            gridViewTasks.Columns[0].HeaderText = "Идентификатор";
            gridViewTasks.Columns[1].HeaderText = "Статус";
            gridViewTasks.Columns[1].Width *= 3;
            gridViewTasks.Columns[2].HeaderText = "Выполнено";
            gridViewTasks.Columns[2].Width = (int)(gridViewTasks.Columns[2].Width * 1.2);
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

        private bool RemoveFromKH(Task task, int deleteIndex, int countToDelete, KalugaHouseMedium kh)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(Path.Combine("tasks", task.info.id + ".xml"));

            var rowDataElement = xmlDoc.DocumentElement.GetElementsByTagName("ROWDATA");

            if (rowDataElement.Count != 1) {
                Log("Неправильный формат файла.");
                return false;
            }

            var rows = rowDataElement[0].ChildNodes;

            if (!loginKhIfNeed(kh)) {
                return false;
            }

            for (int i = deleteIndex; i < task.info.count && i < deleteIndex + countToDelete; ++i) {
                var secId = rows[i].Attributes["RLT_MAIN_ID"].Value;
                var nodeId = rows[i].Attributes["RLT_MAIN_NODEID"].Value.Trim();
                secId = secId.Substring(0, secId.Length - (1 + nodeId.Length));
                kh.RemoveItemBySecondId(secId);
            }
            return true;
        }

        private void btnShowAddForm_Click(object sender, EventArgs e)
        {
            var form = new AddTaskForm();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var taskInfo = form.ResultTaskInfo;

                var taskState = new TaskState();
                taskState.index = 0;
                taskState.Serialize(Path.Combine("tasks", taskInfo.id + ".state.data"));

                var task = new Task(taskInfo.id);
                tasksManager.tasks.Add(task);
                tasksManager.Save();

                RefreshTable(sender, null);

                Log("Задача успешно добавлена. Количество добавленых записей: " + task.info.count + ".");
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
                    string.Format("{0}{1}", task.info.type == TaskType.ApartmentAdding ? "[Квартиры доб.] " : task.info.type == TaskType.ApartmentDeleting ? "[Квартиры удал.] " : "", state),
                    task.info.type == TaskType.ApartmentAdding ? (task.state.lastRunDate.Date == DateTime.Now.Date ? "Сегодня выполнено" : "Ожидание") : task.state.index.ToString(),
                    task.info.count,
                    task.info.creation,
                    task.info.end);
            }
        }

        private bool parseTimeSpans()
        {
            try {
                tsStartWork = DateTime.Parse(timeStartWork.Text);
                tsEndWork = DateTime.Parse(timeEndWork.Text);
                tsApartmentAdd = DateTime.Parse(timeApartmentAdd.Text);
                tsApartmentDelete = DateTime.Parse(timeApartmentDelete.Text);
            } catch {
                Log("Неправильный формат времени!");
                return false;
            }
            return true;
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            if (!parseTimeSpans()) return;

            Agency40Medium ag40 = new Agency40Medium(AGENCY40_URL);
            KalugaHouseMedium kh = new KalugaHouseMedium(KALUGA_HOUSE_URL);

            CheckSend(ag40, kh);
            CheckApartmentRemoving(kh);
            CheckApartmentAdd(ag40);
        }

        private void CheckApartmentRemoving(KalugaHouseMedium kh)
        {
            var now = DateTime.Now;

            if (now >= tsApartmentDelete && now < tsApartmentDelete.AddHours(1)) {

                for (int i = 0; i < tasksManager.tasks.Count; ++i) {
                    var task = tasksManager.tasks[i];

                    if (task.state.paused == true || task.deletingErrorTimeout > now)
                        continue;

                    if (task.info.type == TaskType.ApartmentDeleting && task.state.index < task.info.count && task.state.lastRunDate.Date < now.Date) {

                        // Delete from task.index to task.index + dayCountToDelete
                        var daysLeast = (task.info.end.Date - now.Date).Days + 1;
                        if (daysLeast < 1) daysLeast = 1;
                        var entriesLeast = task.info.count - task.state.index;
                        var countToDelete = entriesLeast / daysLeast;

                        if (!loginKhIfNeed(kh)) {
                            return;
                        }

                        if (!RemoveFromKH(task, task.state.index, countToDelete, kh)) {
                            task.deletingErrorTimeout = DateTime.Now.AddMinutes(5);
                        } else {
                            task.state.index += countToDelete;
                            task.state.lastRunDate = now;
                            task.Save();
                        }
                    }
                }
            }
        }

        private void CheckApartmentAdd(Agency40Medium ag40)
        {
            var now = DateTime.Now;

            if (now >= tsApartmentAdd && now < tsApartmentAdd.AddHours(1)) {

                for (int i = 0; i < tasksManager.tasks.Count; ++i) {
                    var task = tasksManager.tasks[i];

                    if (task.state.paused == true || task.addingErrorTimeout > now)
                        continue;

                    if (task.info.type == TaskType.ApartmentAdding && task.info.end.Date >= now.Date && task.state.lastRunDate.Date < now.Date) {

                        if (!loginAg40IfNeed(ag40)) {
                            task.addingErrorTimeout = now.AddMinutes(5);
                            return;
                        }

                        var success = true;
                        int iterationCount = 10;

                        int step = (int)(task.info.count / iterationCount + 0.5);

                        for (var j = 0; j < iterationCount; ++j) {
                            var countLeast = task.info.count - step * j;
                            var count = countLeast < step ? countLeast : step;

                            var xmlDoc = Agency40Medium.GetPartOfXml(Path.Combine("tasks", task.info.id + ".xml"), step * j, count);
                            if (xmlDoc == null) continue;
                            try {
                                ag40.UploadXML(xmlDoc);
                            } catch {
                                task.addingErrorTimeout = now.AddMinutes(5);
                                success = false;
                                break;
                            }
                        }
                        if(success) {
                            task.state.lastRunDate = now;
                            task.Save();
                        }
                    }
                }
            }
        }

        private bool loginAg40IfNeed(Agency40Medium ag40)
        {
            if(!ag40.logged) {
                try {
                    ag40.Login(Ag40Account.Key, Ag40Account.Value);
                } catch (NetMediumException) {
                    Log("Agency40.ru не отвечает.", true);
                    return false;
                } catch (LoginMediumException) {
                    Log("Agency40.ru логин или пароль не подходят.", true);
                    return false;
                } catch (Exception ex) {
                    Log(ex);
                    return false;
                }
            }
            return true;
        }

        private bool loginKhIfNeed(KalugaHouseMedium kh)
        {
            if (!kh.logged) {
                try {
                    kh.Login(KHAccount.Key, KHAccount.Value);
                } catch (NetMediumException) {
                    Log("KalugaHouse.ru не отвечает.");
                    return false;
                } catch (LoginMediumException) {
                    Log("KalugaHouse.ru логин или пароль не подходят.");
                    return false;
                } catch (Exception ex) {
                    Log(ex);
                    return false;
                }
            }
            return true;
        }

        private void CheckSend(Agency40Medium ag40, KalugaHouseMedium kh)
        {
            DateTime now = DateTime.Now;

            if (!(now >= tsStartWork && now < tsEndWork)) return;

            for (int i = 0, taskCount = tasksManager.tasks.Count; i < taskCount; ++i)
            {
                var task = tasksManager.tasks[i];

                if (task.info.type != TaskType.Other)
                    continue;

                if (task.state.paused == true || task.addingErrorTimeout > now)
                    continue;

                if (task.state.addTimeout > now || task.state.index >= task.info.count)
                    continue;

                if(!loginAg40IfNeed(ag40)) {
                    task.addingErrorTimeout = now.AddMinutes(5);
                    return;
                };

                // Удаление записи
                if (!RemoveFromKH(task, task.state.index, 1, kh))
                {
                    task.addingErrorTimeout = now.AddMinutes(5);
                    continue;
                }
                Thread.Sleep(100);

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
                //Thread.Sleep(300);
                //CheckEntryAndLog(task, task.state.index - 1, kh);
            }
        }

        private DateTime CalculateNextAddTimeout(Task task)
        {
            var remainingMinutes = task.info.calcRemainingTimeMinutes(tsStartWork, tsEndWork);
            if (remainingMinutes <= 1) remainingMinutes = 3;
            var remainingCount = task.info.count - task.state.index;

            return DateTime.Now.AddSeconds(remainingMinutes * 60 / remainingCount);
        }

        private void Log(string message, bool alert = false)
        {
            string msg = string.Format("[{0}]: {2}{1}\r\n", DateTime.Now.ToLongTimeString(), message, alert ? "[!]" : "");

            richLog.AppendText(msg);
            File.AppendAllText("Scheduler.log", msg);
        }

        private void Log(Exception ex)
        {
            Log(string.Format("Неопознанная ошибка. Прозьба обратиться к разработчику. [ {0}, Source: {1}, StackTrace: {2} ]", ex.Message, ex.Source, ex.StackTrace), true);
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
            Properties.Settings.Default.MainFormWidth = Width;
            Properties.Settings.Default.MainFormHeight = Height;

            Properties.Settings.Default.TimeStartWork = timeStartWork.Text;
            Properties.Settings.Default.TimeEndWork = timeEndWork.Text;
            Properties.Settings.Default.TimeApartmentAdd = timeApartmentAdd.Text;
            Properties.Settings.Default.TimeApartmentDelete = timeApartmentDelete.Text;

            Properties.Settings.Default.Save();
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
                    task.Save();

                    RefreshTable(sender, null);
                    return;
                }
            }
        }
    }
}
