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
                Day = DayOfWeek.Monday,
                startTime = new DateTime(1, 1, 1, 13, 00, 00),
                finishTime = new DateTime(1, 1, 1, 14, 00, 00)
            });
            mathima_a.Add(new Event
            {
                Day = DayOfWeek.Monday,
                startTime = new DateTime(1, 1, 1, 15, 00, 00),
                finishTime = new DateTime(1, 1, 1, 16, 00, 00)
            });
            var m_a = new LessonWithMultipleTimes(mathima_a);
            optionalEvents.Add(m_a);

            double offset = double.MaxValue;
            Event bestEvent;

            //we also need to search between the multiple LessonWithMultipleTimes

            foreach (var time in m_a.Times)
            {
                //find all the events that are in the same date as mandatory events
                var result = mandatoryEvents.Where(x => x.Day == time.Day).ToList();
                if (result.Count != 0)
                {
                    foreach (var a in result)
                    {
                        //double currOffset = Math.Abs((time.startTime - a.startTime).TotalMinutes);
                        //if(currOffset < offset)
                        //{
                        //    offset = currOffset;
                        //}

                        if (a.startTime < time.startTime)//to ypoxreotiko einai prin apo to ergastirio
                        {
                            var currOffset = (time.startTime - a.finishTime).TotalMinutes;
                            if (currOffset < offset)
                            {
                                offset = currOffset;
                                bestEvent = time;
                                //we also need to track which "time" this was
                            }
                        }
                        else
                        {
                            var currOffset = (a.startTime - time.finishTime).TotalMinutes;
                            if(currOffset < offset)
                            {
                                offset = currOffset;
                                bestEvent = time;
                            }
                        }

                    }
                }
                //handle if there are no events
                else
                {
                    double currOffset = 13; //we should find nearest event and calculate that time
                    if (currOffset < offset)
                    {
                        offset = currOffset;
                        bestEvent = time;
                    }

                }
            }


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

            return DayOfWeek.Saturday; //error
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
