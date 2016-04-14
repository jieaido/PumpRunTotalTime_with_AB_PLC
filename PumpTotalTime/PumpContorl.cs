using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PumpTotalTime
{
    public partial class PumpContorl : UserControl
    {
        public string PumpName { get; set; }
        public string PumpId { get; set; }

        public DateTime TotalRunTime { get; set; }  
        public DateTime LastStartTime { get; set; } 


        public PumpContorl()
        {
            InitializeComponent();
        }

        public PumpContorl(string pumpname, string pumpid, DateTime totalruntime, DateTime laststarttime)
        {
            PumpName = pumpname;
            PumpId = pumpid;
            TotalRunTime = totalruntime;
            LastStartTime = laststarttime;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            
            var s= LastStartTime - TotalRunTime;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void PumpContorl_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
