using System;

namespace MagazinOnline
{
    public class ProdusPerisabil : Produs
    {
        public DateTime DataExpirarii { get; set; }
        public string ConditiiPastrare { get; set; }

        public override string Detalii() => base.Detalii() + $", Expira la: {DataExpirarii.ToShortDateString()}, Conditii: {ConditiiPastrare}";

        public new void Validare()
        {
            base.Validare();
            if (DataExpirarii <= DateTime.Now)
                throw new ArgumentException("Data expirarii trebuie sa fie in viitor.");
            if (string.IsNullOrWhiteSpace(ConditiiPastrare))
                throw new ArgumentException("Conditiile de pastrare nu pot fi goale.");
        }
    }
}