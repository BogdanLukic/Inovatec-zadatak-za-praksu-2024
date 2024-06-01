using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Models
{
    public class Rezervacije
    {
        public int VoziloId { get; set; }
        public int KupacId { get; set; }
        public DateTime PocetakRezervacije { get; set; }
        public DateTime KrajRezervacije { get; set; }

        public override string ToString()
        {
            return "VoziloId: " + VoziloId + " KupacId: " + KupacId + " PocetakRezervacije: " + PocetakRezervacije + " KrajRezervacije: " + KrajRezervacije;
        }

    }
}
