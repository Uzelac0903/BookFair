
using BookFair.Core.Serialization;

namespace BookFair.Core.Models;

public class AutorKnjiga : ISerializable
{
    public string BrojLicneKarte { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public string[] ToCSV()
    {
        return new string[]
        {
            BrojLicneKarte,
            ISBN
        };
    }

    public void FromCSV(string[] values)
    {
        BrojLicneKarte = values[0];
        ISBN = values[1];
    }
}
