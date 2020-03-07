using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1;

namespace ConsoleApp1
{
    public static class ApplicationLogic
    {
        public static void Logic(List<Event> mandatoryEvents, List<LessonWithMultipleTimes> lessonsWithMultipleTimes)
        {
            List<Event> allEvents = new List<Event>();
            allEvents.AddRange(mandatoryEvents);
            foreach (var lesson in lessonsWithMultipleTimes)
            {
                allEvents.AddRange(lesson.Times);
            }

            var notFixed = allEvents.Where(x => x.isFixed = false); //all the not fixed events



            foreach (var notFixedEvent in notFixed)
            {
                //search in allnonfixed if this is the only one with that lessonId. (if it is then automatically add it to final list- make it fixed)
                if(notFixed.Where(x=> x.Lesson == notFixedEvent.Lesson).ToList().Count == 1)
                {
                    notFixedEvent.isFixed = true; //we may need to mark it in another way
                }
                else
                {
                    var sameDayEvents = allEvents.Where(x => x.Day == notFixedEvent.Day).ToList();
                    //exclude itself from the search list
                    var thisEvent = sameDayEvents.Single(x => x.Id == notFixedEvent.Id);
                    sameDayEvents.Remove(thisEvent);

                    //we need to sum the before and after offset but be careful to not sum it with
                    //an event from the same LessonWithMultipleTimes
                    
                    double offset = double.MaxValue;
                    Event bestEvent;

                    //find nearest and calculate offset
                    foreach (var sameDayEvent in sameDayEvents)
                    {
                        if(sameDayEvent.Lesson == notFixedEvent.Lesson)
                        {
                            continue; //maybe we don't need to check this
                        }

                        if (sameDayEvent.startTime < notFixedEvent.startTime)//to ypoxreotiko einai prin apo to ergastirio
                        {
                            var currOffset = (notFixedEvent.startTime - sameDayEvent.finishTime).TotalMinutes;
                            if (currOffset < offset)
                            {
                                offset = currOffset;
                                bestEvent = notFixedEvent;
                                //we also need to track which "time" this was
                            }
                        }
                        else
                        {
                            var currOffset = (a.startTime - time.finishTime).TotalMinutes;
                            if (currOffset < offset)
                            {
                                offset = currOffset;
                                bestEvent = time;
                            }
                        }
                    }




                }
            }
            //we need to make sure that for each LessonWithMultipleTimes there is only one event in the final calendar
        }

    }
}
