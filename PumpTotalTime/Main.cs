using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PumpTotalTime.Properties;

namespace PumpTotalTime
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var s = openFileDialog1.FileName;
                StringBuilder sb = new StringBuilder("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
                sb.Append(s);
                //MessageBox.Show(sb.ToString());
                DbPathLabel.Text = s;
                ConfigPump.DbConstr = sb.ToString();
                ;
                // MessageBox.Show(ConfigPump.PumpList.ToString());

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ConfigPump.DbConstr==null)
            {
                MessageBox.Show(Resources.Main_button1_Click_请先选择数据库文件_);  
                return;
            }
            PHelper.GetAllPumpId();
            foreach (var pumpid in ConfigPump.PumpList)
            {
                var ss = PHelper.GetPumpInfo(pumpid);
                PumpContorl pc = new PumpContorl(ss);
                pc.AutoSize = true;
                flowLayoutPanel1.Controls.Add(pc);
            }
           
            //MessageBox.Show(ss.ToString());
        }

        private void 第二窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1=new Form1();
            f1.Show();
        }
    }
}
