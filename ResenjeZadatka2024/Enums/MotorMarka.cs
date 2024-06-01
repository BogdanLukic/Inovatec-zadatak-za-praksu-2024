using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Enums
{
    public enum MotorMarka
    {
        Yamaha,
        Harley,
        ERROR
    }

    public class MotorMarkaMapper
    {
        public static MotorMarka MapStringToMotorMarka(string MotorMarka)
        {
            switch (MotorMarka)
            {
                case "Yamaha":
                    return Enums.MotorMarka.Yamaha;
                case "Harley":
                    return Enums.MotorMarka.Harley;
                default:
                    return Enums.MotorMarka.ERROR;
            }
        }
    }

}
