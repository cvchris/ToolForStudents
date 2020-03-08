using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1;
using Xunit;

namespace UnitTests
{
    public class Scenario2
    {

        [Fact]
        public void Test()
        {
            var mandatoryEvents = new List<Event>();
            var optionalEvents = new List<LessonWithMultipleTimes>();


            var mathima_a = new List<Event>();
            mathima_a.Add(new Event
            {
                Id = 1,
                IsFixed = false,
                Day = DayOfWeek.Monday,
                startTime = new DateTime(1, 1, 1, 13, 00, 00),
                finishTime = new DateTime(1, 1, 1, 14, 00, 00)
            });
            mathima_a.Add(new Event
            {
                Id = 2,
                IsFixed = false,
                Day = DayOfWeek.Monday,
                startTime = new DateTime(1, 1, 1, 15, 00, 00),
                finishTime = new DateTime(1, 1, 1, 16, 00, 00)
            });
            mathima_a.Add(new Event
            {
                Id = 3,
                IsFixed = false,
                Day = DayOfWeek.Tuesday,
                startTime = new DateTime(1, 1, 1, 15, 00, 00),
                finishTime = new DateTime(1, 1, 1, 16, 00, 00)
            });

            var mathima_b_events = new List<Event>();
            mathima_b_events.Add(new Event
            {
                Id = 4,
                IsFixed = false,
                Day = DayOfWeek.Wednesday,
                startTime = new DateTime(1, 1, 1, 15, 00, 00),
                finishTime = new DateTime(1, 1, 1, 16, 00, 00)
            });
            mathima_b_events.Add(new Event
            {
                Id = 5,
                IsFixed = false,
                Day = DayOfWeek.Wednesday,
                startTime = new DateTime(1, 1, 1, 19, 00, 00),
                finishTime = new DateTime(1, 1, 1, 20, 00, 00)

            });

            mandatoryEvents.Add(new Event
            {
                Id = 6,
                IsFixed = true,
                Day = DayOfWeek.Wednesday,
                startTime = new DateTime(1, 1, 1, 17, 00, 00),
                finishTime = new DateTime(1, 1, 1, 18, 00, 00)
            });

            var m_a = new LessonWithMultipleTimes(mathima_a);
            var m_b = new LessonWithMultipleTimes(mathima_b_events);


            optionalEvents.Add(m_a);
            optionalEvents.Add(m_b);

            var tuple= ApplicationLogic.Logic(mandatoryEvents, optionalEvents, 20);
            var times = tuple.Item1;
            var memory = tuple.Item2;
        }
    }
}
