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

        public Boolean isFixed { get; set; }


        public DateTime startTime { get; set; }
        public DateTime finishTime { get; set; }
    }

   
}
