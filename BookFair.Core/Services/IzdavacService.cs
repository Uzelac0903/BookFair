using BookFair.Core.DAO;
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class IzdavacService
{
    private readonly IzdavacDAO _dao;

    public IzdavacService(IzdavacDAO dao)
    {
        _dao = dao;
    }

    // 8.4 - Prikaz svih izdavaca
    public List<Izdavac> GetAll()
    {
        return _dao.Load();
    }

    // 8.1 - Dodavanje izdavaca
    public bool Add(Izdavac izdavac)
    {
        if (string.IsNullOrWhiteSpace(izdavac.Sifra) ||
            string.IsNullOrWhiteSpace(izdavac.Naziv))
        {
            return false;
        }

        List<Izdavac> izdavaci = _dao.Load();

        if (izdavaci.Any(i => i.Sifra == izdavac.Sifra))
        {
            return false;
        }

        izdavaci.Add(izdavac);
        _dao.Save(izdavaci);

        return true;
    }

    // 8.2 - Izmena izdavaca
    public bool Update(Izdavac izmenjeni)
    {
        List<Izdavac> izdavaci = _dao.Load();

        int index = izdavaci.FindIndex(i =>
            i.Sifra == izmenjeni.Sifra);

        if (index == -1 ||
            string.IsNullOrWhiteSpace(izmenjeni.Naziv))
        {
            return false;
        }

        izdavaci[index] = izmenjeni;
        _dao.Save(izdavaci);

        return true;
    }

    // 8.3 - Brisanje izdavaca
    public bool Delete(string sifra)
    {
        List<Izdavac> izdavaci = _dao.Load();

        Izdavac? izdavac = izdavaci.FirstOrDefault(i =>
            i.Sifra == sifra);

        if (izdavac == null)
        {
            return false;
        }

        izdavaci.Remove(izdavac);
        _dao.Save(izdavaci);

        return true;
    }
}