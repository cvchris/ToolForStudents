using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// If not fixed, we need that
        /// </summary>
        public LessonWithMultipleTimes Lesson { get; set; }

        public DayOfWeek Day { get; set; }

        public bool IsFixed { get; set; }


        public DateTime startTime { get; private set; }
        public DateTime finishTime { get; private set; }

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

        public void SetTime(int startHour,int startMinute,int finishHour,int finishMinute)
        {
            startTime = new DateTime(1, 1, 1, startHour, startMinute, 00);
            finishTime = new DateTime(1, 1, 1, finishHour, finishMinute, 00);
            
        }

        public string DateTimeFormat
        {
            get
            {
                return startTime.ToShortTimeString() + " - " + finishTime.ToShortTimeString();
                //return startTime.ToString("h mm") + " - " + finishTime.ToString("h mm");
                //return startTime.Hour.ToString() + ":" + startTime.Minute.ToString() + " - " + finishTime.Hour.ToString() + ":" + finishTime.Minute.ToString();
            }
        }

        /// <summary>
        /// Used for showing for example: Monday 10:00 AM - 12:00 PM
        /// </summary>
        public string DetailView
        {
            get
            {
                return this.Day + " " + DateTimeFormat;
            }
        }
    }

   
}
