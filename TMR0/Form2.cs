using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMR0
{
    public partial class Form2 : Form
    {
        public Form2(int Fosc,int FoscMul)
        {
            InitializeComponent();
            TimerCalculator calc = new TimerCalculator(Fosc, FoscMul, 0, 0);
            for (int i = 1; i <= 256; i *= 2)
            {
                double min = calc.minTimeForPresc(i);
                double max = calc.maxTimeForPresc(i);
                listView1.Items.Add(new ListViewItem(new string[] { i.ToString(), min.ToString("#.#####E0"), max.ToString("#.#####E0") }));
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
