using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Services
{
    public class Calculation
    {
        public static double Percent(int percent, double percent_of)
        {
            return percent_of * percent / 100;
        }
    }
}
