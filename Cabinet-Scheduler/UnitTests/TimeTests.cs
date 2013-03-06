using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scheduler;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void CalcRemainingTimeMinutesTest()
        {
            var a = new TaskInfo();

            a.end = new DateTime(2013, 03, 14, 14, 0, 0);
            var now = new DateTime(2013, 03, 10, 14, 0, 0);

            var period = a.calcRemainingTimeMinutes(now);

            Assert.AreEqual(period, 4 * 12 * 60 - 1);
        }

        [TestMethod]
        public void CalcRemainingTimeMinutesTest2()
        {
            var a = new TaskInfo();

            a.end = new DateTime(2013, 03, 14, 9, 0, 0);
            var now = new DateTime(2013, 03, 10, 14, 0, 0);

            var period = a.calcRemainingTimeMinutes(now);

            Assert.AreEqual(period, 4 * 12 * 60 - 1 - 5 * 60);
        }
    }
}
