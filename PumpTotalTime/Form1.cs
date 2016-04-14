using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using SharedLibrary;

namespace PumpTotalTime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“dbPumpDataSet.FloatTable”中。您可以根据需要移动或删除它。
            this.floatTableTableAdapter.Fill(this.dbPumpDataSet.FloatTable);

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
               

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                var s= openFileDialog1.FileName;
                StringBuilder sb=new StringBuilder("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
                sb.Append(s);
                //MessageBox.Show(sb.ToString());
                DbPathLabel.Text = s;
                ConfigPump.DbConstr = sb.ToString();
;                
               // MessageBox.Show(ConfigPump.PumpList.ToString());

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

          PHelper.GetAllPumpId();
            TimeSpan s= PHelper.GetPumpInfo(ConfigPump.PumpList.First());
            MessageBox.Show(s.ToString());
        }
    }
}
