
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

    Console.Write("Broj clanske karte posetioca: ");
    string brojKarte = Console.ReadLine() ?? "";

    Posetilac? postojeci = service.GetAll()
        .FirstOrDefault(p => p.BrojClanskeKarte == brojKarte);

    if (postojeci == null)
    {
        Console.WriteLine("Posetilac nije pronadjen.");
        return;
    }

    Console.WriteLine(
        "Unesite nove podatke (Enter zadrzava staru vrednost).");

    Console.Write($"Ime ({postojeci.Ime}): ");
    string? ime = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(ime))
        postojeci.Ime = ime;

    Console.Write($"Prezime ({postojeci.Prezime}): ");
    string? prezime = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(prezime))
        postojeci.Prezime = prezime;

    Console.Write(
        $"Datum rodjenja ({postojeci.DatumRodjenja:dd.MM.yyyy}): ");
    string? datumUnos = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(datumUnos))
    {
        if (!DateTime.TryParseExact(
                datumUnos,
                "dd.MM.yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime datum))
        {
            Console.WriteLine("Neispravan datum.");
            return;
        }

        postojeci.DatumRodjenja = datum;
    }

    postojeci.Adresa ??= new Adresa();

    Console.Write($"Ulica ({postojeci.Adresa.Ulica}): ");
    string? ulica = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(ulica))
        postojeci.Adresa.Ulica = ulica;

    Console.Write($"Broj ({postojeci.Adresa.Broj}): ");
    string? broj = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(broj))
        postojeci.Adresa.Broj = broj;

    Console.Write($"Grad ({postojeci.Adresa.Grad}): ");
    string? grad = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(grad))
        postojeci.Adresa.Grad = grad;

    Console.Write($"Drzava ({postojeci.Adresa.Drzava}): ");
    string? drzava = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(drzava))
        postojeci.Adresa.Drzava = drzava;

    Console.Write($"Telefon ({postojeci.KontaktTelefon}): ");
    string? telefon = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(telefon))
        postojeci.KontaktTelefon = telefon;

    Console.Write($"Email ({postojeci.Email}): ");
    string? email = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(email))
        postojeci.Email = email;

    Console.Write(
        $"Godina clanstva ({postojeci.TrenutnaGodinaClanstva}): ");
    string? godinaUnos = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(godinaUnos))
    {
        if (!int.TryParse(godinaUnos, out int godina))
        {
            Console.WriteLine("Neispravna godina.");
            return;
        }

        postojeci.TrenutnaGodinaClanstva = godina;
    }

    Console.Write(
        $"Status ({postojeci.Status}) [R/V]: ");
    string? statusUnos = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(statusUnos))
    {
        switch (statusUnos.Trim().ToUpperInvariant())
        {
            case "R":
                postojeci.Status = StatusPosetioca.Redovan;
                break;

            case "V":
                postojeci.Status = StatusPosetioca.VIP;
                break;

            default:
                Console.WriteLine("Status mora biti R ili V.");
                return;
        }
    }

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

