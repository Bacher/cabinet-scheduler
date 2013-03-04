using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scheduler
{
    public class TaskState
    {
        public TaskInfo Info;
        public int Index;
        public bool khEmpty;
        public bool working = false;
        public int workingPercent = 0;
        public object locked = new object();
        public bool error = false;
        public DateTime lastSend = DateTime.MinValue;

        public override string ToString()
        {
            return string.Format("{0}={1}={2}", Info.Id, Index, khEmpty);
        }

        public void Load(string text, string taskFolderName)
        {
            var parms = text.Split('=');

            Info = new TaskInfo();
            Info.Load(Path.Combine(taskFolderName, parms[0] + ".meta"));
            Index = int.Parse(parms[1]);
            khEmpty = bool.Parse(parms[2]);
        }
    }

    public class TasksManager
    {
        private static string FILE_HEADER = "[TASKS STATE]";

        public List<TaskState> tasks = new List<TaskState>();

        public void Save(string fileName)
        {
            var stateString = new StringBuilder();

            stateString.AppendLine(FILE_HEADER);

            foreach (var task in tasks)
            {
                stateString.AppendLine(task.ToString());
            }

            var writer = new StreamWriter(fileName);
            writer.Write(stateString);
            writer.Close();
        }

        public bool Load(string fileName, string taskFolderName)
        {
            if (File.Exists(fileName))
            {

                string[] lines = File.ReadAllLines(fileName);

                if (!lines[0].Equals(FILE_HEADER))
                {
                    throw new FormatException("Неправильный формат файла.");
                }

                for (int i = 1; i < lines.Length; ++i)
                {
                    var task = new TaskState();
                    task.Load(lines[i], taskFolderName);
                    tasks.Add(task);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
