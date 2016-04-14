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
        public Pump Pump { get; set; }  

        public PumpContorl()
        {
            InitializeComponent();
        }

        public PumpContorl(Pump pump)
        {
            InitializeComponent();
            Pump = pump;
            LastStartTime.Text = Pump.PumpTimes.LastOrDefault().StartTime.ToString();
            TotalRunTime.Text = Pump.TotalRunTime.ToString();

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1=new    Form1();
            f1.PumpTimes = Pump.PumpTimes;
            f1.ShowDialog();

        }
    }
}
