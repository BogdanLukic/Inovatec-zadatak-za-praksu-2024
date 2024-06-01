using ResenjeZadatka2024.Enums;
using ResenjeZadatka2024.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Models
{
    public class Motor : Vozilo
    {
        public MotorMarka Marka { get; set; }
        public string Model { get; set; }
        public double Potrosnja { get; set; }
        public double Kubikaza { get; set; }
        public double Snaga { get; set; }
        public MotorKaroserija Karoserija { get; set; }

        public double Cena { get; set; }

        public Motor(int Id, TipVozila TipVozila, MotorMarka Marka, string Model, double Potrosnja, double Kubikaza, double Snaga, MotorKaroserija Karoserija, double Cena) : base(Id, TipVozila)
        {
            this.Marka = Marka;
            this.Model = Model;
            this.Potrosnja = Potrosnja;
            this.Kubikaza = Kubikaza;
            this.Snaga = Snaga;
            this.Karoserija = Karoserija;
            this.Cena = Cena;
        }

        public override string ToString()
        {
            return "Id: " + Id + " TipVozila: " + TipVozila + " Marka: " + Marka + " Model: " + Model + 
                " Potrosnja: " + Potrosnja + " Kubikaza: " + Kubikaza + " Snaga: " + Snaga + 
                " Karoserija: " + Karoserija + " Cena: " + Cena;
        }

        public override double CalculateCenaByMarka()
        {
            switch(this.Marka)
            {
                case MotorMarka.Harley:
                    this.Cena += Calculation.Percent(15, this.Cena);
                    if (this.Kubikaza > 1200)
                        this.Cena += Calculation.Percent(10, this.Cena);
                    else
                        this.Cena -= Calculation.Percent(5, this.Cena);
                    break;
                case MotorMarka.Yamaha:
                    this.Cena += Calculation.Percent(10, this.Cena);
                    if (this.Snaga > 180)
                        this.Cena += Calculation.Percent(5, this.Cena);
                    else
                        this.Cena -= Calculation.Percent(10, this.Cena);
                    if (this.Karoserija == MotorKaroserija.Heritage)
                        this.Cena += 50;
                    else if (this.Karoserija == MotorKaroserija.Sport)
                        this.Cena += 100;
                    else
                        this.Cena -= 10;
                    break;
                default:
                    break;
            }
            return this.Cena;
        }
    }
}
