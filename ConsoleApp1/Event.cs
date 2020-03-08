using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    public class Event
    {
        public int Id { get; set; }

        /// <summary>
        /// If not fixed, we need that
        /// </summary>
        public LessonWithMultipleTimes Lesson { get; set; }

        public DayOfWeek Day { get; set; }

        public bool IsFixed { get; set; }


        public DateTime startTime { get; set; }
        public DateTime finishTime { get; set; }

        /// <summary>
        /// Returns true if two events are conflicting with each other
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public bool Conflicts(Event ev)
        {
            if (this.Day != ev.Day)
                return false;

            //two ways of conflicting: if (a.finishtime > b.starttime) or (a.starttime < b.finishtime)
            if (this.startTime < ev.finishTime && this.finishTime > ev.startTime)
            {
                return true;

            }

            return false;
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

    }

   
}
