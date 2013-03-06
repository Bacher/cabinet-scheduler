using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scheduler
{
    public class TasksManager
    {
        private static string TASKS_FILE_NAME = "Scheduler.tasks.ini";

        public List<Task> tasks = new List<Task>();

        public TasksManager()
        {
            if (File.Exists(TASKS_FILE_NAME))
            {
                string[] tasksId = File.ReadAllLines(TASKS_FILE_NAME);

                foreach (string taskId in tasksId)
                {
                    tasks.Add(new Task(taskId));
                }
            }
        }

        public void Save()
        {
            var builder = new StringBuilder();
            foreach (var task in tasks)
            {
                builder.AppendLine(task.info.id);
            }
            var stream = new StreamWriter(TASKS_FILE_NAME);
            stream.Write(builder);
            stream.Close();
        }
    }
}
