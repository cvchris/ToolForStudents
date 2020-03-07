using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1;

namespace ConsoleApp1
{
    public class TimeCalculate
    {
        public Event Event { get; set; }

        public double Time { get; set; }

        public Event BeforeEventDependency { get; set; }

        public Event AfterEventDependency { get; set; }

    }
}
