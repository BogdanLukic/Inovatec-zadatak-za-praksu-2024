using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Enums
{
    public enum AutoMarka
    {
        Mercedes,
        BMW,
        Peugeot,
        ERROR
    }

    public class AutoMarkaMapper
    {
        public static AutoMarka MapStringToAutoMarka(string AutoMarka)
        {
            switch (AutoMarka)
            {
                case "Mercedes":
                    return Enums.AutoMarka.Mercedes;
                case "BMW":
                    return Enums.AutoMarka.BMW;
                case "Peugeot":
                    return Enums.AutoMarka.Peugeot;
                default:
                    return Enums.AutoMarka.ERROR;
            }
        }
    }

}
