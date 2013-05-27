using System;
using System.IO;

namespace Scheduler
{
    class Utilities
    {
        public static void LockThisInstance()
        {
            try {
                File.Create(".lock", 1, FileOptions.DeleteOnClose);
            } catch (Exception) {
                throw new ApplicationException("Application can't be access to file '.lock' and will be closed.");
            }
        }
    }
}
