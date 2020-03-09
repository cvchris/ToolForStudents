using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public static class EventId
    {
        private static int _id = 0;
        public static int getAndIncrementId()
        {
            return ++_id;
        }
    }
}
