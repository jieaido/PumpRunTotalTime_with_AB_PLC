using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using PumpTotalTime.Properties;

namespace PumpTotalTime
{
    public partial class Main : Form
    {
        public List<PumpCfg> pumpDic = new List<PumpCfg>();
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
                PHelper.GetAllPumpId();
                tabControl1.Enabled = true;
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
            flowLayoutPanel1.Controls.Clear();
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
            MessageBox.Show("崔健制作");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pumpname = "";
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("id", typeof(string));
            DataColumn dc2 = new DataColumn("name", typeof(string));
            dt.Columns.AddRange(new DataColumn[] { dc1, dc2 });
            foreach (var pl in ConfigPump.PumpList)
            {
                
                    if (ConfigPump.PumpCfgs!=null&&ConfigPump.PumpCfgs.Find(p => p.pumpid == pl) != null)
                    {
                        pumpname = ConfigPump.PumpCfgs.Find(p => p.pumpid == pl).pumpname;
                    }
                    var dr = dt.NewRow();
                    dr["id"] = pl;
                    dr["name"] = pumpname;
                    dt.Rows.Add(dr);
                

            }
            
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           ConfigPump.PumpCfgs=new List<PumpCfg>();
         
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ConfigPump.PumpCfgs.Add(new PumpCfg()
                {
                    pumpid = dataGridView1[0, i].Value.ToString(),
                    pumpname = dataGridView1[1, i].Value.ToString()
                });
            }
           var writeStream=  JsonConvert.SerializeObject(ConfigPump.PumpCfgs);
           File.WriteAllText("app.cfg",writeStream);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
