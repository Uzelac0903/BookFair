
using BookFair.Core.Models;

namespace BookFair.Core.Services;

public class KnjigaIzdavacService
{
    public void PoveziKnjigeIIzdavace(
        List<Knjiga> knjige,
        List<Izdavac> izdavaci)
    {
        foreach (Izdavac izdavac in izdavaci)
        {
            izdavac.Knjige.Clear();
        }

        foreach (Knjiga knjiga in knjige)
        {
            knjiga.Izdavac = null;

            if (string.IsNullOrWhiteSpace(knjiga.IzdavacSifra))
            {
                continue;
            }

            Izdavac? izdavac = izdavaci.FirstOrDefault(
                i => i.Sifra == knjiga.IzdavacSifra);

            if (izdavac == null)
            {
                continue;
            }

            knjiga.Izdavac = izdavac;

            if (!izdavac.Knjige.Contains(knjiga.ISBN))
            {
                izdavac.Knjige.Add(knjiga.ISBN);
            }
        }
    }
}
