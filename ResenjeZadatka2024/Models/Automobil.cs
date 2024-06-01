using ResenjeZadatka2024.Enums;
using ResenjeZadatka2024.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Models
{
    public class Automobil : Vozilo
    {
        public AutoMarka Marka { get; set; }
        public string Model { get; set; }
        public double Potrosnja { get; set; }
        public double Kilometraza { get; set; }
        // tip
        public AutoKaroserija Karoserija { get; set; }

        public double Cena { get; set; }

        public Automobil(int Id, TipVozila TipVozila, AutoMarka Marka, string Model, double Potrosnja, double Kilometraza, AutoKaroserija Karoserija, double Cena) : base(Id, TipVozila)
        {
            this.Marka = Marka;
            this.Model = Model;
            this.Potrosnja = Potrosnja;
            this.Kilometraza = Kilometraza;
            this.Karoserija = Karoserija;
            this.Cena = Cena;
        }

        public override string ToString()
        {
            return "Id: " + Id + " TipVozila: " + TipVozila + " Marka: " + Marka + " Model: " + Model + 
                " Potrosnja: " + Potrosnja + " Kilometraza: " + Kilometraza + " Karoserija: " + Karoserija + 
                " Cena: " + Cena;
        }

        public override double CalculateCenaByMarka()
        {
            switch (this.Marka)
            {
                case AutoMarka.Mercedes:
                    if (this.Kilometraza < 50000)
                        this.Cena += Calculation.Percent(6, this.Cena);
                    if(this.Karoserija == AutoKaroserija.Limuzina)
                        this.Cena += Calculation.Percent(7, this.Cena);
                    else if(this.Karoserija == AutoKaroserija.Hecbek && this.Kilometraza > 100000 )
                        this.Cena -= Calculation.Percent(3, this.Cena);
                    break;

                case AutoMarka.BMW:
                    if(this.Potrosnja < 7)
                        this.Cena += Calculation.Percent(15, this.Cena);
                    else if(this.Potrosnja > 7)
                        this.Cena -= Calculation.Percent(10, this.Cena);
                    else
                        this.Cena -= Calculation.Percent(15, this.Cena);
                    break;

                case AutoMarka.Peugeot:
                    if(this.Karoserija == AutoKaroserija.Limuzina)
                        this.Cena += Calculation.Percent(15, this.Cena);
                    else if(this.Karoserija == AutoKaroserija.Karavan)
                        this.Cena += Calculation.Percent(20, this.Cena);
                    else
                        this.Cena -= Calculation.Percent(5, this.Cena);
                    break;
                
                default:
                    break;
            }
            return this.Cena;
        }
    }
}
