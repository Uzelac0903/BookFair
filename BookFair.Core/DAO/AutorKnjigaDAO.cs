
using BookFair.Core.Models;
using BookFair.Core.Serialization;

namespace BookFair.Core.DAO;

public class AutorKnjigaDAO
{
    private readonly string _filePath;

    private readonly Serializer<AutorKnjiga> _serializer = new();

    public AutorKnjigaDAO(string dataDirectory)
    {
        _filePath = Path.Combine(dataDirectory, "autori_knjige.csv");
    }

    public List<AutorKnjiga> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<AutorKnjiga>();
        }

        string[] lines = File.ReadAllLines(_filePath);

        return _serializer.FromCSV(
            lines.Where(line => !string.IsNullOrWhiteSpace(line))
        );
    }

    public void Save(List<AutorKnjiga> veze)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }

        string csv = _serializer.ToCSV(veze);

        File.WriteAllText(_filePath, csv);
    }
}
