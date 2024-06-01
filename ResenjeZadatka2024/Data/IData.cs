using ResenjeZadatka2024.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Data
{
    public interface IData
    {
        public List<Vozilo> ReadVehicleCsv(string file);
        public List<T> ReadCsv<T>(string file);
        public void CalculateCenaByMarka();
        public void CalculateCenaByOprema();
        public void PrintVehicles();
        public void PrintCustomers();
        public void Rental();
        public void WriteInCsv();
        public void OverwriteBudzetInKupci();

    }
}
