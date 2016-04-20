using System;
using System.Linq;
using System.Windows.Forms;
using PumpTotalTime.Properties;


namespace PumpTotalTime
{
    public partial class PumpContorl : UserControl
    {
        public PumpContorl()
        {
            InitializeComponent();
        }

        public PumpContorl(Pump pump)
        {
            InitializeComponent();
            Pump = pump;
            var s = Pump.PumpTimes.LastOrDefault();
            LastStartTime.Text = s?.StartTime.ToString() ?? "未找到启动时间";
            PumpNameLabel.Text = Pump.PumpName;
            TotalRunTime.Text = Pump.TotalRunTime.Days + Resources.PumpContorl_PumpContorl_天 + Pump.TotalRunTime.Hours +
                                Resources.PumpContorl_PumpContorl_小时 + Pump.TotalRunTime.Minutes +
                                Resources.PumpContorl_PumpContorl_分钟;
        }

        public Pump Pump { get; set; }


        private void button2_Click(object sender, EventArgs e)
        {
            var f1 = new InfoFrm {PumpTimes = Pump.PumpTimes};
            f1.ShowDialog();
        }

        private void PumpContorl_Load(object sender, EventArgs e)
        {

        }

        private void JiaYouBtn_Click(object sender, EventArgs e)
        {
             MessageBox.Show("ss");
            string cmdtxt = "insert into floattable values";
        }
    }
}