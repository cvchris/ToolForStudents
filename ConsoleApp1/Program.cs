using System;
using System.Collections.Generic;
using System.Linq;
using Logic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int idCounter = 0; //provides the id
            var mandatoryEvents = new List<Event>();
            var optionalEvents = new List<LessonWithMultipleTimes>();
            //format: Mon 1000-1200
            double roundtriptime = 0;

            Console.WriteLine("Enter how much time to go to the university and back (in minutes)");
            var ii = Console.ReadLine();
            while(!double.TryParse(ii,out roundtriptime))
            {
                Console.WriteLine("Error, try again");
                ii = Console.ReadLine();
            }

            Console.WriteLine("Enter the fixed lessons in the format: Name,Day 1000-1200,Day 1400-1600");
            Console.WriteLine("To go to the non fixed, type \"c\"");

            var input = Console.ReadLine();
            while (input != "exit" && input != "c")
            {
                List<Event> evTemp = ConsoleHelpers.ParseData(input,true,ref idCounter);
                mandatoryEvents.AddRange(evTemp);
                input = Console.ReadLine();
            }

            if(input == "c")
            {
                Console.WriteLine("Please type the non fixed in the same format. When done type \"done\" ");
                input = Console.ReadLine();
                while(input !="done")
                {
                    List<Event> evTemp = ConsoleHelpers.ParseData(input, false, ref idCounter);
                    LessonWithMultipleTimes lesson = new LessonWithMultipleTimes(evTemp,false);
                    optionalEvents.Add(lesson);
                    input = Console.ReadLine();
                }
            }
            var tuple = ApplicationLogic.Logic(mandatoryEvents, optionalEvents, roundtriptime);
            var times = tuple.Item1;
            var combinations = tuple.Item2;
            TimeCalculate min = times.OrderBy(x => x.Time).First();
            List<int> idsToAdd = combinations[min.MemoryId];
            List<Event> toDisplay = mandatoryEvents.ToList(); //arxika vale ola ta fixed events
            List<Event> allOptionalEvents = new List<Event>();
            foreach (var lesson in optionalEvents)
            {
                allOptionalEvents.AddRange(lesson.Times);
            }
            foreach(var id in idsToAdd)
            {
                toDisplay.Add(allOptionalEvents.First(x => x.Id == id));
            }

            toDisplay = toDisplay
                .OrderBy(x => ((int)x.Day + 6) % 7)
                .ThenBy(x=> x.startTime)
                .ToList();


            Console.WriteLine("To teliko programma einai:");
            
            foreach(var ev in toDisplay)
            {
                Console.WriteLine("Day "+ ev.Day + " Mathima: " + ev.Name +" Start: " + ev.startTime.Hour+":"+ev.startTime.Minute +" Finish: "+ ev.finishTime.Hour +":"+ev.finishTime.Minute);
            }




        }
    }
}
