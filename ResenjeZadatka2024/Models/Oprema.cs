using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Models
{
    public class Oprema
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Cena { get; set; }
        public int PovecavaCenu { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + " Naziv: " + Naziv + " Cena: " + Cena + " PovecavaCenu: " + PovecavaCenu;
        }

    }
}
