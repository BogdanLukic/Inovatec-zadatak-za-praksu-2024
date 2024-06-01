using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Enums
{
    public enum AutoKaroserija
    {
        Limuzina,
        Hecbek,
        Karavan,
        Kupe,
        Kabriolet,
        Minivan,
        SUV,
        Pickup,
        ERROR
    }

    public class AutoKaroserijaMapper
    {
        public static AutoKaroserija MapStringToAutoKaroserija(string AutoKaroserija)
        {
            switch (AutoKaroserija)
            {
                case "Limuzina":
                    return Enums.AutoKaroserija.Limuzina;
                case "Hecbek":
                    return Enums.AutoKaroserija.Hecbek;
                case "Karavan":
                    return Enums.AutoKaroserija.Karavan;
                case "Kupe":
                    return Enums.AutoKaroserija.Kupe;
                case "Kabriolet":
                    return Enums.AutoKaroserija.Kabriolet;
                case "Minivan":
                    return Enums.AutoKaroserija.Minivan;
                case "SUV":
                    return Enums.AutoKaroserija.SUV;
                case "Pickup":
                    return Enums.AutoKaroserija.Pickup;
                default:
                    return Enums.AutoKaroserija.ERROR;
            }
        }    
    }

}
