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

    public StatusPosetioca Status { get; set; } = StatusPosetioca.Redovan;

    // Adresa posetioca
    public Adresa Adresa { get; set; } = new();

    // Podaci izvedeni iz kupovina
    public double ProsecnaOcenaRecenzija { get; set; }

    // ISBN brojevi kupljenih knjiga predstavljeni kroz kupovine
    public List<Kupovina> KupljeneKnjige { get; set; } = new();

    // ISBN brojevi knjiga na listi zelja
    public List<string> ListaZelja { get; set; } = new();
}