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
        public List<DateTime> deletedChunks;
        public DateTime addTimeout;
        public DateTime removeTimeout;
        public object locked = new object();
        public bool backgroundDeleting = false;

        public void Serialize(string fileName)
        {
            new BinaryFormatter().Serialize(new FileStream(fileName, FileMode.Create), this);
        }

        public static TaskState Unserialize(string fileName)
        {
            return new BinaryFormatter().Deserialize(new FileStream(fileName, FileMode.Open)) as TaskState;
        }
    }
}
