using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using Medium;
using Scheduler;

namespace UnitTests
{
    [TestClass]
    public class TaskTests
    {
        //[TestMethod]
        //public void TaskSavingTest()
        //{
        //    var task = new TaskInfo();

        //    task.Id = "###31";
        //    task.Interval = 5;
        //    task.Start = DateTime.Now;
        //    task.Type = TaskType.Дома;
        //    task.Count = 131;

        //    task.Save("TestFile.meta");

        //    var task2 = new TaskInfo();
        //    task2.Load("TestFile.meta");

        //    Assert.AreEqual(task.Id, task2.Id);
        //    Assert.AreEqual(task.Count, task2.Count);
        //    Assert.AreEqual(task.Interval, task2.Interval);
        //    Assert.AreEqual(task.Type, task2.Type);
        //    Assert.AreEqual(task.Start.Date, task2.Start.Date);
        //}

        //[TestMethod]
        //public void TasksSavingTest()
        //{
        //    Directory.CreateDirectory("testTasks");

        //    var task = new TaskInfo();

        //    task.Id = "myId31";
        //    task.Interval = 5;
        //    task.Start = DateTime.Now;
        //    task.Type = TaskType.Дома;
        //    task.Count = 131;

        //    task.Save("testTasks/myId31.meta");

        //    var taskState = new TaskState();

        //    taskState.Info = task;
        //    taskState.Index = 100;
        //    taskState.khEmpty = true;

        //    var tasks = new TasksManager();

        //    tasks.tasks.Add(taskState);

        //    tasks.Save("file.meta");


        //    var tasks2 = new TasksManager();
        //    tasks2.Load("file.meta", "testTasks");

        //    Assert.AreEqual(tasks.tasks[0].Info.Id, tasks2.tasks[0].Info.Id);
        //    Assert.AreEqual(tasks.tasks[0].Index, tasks2.tasks[0].Index);
        //    Assert.AreEqual(tasks.tasks[0].khEmpty, tasks2.tasks[0].khEmpty);
        //}
    }
}
