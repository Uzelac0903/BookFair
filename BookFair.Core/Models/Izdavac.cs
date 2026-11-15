namespace BookFair.Core.Models;

public class Izdavac
{
    public string Sifra { get; set; } = string.Empty;

    public string Naziv { get; set; } = string.Empty;

    public string? SefBrojLicneKarte { get; set; }

    public List<string> Autori { get; set; } = new();

    public List<string> Knjige { get; set; } = new();
}