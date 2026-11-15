
using BookFair.Core.DAO;
using BookFair.Core.Models;
using BookFair.Core.Services;

string dataDirectory = Path.Combine(
    Directory.GetCurrentDirectory(), "Data");

PosetilacDAO dao = new(dataDirectory);
PosetilacService service = new(dao);

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
            IzmeniPosetioca();
            break;

        case "3":
            ObrisiPosetioca();
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

// 8.1 - Dodavanje
void DodajPosetioca()
{
    Console.WriteLine("\n--- DODAVANJE POSETIOCA ---");

    Console.Write("Broj clanske karte: ");
    string brojKarte = Console.ReadLine() ?? "";

    Console.Write("Ime: ");
    string ime = Console.ReadLine() ?? "";

    Console.Write("Prezime: ");
    string prezime = Console.ReadLine() ?? "";

    Console.Write("Datum rodjenja (dd.MM.yyyy): ");
    if (!DateTime.TryParseExact(
            Console.ReadLine(),
            "dd.MM.yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime datumRodjenja))
    {
        Console.WriteLine("Neispravan datum rodjenja.");
        return;
    }

    Console.WriteLine("\n--- ADRESA ---");

    Console.Write("Ulica: ");
    string ulica = Console.ReadLine() ?? "";

    Console.Write("Broj: ");
    string broj = Console.ReadLine() ?? "";

    Console.Write("Grad: ");
    string grad = Console.ReadLine() ?? "";

    Console.Write("Drzava: ");
    string drzava = Console.ReadLine() ?? "";

    Console.Write("\nKontakt telefon: ");
    string telefon = Console.ReadLine() ?? "";

    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "";

    Console.Write("Trenutna godina clanstva: ");
    if (!int.TryParse(Console.ReadLine(), out int godina))
    {
        Console.WriteLine("Neispravna godina clanstva.");
        return;
    }

    Console.Write("Status (R - redovan, V - VIP): ");
    string statusUnos = (Console.ReadLine() ?? "")
        .Trim().ToUpperInvariant();

    if (statusUnos != "R" && statusUnos != "V")
    {
        Console.WriteLine("Status mora biti R ili V.");
        return;
    }

    StatusPosetioca status = statusUnos == "V"
        ? StatusPosetioca.VIP
        : StatusPosetioca.Redovan;

    Posetilac novi = new()
    {
        BrojClanskeKarte = brojKarte,
        Ime = ime,
        Prezime = prezime,
        DatumRodjenja = datumRodjenja,
        Adresa = new Adresa
        {
            Ulica = ulica,
            Broj = broj,
            Grad = grad,
            Drzava = drzava
        },
        KontaktTelefon = telefon,
        Email = email,
        TrenutnaGodinaClanstva = godina,
        Status = status
    };

    bool uspesno = service.Add(novi);

    Console.WriteLine(uspesno
        ? "Posetilac je uspesno dodat."
        : "Dodavanje nije uspelo.");
}


// 8.2 - Izmena
void IzmeniPosetioca()
{
    Console.WriteLine("\n--- IZMENA POSETIOCA ---");

    Console.Write("Broj clanske karte: ");
    string brojKarte = Console.ReadLine() ?? "";

    Posetilac? postojeci = service.GetAll()
        .FirstOrDefault(p => p.BrojClanskeKarte == brojKarte);

    if (postojeci == null)
    {
        Console.WriteLine("Posetilac nije pronadjen.");
        return;
    }

    Console.Write("Novo ime: ");
    string novoIme = Console.ReadLine() ?? "";

    Console.Write("Novo prezime: ");
    string novoPrezime = Console.ReadLine() ?? "";

    postojeci.Ime = novoIme;
    postojeci.Prezime = novoPrezime;

    bool uspesno = service.Update(postojeci);

    Console.WriteLine(uspesno
        ? "Posetilac je uspesno izmenjen."
        : "Izmena nije uspela.");
}

// 8.3 - Brisanje
void ObrisiPosetioca()
{
    Console.WriteLine("\n--- BRISANJE POSETIOCA ---");

    Console.Write("Broj clanske karte: ");
    string brojKarte = Console.ReadLine() ?? "";

    bool uspesno = service.Delete(brojKarte);

    Console.WriteLine(uspesno
        ? "Posetilac je uspesno obrisan."
        : "Posetilac nije pronadjen.");
}

// 8.4 - Prikaz
void PrikaziPosetioce()
{
    List<Posetilac> posetioci = service.GetAll();

    if (posetioci.Count == 0)
    {
        Console.WriteLine("Nema posetilaca.");
        return;
    }

    Console.WriteLine("\n--- LISTA POSETILACA ---");

    foreach (Posetilac p in posetioci)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Clanska karta: {p.BrojClanskeKarte}");
        Console.WriteLine($"Ime: {p.Ime}");
        Console.WriteLine($"Prezime: {p.Prezime}");
        Console.WriteLine($"Datum rodjenja: {p.DatumRodjenja:dd.MM.yyyy}");
        Console.WriteLine(
            $"Adresa: {p.Adresa?.Ulica} {p.Adresa?.Broj}, " +
            $"{p.Adresa?.Grad}, {p.Adresa?.Drzava}");
        Console.WriteLine($"Telefon: {p.KontaktTelefon}");
        Console.WriteLine($"Email: {p.Email}");
        Console.WriteLine($"Godina clanstva: {p.TrenutnaGodinaClanstva}");
        Console.WriteLine($"Status: {p.Status}");
        Console.WriteLine($"Prosecna ocena: {p.ProsecnaOcenaRecenzija:F2}");
        Console.WriteLine($"Broj kupovina: {p.KupljeneKnjige?.Count ?? 0}");
        Console.WriteLine($"Broj knjiga na listi zelja: {p.ListaZelja?.Count ?? 0}");
    }
}

