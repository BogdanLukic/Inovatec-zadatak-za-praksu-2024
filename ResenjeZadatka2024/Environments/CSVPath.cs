using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResenjeZadatka2024.Environments
{
    public class CSVPath
    {
        private static string default_path = "../../../../Csv-s/";  // + fajl.csv
        public static string Default_Path { get { return default_path; } }
        public static string GetPath(string file) {
            return Default_Path + file + ".csv";
        }
    }
}
