using ResenjeZadatka2024.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Models
{
    public abstract class Vozilo
    {
        public int Id { get; set; }
        public TipVozila TipVozila { get; set; }

        public Vozilo(int Id, TipVozila TipVozila) {
            this.Id = Id;
            this.TipVozila = TipVozila;
        }

        public abstract double CalculateCenaByMarka();

    }
}
