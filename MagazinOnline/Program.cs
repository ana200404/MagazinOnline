using System;

namespace MagazinOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            Magazin magazin = new Magazin();
            SalvareIncarcareService.IncarcaDate(magazin);

            bool ruleaza = true;
            while (ruleaza)
            {
                Console.WriteLine("1. Utilizator\n2. Administrator\n3. Iesire");
                var optiune = Console.ReadLine()?.Trim();

                switch (optiune)
                {
                    case "1":
                        MeniuUtilizator(magazin);
                        break;
                    case "2":
                        MeniuAdministrator(magazin);
                        break;
                    case "3":
                        SalvareIncarcareService.SalveazaDate(magazin);
                        ruleaza = false;
                        break;
                    default:
                        Console.WriteLine("Optiune invalida. Va rugam sa introduceti 1, 2 sau 3.");
                        break;
                }
            }
        }

        static void MeniuUtilizator(Magazin magazin)
        {
            bool ruleaza = true;
            while (ruleaza)
            {
                Console.WriteLine("\n--- Meniu Utilizator ---");
                Console.WriteLine(
                    "1. Vizualizare produse\n2. Cautare produs\n3. Ordonare produse dupa pret\n4. Plasare comanda\n5. Inapoi");
                var optiune = Console.ReadLine()?.Trim();

                switch (optiune)
                {
                    case "1":
                        var produse = magazin.VizualizeazaProduse();
                        foreach (var produs in produse)
                        {
                            Console.WriteLine(produs.Detalii());
                        }

                        break;
                    case "2":
                        Console.Write("Introduceti numele produsului: ");
                        var nume = Console.ReadLine();
                        var rezultate = magazin.CautaProdus(nume);
                        if (rezultate.Any())
                        {
                            foreach (var produs in rezultate)
                            {
                                Console.WriteLine(produs.Detalii());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nu s-au gasit produse cu acest nume.");
                        }

                        break;
                    case "3":
                        Console.Write("Ordonare crescatoare (1) sau descrescatoare (2): ");
                        bool crescator = Console.ReadLine()?.Trim() == "1";
                        var produseOrdonate = magazin.OrdoneazaProduse(crescator);
                        foreach (var produs in produseOrdonate)
                        {
                            Console.WriteLine(produs.Detalii());
                        }

                        break;
                    case "4":
                        Console.Write("Nume client: ");
                        var numeClient = Console.ReadLine();
                        Console.Write("Telefon: ");
                        var telefon = Console.ReadLine();
                        Console.Write("Email: ");
                        var email = Console.ReadLine();
                        Console.Write("Adresa de livrare: ");
                        var adresa = Console.ReadLine();
                        try
                        {
                            magazin.AdaugaComanda(new Comanda
                            {
                                NumeClient = numeClient,
                                Telefon = telefon,
                                Email = email,
                                AdresaLivrare = adresa,
                                DataComenzii = DateTime.Now
                            });
                            Console.WriteLine("Comanda a fost plasata cu succes.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Eroare: {ex.Message}");
                        }

                        break;
                    case "5":
                        ruleaza = false;
                        break;
                    default:
                        Console.WriteLine("Optiune invalida. Va rugam sa introduceti un numar intre 1 si 5.");
                        break;
                }
            }
        }

        static void MeniuAdministrator(Magazin magazin)
        {
            bool ruleaza = true;
            while (ruleaza)
            {
                Console.WriteLine("\n--- Meniu Administrator ---");
                Console.WriteLine(
                    "1. Adaugare produs\n2. Scoatere produs\n3. Modificare stoc\n4. Vizualizare comenzi\n5. Procesare comanda\n6. Inapoi");
                var optiune = Console.ReadLine()?.Trim();

                switch (optiune)
                {
                    case "1":
                        Console.Write("Tip produs (1 - Generic, 2 - Perisabil, 3 - Electrocasnic): ");
                        var tip = Console.ReadLine()?.Trim();
                        Console.Write("Denumire: ");
                        var denumire = Console.ReadLine();
                        Console.Write("Pret: ");
                        var pret = decimal.Parse(Console.ReadLine());
                        Console.Write("Stoc: ");
                        var stoc = int.Parse(Console.ReadLine());

                        try
                        {
                            if (tip == "1")
                            {
                                magazin.AdaugaProdus(new Produs { Denumire = denumire, Pret = pret, Stoc = stoc });
                            }
                            else if (tip == "2")
                            {
                                Console.Write("Data expirarii (yyyy-MM-dd): ");
                                var dataExpirarii = DateTime.Parse(Console.ReadLine());
                                Console.Write("Conditii de pastrare: ");
                                var conditii = Console.ReadLine();
                                magazin.AdaugaProdus(new ProdusPerisabil
                                {
                                    Denumire = denumire, Pret = pret, Stoc = stoc, DataExpirarii = dataExpirarii,
                                    ConditiiPastrare = conditii
                                });
                            }
                            else if (tip == "3")
                            {
                                Console.Write("Clasa energetica: ");
                                var clasa = Console.ReadLine();
                                Console.Write("Putere maxima (W): ");
                                var putere = int.Parse(Console.ReadLine());
                                magazin.AdaugaProdus(new ProdusElectrocasnic
                                {
                                    Denumire = denumire, Pret = pret, Stoc = stoc, ClasaEnergetica = clasa,
                                    PutereMaxima = putere
                                });
                            }

                            Console.WriteLine("Produs adaugat cu succes.");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Eroare: {ex.Message}");
                        }

                        break;
                    case "2":
                        Console.Write("Introduceti denumirea produsului de scos: ");
                        var denumireScoasa = Console.ReadLine();
                        try
                        {
                            magazin.ScoateProdus(denumireScoasa);
                            Console.WriteLine("Produsul a fost scos cu succes.");
                        }
                        catch (KeyNotFoundException ex)
                        {
                            Console.WriteLine($"Eroare: {ex.Message}");
                        }

                        break;
                    case "3":
                        Console.Write("Introduceti denumirea produsului pentru modificarea stocului: ");
                        var denumireModificata = Console.ReadLine();
                        Console.Write("Introduceti noul stoc: ");
                        var nouStoc = int.Parse(Console.ReadLine());
                        try
                        {
                            magazin.ModificaStoc(denumireModificata, nouStoc);
                            Console.WriteLine("Stocul a fost modificat cu succes.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Eroare: {ex.Message}");
                        }

                        break;
                    case "4":
                        var comenzi = magazin.VizualizeazaComenzi();
                        if (comenzi.Count == 0)
                        {
                            Console.WriteLine("Nu exista comenzi plasate.");
                        }
                        else
                        {
                            foreach (var comanda in comenzi)
                            {
                                Console.WriteLine(
                                    $"{comanda.NumeClient} - {comanda.Status}, Data: {comanda.DataComenzii}");
                            }
                        }

                        break;
                    case "5":
                        Console.Write("Introduceti numele clientului pentru comanda de procesat: ");
                        var numeClientProcesare = Console.ReadLine();
                        var comandaDeProcesat = magazin.VizualizeazaComenzi().FirstOrDefault(c =>
                            c.NumeClient.Equals(numeClientProcesare, StringComparison.OrdinalIgnoreCase));
                        if (comandaDeProcesat == null)
                        {
                            Console.WriteLine("Nu s-a gasit nicio comanda pentru acest client.");
                        }
                        else
                        {
                            Console.Write("Introduceti data livrarii (yyyy-MM-dd): ");
                            var dataLivrare = DateTime.Parse(Console.ReadLine());
                            try
                            {
                                comandaDeProcesat.ProceseazaComanda(dataLivrare);
                                Console.WriteLine("Comanda a fost procesata cu succes.");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Eroare: {ex.Message}");
                            }
                        }

                        break;
                    case "6":
                        ruleaza = false;
                        break;
                    default:
                        Console.WriteLine("Optiune invalida. Va rugam sa introduceti un numar intre 1 si 6.");
                        break;
                }
            }
        }
    }
}

