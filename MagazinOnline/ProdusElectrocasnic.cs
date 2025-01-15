using System;

namespace MagazinOnline
{
    public class ProdusElectrocasnic : Produs
    {
        public string ClasaEnergetica { get; set; }
        public int PutereMaxima { get; set; }

        public override string Detalii() => base.Detalii() + $", Clasa energetica: {ClasaEnergetica}, Putere maxima: {PutereMaxima}W";

        public new void Validare()
        {
            base.Validare();
            if (string.IsNullOrWhiteSpace(ClasaEnergetica))
                throw new ArgumentException("Clasa energetica nu poate fi goala.");
            if (PutereMaxima <= 0)
                throw new ArgumentException("Puterea maxima trebuie sa fie pozitiva.");
        }
    }
}