using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PumpTotalTime
{
    public class Pump
    {
        public List<PumpTime> PumpTimes=new List<PumpTime>();
        public string PumpName { get; set; }
        public string PumpId { get; set; }

        public TimeSpan TotalRunTime { get; set; }
       // public DateTime LastStartTime { get; set; }
    }

    public class PumpTime
    {
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }

//        public TimeSpan RunTime
//        {
//            get { return StopTime - StartTime; }
//        }原来是这样写的
        public TimeSpan RunTime => StopTime - StartTime;
    }
}
