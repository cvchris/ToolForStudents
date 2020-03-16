using Logic;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class CheckForOverlapping
    {
        [Fact]
        public void Test()
        {
            var ev = new Event
            {
                Day = DayOfWeek.Monday,
            };
            ev.SetTime(10, 00, 12, 00);
            var ev2 = new Event
            {
                Day = DayOfWeek.Monday
            };
            ev2.SetTime(11, 00, 13, 00);
            Assert.True(ev.Conflicts(ev2));
            List<Event> listEv = new List<Event>();
            listEv.Add(ev);
            listEv.Add(ev2);
            var result = new ApplicationLogic().checkForOverLapping(listEv);
            Assert.True(result);


        }

    }
}
