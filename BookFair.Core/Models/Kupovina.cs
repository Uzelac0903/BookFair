using BookFair.Core.Serialization;
using System.Globalization;

namespace BookFair.Core.Models;

public class Kupovina : ISerializable
{
    public string BrojClanskeKarte { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateTime DatumKupovine { get; set; }

    public int Ocena { get; set; }

    public string Komentar { get; set; } = string.Empty;

    public string[] ToCSV()
    {
        return new string[]
        {
            BrojClanskeKarte,
            ISBN,
            DatumKupovine.ToString("O"),
            Ocena.ToString(CultureInfo.InvariantCulture),
            Komentar
        };
    }

    public void FromCSV(string[] values)
    {
        BrojClanskeKarte = values[0];
        ISBN = values[1];

        DatumKupovine = DateTime.ParseExact(
            values[2],
            "O",
            CultureInfo.InvariantCulture
        );

        Ocena = int.Parse(
            values[3],
            CultureInfo.InvariantCulture
        );

        Komentar = values[4];
    }
}