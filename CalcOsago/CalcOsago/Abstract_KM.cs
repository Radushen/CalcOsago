using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CalcOsago
{
    public abstract class Abstract_KM
    {
        public abstract double CheckCondition(int power_ts);
    }

    public class KM : Abstract_KM
    {
        private int power_ts;

        public KM(int power_ts)
        {
            this.power_ts = power_ts;
        }

        public override double CheckCondition(int power_ts)
        {
            double result = 0;

            if (power_ts <= 50)
            {
                result = 0.6;
            }
            else if (50 < power_ts & power_ts <= 70)
            {
                result = 1;
            }
            else if (70 < power_ts & power_ts <= 100)
            {
                result = 1.1;
            }
            else if (100 < power_ts & power_ts <= 120)
            {
                result = 1.2;
            }
            else if (120 < power_ts & power_ts <= 150)
            {
                result = 1.4;
            }
            else if (power_ts > 150)
            {
                result = 1.6;
            }
            return result;
        }
    }
}
