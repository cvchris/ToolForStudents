using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    /// <summary>
    /// It stores each state with the id of this state
    /// </summary>
    public class TimeCalculate
    {
        /// <summary>
        /// The location on the table Memory
        /// </summary>
        public int MemoryId { get; set; }

        public double Time { get; set; }

        /// <summary>
        /// We should store if this state has overlapping.
        /// We could check to dismiss states that have overlapping & if all states have overlapping, throw warning.
        /// </summary>
        public bool HasOverlapping { get; set; }

    }
}
