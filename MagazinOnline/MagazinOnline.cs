using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MagazinOnline
{
    public class Produs
    {
        public string Denumire { get; set; }
        public decimal Pret { get; set; }
        public int Stoc { get; set; }

        public virtual string Detalii() => $"{Denumire} - {Pret} RON, Stoc: {Stoc} bucati";
    }

    public class ProdusPerisabil : Produs
    {
        public DateTime DataExpirarii { get; set; }
        public string ConditiiPastrare { get; set; }

        public override string Detalii() => base.Detalii() +
                                            $", Expira la: {DataExpirarii.ToShortDateString()}, Conditii: {ConditiiPastrare}";
    }

    public class ProdusElectrocasnic : Produs
    {
        public string ClasaEnergetica { get; set; }
        public int PutereMaxima { get; set; }

        public override string Detalii() =>
            base.Detalii() + $", Clasa energetica: {ClasaEnergetica}, Putere maxima: {PutereMaxima}W";
    }

    public class Magazin
    {
        private List<Produs> produse = new List<Produs>();
        private List<Comanda> comenzi = new List<Comanda>();

        public void AdaugaProdus(Produs produs)
        {
            produse.Add(produs);
        }

        public void ScoateProdus(string denumire)
        {
            produse.RemoveAll(p => p.Denumire.Equals(denumire, StringComparison.OrdinalIgnoreCase));
        }

        public void ModificaStoc(string denumire, int nouStoc)
        {
            var produs = produse.FirstOrDefault(p => p.Denumire.Equals(denumire, StringComparison.OrdinalIgnoreCase));
            if (produs != null)
            {
                produs.Stoc = nouStoc;
            }
        }

        public List<Produs> VizualizeazaProduse() => produse;

        public List<Produs> CautaProdus(string nume) =>
            produse.Where(p => p.Denumire.Contains(nume, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Produs> OrdoneazaProduse(bool crescator)
        {
            return crescator ? produse.OrderBy(p => p.Pret).ToList() : produse.OrderByDescending(p => p.Pret).ToList();
        }

        public void AdaugaComanda(Comanda comanda)
        {
            comenzi.Add(comanda);
        }

        public List<Comanda> VizualizeazaComenzi() => comenzi;
    }
}