
using BookFair.Core.Models;
using BookFair.Core.Serialization;

namespace BookFair.Core.DAO;

public class AdresaDAO
{
    private readonly string _filePath;
    private readonly Serializer<Adresa> _serializer = new();

    public AdresaDAO(string dataDirectory)
    {
        _filePath = Path.Combine(
            dataDirectory,
            "adrese.csv");
    }

    public List<Adresa> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Adresa>();
        }

        string[] lines = File.ReadAllLines(_filePath);

        return _serializer.FromCSV(
            lines.Where(line =>
                !string.IsNullOrWhiteSpace(line)));
    }

    public void Save(List<Adresa> adrese)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }

        string csv = _serializer.ToCSV(adrese);

        File.WriteAllText(_filePath, csv);
    }
}
