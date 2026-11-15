using BookFair.Core.DAO;
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class KupovinaService
{
    private readonly KupovinaDAO _dao;

    public KupovinaService(KupovinaDAO dao)
    {
        _dao = dao;
    }

    // 8.4 - Prikaz svih kupovina
    public List<Kupovina> GetAll()
    {
        return _dao.Load();
    }

    // 8.1 - Dodavanje kupovine
    public bool Add(Kupovina kupovina)
    {
        if (!ValidnaKupovina(kupovina))
            return false;

        List<Kupovina> kupovine = _dao.Load();

        kupovine.Add(kupovina);
        _dao.Save(kupovine);

        return true;
    }

    // 8.2 - Izmena kupovine
    public bool Update(int index, Kupovina izmenjena)
    {
        if (!ValidnaKupovina(izmenjena))
            return false;

        List<Kupovina> kupovine = _dao.Load();

        if (index < 0 || index >= kupovine.Count)
            return false;

        kupovine[index] = izmenjena;
        _dao.Save(kupovine);

        return true;
    }

    // 8.3 - Brisanje kupovine
    public bool Delete(int index)
    {
        List<Kupovina> kupovine = _dao.Load();

        if (index < 0 || index >= kupovine.Count)
            return false;

        kupovine.RemoveAt(index);
        _dao.Save(kupovine);

        return true;
    }

    private static bool ValidnaKupovina(Kupovina kupovina)
    {
        return kupovina != null
               && !string.IsNullOrWhiteSpace(
                   kupovina.BrojClanskeKarte)
               && !string.IsNullOrWhiteSpace(
                   kupovina.ISBN)
               && kupovina.Ocena >= 1
               && kupovina.Ocena <= 5
               && kupovina.DatumKupovine != default
               && kupovina.DatumKupovine <= DateTime.Now;
    }
}