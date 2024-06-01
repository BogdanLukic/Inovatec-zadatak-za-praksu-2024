using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResenjeZadatka2024.Models;
using ResenjeZadatka2024.Environments;
using ResenjeZadatka2024.Enums;
using System.Runtime.InteropServices;
using ResenjeZadatka2024.Services;

namespace ResenjeZadatka2024.Data
{
    internal class DataCsv : IData
    {
        private static IData data;
        private readonly static Object key = new Object();

        public static IData Data
        {
            get
            {
                if (data == null)
                {
                    lock (key)
                    {
                        if (data == null) data = new DataCsv();
                    }
                }
                return data;
            }
        }

        private List<Kupac> Kupci;
        private List<Oprema> Oprema;
        private List<Rezervacije> Rezervacije;
        private List<Vozilo> Vozila;
        private List<VoziloOprema> VoziloOprema;
        private List<ZahteviZaRezervaciju> ZahteviZaRezervaciju;

        private List<Rezervacije> NoviZahteviZaRezervaciju = new List<Rezervacije>();

        private DataCsv()
        {
            //load kupci
            this.Kupci = ReadCsv<Kupac>("kupci");
            //load oprema
            this.Oprema = ReadCsv<Oprema>("oprema");
            //load rezervacije
            this.Rezervacije = ReadCsv<Rezervacije>("rezervacije");
            // load cars
            this.Vozila = ReadVehicleCsv("vozila");
            //load VoziloOprema
            this.VoziloOprema = ReadCsv<VoziloOprema>("vozilo_oprema");
            //load ZahteviZaRezervaciju
            this.ZahteviZaRezervaciju = ReadCsv<ZahteviZaRezervaciju>("zahtevi_za_rezervacije");
        }

        public List<T> ReadCsv<T>(string file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var reader = new StreamReader(CSVPath.GetPath(file)))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<T>();
                var list = new List<T>();
                foreach (var record in records)
                {   
                    list.Add(record);
                }
                return list;
            }
        }

        public List<Vozilo> ReadVehicleCsv(string file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var reader = new StreamReader(CSVPath.GetPath(file)))
            using (var csv = new CsvReader(reader, config))
            {
                Vozilo vozilo = null;
                List<Vozilo> list = new List<Vozilo>();
                while (csv.Read())
                {
                    var id = -1;
                    try
                    {
                        id = int.Parse(csv[0]);
                    }
                    catch (Exception e)
                    {
                        csv.Read();
                        id = int.Parse(csv[0]);
                    }

                    var tip = csv.GetField(1);

                    var marka = csv[2];
                    var model = csv[3];
                    var potrosnja = double.Parse(csv[4]);
                    var karoserija = csv[8];
                    switch (tip)
                    {
                        case "Automobil":
                            var kilometraza = double.Parse(csv[6]);

                            vozilo = new Automobil(
                                id,
                                Enums.TipVozila.Automoil,
                                Enums.AutoMarkaMapper.MapStringToAutoMarka(marka),
                                model,
                                potrosnja,
                                kilometraza,
                                Enums.AutoKaroserijaMapper.MapStringToAutoKaroserija(karoserija),
                                200);
                            break;
                        case "Motor":
                            var kubikaza = int.Parse(csv[5]);
                            var snaga = double.Parse(csv[7]);

                            vozilo = new Motor(
                                id,
                                Enums.TipVozila.Motor,
                                Enums.MotorMarkaMapper.MapStringToMotorMarka(marka),
                                model,
                                potrosnja,
                                kubikaza,
                                snaga,
                                Enums.MotorKaroserijaMapper.MapStringToMotorKaroserija(karoserija),
                                100
                                );
                            break;
                    }
                    if (vozilo != null)
                        list.Add(vozilo);
                }
                return list;
            }
        }

        public void CalculateCenaByMarka()
        {
            foreach (Vozilo v in Vozila)
            {
                v.CalculateCenaByMarka();
            }
        }

        public void CalculateCenaByOprema()
        {
            var info = from vozilo_oprema in VoziloOprema
                       join oprema in Oprema on vozilo_oprema.OpremaId equals oprema.Id
                       select new { vozilo_oprema.VoziloId, vozilo_oprema.OpremaId, oprema.Cena, oprema.PovecavaCenu };

            foreach (var i in info)
            {
                var vozilo = Vozila.Where(x => x.Id == i.VoziloId).FirstOrDefault();
                if (vozilo != null)
                {
                    if (i.PovecavaCenu == 1)
                    {
                        if (vozilo is Automobil)
                        {
                            Automobil voziloA = (Automobil)vozilo;
                            voziloA.Cena += i.Cena;
                        }

                        else if (vozilo is Motor)
                        {
                            Motor voziloM = (Motor)vozilo;
                            voziloM.Cena += i.Cena;
                        }
                    }
                    else if (i.PovecavaCenu == 0)
                    {
                        if (vozilo is Automobil)
                        {
                            Automobil voziloA = (Automobil)vozilo;
                            voziloA.Cena -= i.Cena;
                        }

                        else if (vozilo is Motor)
                        {
                            Motor voziloM = (Motor)vozilo;
                            voziloM.Cena -= i.Cena;
                        }
                    }
                }

            }

        }

        public void PrintVehicles()
        {
            foreach (Vozilo v in Vozila)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine("=======================================================================");
        }

        public void PrintCustomers()
        {
            foreach (Kupac k in Kupci)
            {
                k.CalculateDiscount();
                Console.WriteLine(k);
            }
            Console.WriteLine("=======================================================================");
        }

        public void Rental()
        {
            var zahteviIKupci = from zahtevi_za_rezetvaciju in ZahteviZaRezervaciju
                                join kupci in Kupci on zahtevi_za_rezetvaciju.KupacId equals kupci.Id
                                select new
                                {
                                    zahtevi_za_rezetvaciju.VoziloId,
                                    zahtevi_za_rezetvaciju.KupacId,
                                    zahtevi_za_rezetvaciju.DatumDolaska,
                                    zahtevi_za_rezetvaciju.PocetakRezervacije,
                                    zahtevi_za_rezetvaciju.BrojDana,
                                    kupci.Ime,
                                    kupci.Prezime,
                                    kupci.Budzet,
                                    kupci.Clanarina,
                                    kupci.Popust
                                };

            zahteviIKupci = zahteviIKupci.OrderBy(x => x.DatumDolaska).ThenByDescending(x => x.Clanarina).ToList();

            foreach(var i in zahteviIKupci)
            {

                if(!CarIsRented(i.VoziloId, i.PocetakRezervacije, i.PocetakRezervacije.AddDays(i.BrojDana)))
                {
                    
                    var vozilo = Vozila.Where(x=>x.Id == i.VoziloId).FirstOrDefault();
                    var pricePerDay= -1.0;
                    if(vozilo != null)
                    {
                        if (vozilo.TipVozila == TipVozila.Automoil)
                        {
                            Automobil voziloA = (Automobil)vozilo;
                            pricePerDay = voziloA.Cena;
                        }
                        else
                        {
                            Motor voziloM = (Motor)vozilo;
                            pricePerDay = voziloM.Cena;
                        }
                    }
                    double totalPrice = pricePerDay * i.BrojDana;

                    if (i.Popust > 0)
                        totalPrice -= Calculation.Percent(i.Popust, totalPrice);

                    if(i.Budzet >= totalPrice)
                    {
                        Kupci.Where(x=>x.Id == i.KupacId).FirstOrDefault().Budzet -= totalPrice;
                        Rezervacije r = new Rezervacije { 
                                                            VoziloId =  i.VoziloId, 
                                                            KupacId = i.KupacId, 
                                                            PocetakRezervacije = i.PocetakRezervacije, 
                                                            KrajRezervacije = i.PocetakRezervacije.AddDays(i.BrojDana) 
                                                        };
                        NoviZahteviZaRezervaciju.Add(r);
                    }
                    else
                    {
                        Console.WriteLine(i.Ime + i.Prezime + " nije iznajmio vozilo: " + i.VoziloId + " jer nema budzet");
                    }

                }
                else
                {
                    Console.WriteLine(i.Ime + i.Prezime + " nije iznajmio vozilo: " + i.VoziloId + " jer vozilo nije slobodno.");
                }
            }

        }

        private bool CarIsRented(int CarId, DateTime from, DateTime to)
        {
            var r = Rezervacije.Any(x => x.VoziloId == CarId &&
                                x.PocetakRezervacije < to && x.KrajRezervacije > from );
            var nr = false;
            nr = NoviZahteviZaRezervaciju.Any(x => x.VoziloId == CarId &&
                                              x.PocetakRezervacije < to && x.KrajRezervacije > from);

            if (r == false && nr == false)
                return false;
            return true;
        }

        public void WriteInCsv()
        {
            using (var writer = new StreamWriter(CSVPath.Default_Path + "nove_rezervacije.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField("VoziloId");
                csv.WriteField("KupacId");
                csv.WriteField("PocetakRezervacije");
                csv.WriteField("KrajRezervacije");
                csv.NextRecord();
                foreach (var nz in NoviZahteviZaRezervaciju)
                {
                    csv.WriteField(nz.VoziloId);
                    csv.WriteField(nz.KupacId);
                    csv.WriteField(nz.PocetakRezervacije.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
                    csv.WriteField(nz.KrajRezervacije.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
                    csv.NextRecord();
                }
            }
        }

        public void OverwriteBudzetInKupci()
        {
            var list = new List<Kupac>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var reader = new StreamReader(CSVPath.GetPath("kupci")))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Kupac>();
                
                foreach (Kupac record in records)
                {
                    record.Budzet = Kupci.Where(x=>x.Id == record.Id).FirstOrDefault().Budzet;
                    list.Add(record);
                }
            }

            using (var writer = new StreamWriter(CSVPath.GetPath("kupci")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list);
            }
        }
    }
}
