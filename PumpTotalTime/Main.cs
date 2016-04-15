using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
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
                var sb = new StringBuilder("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
                sb.Append(s);
                //MessageBox.Show(sb.ToString());
                DbPathLabel.Text = s;
                ConfigPump.DbConstr = sb.ToString();

                PHelper.GetAllPumpId();
                tabControl1.Enabled = true;

                // MessageBox.Show(ConfigPump.ReadDbPumpidList.ToString());
            }
        }


        private void 第二窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("AB系列PLC的电机运行时统计软件\n" + Application.ProductVersion + "\n" +
                            "Cui Jian 保留所有权利\n ", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            var dc1 = new DataColumn("id", typeof (string));
            var dc2 = new DataColumn("name", typeof (string));
            dt.Columns.AddRange(new[] {dc1, dc2});
            //PHelper.GetAllPumpId();
            foreach (var pl in ConfigPump.MergePumpListCfg)
            {
                var dr = dt.NewRow();
                dr["id"] = pl.Key;
                dr["name"] = pl.Value;
                dt.Rows.Add(dr);
            }

            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tempWriteList = new List<PumpCfg>();

            for (var i = 0; i < dataGridView1.Rows.Count; i++)
            {
                tempWriteList.Add(new PumpCfg
                {
                    Pumpid = dataGridView1[0, i].Value.ToString(),
                    Pumpname = dataGridView1[1, i].Value.ToString()
                });
            }
            var writeStream = JsonConvert.SerializeObject(tempWriteList);
            File.WriteAllText("app.cfg", writeStream);
            MessageBox.Show(Resources.Main_button3_Click_写入成功);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CalcBtn(object sender, EventArgs e)
        {
            if (ConfigPump.DbConstr == null)
            {
                MessageBox.Show(Resources.Main_button1_Click_请先选择数据库文件_);
                return;
            }
            PHelper.GetAllPumpId();
            flowLayoutPanel1.Controls.Clear();
            foreach (var pumpid in ConfigPump.ReadDbPumpidList)
            {
                var ss = PHelper.GetPumpInfo(pumpid);
                var pc = new PumpContorl(ss);

                flowLayoutPanel1.Controls.Add(pc);
            }
        }
    }
}