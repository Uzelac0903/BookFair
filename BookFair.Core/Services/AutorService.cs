
using BookFair.Core.DAO;
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class AutorService
{
    private readonly AutorDAO _dao;
    private readonly AutorKnjigaDAO _autorKnjigaDAO;
    public AutorService(AutorDAO dao, AutorKnjigaDAO autorKnjigaDAO)
    {
        _dao = dao;
        _autorKnjigaDAO = autorKnjigaDAO;
    }
    // 8.4 - Prikaz svih autora
    public List<Autor> GetAll()
    {
        return _dao.Load();
    }

    // 8.1 - Dodavanje autora
    public bool Add(Autor autor)
    {
        if (autor == null ||
            string.IsNullOrWhiteSpace(autor.Ime) ||
            string.IsNullOrWhiteSpace(autor.Prezime) ||
            string.IsNullOrWhiteSpace(autor.BrojLicneKarte) ||
            autor.GodineIskustva < 0)
        {
            return false;
        }

        List<Autor> autori = _dao.Load();

        if (autori.Any(a =>
            a.BrojLicneKarte == autor.BrojLicneKarte))
        {
            return false;
        }

        autori.Add(autor);
        _dao.Save(autori);

        return true;
    }

    // 8.2 - Izmena autora
    public bool Update(Autor izmenjeni)
    {
        if (izmenjeni == null ||
            string.IsNullOrWhiteSpace(izmenjeni.Ime) ||
            string.IsNullOrWhiteSpace(izmenjeni.Prezime) ||
            string.IsNullOrWhiteSpace(izmenjeni.BrojLicneKarte) ||
            izmenjeni.GodineIskustva < 0)
        {
            return false;
        }

        List<Autor> autori = _dao.Load();

        int index = autori.FindIndex(a =>
            a.BrojLicneKarte == izmenjeni.BrojLicneKarte);

        if (index == -1)
        {
            return false;
        }

        autori[index] = izmenjeni;

        _dao.Save(autori);

        return true;
    }

    // 8.3 - Brisanje autora
    public bool Delete(string brojLicneKarte)
    {
        List<Autor> autori = _dao.Load();

        Autor? autor = autori.FirstOrDefault(a =>
            a.BrojLicneKarte == brojLicneKarte);

        if (autor == null)
        {
            return false;
        }

        List<AutorKnjiga> veze = _autorKnjigaDAO.Load();

        veze.RemoveAll(v =>
            v.BrojLicneKarte == brojLicneKarte);

        autori.Remove(autor);

        _autorKnjigaDAO.Save(veze);
        _dao.Save(autori);

        return true;
    }

}
