using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Scheduler
{
    [Serializable]
    public class TaskInfo
    {
        public string id;
        public bool apartment;
        public DateTime creation;
        public DateTime end;
        public int count;

        public int calcRemainingTimeMinutes()
        {
            var now = DateTime.Now;

            int sum = 0;

            if (now.Date == end.Date)
            {
                sum += (int)(end - now).TotalMinutes;
            }
            else
            {
                var endOfday = new DateTime(now.Year, now.Month, now.Day, 20, 59, 59);

                if(endOfday > now)
                    sum += (int)(endOfday - now).TotalMinutes;

                if (now.AddDays(1).Date < end.Date)
                    sum += (int)(end.Date - now.AddDays(1).Date).TotalDays * 12 * 60;

                var startOfEndDay = new DateTime(end.Year, end.Month, end.Day, 9, 0, 0);
                if (end > startOfEndDay)
                    sum += (int)(end - startOfEndDay).TotalMinutes;
            }

            return sum;
        }

        public void Serialize(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Create);
            new BinaryFormatter().Serialize(stream, this);
            stream.Close();
        }

        public static TaskInfo Unserialize(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open);
            var info = new BinaryFormatter().Deserialize(stream) as TaskInfo;
            stream.Close();
            return info;
        }
    }
}
