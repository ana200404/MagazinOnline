using System;

namespace MagazinOnline
{
    public class Produs
    {
        public string Denumire { get; set; }
        public decimal Pret { get; set; }
        public int Stoc { get; set; }

        public virtual string Detalii() => $"{Denumire} - {Pret} RON, Stoc: {Stoc} bucati";

        public void Validare()
        {
            if (string.IsNullOrWhiteSpace(Denumire))
                throw new ArgumentException("Denumirea produsului nu poate fi goala.");
            if (Pret <= 0)
                throw new ArgumentException("Pretul trebuie sa fie pozitiv.");
            if (Stoc < 0)
                throw new ArgumentException("Stocul nu poate fi negativ.");
        }
    }
}