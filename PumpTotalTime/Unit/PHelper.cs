using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Newtonsoft.Json;
using SharedLibrary;

namespace PumpTotalTime
{
    public static class PHelper
    {

        /// <summary>
        /// 获取所有Pump的Id信息并比对配置文件的信息
        /// </summary>
        public static void GetAllPumpId()
        {
            using (var dbc = new OleDbConnection(ConfigPump.DbConstr))
            {
                ConfigPump.ReadDbPumpidList.Clear();
                ConfigPump.ReadPumpCfgs=new List<PumpCfg>();
                ConfigPump.MergePumpListCfg.Clear();
                var cmdtxt = "select distinct TagName  from floatTable";
                //OleDbCommand ocmDbCommand = new OleDbCommand(cmdtxt, dbc);
                // dbc.Open();

                var reader = SqlHelper.ExecuteReader(CommandType.Text, cmdtxt);

                while (reader.Read())
                {
                    ConfigPump.ReadDbPumpidList.Add(reader[0].ToString());
                }
            }

            ConfigPump.ReadPumpCfgs = GetAlreadyConfig();
            MergePumpList();
        }
        /// <summary>
        /// 获取电机的运行信息
        /// </summary>
        /// <param name="pumpid">电机的id</param>
        /// <returns>Pump信息</returns>
        public static Pump GetPumpInfo(string pumpid)

        {
            var pumpTimes = new List<PumpTime>();
            var firstdt = new DateTime();
            var lastRunDateTime = new DateTime();
            var firstStart = true;
            var firststop = true;
            var ts = new TimeSpan();

            var cmdtxt = "select DateAndTime,Val from FloatTable where TagName=?";
            var reader = SqlHelper.ExecuteReader(CommandType.Text, cmdtxt, new OleDbParameter("p1", pumpid));
            var pt = new PumpTime();
            while (reader.Read())
            {
                if (reader["Val"].ToString() == "1")
                {
                    if (firstStart)
                    {
                        pt.StartTime = DateTime.Parse(reader["DateAndTime"].ToString());
                        firstStart = false;
                        firstdt = DateTime.Parse(reader["DateAndTime"].ToString());
                    }
                    else
                    {
                        ts += DateTime.Parse(reader["DateAndTime"].ToString()) - firstdt;
                        firstdt = DateTime.Parse(reader["DateAndTime"].ToString());
                    }
                    lastRunDateTime = DateTime.Parse(reader["DateAndTime"].ToString());
                    firststop = true;
                }
                else
                {
                    if (firststop)
                    {
                        if (pt.StartTime != DateTime.MinValue)
                        {
                            pt.StopTime = DateTime.Parse(reader["DateAndTime"].ToString());
                            pumpTimes.Add(pt);
                            pt = new PumpTime();
                            firststop = false;
                        }
                    }
                    firstStart = true;
                }
            }
            if (pt.StopTime == DateTime.MinValue && pt.StartTime != DateTime.MinValue)
            {
                pt.StopTime = lastRunDateTime;
                pumpTimes.Add(pt);
            }


            var pump = new Pump();

            pump.PumpId = pumpid;
            if (ConfigPump.MergePumpListCfg != null && ConfigPump.MergePumpListCfg.ContainsKey(pumpid))
            {
                pump.PumpName = ConfigPump.MergePumpListCfg[pumpid].ToString();
            }
            else
            {
                pump.PumpName = pumpid;
            }

            pump.PumpTimes = pumpTimes;
            pump.TotalRunTime = ts;


            return pump;
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns>配置文件</returns>
        private static List<PumpCfg> GetAlreadyConfig()
        {
            try
            {
                if (!File.Exists("app.cfg"))
                {
                    return null;
                }
                var filepath = File.ReadAllText("app.cfg");
                var ss = JsonConvert.DeserializeObject<List<PumpCfg>>(filepath);

                return ss;


                // File.WriteAllText("app.txt", JsonConvert.SerializeObject(ss));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 合并读取的信息和配置文件上的信息
        /// </summary>
        private static void MergePumpList()
        {
            
            if (ConfigPump.ReadPumpCfgs == null)
            {
                foreach (var pumpid in ConfigPump.ReadDbPumpidList)
                {
                    ConfigPump.MergePumpListCfg.Add(pumpid, "");
                }
            }
            else
            {
                foreach (var pumpid in ConfigPump.ReadDbPumpidList)
                {
                    if (ConfigPump.ReadPumpCfgs.Exists(p => p.Pumpid == pumpid))
                    {
                        ConfigPump.MergePumpListCfg.Add(pumpid,
                            ConfigPump.ReadPumpCfgs.Find(p => p.Pumpid == pumpid).Pumpname);
                    }
                    else
                    {
                        ConfigPump.MergePumpListCfg.Add(pumpid, "");
                    }
                }
            }
        }

       
    }
}