using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Scheduler
{
    [Serializable]
    public class TaskState
    {
        public int index;
        public DateTime addTimeout;
        public DateTime lastRunDate = DateTime.MinValue;
        public bool paused = false;

        public void Serialize(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Create);
            new BinaryFormatter().Serialize(stream, this);
            stream.Close();
        }

        public static TaskState Unserialize(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open);
            var state = new BinaryFormatter().Deserialize(stream) as TaskState;
            stream.Close();
            return state;
        }
    }
}
