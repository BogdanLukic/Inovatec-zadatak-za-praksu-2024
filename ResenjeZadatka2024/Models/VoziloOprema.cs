using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Models
{
    public class VoziloOprema
    {
        public int VoziloId { get; set; }
        public int OpremaId { get; set; }

        public override string ToString()
        {
            return "VoziloId: " + VoziloId + " OpremaId: " + OpremaId;
        }

    }
}
