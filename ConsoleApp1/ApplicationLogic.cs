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

            var notFixed = allEvents.Where(x => x.isFixed = false);

            foreach (var notFixedEvent in notFixed)
            {
                //search in allnonfixed if this is the only one with that lessonId. (if it is then automatically add it to final list- make it fixed)
                if(notFixed.Where(x=> x.Lesson == notFixedEvent.Lesson).ToList().Count == 1)
                {
                    notFixedEvent.isFixed = true;
                }
                else
                {
                    var sameDayEvents = allEvents.Where(x => x.Day == notFixedEvent.Day).ToList();



                }
            }
        }

    }
}
