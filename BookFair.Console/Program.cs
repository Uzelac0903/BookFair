
using BookFair.Core.DAO;
using BookFair.Core.Models;
using BookFair.Core.Services;

string dataDirectory = Path.Combine(
    Directory.GetCurrentDirectory(), "Data");

PosetilacDAO dao = new(dataDirectory);
PosetilacService service = new(dao);

KupovinaDAO kupovinaDAO = new(dataDirectory);
KupovinaService kupovinaService = new(kupovinaDAO);

IzdavacDAO izdavacDAO = new(dataDirectory);
IzdavacService izdavacService = new(izdavacDAO);

AutorDAO autorDAO = new(dataDirectory);
KnjigaDAO knjigaDAO = new(dataDirectory);
AutorKnjigaDAO autorKnjigaDAO = new(dataDirectory);
AutorService autorService = new(autorDAO, autorKnjigaDAO);
KnjigaService knjigaService = new(knjigaDAO, autorKnjigaDAO);

while (true)
{
    Console.WriteLine("\n========== BOOKFAIR ==========");
    Console.WriteLine("1. Dodaj posetioca");
    Console.WriteLine("2. Izmeni posetioca");
    Console.WriteLine("3. Obrisi posetioca");
    Console.WriteLine("4. Prikazi posetioce");
    
    Console.WriteLine("\n--- KUPOVINE ---");
    Console.WriteLine("5. Dodaj kupovinu");
    Console.WriteLine("6. Izmeni kupovinu");
    Console.WriteLine("7. Obrisi kupovinu");
    Console.WriteLine("8. Prikazi kupovine");
    
    Console.WriteLine("\n--- IZDAVACI ---");
    Console.WriteLine("9. Dodaj izdavaca");
    Console.WriteLine("10. Izmeni izdavaca");
    Console.WriteLine("11. Obrisi izdavaca");
    Console.WriteLine("12. Prikazi izdavace");

    Console.WriteLine("\n--- AUTORI ---");
    Console.WriteLine("13. Dodaj autora");
    Console.WriteLine("14. Izmeni autora");
    Console.WriteLine("15. Obrisi autora");
    Console.WriteLine("16. Prikazi autore");

    Console.WriteLine("\n--- KNJIGE ---");
    Console.WriteLine("17. Prikazi knjige");
    Console.WriteLine("18. Obrisi knjigu");

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
        
        case "5":
            DodajKupovinu();
            break;

        case "6":
            IzmeniKupovinu();
            break;

        case "7":
            ObrisiKupovinu();
            break;

        case "8":
            PrikaziKupovine();
            break;
        
        case "9":
            DodajIzdavaca();
            break;

        case "10":
            IzmeniIzdavaca();
            break;

        case "11":
            ObrisiIzdavaca();
            break;

        case "12":
            PrikaziIzdavace();
            break;

        case "13":
            DodajAutora();
            break;

        case "14":
            IzmeniAutora();
            break;

        case "15":
            ObrisiAutora();
            break;

        case "16":
            PrikaziAutore();
            break;

        case "17":
            PrikaziKnjige();
            break;

        case "18":
            ObrisiKnjigu();
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

void DodajKupovinu()
{
    Console.WriteLine("\n--- DODAVANJE KUPOVINE ---");

    Console.Write("Broj clanske karte: ");
    string brojKarte = Console.ReadLine() ?? "";

    Console.Write("ISBN knjige: ");
    string isbn = Console.ReadLine() ?? "";

    Console.Write("Datum kupovine (dd.MM.yyyy): ");
    if (!DateTime.TryParseExact(
            Console.ReadLine(),
            "dd.MM.yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime datum))
    {
        Console.WriteLine("Neispravan datum.");
        return;
    }

    Console.Write("Ocena (1-5): ");
    if (!int.TryParse(Console.ReadLine(), out int ocena)
        || ocena < 1 || ocena > 5)
    {
        Console.WriteLine("Ocena mora biti od 1 do 5.");
        return;
    }

    Console.Write("Komentar: ");
    string komentar = Console.ReadLine() ?? "";

    Kupovina nova = new()
    {
        BrojClanskeKarte = brojKarte,
        ISBN = isbn,
        DatumKupovine = datum,
        Ocena = ocena,
        Komentar = komentar
    };

    bool uspesno = kupovinaService.Add(nova);

    Console.WriteLine(uspesno
        ? "Kupovina je uspesno dodata."
        : "Dodavanje kupovine nije uspelo.");
}


void PrikaziKupovine()
{
    List<Kupovina> kupovine = kupovinaService.GetAll();

    if (kupovine.Count == 0)
    {
        Console.WriteLine("Nema kupovina.");
        return;
    }

    Console.WriteLine("\n--- LISTA KUPOVINA ---");

    for (int i = 0; i < kupovine.Count; i++)
    {
        Kupovina k = kupovine[i];

        Console.WriteLine($"\nKupovina broj: {i + 1}");
        Console.WriteLine($"Posetilac: {k.BrojClanskeKarte}");
        Console.WriteLine($"ISBN: {k.ISBN}");
        Console.WriteLine($"Datum: {k.DatumKupovine:dd.MM.yyyy}");
        Console.WriteLine($"Ocena: {k.Ocena}");
        Console.WriteLine($"Komentar: {k.Komentar}");
    }
}


void IzmeniKupovinu()
{
    Console.WriteLine("\n--- IZMENA KUPOVINE ---");

    List<Kupovina> kupovine = kupovinaService.GetAll();

    if (kupovine.Count == 0)
    {
        Console.WriteLine("Nema kupovina.");
        return;
    }

    PrikaziKupovine();

    Console.Write("Unesi broj kupovine: ");

    if (!int.TryParse(Console.ReadLine(), out int broj)
        || broj < 1 || broj > kupovine.Count)
    {
        Console.WriteLine("Neispravan broj kupovine.");
        return;
    }

    int index = broj - 1;
    Kupovina postojeca = kupovine[index];

    // Pravimo kopiju kako ne bismo menjali original
    // dok korisnik ne zavrsi unos.
    Kupovina izmenjena = new()
    {
        BrojClanskeKarte = postojeca.BrojClanskeKarte,
        ISBN = postojeca.ISBN,
        DatumKupovine = postojeca.DatumKupovine,
        Ocena = postojeca.Ocena,
        Komentar = postojeca.Komentar
    };

    Console.Write($"Broj clanske karte ({izmenjena.BrojClanskeKarte}): ");
    string? karta = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(karta))
        izmenjena.BrojClanskeKarte = karta;

    Console.Write($"ISBN ({izmenjena.ISBN}): ");
    string? isbn = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(isbn))
        izmenjena.ISBN = isbn;

    Console.Write($"Datum ({izmenjena.DatumKupovine:dd.MM.yyyy}): ");
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

        izmenjena.DatumKupovine = datum;
    }

    Console.Write($"Ocena ({izmenjena.Ocena}): ");
    string? ocenaUnos = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(ocenaUnos))
    {
        if (!int.TryParse(ocenaUnos, out int ocena)
            || ocena < 1 || ocena > 5)
        {
            Console.WriteLine("Ocena mora biti od 1 do 5.");
            return;
        }

        izmenjena.Ocena = ocena;
    }

    Console.Write($"Komentar ({izmenjena.Komentar}): ");
    string? komentar = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(komentar))
        izmenjena.Komentar = komentar;

    bool uspesno = kupovinaService.Update(index, izmenjena);

    Console.WriteLine(uspesno
        ? "Kupovina je uspesno izmenjena."
        : "Izmena nije uspela.");
}


void ObrisiKupovinu()
{
    Console.WriteLine("\n--- BRISANJE KUPOVINE ---");

    List<Kupovina> kupovine = kupovinaService.GetAll();

    if (kupovine.Count == 0)
    {
        Console.WriteLine("Nema kupovina.");
        return;
    }

    PrikaziKupovine();

    Console.Write("Unesi broj kupovine za brisanje: ");

    if (!int.TryParse(Console.ReadLine(), out int broj)
        || broj < 1 || broj > kupovine.Count)
    {
        Console.WriteLine("Neispravan broj kupovine.");
        return;
    }

    int index = broj - 1;

    Console.Write("Potvrdi brisanje (D/N): ");
    string potvrda = (Console.ReadLine() ?? "")
        .Trim().ToUpperInvariant();

    if (potvrda != "D")
    {
        Console.WriteLine("Brisanje je otkazano.");
        return;
    }

    bool uspesno = kupovinaService.Delete(index);

    Console.WriteLine(uspesno
        ? "Kupovina je uspesno obrisana."
        : "Brisanje nije uspelo.");
}


void DodajIzdavaca()
{
    Console.WriteLine("\n--- DODAVANJE IZDAVACA ---");

    Console.Write("Sifra izdavaca: ");
    string sifra = Console.ReadLine() ?? "";

    Console.Write("Naziv izdavaca: ");
    string naziv = Console.ReadLine() ?? "";

    Izdavac novi = new()
    {
        Sifra = sifra,
        Naziv = naziv
    };

    bool uspesno = izdavacService.Add(novi);

    Console.WriteLine(uspesno
        ? "Izdavac je uspesno dodat."
        : "Dodavanje izdavaca nije uspelo.");
}


void PrikaziIzdavace()
{
    List<Izdavac> izdavaci = izdavacService.GetAll();

    if (izdavaci.Count == 0)
    {
        Console.WriteLine("Nema izdavaca.");
        return;
    }

    Console.WriteLine("\n--- LISTA IZDAVACA ---");

    foreach (Izdavac i in izdavaci)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Sifra: {i.Sifra}");
        Console.WriteLine($"Naziv: {i.Naziv}");
        Console.WriteLine(
            $"Sef (broj licne karte): {i.SefBrojLicneKarte ?? "Nije postavljen"}");
        Console.WriteLine($"Broj autora: {i.Autori.Count}");
        Console.WriteLine($"Broj knjiga: {i.Knjige.Count}");
    }
}


void IzmeniIzdavaca()
{
    Console.WriteLine("\n--- IZMENA IZDAVACA ---");

    Console.Write("Sifra izdavaca: ");
    string sifra = Console.ReadLine() ?? "";

    Izdavac? postojeci = izdavacService.GetAll()
        .FirstOrDefault(i => i.Sifra == sifra);

    if (postojeci == null)
    {
        Console.WriteLine("Izdavac nije pronadjen.");
        return;
    }

    Console.Write($"Novi naziv ({postojeci.Naziv}): ");
    string? naziv = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(naziv))
        postojeci.Naziv = naziv;

    bool uspesno = izdavacService.Update(postojeci);

    Console.WriteLine(uspesno
        ? "Izdavac je uspesno izmenjen."
        : "Izmena nije uspela.");
}


void ObrisiIzdavaca()
{
    Console.WriteLine("\n--- BRISANJE IZDAVACA ---");

    Console.Write("Sifra izdavaca: ");
    string sifra = Console.ReadLine() ?? "";

    Console.Write("Potvrdi brisanje (D/N): ");
    string potvrda = (Console.ReadLine() ?? "")
        .Trim().ToUpperInvariant();

    if (potvrda != "D")
    {
        Console.WriteLine("Brisanje je otkazano.");
        return;
    }

    bool uspesno = izdavacService.Delete(sifra);

    Console.WriteLine(uspesno
        ? "Izdavac je uspesno obrisan."
        : "Izdavac nije pronadjen.");
}

void DodajAutora()
{
    Console.WriteLine("\n--- DODAVANJE AUTORA ---");

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

    Console.Write("\nTelefon: ");
    string telefon = Console.ReadLine() ?? "";

    Console.Write("Email: ");
    string email = Console.ReadLine() ?? "";

    Console.Write("Broj licne karte: ");
    string brojLicneKarte = Console.ReadLine() ?? "";

    Console.Write("Godine iskustva: ");
    if (!int.TryParse(Console.ReadLine(), out int godineIskustva)
        || godineIskustva < 0)
    {
        Console.WriteLine("Neispravne godine iskustva.");
        return;
    }

    Autor novi = new()
    {
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
        Telefon = telefon,
        Email = email,
        BrojLicneKarte = brojLicneKarte,
        GodineIskustva = godineIskustva
    };

    bool uspesno = autorService.Add(novi);

    Console.WriteLine(uspesno
        ? "Autor je uspesno dodat."
        : "Dodavanje autora nije uspelo.");
}

void IzmeniAutora()
{
    Console.WriteLine("\n--- IZMENA AUTORA ---");

    Console.Write("Broj licne karte autora: ");
    string brojLicneKarte = Console.ReadLine() ?? "";

    Autor? postojeci = autorService.GetAll()
        .FirstOrDefault(a => a.BrojLicneKarte == brojLicneKarte);

    if (postojeci == null)
    {
        Console.WriteLine("Autor nije pronadjen.");
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
            Console.WriteLine("Neispravan datum rodjenja.");
            return;
        }

        postojeci.DatumRodjenja = datum;
    }

    Console.WriteLine("\n--- ADRESA ---");

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

    Console.Write($"Telefon ({postojeci.Telefon}): ");
    string? telefon = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(telefon))
        postojeci.Telefon = telefon;

    Console.Write($"Email ({postojeci.Email}): ");
    string? email = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(email))
        postojeci.Email = email;

    Console.Write(
        $"Godine iskustva ({postojeci.GodineIskustva}): ");
    string? iskustvoUnos = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(iskustvoUnos))
    {
        if (!int.TryParse(iskustvoUnos, out int iskustvo)
            || iskustvo < 0)
        {
            Console.WriteLine("Neispravne godine iskustva.");
            return;
        }

        postojeci.GodineIskustva = iskustvo;
    }

    bool uspesno = autorService.Update(postojeci);

    Console.WriteLine(uspesno
        ? "Autor je uspesno izmenjen."
        : "Izmena autora nije uspela.");
}

void ObrisiAutora()
{
    Console.WriteLine("\n--- BRISANJE AUTORA ---");

    Console.Write("Broj licne karte autora: ");
    string brojLicneKarte = Console.ReadLine() ?? "";

    Autor? postojeci = autorService.GetAll()
        .FirstOrDefault(a => a.BrojLicneKarte == brojLicneKarte);

    if (postojeci == null)
    {
        Console.WriteLine("Autor nije pronadjen.");
        return;
    }

    Console.WriteLine(
        $"Autor: {postojeci.Ime} {postojeci.Prezime}");

    Console.Write("Potvrdi brisanje (D/N): ");
    string potvrda = (Console.ReadLine() ?? "")
        .Trim().ToUpperInvariant();

    if (potvrda != "D")
    {
        Console.WriteLine("Brisanje je otkazano.");
        return;
    }

    bool uspesno = autorService.Delete(brojLicneKarte);

    Console.WriteLine(uspesno
        ? "Autor je uspesno obrisan."
        : "Brisanje autora nije uspelo.");
}

void PrikaziAutore()
{
    List<Autor> autori = autorService.GetAll();

    if (autori.Count == 0)
    {
        Console.WriteLine("Nema autora.");
        return;
    }

    List<Knjiga> knjige = knjigaDAO.Load();
    List<AutorKnjiga> veze = autorKnjigaDAO.Load();

    AutorKnjigaService autorKnjigaService = new();

    autorKnjigaService.PoveziAutoreIKnjige(
        autori, knjige, veze);

    Console.WriteLine("\n--- LISTA AUTORA ---");

    foreach (Autor autor in autori)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Ime: {autor.Ime}");
        Console.WriteLine($"Prezime: {autor.Prezime}");
        Console.WriteLine(
            $"Datum rodjenja: {autor.DatumRodjenja:dd.MM.yyyy}");

        Console.WriteLine(
            $"Adresa: {autor.Adresa.Ulica} {autor.Adresa.Broj}, " +
            $"{autor.Adresa.Grad}, {autor.Adresa.Drzava}");

        Console.WriteLine($"Telefon: {autor.Telefon}");
        Console.WriteLine($"Email: {autor.Email}");
        Console.WriteLine(
            $"Broj licne karte: {autor.BrojLicneKarte}");
        Console.WriteLine(
            $"Godine iskustva: {autor.GodineIskustva}");

        Console.WriteLine("Knjige:");

        if (autor.Knjige.Count == 0)
        {
            Console.WriteLine("  Nema knjiga.");
        }
        else
        {
            foreach (Knjiga knjiga in autor.Knjige)
            {
                Console.WriteLine(
                    $"  - {knjiga.Naziv} (ISBN: {knjiga.ISBN})");
            }
        }
    }
}

void PrikaziKnjige()
{
    List<Knjiga> knjige = knjigaService.GetAll();

    if (knjige.Count == 0)
    {
        Console.WriteLine("Nema knjiga.");
        return;
    }

    List<Autor> autori = autorService.GetAll();
    List<AutorKnjiga> veze = autorKnjigaDAO.Load();

    AutorKnjigaService autorKnjigaService = new();

    autorKnjigaService.PoveziAutoreIKnjige(
        autori, knjige, veze);
    List<Izdavac> izdavaci = izdavacService.GetAll();

    KnjigaIzdavacService knjigaIzdavacService = new();

    knjigaIzdavacService.PoveziKnjigeIIzdavace(
        knjige,
        izdavaci);
    Console.WriteLine("\n--- LISTA KNJIGA ---");

    foreach (Knjiga knjiga in knjige)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine($"ISBN: {knjiga.ISBN}");
        Console.WriteLine($"Naziv: {knjiga.Naziv}");
        Console.WriteLine($"Zanr: {knjiga.Zanr}");
        Console.WriteLine($"Godina izdanja: {knjiga.GodinaIzdanja}");
        Console.WriteLine($"Cena: {knjiga.Cena:F2}");
        Console.WriteLine($"Broj strana: {knjiga.BrojStrana}");

        Console.WriteLine(
        $"Sifra izdavaca: {knjiga.IzdavacSifra ?? "Nije dodeljen"}");

        Console.WriteLine(
            $"Naziv izdavaca: {knjiga.Izdavac?.Naziv ?? "Nije povezan"}");

        Console.WriteLine("Autori:");

        if (knjiga.Autori.Count == 0)
        {
            Console.WriteLine("  Nema autora.");
        }
        else
        {
            foreach (Autor autor in knjiga.Autori)
            {
                Console.WriteLine(
                    $"  - {autor.Ime} {autor.Prezime}");
            }
        }

        Console.WriteLine($"Broj kupaca: {knjiga.Kupci.Count}");
        Console.WriteLine(
            $"Broj posetilaca na listi zelja: {knjiga.ListaZelja.Count}");
    }
}

void ObrisiKnjigu()
{
    Console.WriteLine("\n--- BRISANJE KNJIGE ---");

    Console.Write("ISBN knjige: ");
    string isbn = Console.ReadLine() ?? "";

    Knjiga? postojeca = knjigaService.GetAll()
        .FirstOrDefault(k => k.ISBN == isbn);

    if (postojeca == null)
    {
        Console.WriteLine("Knjiga nije pronadjena.");
        return;
    }

    Console.WriteLine($"Knjiga: {postojeca.Naziv}");

    Console.Write("Potvrdi brisanje (D/N): ");
    string potvrda = (Console.ReadLine() ?? "")
        .Trim().ToUpperInvariant();

    if (potvrda != "D")
    {
        Console.WriteLine("Brisanje je otkazano.");
        return;
    }

    bool uspesno = knjigaService.Delete(isbn);

    Console.WriteLine(uspesno
        ? "Knjiga je uspesno obrisana."
        : "Brisanje knjige nije uspelo.");
}




