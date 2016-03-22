using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMR0
{
    internal class TimerCalculator
    {
        private double Fosc;
        private double T;
        internal TimerCalculator(int Fosc, int FoscMul, int T, int Tdiv)
        {
            this.Fosc = (double)Fosc * (double)FoscMul;
            this.T = (double)T / (double)Tdiv;
        }
        internal bool CalcPrescaler(out int Prescaler)
         {
             int[] presc = { 1, 2, 4, 8, 16, 32, 128, 256 };
             for (int i = 0; i < presc.Length; i++)
             {
                 double min = minTimeForPresc(presc[i]);
                 double max = maxTimeForPresc(presc[i]);
                 if (T <= max && T >= min)
                 {
                     Prescaler = presc[i];
                     return true;
                 }
             }
             Prescaler = 0;
             return false;
         }
        internal double minTimeForPresc(int Presc)
        {
            return TimeForPresc(Presc, 255);
        }
        internal double maxTimeForPresc(int Presc)
        {
            return TimeForPresc(Presc, 0);
        }
        internal double TimeForPresc(int Presc, int TMR0)
        {
            return 4 * (256 - TMR0) * Presc / Fosc;
        }
        internal double CalcTimer(int Prescaler)
        {
            return (256 - (T * Fosc / (4 * Prescaler)));
        }
    }
}
