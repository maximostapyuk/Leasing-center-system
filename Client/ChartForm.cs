using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ChartForm : Form
    {
        public int[] statistic;
        public ChartForm(int [] vs)
        {
            InitializeComponent();
            this.statistic = vs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Chart.Series[0].Points.Clear();
            int i = 0;
            while (i < statistic.Length)
            {
                this.Chart.Series[0].Points.AddXY(i + 1, statistic[i]);
                i++;
            }
        }

        
    }
}
