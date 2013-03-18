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
        public List<KeyValuePair<DateTime, int>> deletedChunks = new List<KeyValuePair<DateTime, int>>();
        public DateTime addTimeout;
        [field: NonSerialized]
        public object locked = new object();
        [field: NonSerialized]
        public bool backgroundDeleting = false;

        public DateTime GetLastDeletingDate()
        {
            if (deletedChunks.Count == 0)
                return DateTime.MinValue;
            else if(deletedChunks.Count == 1)
                return deletedChunks[0].Key.AddMinutes(-30);
            else
                return deletedChunks[deletedChunks.Count - 1].Key;
        }

        public bool CheckAvailableDeleteIndex(DateTime now, int index)
        {
            for (int i = 0, count = deletedChunks.Count; i < count; ++i)
            {
                var pair = deletedChunks[i];
                var THREE = 3;
                if (pair.Key.AddDays(THREE) < now && index < pair.Value)
                    return true;
            }
            return false;
        }

        public int GetDeletingIndex()
        {
            if (deletedChunks.Count == 0)
                return 0;
            else
                return deletedChunks[deletedChunks.Count - 1].Value;
        }

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
