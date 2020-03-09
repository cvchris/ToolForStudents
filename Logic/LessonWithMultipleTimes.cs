using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    /// <summary>
    /// πχ εργαστήριο που έχει πολλές ώρες και μέρες
    /// </summary>
    public class LessonWithMultipleTimes
    {
        public bool IsFixed { get; private set; }
        public List<Event> Times { get; private set; }

        public LessonWithMultipleTimes(List<Event> times,bool isFixed)
        {
            foreach (var ev in times)
            {
                ev.Lesson = this;
            }
            IsFixed = isFixed;
            Times = times;
        }

        public string Name { get; set; }

        public string NumberOfEvents
        {
            get
            {
                return Times.Count + " φορές";
            }
        }
    }
}
