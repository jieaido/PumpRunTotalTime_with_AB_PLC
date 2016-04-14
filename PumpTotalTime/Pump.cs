using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PumpTotalTime
{
    public class Pump
    {
        public string PumpName { get; set; }
        public string PumpId { get; set; }

        public TimeSpan TotalRunTime { get; set; }
       // public DateTime LastStartTime { get; set; }
    }
}
