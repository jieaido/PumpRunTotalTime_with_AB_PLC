using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PumpTotalTime
{
    public class ConfigPump
    {
        public static HashSet<string> PumpList=new HashSet<string>();
        public static string DbConstr;
        public static List<PumpCfg> PumpCfgs=new List<PumpCfg>();
    }
}
