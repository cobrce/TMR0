using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMR0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar>10 ) e.Handled = true;
        }       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int Fosc, FoscMul, T, Tdiv;
            string text1 = "", text2 = "", text3 = "", text4 = "";

            bool AllData = ReadGUI(out Fosc, out FoscMul, out T, out Tdiv); // ReadGUI is true when no var is null, i.e we can culculate TMR0
            TimerCalculator Tcalc = new TimerCalculator(Fosc, FoscMul, T, Tdiv);
            
            if (AllData)
            {                
                int Presc;
                if (Tcalc.CalcPrescaler(out Presc))
                {
                    double TMR0 = Tcalc.CalcTimer(Presc);
                    if (TMR0 > 0)
                    {
                        text3 = Presc.ToString();
                        text4 = Math.Round(TMR0).ToString();
                    }
                    
                }
            }

            if (Fosc !=0 && FoscMul !=0)
            {
                double max = Tcalc.maxTimeForPresc(256);
                double min = Tcalc.minTimeForPresc(1);

                text1 = (max > 0 && min > 0) ? string.Format(CultureInfo.InvariantCulture, "Min : {0:#.#####E0} s", min) : "-";
                text2 = (max > 0 && min > 0) ? string.Format("Max : {0:#.#####E0} s", max) : "-"; 
            }
            label5.Text = text1;
            label6.Text = text2;
            textBox3.Text = text3;
            textBox4.Text = text4;
        }

        private bool ReadGUI(out int Fosc, out int FoscMul, out int T, out int Tdiv)
        {
            bool retval = true;
            try
            {
                if (textBox1.Text !="")              
                Fosc = int.Parse(textBox1.Text);
                else
                {
                    Fosc = 0;
                    retval = false;
                }
                FoscMul = 1;
                switch (comboBox1.Text.ToLower())
                {
                    case "mhz":
                        FoscMul = 1000000;
                        break;
                    case "khz":
                        FoscMul = 1000;
                        break;
                }
                if (textBox2.Text != "")
                    T = int.Parse(textBox2.Text);
                else
                {
                    T = 0;
                    retval = false;
                }
                Tdiv = 1;
                switch (comboBox2.Text.ToLower())
                {
                    case "ms":
                        Tdiv = 1000;
                        break;
                    case "us":
                        Tdiv = 1000000;
                        break;
                }
                return retval;
            }
            catch
            {
                Fosc = FoscMul = T = Tdiv = 0;
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Fosc,FoscMul, any;
            ReadGUI(out Fosc, out FoscMul, out any, out any);
            if (Fosc!=0 && FoscMul != 0)
            {
                Form2 frm2 = new Form2(Fosc, FoscMul);
                frm2.ShowDialog(this);
            }
        }
    }
}
