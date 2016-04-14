using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharedLibrary;

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
//                if (reader["Val"].ToString()=="1")
//                {
//                    if (firstCalc)
//                    {
//                        var pumptimes=new PumpTime();
//                        pumptimes.StartTime= DateTime.Parse(reader["DateAndTime"].ToString());
//                        firstdt = DateTime.Parse(reader["DateAndTime"].ToString());
//                        firstCalc = false;
//                    }
//                    else
//                    {
//                        ts += DateTime.Parse(reader["DateAndTime"].ToString()) - firstdt;
//                        firstdt = DateTime.Parse(reader["DateAndTime"].ToString());
//                    }
//                    
//                }
//                else
//                {
//                    firstCalc = true;
//                }
               
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
                        pt.StopTime = DateTime.Parse(reader["DateAndTime"].ToString());
                        pumpTimes.Add(pt);
                        pt=new PumpTime();
                        firststop = false;
                    }
                    firstStart = true;

                }
            }
            if (pt.StopTime==DateTime.MinValue)
            {
                pt.StopTime = lastRunDateTime;
            }
            pumpTimes.Add(pt);
           // reader.Close();
            Pump pump = new Pump()
            {
                PumpId = pumpid,
                PumpName = "ceshi",
                PumpTimes = pumpTimes,
                TotalRunTime = ts
            };

          
            return pump;
        }

    }
}
