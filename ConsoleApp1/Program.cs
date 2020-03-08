using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var mandatoryEvents = new List<Event>();
            var optionalEvents = new List<LessonWithMultipleTimes>();
            //format: Mon 1000-1200



            var input = Console.ReadLine();
            while (input != "exit" && input != "c")
            {
                readData(input);
                input = Console.ReadLine();
            }

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

            ApplicationLogic.Logic(mandatoryEvents, optionalEvents, 1);


        }

        static DayOfWeek GetDay(string d)
        {
            if (d == "Mon")
                return DayOfWeek.Monday;
            else if (d == "Tue")
                return DayOfWeek.Tuesday;
            else if (d == "Wed")
                return DayOfWeek.Wednesday;
            else if (d == "Thu")
                return DayOfWeek.Thursday;
            else if (d == "Fri")
                return DayOfWeek.Friday;
            else if (d == "Sat")
                return DayOfWeek.Saturday;
            else if (d == "Sun")
                return DayOfWeek.Sunday;
            else
                throw new Exception("Not Valid input");
            
        }

        static Event readData(string input)
        {
            var ev = new Event();
            var d = input.Substring(0, 3);
            ev.Day = GetDay(d);

            var startTime = input.Substring(4, 4);
            var startHour = int.Parse(startTime.Substring(0, 2));
            var startMin = int.Parse(startTime.Substring(2, 2));

            var finishTime = input.Substring(9, 4);
            var finishHour = int.Parse(finishTime.Substring(0, 2));
            var finishMin = int.Parse(finishTime.Substring(2, 2));

            int.Parse(startTime);
            ev.startTime = new DateTime(1, 1, 1, startHour, startMin, 0);
            ev.finishTime = new DateTime(1, 1, 1, finishHour, finishMin, 0);

            return ev;
        }

    }
}
