
using BookFair.Core.Models;
using BookFair.Core.Serialization;

namespace BookFair.Core.DAO;

public class AutorDAO
{
    private readonly string _filePath;

    private readonly Serializer<Autor> _serializer = new();

    public AutorDAO(string dataDirectory)
    {
        _filePath = Path.Combine(dataDirectory, "autori.csv");
    }

    public List<Autor> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Autor>();
        }

        string[] lines = File.ReadAllLines(_filePath);

        return _serializer.FromCSV(
            lines.Where(line => !string.IsNullOrWhiteSpace(line))
        );
    }

    public void Save(List<Autor> autori)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }

        string csv = _serializer.ToCSV(autori);

        File.WriteAllText(_filePath, csv);
    }
}
