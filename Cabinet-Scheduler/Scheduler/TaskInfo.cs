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

            var endOfday = new DateTime(now.Year, now.Month, now.Day, 20, 59, 59);
            if (endOfday > now && endOfday <= end)
                sum += (int)(endOfday - now).TotalMinutes;

            if (now.AddDays(1).Date < end.Date)
                sum += (int)(end.Date - now.AddDays(1).Date).TotalDays * 12 * 60;

            var startOfEndDay = new DateTime(end.Year, end.Month, end.Day, 9, 0, 0);
            if(end > startOfEndDay)
                sum += (int)(end - startOfEndDay).TotalMinutes;

            return sum;
        }

        public void Serialize(string fileName)
        {
            new BinaryFormatter().Serialize(new FileStream(fileName, FileMode.Create), this);
        }

        public static TaskInfo Unserialize(string fileName)
        {
            return new BinaryFormatter().Deserialize(new FileStream(fileName, FileMode.Open)) as TaskInfo;
        }
    }
}
