using System;

namespace MagazinOnline
{
    public class Comanda
    {
        public string NumeClient { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string AdresaLivrare { get; set; }
        public DateTime DataComenzii { get; set; }
        public string Status { get; set; } = "In asteptare";

        public void ProceseazaComanda(DateTime dataLivrare)
        {
            if (dataLivrare <= DateTime.Now)
                throw new ArgumentException("Data livrarii trebuie sa fie in viitor.");
            Status = "In curs de livrare";
            DataComenzii = dataLivrare;
        }

        public void Validare()
        {
            if (string.IsNullOrWhiteSpace(NumeClient))
                throw new ArgumentException("Numele clientului nu poate fi gol.");
            if (string.IsNullOrWhiteSpace(Telefon) || Telefon.Length < 10)
                throw new ArgumentException("Telefonul trebuie sa contina cel putin 10 cifre.");
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
                throw new ArgumentException("Emailul nu este valid.");
            if (string.IsNullOrWhiteSpace(AdresaLivrare))
                throw new ArgumentException("Adresa de livrare nu poate fi goala.");
        }
    }
}