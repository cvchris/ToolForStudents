using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public static class ApplicationLogic
    {
        private static List<Event> _allEvents;
        private static List<string> combinations = new List<string>();

        //roundTripWeight = the time to go to the university and back. (We assume that this will be done once a day).
        public static Tuple<List<TimeCalculate>,List<List<int>>> Logic(List<Event> mandatoryEvents, List<LessonWithMultipleTimes> lessonsWithMultipleTimes, double roundTripWeight)
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
                var result = CalculateTotalSpareTime(temp,roundTripWeight);
                times.Add(new TimeCalculate { MemoryId = counter, Time = result });
                counter++;
            }

           return Tuple.Create(times, memory);
        }

        private static void checkForOverlapping()
        {
            //get by day.
            IEnumerable<DayOfWeek> values = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>(); //gets all days in Enumerable
            foreach (DayOfWeek day in values)
            {
                var allEventsInSpecificDay = _allEvents.Where(x => x.Day == day).ToList();
                allEventsInSpecificDay = allEventsInSpecificDay.OrderBy(x => x.startTime).ToList();

                //not working, we could be having more that one overlapping

                //σωστος τροπος: για καθε event στην ημερα, τσεκαρε με ΟΛΑ τα υπολοιπα events εαν γίνεται καποιο overlapping... ισως ενας τροπος optimization είναι να μην κοιταει με τα προηγουμενα του (να το σιγουρεψω).

                //check for overlapping
                for (int i = 0; i < allEventsInSpecificDay.Count; i++)
                {
                    foreach (var ev in allEventsInSpecificDay)
                    {
                        if (ev != allEventsInSpecificDay[i]) //we don't want to check with itself
                        {
                            if (ev.Conflicts(allEventsInSpecificDay[i]))
                            {
                                //ev and allEventsInSpecificDay[i] conflict.


                                //check if either of them is non-fixed
                                if (allEventsInSpecificDay[i].IsFixed && ev.IsFixed)
                                {
                                    throw new Exception("We have a problem because both are fixed.");
                                }
                                else
                                {
                                    if (!allEventsInSpecificDay[i].IsFixed && !ev.IsFixed)
                                    {
                                        //TODO: both not fixed, find which is optimal to delete from the two
                                    }

                                    bool wasDeletedA = false;
                                    if (!allEventsInSpecificDay[i].IsFixed)
                                    {
                                        wasDeletedA = deleteIfNoProblem(allEventsInSpecificDay[i].Id);
                                    }

                                    bool wasDeletedB = false;
                                    if (!ev.IsFixed && !wasDeletedA) //meaning that the other one wasn't deleted. If it was deleted, problem solved.
                                    {
                                        wasDeletedB = deleteIfNoProblem(ev.Id);
                                    }

                                    if (!wasDeletedA && !wasDeletedB)
                                    {
                                        throw new Exception("We have a problem, both that are overlapping are not fixed and cannot be deleted.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// if it is not the only one appearence, we can delete it
        /// </summary>
        /// <returns>True if it was deleted</returns>
        private static bool deleteIfNoProblem(int EventId)
        {
            var thisEvent = _allEvents.First(x => x.Id == EventId);

            //check if this event has another appearence
            var otherEventsFromSameLesson = _allEvents.Where(x => x.Lesson == thisEvent.Lesson).ToList();
            if (otherEventsFromSameLesson.Count > 1)
            {
                //check if the other ones are overlapping?
                //for now we will just delete this event because it is not required
                _allEvents.Remove(_allEvents.Single(x => x.Id == EventId));
                return true;
            }
            else
            {
                //this event cannot be deleted because there are not other options
                return false;

            }
        }
        static void combos(int pos, List<List<Event>>c, String soFar)
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

        static double CalculateTotalSpareTime(List<Event> state,double roundTripWeight)
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
