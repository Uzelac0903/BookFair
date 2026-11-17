using BookFair.Core.Serialization;

namespace BookFair.Core.Models;

public class Autor : ISerializable
{
    public string Ime { get; set; } = string.Empty;

    public string Prezime { get; set; } = string.Empty;

    public DateTime DatumRodjenja { get; set; }

    public Adresa Adresa { get; set; } = new Adresa();

    public string Telefon { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string BrojLicneKarte { get; set; } = string.Empty;

    public int GodineIskustva { get; set; }

    public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();


    public string[] ToCSV()
    {
        return new string[]
        {
        Ime,
        Prezime,
        DatumRodjenja.ToString("O"),
        Adresa.Ulica,
        Adresa.Broj,
        Adresa.Grad,
        Adresa.Drzava,
        Telefon,
        Email,
        BrojLicneKarte,
        GodineIskustva.ToString()
        };
    }

    public void FromCSV(string[] values)
    {
        Ime = values[0];
        Prezime = values[1];
        DatumRodjenja = DateTime.ParseExact(
            values[2],
            "O",
            System.Globalization.CultureInfo.InvariantCulture
        );

        Adresa = new Adresa
        {
            Ulica = values[3],
            Broj = values[4],
            Grad = values[5],
            Drzava = values[6]
        };

        Telefon = values[7];
        Email = values[8];
        BrojLicneKarte = values[9];
        GodineIskustva = int.Parse(values[10]);
    }


}

