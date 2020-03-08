using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1;

namespace ConsoleApp1
{
    public class ConsoleHelpers
    {
        public static DayOfWeek GetDay(string d)
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

        public static List<Event> ParseData(string input,bool isFixed,ref int id)
        {
            string[] data = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var evName = data[0];
            List<Event> events = new List<Event>();
            for (int i = 1; i < data.Length; i++)
            {
                var ev = new Event();
                ev.Id = ++id;
                ev.Name = evName;
                var d = data[i].Substring(0, 3);
                ev.Day = GetDay(d);
                var startTime = data[i].Substring(4, 4);
                int startHour = int.Parse(startTime.Substring(0, 2));
                int startMin = int.Parse(startTime.Substring(2, 2));

                var finishTime = data[i].Substring(9, 4);
                int finishHour = int.Parse(finishTime.Substring(0, 2));
                int finishMin = int.Parse(finishTime.Substring(2, 2));
                ev.SetTime(startHour, startMin, finishHour, finishMin);
                ev.IsFixed = isFixed;
                events.Add(ev);
            }

            return events;
        }

    }
}
