using System;
using System.Collections.Generic;
using System.IO;

namespace MagazinOnline
{
    public static class SalvareIncarcareService
    {
        private static readonly string fisierProduse = "produse.txt";
        private static readonly string fisierComenzi = "comenzi.txt";

        public static void SalveazaDate(Magazin magazin)
        {
            try
            {
                using (var sw = new StreamWriter(fisierProduse))
                {
                    foreach (var produs in magazin.VizualizeazaProduse())
                    {
                        sw.WriteLine(SerializareProdus(produs));
                    }
                }

                using (var sw = new StreamWriter(fisierComenzi))
                {
                    foreach (var comanda in magazin.VizualizeazaComenzi())
                    {
                        sw.WriteLine(SerializareComanda(comanda));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la salvarea datelor: {ex.Message}");
            }
        }

        public static void IncarcaDate(Magazin magazin)
        {
            try
            {
                if (File.Exists(fisierProduse))
                {
                    foreach (var linie in File.ReadAllLines(fisierProduse))
                    {
                        var produs = DeserializareProdus(linie);
                        if (produs != null)
                        {
                            magazin.AdaugaProdus(produs);
                        }
                    }
                }

                if (File.Exists(fisierComenzi))
                {
                    foreach (var linie in File.ReadAllLines(fisierComenzi))
                    {
                        var comanda = DeserializareComanda(linie);
                        if (comanda != null)
                        {
                            magazin.AdaugaComanda(comanda);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la incarcarea datelor: {ex.Message}");
            }
        }

        private static string SerializareProdus(Produs produs)
        {
            if (produs is ProdusPerisabil pp)
            {
                return $"P,{pp.Denumire},{pp.Pret},{pp.Stoc},{pp.DataExpirarii},{pp.ConditiiPastrare}";
            }
            else if (produs is ProdusElectrocasnic pe)
            {
                return $"E,{pe.Denumire},{pe.Pret},{pe.Stoc},{pe.ClasaEnergetica},{pe.PutereMaxima}";
            }
            return $"G,{produs.Denumire},{produs.Pret},{produs.Stoc}";
        }

        private static Produs DeserializareProdus(string linie)
        {
            var parts = linie.Split(',');
            if (parts.Length < 4) return null;
            switch (parts[0])
            {
                case "P":
                    return new ProdusPerisabil
                    {
                        Denumire = parts[1],
                        Pret = decimal.Parse(parts[2]),
                        Stoc = int.Parse(parts[3]),
                        DataExpirarii = DateTime.Parse(parts[4]),
                        ConditiiPastrare = parts[5]
                    };
                case "E":
                    return new ProdusElectrocasnic
                    {
                        Denumire = parts[1],
                        Pret = decimal.Parse(parts[2]),
                        Stoc = int.Parse(parts[3]),
                        ClasaEnergetica = parts[4],
                        PutereMaxima = int.Parse(parts[5])
                    };
                case "G":
                    return new Produs
                    {
                        Denumire = parts[1],
                        Pret = decimal.Parse(parts[2]),
                        Stoc = int.Parse(parts[3])
                    };
                default:
                    return null;
            }
        }

        private static string SerializareComanda(Comanda comanda)
        {
            return $"{comanda.NumeClient},{comanda.Telefon},{comanda.Email},{comanda.AdresaLivrare},{comanda.DataComenzii},{comanda.Status}";
        }

        private static Comanda DeserializareComanda(string linie)
        {
            var parts = linie.Split(',');
            if (parts.Length < 6) return null;
            return new Comanda
            {
                NumeClient = parts[0],
                Telefon = parts[1],
                Email = parts[2],
                AdresaLivrare = parts[3],
                DataComenzii = DateTime.Parse(parts[4]),
                Status = parts[5]
            };
        }
    }
}
