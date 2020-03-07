using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1;

namespace ConsoleApp1
{
    /// <summary>
    /// Class that we will need to calculate which Events to remove
    /// </summary>
    public class TimeCalculate
    {
        public Event Event { get; set; }

        public double Time { get; set; }

        public List<Event> Dependencies { get; set; }

        //public Event BeforeEventDependency { get; set; }

        //public Event AfterEventDependency { get; set; }

    }
}
