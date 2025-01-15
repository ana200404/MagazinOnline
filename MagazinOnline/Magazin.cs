using System;
using System.Collections.Generic;
using System.Linq;

namespace MagazinOnline
{
    public class Magazin
    {
        private List<Produs> produse = new List<Produs>();
        private List<Comanda> comenzi = new List<Comanda>();

        public void AdaugaProdus(Produs produs)
        {
            produs.Validare();
            produse.Add(produs);
        }

        public void ScoateProdus(string denumire)
        {
            var produs = produse.FirstOrDefault(p => p.Denumire.Equals(denumire, StringComparison.OrdinalIgnoreCase));
            if (produs == null)
                throw new KeyNotFoundException("Produsul nu a fost gasit.");
            produse.Remove(produs);
        }

        public void ModificaStoc(string denumire, int nouStoc)
        {
            var produs = produse.FirstOrDefault(p => p.Denumire.Equals(denumire, StringComparison.OrdinalIgnoreCase));
            if (produs == null)
                throw new KeyNotFoundException("Produsul nu a fost gasit.");
            if (nouStoc < 0)
                throw new ArgumentException("Stocul nu poate fi negativ.");
            produs.Stoc = nouStoc;
        }

        public List<Produs> VizualizeazaProduse() => produse;

        public List<Produs> CautaProdus(string nume) => produse.Where(p => p.Denumire.Contains(nume, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Produs> OrdoneazaProduse(bool crescator)
        {
            return crescator ? produse.OrderBy(p => p.Pret).ToList() : produse.OrderByDescending(p => p.Pret).ToList();
        }

        public void AdaugaComanda(Comanda comanda)
        {
            comanda.Validare();
            comenzi.Add(comanda);
        }

        public List<Comanda> VizualizeazaComenzi() => comenzi;
    }
}