using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcOsagoSkeleton
{
    public abstract class InsuranceCalculator
    {
        public abstract double CalculatePremium(int driversCount, int experience, int age, int horsepower);
    }
}
