using BookFair.Core.DAO;
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class PosetilacService
{
    private readonly PosetilacDAO _dao;

    public PosetilacService(PosetilacDAO dao)
    {
        _dao = dao;
    }

    // 8.4 - Prikaz svih posetilaca
    public List<Posetilac> GetAll()
    {
        return _dao.Load();
    }

    // 8.1 - Dodavanje posetioca
    public bool Add(Posetilac posetilac)
    {
        List<Posetilac> posetioci = _dao.Load();

        if (string.IsNullOrWhiteSpace(posetilac.BrojClanskeKarte))
        {
            return false;
        }

        if (posetioci.Any(p =>
                p.BrojClanskeKarte == posetilac.BrojClanskeKarte))
        {
            return false;
        }

        posetioci.Add(posetilac);
        _dao.Save(posetioci);

        return true;
    }

    // 8.2 - Izmena posetioca
    public bool Update(Posetilac izmenjeni)
    {
        List<Posetilac> posetioci = _dao.Load();

        int index = posetioci.FindIndex(p =>
            p.BrojClanskeKarte == izmenjeni.BrojClanskeKarte);

        if (index == -1)
        {
            return false;
        }

        posetioci[index] = izmenjeni;
        _dao.Save(posetioci);

        return true;
    }

    // 8.3 - Brisanje posetioca
    public bool Delete(string brojClanskeKarte)
    {
        List<Posetilac> posetioci = _dao.Load();

        Posetilac? posetilac = posetioci.FirstOrDefault(p =>
            p.BrojClanskeKarte == brojClanskeKarte);

        if (posetilac == null)
        {
            return false;
        }

        posetioci.Remove(posetilac);
        _dao.Save(posetioci);

        return true;
    }
}