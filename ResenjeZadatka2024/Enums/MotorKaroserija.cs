using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Enums
{
    public enum MotorKaroserija
    {
        Adventure, 
        Heritage, 
        Tour, 
        Roadster, 
        UrbanMobility, Urban_Mobility, Urbanmobility, 
        Sport,
        ERROR
    }

    public class MotorKaroserijaMapper
    {
        public static MotorKaroserija MapStringToMotorKaroserija(string MotorKaroserija)
        {
            switch(MotorKaroserija)
            {
                case "Adventure":
                    return Enums.MotorKaroserija.Adventure;
                case "Heritage":
                    return Enums.MotorKaroserija.Heritage;
                case "Tour":
                    return Enums.MotorKaroserija.Tour;
                case "Roadster":
                    return Enums.MotorKaroserija.Roadster;
                case "UrbanMobility":
                    return Enums.MotorKaroserija.UrbanMobility;
                case "Urban_Mobility":
                    return Enums.MotorKaroserija.UrbanMobility;
                case "Urbanmobility":
                    return Enums.MotorKaroserija.UrbanMobility;
                case "Sport":
                    return Enums.MotorKaroserija.Sport;
                default:
                    return Enums.MotorKaroserija.ERROR;
            }
        }
    }

}
