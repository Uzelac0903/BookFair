using BookFair.Core.Serialization;

namespace BookFair.Core.Models;

public class Knjiga : ISerializable
{
    public string ISBN { get; set; } = string.Empty;

    public string Naziv { get; set; } = string.Empty;

    public string Zanr { get; set; } = string.Empty;

    public int GodinaIzdanja { get; set; }

    public decimal Cena { get; set; }

    public int BrojStrana { get; set; }

    public List<Autor> Autori { get; set; } = new List<Autor>();

    public Izdavac? Izdavac { get; set; }
    public string? IzdavacSifra { get; set; }

    public List<Posetilac> Kupci { get; set; } = new List<Posetilac>();

    public List<Posetilac> ListaZelja { get; set; } = new List<Posetilac>();

    public string[] ToCSV()
    {
        return new string[]
        {
        ISBN,
        Naziv,
        Zanr,
        GodinaIzdanja.ToString(),
        Cena.ToString(System.Globalization.CultureInfo.InvariantCulture),
        BrojStrana.ToString(),
        Izdavac?.Sifra ?? IzdavacSifra ?? string.Empty
        };
    }

    public void FromCSV(string[] values)
    {
        ISBN = values[0];
        Naziv = values[1];
        Zanr = values[2];
        GodinaIzdanja = int.Parse(values[3]);

        Cena = decimal.Parse(
            values[4],
            System.Globalization.CultureInfo.InvariantCulture
        );

        BrojStrana = int.Parse(values[5]);

        IzdavacSifra = string.IsNullOrEmpty(values[6])
            ? null
            : values[6];
    }

}

