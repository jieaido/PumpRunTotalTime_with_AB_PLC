using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PumpTotalTime
{
    public partial class InfoFrm : Form
    {
        public List<PumpTime> PumpTimes = new List<PumpTime>();

        public InfoFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = PumpTimes;
            dataGridView1.DataSource = bindingSource1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}