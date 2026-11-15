namespace BookFair.Core.Models;

public class Kupovina
{
    public string BrojClanskeKarte { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateTime DatumKupovine { get; set; }

    public int Ocena { get; set; }

    public string Komentar { get; set; } = string.Empty;
}