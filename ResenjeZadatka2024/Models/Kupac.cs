using ResenjeZadatka2024.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Models
{
    public class Kupac
    {
        public int Id { get; set; }
        public string Ime{ get; set; }
        public string Prezime { get; set; }
        public double Budzet { get; set; }
        public Clanarina? Clanarina { get; set; }

        public int Popust = 0;

        public void CalculateDiscount()
        {
            if (Clanarina == Enums.Clanarina.VIP)
                this.Popust = 20;
            else if (Clanarina == Enums.Clanarina.Basic)
                this.Popust = 10;
        }

        public override string ToString()
        {
            return "Id: " + Id + " Ime: " + Ime + " Prezime: " + Prezime + " Budzet: " + Budzet + " Clanarina: " + Clanarina + " Moguc Popust: " + Popust;
        }

    }
}
