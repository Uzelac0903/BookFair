using BookFair.Core.Serialization;

namespace BookFair.Core.Models;

public class Izdavac : ISerializable
{
    public string Sifra { get; set; } = string.Empty;

    public string Naziv { get; set; } = string.Empty;

    public string? SefBrojLicneKarte { get; set; }

    public List<string> Autori { get; set; } = new();

    public List<string> Knjige { get; set; } = new();

    public string[] ToCSV()
    {
        return new string[]
        {
            Sifra,
            Naziv,
            SefBrojLicneKarte ?? string.Empty
        };
    }

    public void FromCSV(string[] values)
    {
        Sifra = values[0];
        Naziv = values[1];

        SefBrojLicneKarte = string.IsNullOrEmpty(values[2])
            ? null
            : values[2];
    }
}