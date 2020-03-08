using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1;

namespace ConsoleApp1
{
    /// <summary>
    /// πχ εργαστήριο που έχει πολλές ώρες και μέρες
    /// </summary>
    public class LessonWithMultipleTimes
    {
        public List<Event> Times { get; set; }

        public LessonWithMultipleTimes(List<Event> times)
        {
            foreach (var ev in times)
            {
                ev.Lesson = this;
            }

            Times = times;
        }

    }
}
