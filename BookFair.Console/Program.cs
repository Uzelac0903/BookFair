
using BookFair.Core.DAO;
using BookFair.Core.Models;

string dataDirectory = Path.Combine(
    Directory.GetCurrentDirectory(), "Data");

PosetilacDAO dao = new(dataDirectory);

while (true)
{
    Console.WriteLine("\n========== BOOKFAIR ==========");
    Console.WriteLine("1. Dodaj posetioca");
    Console.WriteLine("2. Izmeni posetioca");
    Console.WriteLine("3. Obrisi posetioca");
    Console.WriteLine("4. Prikazi posetioce");
    Console.WriteLine("0. Izlaz");
    Console.Write("Izaberite opciju: ");

    string? izbor = Console.ReadLine();

    switch (izbor)
    {
        case "1":
            DodajPosetioca();
            break;

        case "2":
            Console.WriteLine("Izmena jos nije implementirana.");
            break;

        case "3":
            Console.WriteLine("Brisanje jos nije implementirano.");
            break;

        case "4":
            PrikaziPosetioce();
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Nepostojeca opcija.");
            break;
    }
}

void DodajPosetioca()
{
    Console.Write("Broj clanske karte: ");
    string brojKarte = Console.ReadLine() ?? "";

    List<Posetilac> posetioci = dao.Load();

    if (posetioci.Any(p => p.BrojClanskeKarte == brojKarte))
    {
        Console.WriteLine("Posetilac vec postoji.");
        return;
    }

    Console.Write("Ime: ");
    string ime = Console.ReadLine() ?? "";

    Console.Write("Prezime: ");
    string prezime = Console.ReadLine() ?? "";

    Posetilac novi = new()
    {
        BrojClanskeKarte = brojKarte,
        Ime = ime,
        Prezime = prezime,
        Status = StatusPosetioca.Redovan
    };

    posetioci.Add(novi);
    dao.Save(posetioci);

    Console.WriteLine("Posetilac je uspesno dodat.");
}

void PrikaziPosetioce()
{
    List<Posetilac> posetioci = dao.Load();

    if (posetioci.Count == 0)
    {
        Console.WriteLine("Nema posetilaca.");
        return;
    }

    Console.WriteLine("\n--- LISTA POSETILACA ---");

    foreach (Posetilac p in posetioci)
    {
        Console.WriteLine(
            $"{p.BrojClanskeKarte} | {p.Ime} {p.Prezime} | {p.Status}");
    }
}
