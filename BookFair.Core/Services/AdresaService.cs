
using BookFair.Core.DAO;
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class AdresaService
{
    private readonly AdresaDAO _dao;

    public AdresaService(AdresaDAO dao)
    {
        _dao = dao;
    }

    // 8.4 - Prikaz svih adresa
    public List<Adresa> GetAll()
    {
        return _dao.Load();
    }

    // 8.1 - Dodavanje adrese
    public bool Add(Adresa adresa)
    {
        if (adresa == null ||
            adresa.Id <= 0 ||
            string.IsNullOrWhiteSpace(adresa.Ulica) ||
            string.IsNullOrWhiteSpace(adresa.Broj) ||
            string.IsNullOrWhiteSpace(adresa.Grad) ||
            string.IsNullOrWhiteSpace(adresa.Drzava))
        {
            return false;
        }

        List<Adresa> adrese = _dao.Load();

        if (adrese.Any(a => a.Id == adresa.Id))
        {
            return false;
        }

        adrese.Add(adresa);
        _dao.Save(adrese);

        return true;
    }

    // 8.2 - Izmena adrese
    public bool Update(Adresa izmenjena)
    {
        if (izmenjena == null ||
            izmenjena.Id <= 0 ||
            string.IsNullOrWhiteSpace(izmenjena.Ulica) ||
            string.IsNullOrWhiteSpace(izmenjena.Broj) ||
            string.IsNullOrWhiteSpace(izmenjena.Grad) ||
            string.IsNullOrWhiteSpace(izmenjena.Drzava))
        {
            return false;
        }

        List<Adresa> adrese = _dao.Load();

        int index = adrese.FindIndex(a =>
            a.Id == izmenjena.Id);

        if (index == -1)
        {
            return false;
        }

        adrese[index] = izmenjena;
        _dao.Save(adrese);

        return true;
    }

    // 8.3 - Brisanje adrese
    public bool Delete(int id)
    {
        List<Adresa> adrese = _dao.Load();

        Adresa? adresa = adrese.FirstOrDefault(a =>
            a.Id == id);

        if (adresa == null)
        {
            return false;
        }

        adrese.Remove(adresa);
        _dao.Save(adrese);

        return true;
    }
}
