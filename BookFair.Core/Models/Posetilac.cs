namespace BookFair.Core.Models;

public class Posetilac
{
    public string BrojClanskeKarte { get; set; } = string.Empty;

    public string Ime { get; set; } = string.Empty;

    public string Prezime { get; set; } = string.Empty;

    public DateTime DatumRodjenja { get; set; }

    public string KontaktTelefon { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int TrenutnaGodinaClanstva { get; set; }
}