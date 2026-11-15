
using System;
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class AutorService
{
    public Autor DodajAutora()
    {
        Console.WriteLine("=== DODAVANJE AUTORA ===");

        Autor autor = new Autor();

        Console.Write("Ime: ");
        autor.Ime = Console.ReadLine() ?? "";

        Console.Write("Prezime: ");
        autor.Prezime = Console.ReadLine() ?? "";

        Console.Write("Datum rođenja: ");
        autor.DatumRodjenja = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("--- ADRESA ---");

        Console.Write("Ulica: ");
        autor.Adresa.Ulica = Console.ReadLine() ?? "";

        Console.Write("Broj: ");
        autor.Adresa.Broj = Console.ReadLine() ?? "";

        Console.Write("Grad: ");
        autor.Adresa.Grad = Console.ReadLine() ?? "";

        Console.Write("Država: ");
        autor.Adresa.Drzava = Console.ReadLine() ?? "";

        Console.Write("Telefon: ");
        autor.Telefon = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        autor.Email = Console.ReadLine() ?? "";

        Console.Write("Broj lične karte: ");
        autor.BrojLicneKarte = Console.ReadLine() ?? "";

        Console.Write("Godine iskustva: ");
        autor.GodineIskustva = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Autor je uspešno unet!");

        return autor;
    }
}
