using System;
using System.Collections.Generic;
using System.Text;


namespace BookFair.Core.Models;

public class Knjiga
{
    public string ISBN { get; set; } = string.Empty;

    public string Naziv { get; set; } = string.Empty;

    public string Zanr { get; set; } = string.Empty;

    public int GodinaIzdanja { get; set; }

    public decimal Cena { get; set; }

    public int BrojStrana { get; set; }

    public List<Autor> Autori { get; set; } = new List<Autor>();

    public Izdavac? Izdavac { get; set; }

    public List<Posetilac> Kupci { get; set; } = new List<Posetilac>();

    public List<Posetilac> ListaZelja { get; set; } = new List<Posetilac>();
}

