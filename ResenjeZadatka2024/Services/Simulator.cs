using ResenjeZadatka2024.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Services
{

    public class Simulator
    {
        private static IData data = null;
        public static void StartSimulation()
        {
            // Load data from csv
            data = DataCsv.Data;

            // Calculating FIRST discount
            data.CalculateCenaByMarka();

            // Calculating SECOUND discount
            data.CalculateCenaByOprema();

            // Printing Vehicles
            data.PrintVehicles();

            // Printing Customers
            data.PrintCustomers();

            Console.WriteLine("Oni koji nisu uspeli da iznajme vozilo: ");
            Console.WriteLine("======================================");

            // Rental
            data.Rental();

            // WriteInCsv - nove_rezervacije.csv
            data.WriteInCsv();

            // OverwriteBudzetInKupci
            data.OverwriteBudzetInKupci();

        }
    }
}
