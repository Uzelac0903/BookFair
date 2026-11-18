
using BookFair.Core.Models;
using BookFair.Core.Serialization;

namespace BookFair.Core.DAO;

public class KnjigaDAO
{
    private readonly string _filePath;

    private readonly Serializer<Knjiga> _serializer = new();

    public KnjigaDAO(string dataDirectory)
    {
        _filePath = Path.Combine(dataDirectory, "knjige.csv");
    }

    public List<Knjiga> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Knjiga>();
        }

        string[] lines = File.ReadAllLines(_filePath);

        return _serializer.FromCSV(
            lines.Where(line => !string.IsNullOrWhiteSpace(line))
        );
    }

    public void Save(List<Knjiga> knjige)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }

        string csv = _serializer.ToCSV(knjige);

        File.WriteAllText(_filePath, csv);
    }
}
