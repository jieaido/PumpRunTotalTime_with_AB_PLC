using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using SharedLibrary;

namespace PumpTotalTime
{
    public  static class PHelper
    {
        public static void GetAllPumpId()
        {
            using (OleDbConnection dbc = new OleDbConnection(ConfigPump.DbConstr))
            {
                string cmdtxt = "select distinct TagName  from floatTable";
                OleDbCommand ocmDbCommand = new OleDbCommand(cmdtxt, dbc);
                dbc.Open();
                var reader = ocmDbCommand.ExecuteReader();

                while (reader.Read())
                {
                    ConfigPump.PumpList.Add(reader[0].ToString());
                }   
            }
        }

        public static TimeSpan GetPumpInfo(string pumpid)

        {
            DateTime firstdt=new DateTime();
            bool firstCalc = true;
            TimeSpan ts=new TimeSpan();

            string cmdtxt = "select DateAndTime,Val from FloatTable where TagName=?";
            var reader= SqlHelper.ExecuteReader(CommandType.Text, cmdtxt, new OleDbParameter("p1", pumpid));
            while (reader.Read())
            {
                if (reader["Val"].ToString()=="1")
                {
                    if (firstCalc)
                    {
                        firstdt = DateTime.Parse(reader["DateAndTime"].ToString());
                        firstCalc = false;
                    }
                    else
                    {
                        ts += DateTime.Parse(reader["DateAndTime"].ToString()) - firstdt;
                        firstdt = DateTime.Parse(reader["DateAndTime"].ToString());
                    }
                    
                }
                else
                {
                    firstCalc = true;
                }
            }
            reader.Close();
           
            return ts;
        }

    }
}
