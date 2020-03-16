using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class ApplicationLogic
    {
        private List<Event> _allEvents;
        private List<string> combinations = new List<string>();

        //roundTripWeight = the time to go to the university and back. (We assume that this will be done once a day).
        public Tuple<List<TimeCalculate>,List<List<int>>> Logic(List<Event> mandatoryEvents, List<LessonWithMultipleTimes> lessonsWithMultipleTimes, double roundTripWeight)
        {
            List<Event> allEvents = new List<Event>();
            allEvents.AddRange(mandatoryEvents);
            List<List<Event>> asd= new List<List<Event>>(); 
            foreach (var lesson in lessonsWithMultipleTimes)
            {
                asd.Add(lesson.Times);
                allEvents.AddRange(lesson.Times);
            }
            _allEvents = allEvents;

            //check for all overlapping events. if an non-fixed event has multiple occasions and one occasion is at the same time as one fixed, we should delete it from allEvents.
            //also if the non-fixed event has only one occassion (LessonWithMultipleETimes.Events.Count =1), and this is overlapping with a fixed event, log warning that there is no way that you can go to both
            //checkForOverlapping(); //has bug, to fix

            //we need to make a tree with all the available options
            //we need to allocate 
            int memoryToAlloc = 1;
            foreach (var lesson in lessonsWithMultipleTimes)
            {
                memoryToAlloc *= lesson.Times.Count; //that many combinations
            }

            //we need to have a list with memoryToAlloc positions and in each position add a combination of ids, it should store lessonsWithMultipleTimes.Count letters in each one.
            List<List<int>> memory = new List<List<int>>(); //this stores the ids foreach possible combination

            combos(0, asd, string.Empty); //gets all combinations
            
            for (int i = 0; i < combinations.Count; i++)
            {
                var tempList = new List<int>();
                var splitted = combinations[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var str in splitted)
                {
                    tempList.Add(int.Parse(str));
                }
                memory.Add(tempList);
            }

            List<TimeCalculate> times = new List<TimeCalculate>();
            int counter = 0;
            foreach (var comb in memory)
            {
                List<Event> temp = mandatoryEvents.ToList(); //without destroying the original list
                foreach (var EventId in comb)
                {
                    Event ev = _allEvents.First(x => x.Id == EventId);
                    temp.Add(ev);
                }

                var hasOverlapping = checkForOverLapping(temp);

                var result = CalculateTotalSpareTime(temp,roundTripWeight);
                times.Add(new TimeCalculate { MemoryId = counter, Time = result, HasOverlapping = hasOverlapping });
                counter++;
            }

           return Tuple.Create(times, memory);
        }


        
        void combos(int pos, List<List<Event>>c, String soFar)
        {
            if (pos == c.Count)
            {
                combinations.Add(soFar);
                //System.out.writeln(soFar);
                return;
            }
            for (int i = 0; i != c[pos].Count; i++)
            {
                combos(pos + 1, c, soFar + c[pos][i] + ",");
            }
        }

        public bool checkForOverLapping(List<Event> state)
        {
            bool hasOverlap = false;
            IEnumerable<DayOfWeek> days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(); //gets all days in Enumerable
            foreach(DayOfWeek day in days)
            {
                var allEventsInSpecificDay = state.Where(x => x.Day == day).ToList();
                allEventsInSpecificDay = allEventsInSpecificDay.OrderBy(x => x.startTime).ToList();
                //check if there is overlap
                //hasOverlap = true;
                for (int i = 0; i < allEventsInSpecificDay.Count; i++)
                {
                    for (int j = i+1; j < allEventsInSpecificDay.Count; j++)
                    {
                        if(allEventsInSpecificDay[i].Conflicts(allEventsInSpecificDay[j]))
                        {
                            hasOverlap = true;
                        }
                    }
                }

            }
            return hasOverlap;
        }

        private double CalculateTotalSpareTime(List<Event> state,double roundTripWeight)
        {
            //double tempWeight = roundTripWeight; //initialise it with the time he has to go to the uni and back
            IEnumerable<DayOfWeek> values = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(); //gets all days in Enumerable
            double xronospetamenos = 0;
            foreach (DayOfWeek day in values)
            {
                var currentDayEvents = state.Where(x => x.Day == day).ToList();
                currentDayEvents = currentDayEvents.OrderBy(x => x.startTime).ToList();

                if(currentDayEvents.Count >= 1)
                {
                    xronospetamenos += roundTripWeight;
                }

                for (int i = 0; i < currentDayEvents.Count - 1; i++)
                {
                    var afterEvent = currentDayEvents[i+1];
                    xronospetamenos += (afterEvent.startTime - currentDayEvents[i].finishTime).TotalMinutes;
                }
            }
                
            return xronospetamenos;
        }


    }
}
