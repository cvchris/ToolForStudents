using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1;
using Xunit;

namespace UnitTests
{
    public class Scenario1
    {
        [Fact]
        public void Test1()
        {
            List<Event> ypoxreotika = new List<Event>();
            var ypoxreotiko_a = new Event()
            {
                Day = DayOfWeek.Monday,
                startTime = new DateTime(1, 1, 1, 12, 00, 00),
                finishTime = new DateTime(1, 1, 1, 14, 00,00),
                IsFixed = true,
                Id = 1
            };

            var ypoxreotiko_b = new Event()
            {
                Id = 2,
                Day = DayOfWeek.Monday,
                startTime = new DateTime(1, 1, 1, 18, 00, 00),
                finishTime = new DateTime(1, 1, 1, 19, 00,00),
                IsFixed = true
            };
            ypoxreotika.Add(ypoxreotiko_a);
            ypoxreotika.Add(ypoxreotiko_b);

            List<Event> ergastirioA = new List<Event>();

            ergastirioA.Add(new Event
            {
                Id = 3,
                Day = DayOfWeek.Monday,
                IsFixed = false,
                startTime = new DateTime(1,1,1,10,00,00),
                finishTime = new DateTime(1, 1, 1, 11, 00, 00)
            });
            ergastirioA.Add(new Event
            {
                Id = 4,
                Day = DayOfWeek.Monday,
                IsFixed = false,
                startTime = new DateTime(1, 1, 1, 15, 00, 00),
                finishTime = new DateTime(1, 1, 1, 16, 00, 00)
            });


            
            List<LessonWithMultipleTimes> lessonWithMultiples = new List<LessonWithMultipleTimes>();
            lessonWithMultiples.Add(new LessonWithMultipleTimes(ergastirioA));

            var tuple = ApplicationLogic.Logic(ypoxreotika, lessonWithMultiples, 20);

            var times = tuple.Item1;
            var memory = tuple.Item2;

            

        }

    }
}
