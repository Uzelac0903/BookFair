using System;
using System.Collections.Generic;
using System.Text;


namespace BookFair.Core.Models;

public class Autor
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
}

