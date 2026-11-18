
using BookFair.Core.DAO;
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class KnjigaService
{
    private readonly KnjigaDAO _dao;
    private readonly AutorKnjigaDAO _autorKnjigaDAO;

    public KnjigaService(
        KnjigaDAO dao,
        AutorKnjigaDAO autorKnjigaDAO)
    {
        _dao = dao;
        _autorKnjigaDAO = autorKnjigaDAO;
    }
    
    
    // 8.1 - Dodavanje knjige
    public bool Add(Knjiga knjiga)
    {
        List<Knjiga> knjige = _dao.Load();

        if (string.IsNullOrWhiteSpace(knjiga.ISBN) ||
            string.IsNullOrWhiteSpace(knjiga.Naziv) ||
            string.IsNullOrWhiteSpace(knjiga.Zanr) ||
            knjiga.GodinaIzdanja <= 0 ||
            knjiga.Cena < 0 ||
            knjiga.BrojStrana <= 0)
        {
            return false;
        }

        bool postoji = knjige.Any(k => k.ISBN == knjiga.ISBN);

        if (postoji)
        {
            return false;
        }

        knjige.Add(knjiga);
        _dao.Save(knjige);

        return true;
    }

    // 8.2 - Izmena knjige
    public bool Update(Knjiga izmenjena)
    {
        List<Knjiga> knjige = _dao.Load();

        Knjiga? postojeca = knjige.FirstOrDefault(k =>
            k.ISBN == izmenjena.ISBN);

        if (postojeca == null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(izmenjena.Naziv) ||
            string.IsNullOrWhiteSpace(izmenjena.Zanr) ||
            izmenjena.GodinaIzdanja <= 0 ||
            izmenjena.Cena < 0 ||
            izmenjena.BrojStrana <= 0)
        {
            return false;
        }

        postojeca.Naziv = izmenjena.Naziv;
        postojeca.Zanr = izmenjena.Zanr;
        postojeca.GodinaIzdanja = izmenjena.GodinaIzdanja;
        postojeca.Cena = izmenjena.Cena;
        postojeca.BrojStrana = izmenjena.BrojStrana;
        postojeca.IzdavacSifra = izmenjena.IzdavacSifra;
        postojeca.Izdavac = null;

        _dao.Save(knjige);

        return true;
    }
    
    // 8.4 - Prikaz svih knjiga
    public List<Knjiga> GetAll()
    {
        return _dao.Load();
    }

    // 8.3 - Brisanje knjige
    public bool Delete(string isbn)
    {
        List<Knjiga> knjige = _dao.Load();

        Knjiga? knjiga = knjige.FirstOrDefault(k =>
            k.ISBN == isbn);

        if (knjiga == null)
        {
            return false;
        }

        List<AutorKnjiga> veze = _autorKnjigaDAO.Load();

        veze.RemoveAll(v => v.ISBN == isbn);

        knjige.Remove(knjiga);

        _autorKnjigaDAO.Save(veze);
        _dao.Save(knjige);

        return true;
    }
}
