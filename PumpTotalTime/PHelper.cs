using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SharedLibrary;
using Newtonsoft.Json;

namespace PumpTotalTime
{
    public  static class PHelper
    {
        public static void GetAllPumpId()
        {
            using (OleDbConnection dbc = new OleDbConnection(ConfigPump.DbConstr))
            {
                ConfigPump.PumpList.Clear();
                string cmdtxt = "select distinct TagName  from floatTable";
                //OleDbCommand ocmDbCommand = new OleDbCommand(cmdtxt, dbc);
               // dbc.Open();
               
                var reader = SqlHelper.ExecuteReader(CommandType.Text, cmdtxt);

                while (reader.Read())
                {
                    ConfigPump.PumpList.Add(reader[0].ToString());
                }   
            }

            ConfigPump.PumpCfgs = GetAlreadyConfig();
        }

        public static Pump GetPumpInfo(string pumpid)

        {
            List<PumpTime> pumpTimes=new List<PumpTime>();
            DateTime firstdt=new DateTime();
            DateTime lastRunDateTime=new    DateTime();
            bool firstStart = true;
            bool firststop   = true;
            TimeSpan ts=new TimeSpan();

            string cmdtxt = "select DateAndTime,Val from FloatTable where TagName=?";
            var reader= SqlHelper.ExecuteReader(CommandType.Text, cmdtxt, new OleDbParameter("p1", pumpid));
            var pt = new PumpTime();
            while (reader.Read())
            {
              
                if (reader["Val"].ToString()=="1")
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
                        if (pt.StartTime!=DateTime.MinValue)
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
            if (pt.StopTime==DateTime.MinValue&&pt.StartTime!=DateTime.MinValue)
            {

                pt.StopTime = lastRunDateTime;
                pumpTimes.Add(pt);
            }


            Pump pump = new Pump();

            pump.PumpId = pumpid;
            if (ConfigPump.PumpCfgs!=null && ConfigPump.PumpCfgs.Find(p => p.pumpid == pumpid)!=null)
            {
                pump.PumpName = ConfigPump.PumpCfgs.Find(p => p.pumpid == pumpid).pumpname;
            }
            else
            {
                pump.PumpName = pumpid;
            }
             
            pump.PumpTimes = pumpTimes;
            pump.TotalRunTime = ts;
            

          
            return pump;
        }

        public static List<PumpCfg> GetAlreadyConfig()
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
    }
}
public class PumpCfg
{
    public string pumpid;
    public string pumpname;
}
