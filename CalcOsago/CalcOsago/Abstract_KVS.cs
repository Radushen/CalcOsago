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
    public abstract class Abstract_KVS
    {
        public abstract double CheckCondition(int driver_age, int driver_stage);
    }

    public class KVS : Abstract_KVS
    {
        private int driver_age;
        private int driver_stage;

        public KVS(int driver_age, int driver_stage)
        {
            this.driver_age = driver_age;
            this.driver_stage = driver_stage;
        }

        public override double CheckCondition(int driver_age, int driver_stage)
        {
            double result = 0;

            if ((16 <= driver_age & driver_age <= 21) & (driver_stage == 0))
            {
                result = 2.27;
            }

            else if ((16 <= driver_age & driver_age <= 21) & (driver_stage == 1))
            {
                result = 1.92;
            }
            else if ((16 <= driver_age & driver_age <= 21) & (driver_stage == 2))
            {
                result = 1.84;
            }
            else if ((16 <= driver_age & driver_age <= 21) & (3 <= driver_stage & driver_stage <= 4))
            {
                result = 1.65;
            }
            else if ((16 <= driver_age & driver_age <= 21) & (5 <= driver_stage & driver_stage <= 6))
            {
                result = 1.62;
            }
            else if ((22 <= driver_age & driver_age <= 24) & (driver_stage == 0))
            {
                result = 1.88;
            }
            else if ((22 <= driver_age & driver_age <= 24) & (driver_stage == 1))
            {
                result = 1.72;
            }
            else if ((22 <= driver_age & driver_age <= 24) & (driver_stage == 2))
            {
                result = 1.71;
            }
            else if ((22 <= driver_age & driver_age <= 24) & (3 <= driver_stage & driver_stage <= 4))
            {
                result = 1.13;
            }
            else if ((22 <= driver_age & driver_age <= 24) & (5 <= driver_stage & driver_stage <= 6))
            {
                result = 1.10;
            }
            else if ((22 <= driver_age & driver_age <= 24) & (7 <= driver_stage & driver_stage <= 9))
            {
                result = 1.09;
            }
            else if ((25 <= driver_age & driver_age <= 29) & (driver_stage == 0))
            {
                result = 1.72;
            }
            else if ((25 <= driver_age & driver_age <= 29) & (driver_stage == 1))
            {
                result = 1.60;
            }
            else if ((25 <= driver_age & driver_age <= 29) & (driver_stage == 2))
            {
                result = 1.54;
            }
            else if ((25 <= driver_age & driver_age <= 29) & (3 <= driver_stage & driver_stage <= 4))
            {
                result = 1.09;
            }
            else if ((25 <= driver_age & driver_age <= 29) & (5 <= driver_stage & driver_stage <= 6))
            {
                result = 1.08;
            }
            else if ((25 <= driver_age & driver_age <= 29) & (7 <= driver_stage & driver_stage <= 9))
            {
                result = 1.07;
            }
            else if ((25 <= driver_age & driver_age <= 29) & (10 <= driver_stage & driver_stage <= 14))
            {
                result = 1.02;
            }
            else if ((30 <= driver_age & driver_age <= 34) & (driver_stage == 0))
            {
                result = 1.56;
            }
            else if ((30 <= driver_age & driver_age <= 34) & (driver_stage == 1))
            {
                result = 1.50;
            }
            else if ((30 <= driver_age & driver_age <= 34) & (driver_stage == 2))
            {
                result = 1.48;
            }
            else if ((30 <= driver_age & driver_age <= 34) & (3 <= driver_stage & driver_stage <= 4))
            {
                result = 1.04;
            }
            else if ((30 <= driver_age & driver_age <= 34) & (5 <= driver_stage & driver_stage <= 6))
            {
                result = 1.05;
            }
            else if ((30 <= driver_age & driver_age <= 34) & (7 <= driver_stage & driver_stage <= 9))
            {
                result = 1.01;
            }
            else if ((30 <= driver_age & driver_age <= 34) & (10 <= driver_stage & driver_stage <= 14))
            {
                result = 0.97;
            }
            else if ((30 <= driver_age & driver_age <= 34) & (driver_stage > 14))
            {
                result = 0.95;
            }
            else if ((35 <= driver_age & driver_age <= 39) & (driver_stage == 0))
            {
                result = 1.54;
            }
            else if ((35 <= driver_age & driver_age <= 39) & (driver_stage == 1))
            {
                result = 1.47;
            }
            else if ((35 <= driver_age & driver_age <= 39) & (driver_stage == 2))
            {
                result = 1.46;
            }
            else if ((35 <= driver_age & driver_age <= 39) & (3 <= driver_stage & driver_stage <= 4))
            {
                result = 1.00;
            }
            else if ((35 <= driver_age & driver_age <= 39) & (5 <= driver_stage & driver_stage <= 6))
            {
                result = 0.97;
            }
            else if ((35 <= driver_age & driver_age <= 39) & (7 <= driver_stage & driver_stage <= 9))
            {
                result = 0.95;
            }
            else if ((35 <= driver_age & driver_age <= 39) & (10 <= driver_stage & driver_stage <= 14))
            {
                result = 0.94;
            }
            else if ((35 <= driver_age & driver_age <= 39) & (driver_stage > 14))
            {
                result = 0.93;
            }
            else if ((40 <= driver_age & driver_age <= 49) & (driver_stage == 0))
            {
                result = 1.59;
            }
            else if ((40 <= driver_age & driver_age <= 49) & (driver_stage == 1))
            {
                result = 1.44;
            }
            else if ((40 <= driver_age & driver_age <= 49) & (driver_stage == 2))
            {
                result = 1.43;
            }
            else if ((40 <= driver_age & driver_age <= 49) & (3 <= driver_stage & driver_stage <= 4))
            {
                result = 0.96;
            }
            else if ((40 <= driver_age & driver_age <= 49) & (5 <= driver_stage & driver_stage <= 6))
            {
                result = 0.95;
            }
            else if ((40 <= driver_age & driver_age <= 49) & (7 <= driver_stage & driver_stage <= 9))
            {
                result = 0.94;
            }
            else if ((40 <= driver_age & driver_age <= 49) & (10 <= driver_stage & driver_stage <= 14))
            {
                result = 0.93;
            }
            else if ((40 <= driver_age & driver_age <= 49) & (driver_stage > 14))
            {
                result = 0.91;
            }
            else if ((50 <= driver_age & driver_age <= 59) & (driver_stage == 0))
            {
                result = 1.46;
            }
            else if ((50 <= driver_age & driver_age <= 59) & (driver_stage == 1))
            {
                result = 1.40;
            }
            else if ((50 <= driver_age & driver_age <= 59) & (driver_stage == 2))
            {
                result = 1.39;
            }
            else if ((50 <= driver_age & driver_age <= 59) & (3 <= driver_stage & driver_stage <= 4))
            {
                result = 0.93;
            }
            else if ((50 <= driver_age & driver_age <= 59) & (5 <= driver_stage & driver_stage <= 6))
            {
                result = 0.92;
            }
            else if ((50 <= driver_age & driver_age <= 59) & (7 <= driver_stage & driver_stage <= 9))
            {
                result = 0.91;
            }
            else if ((50 <= driver_age & driver_age <= 59) & (10 <= driver_stage & driver_stage <= 14))
            {
                result = 0.90;
            }
            else if ((50 <= driver_age & driver_age <= 59) & (driver_stage > 14))
            {
                result = 0.86;
            }
            else if (driver_age > 59 & driver_stage == 0)
            {
                result = 1.43;
            }
            else if (driver_age > 59 & driver_stage == 1)
            {
                result = 1.36;
            }
            else if (driver_age > 59 & driver_stage == 2)
            {
                result = 1.35;
            }
            else if (driver_age > 59 & (3 <= driver_stage & driver_stage <= 4))
            {
                result = 0.91;
            }
            else if (driver_age > 59 & (5 <= driver_stage & driver_stage <= 6))
            {
                result = 0.90;
            }
            else if (driver_age > 59 & (7 <= driver_stage & driver_stage <= 9))
            {
                result = 0.89;
            }
            else if (driver_age > 59 & (10 <= driver_stage & driver_stage <= 14))
            {
                result = 0.88;
            }
            else if (driver_age > 59 & driver_stage > 14)
            {
                result = 0.83;
            }
            return result;
        }
    }
}
