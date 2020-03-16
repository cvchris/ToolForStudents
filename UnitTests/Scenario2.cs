using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;
using Logic;
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
            var m_a_a = new Event
            {
                Id = 1,
                IsFixed = false,
                Day = DayOfWeek.Monday,
            };
            m_a_a.SetTime(13, 00, 14, 00);

            mathima_a.Add(m_a_a);
            var m_a_b = new Event
            {
                Id = 2,
                IsFixed = false,
                Day = DayOfWeek.Monday,
            };
            m_a_b.SetTime(15, 00, 16, 00);
            mathima_a.Add(m_a_b);
            var m_a_c = new Event
            {
                Id = 3,
                IsFixed = false,
                Day = DayOfWeek.Tuesday,
            };
            m_a_c.SetTime(15, 00, 16, 00);
            mathima_a.Add(m_a_c);

            var mathima_b_events = new List<Event>();
            var m_b_a = new Event
            {
                Id = 4,
                IsFixed = false,
                Day = DayOfWeek.Wednesday,
            };
            m_b_a.SetTime(15, 00, 16, 00);
            mathima_b_events.Add(m_b_a);
            var m_b_b = new Event
            {
                Id = 5,
                IsFixed = false,
                Day = DayOfWeek.Wednesday,
            };
            m_b_b.SetTime(19, 00, 20, 00);
            mathima_b_events.Add(m_b_b);

            var mandatoryA = new Event
            {
                Id = 6,
                IsFixed = true,
                Day = DayOfWeek.Wednesday,
            };
            mandatoryA.SetTime(17, 00, 18, 00);

            mandatoryEvents.Add(mandatoryA);

            var m_a = new LessonWithMultipleTimes(mathima_a, false);
            var m_b = new LessonWithMultipleTimes(mathima_b_events, false);


            optionalEvents.Add(m_a);
            optionalEvents.Add(m_b);

            var tuple = new ApplicationLogic().Logic(mandatoryEvents, optionalEvents, 20);
            var times = tuple.Item1;
            var memory = tuple.Item2;
        }
    }
}
