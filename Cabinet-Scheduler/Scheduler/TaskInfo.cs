using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scheduler
{
    public enum TaskType : int
    {
        Квартиры = 0,
        Комнаты = 1,
        Участоки = 2,
        Дома = 3,
        Другое = 4
    }

    public class TaskInfo
    {
        private static string INFO_FILE_HEADER = "[Task Info]";

        public string Id;
        public int Count;
        public TaskType Type;
        public DateTime Start;
        public int Interval;
        public DateTime Added;

        public void Save(string fileName)
        {
            var info = new StringBuilder();

            info.AppendLine(INFO_FILE_HEADER);
            info.AppendFormat("{0}={1}\n", "id", Id);
            info.AppendFormat("{0}={1}\n", "count", Count);
            info.AppendFormat("{0}={1}\n", "type", (int)Type);
            info.AppendFormat("{0}={1}\n", "start", Start);
            info.AppendFormat("{0}={1}\n", "interval", Interval);
            info.AppendFormat("{0}={1}\n", "added", Added);

            var writer = new StreamWriter(fileName);
            writer.Write(info);
            writer.Close();
        }

        public void Load(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            if(!lines[0].Equals(INFO_FILE_HEADER))
            {
                throw new FormatException("Неправильный формат файла.");
            }

            Id = lines[1].Split('=')[1];
            Count = int.Parse(lines[2].Split('=')[1]);
            Type = (TaskType)int.Parse(lines[3].Split('=')[1]);
            Start = DateTime.Parse(lines[4].Split('=')[1]);
            Interval = int.Parse(lines[5].Split('=')[1]);
            Added = DateTime.Parse(lines[6].Split('=')[1]);
        }
    }
}
