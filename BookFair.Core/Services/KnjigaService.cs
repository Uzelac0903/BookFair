
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
