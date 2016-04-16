using System.Collections.Generic;

namespace PumpTotalTime
{
    public  class ConfigPump
    {
        public static List<Pump> Pumpinfos=new List<Pump>();
        public static HashSet<string> ReadDbPumpidList = new HashSet<string>();
        public static string DbConstr;
        public static List<PumpCfg> ReadPumpCfgs = new List<PumpCfg>();
        public static Dictionary<string, string> MergePumpListCfg = new Dictionary<string, string>();
    }

    public class PumpCfg
    {
        public string Pumpid;
        public string Pumpname;
    }
}