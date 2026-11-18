
using BookFair.Core.Serialization;
using System.Globalization;

namespace BookFair.Core.Models;

public class Posetilac : ISerializable
{
    public string BrojClanskeKarte { get; set; } = string.Empty;

    public string Ime { get; set; } = string.Empty;

    public string Prezime { get; set; } = string.Empty;

    public DateTime DatumRodjenja { get; set; }

    public string KontaktTelefon { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int TrenutnaGodinaClanstva { get; set; }

    public StatusPosetioca Status { get; set; }

    public Adresa Adresa { get; set; } = new();

    public double ProsecnaOcenaRecenzija { get; set; }

    public List<Kupovina> KupljeneKnjige { get; set; } = new();

    public List<string> ListaZelja { get; set; } = new();

    public string[] ToCSV()
    {
        return new string[]
        {
            BrojClanskeKarte,
            Ime,
            Prezime,
            DatumRodjenja.ToString("O"),
            KontaktTelefon,
            Email,
            TrenutnaGodinaClanstva.ToString(
                CultureInfo.InvariantCulture
            ),
            Status.ToString(),
            Adresa.Ulica,
            Adresa.Broj,
            Adresa.Grad,
            Adresa.Drzava,
            ProsecnaOcenaRecenzija.ToString(
                CultureInfo.InvariantCulture
            )
        };
    }

    public void FromCSV(string[] values)
    {
        BrojClanskeKarte = values[0];
        Ime = values[1];
        Prezime = values[2];

        DatumRodjenja = DateTime.ParseExact(
            values[3],
            "O",
            CultureInfo.InvariantCulture
        );

        KontaktTelefon = values[4];
        Email = values[5];

        TrenutnaGodinaClanstva = int.Parse(
            values[6],
            CultureInfo.InvariantCulture
        );

        Status = Enum.Parse<StatusPosetioca>(
            values[7]
        );

        Adresa = new Adresa
        {
            Ulica = values[8],
            Broj = values[9],
            Grad = values[10],
            Drzava = values[11]
        };

        ProsecnaOcenaRecenzija = double.Parse(
            values[12],
            CultureInfo.InvariantCulture
        );
    }
}
