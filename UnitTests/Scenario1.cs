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
                IsFixed = true,
                Id = 1,
            };
            ypoxreotiko_a.SetTime(12, 00, 14, 00);

            var ypoxreotiko_b = new Event()
            {
                Id = 2,
                Day = DayOfWeek.Monday,
                IsFixed = true
            };
            ypoxreotiko_b.SetTime(18, 00, 19, 00);
            ypoxreotika.Add(ypoxreotiko_a);
            ypoxreotika.Add(ypoxreotiko_b);

            List<Event> ergastirioA = new List<Event>();


            var ergastirioA_A = new Event
            {
                Id = 3,
                Day = DayOfWeek.Monday,
                IsFixed = false,
            };
            ergastirioA_A.SetTime(10, 00, 11, 00);


            ergastirioA.Add(ergastirioA_A);

            var ergastirioA_B = new Event
            {
                Id = 4,
                Day = DayOfWeek.Monday,
                IsFixed = false,
            };
            ergastirioA_B.SetTime(15, 00, 16, 00);

            ergastirioA.Add(ergastirioA_B);


            
            List<LessonWithMultipleTimes> lessonWithMultiples = new List<LessonWithMultipleTimes>();
            lessonWithMultiples.Add(new LessonWithMultipleTimes(ergastirioA));

            var tuple = ApplicationLogic.Logic(ypoxreotika, lessonWithMultiples, 20);

            var times = tuple.Item1;
            var memory = tuple.Item2;

            

        }

    }
}
