
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class AutorKnjigaService
{
    public void PoveziAutoreIKnjige(
        List<Autor> autori,
        List<Knjiga> knjige,
        List<AutorKnjiga> veze)
    {
        foreach (Autor autor in autori)
        {
            autor.Knjige.Clear();
        }

        foreach (Knjiga knjiga in knjige)
        {
            knjiga.Autori.Clear();
        }

        foreach (AutorKnjiga veza in veze)
        {
            Autor? autor = autori.FirstOrDefault(
                a => a.BrojLicneKarte == veza.BrojLicneKarte);

            Knjiga? knjiga = knjige.FirstOrDefault(
                k => k.ISBN == veza.ISBN);

            if (autor == null || knjiga == null)
            {
                continue;
            }

            if (!autor.Knjige.Contains(knjiga))
            {
                autor.Knjige.Add(knjiga);
            }

            if (!knjiga.Autori.Contains(autor))
            {
                knjiga.Autori.Add(autor);
            }
        }
    }
}
