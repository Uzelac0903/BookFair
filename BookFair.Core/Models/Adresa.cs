
using BookFair.Core.Serialization;
using System.Globalization;

namespace BookFair.Core.Models;

public class Adresa : ISerializable
{
    public int Id { get; set; }

    public string Ulica { get; set; } = string.Empty;

    public string Broj { get; set; } = string.Empty;

    public string Grad { get; set; } = string.Empty;

    public string Drzava { get; set; } = string.Empty;

    public string[] ToCSV()
    {
        return new string[]
        {
            Id.ToString(CultureInfo.InvariantCulture),
            Ulica,
            Broj,
            Grad,
            Drzava
        };
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(
            values[0],
            CultureInfo.InvariantCulture
        );

        Ulica = values[1];
        Broj = values[2];
        Grad = values[3];
        Drzava = values[4];
    }
}
