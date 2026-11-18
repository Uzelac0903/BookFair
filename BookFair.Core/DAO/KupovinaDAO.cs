using BookFair.Core.Models;
using BookFair.Core.Serialization;

namespace BookFair.Core.DAO;

public class KupovinaDAO
{
    private readonly string _filePath;
    private readonly Serializer<Kupovina> _serializer = new();

    public KupovinaDAO(string dataDirectory)
    {
        Directory.CreateDirectory(dataDirectory);
        _filePath = Path.Combine(dataDirectory, "kupovine.csv");
    }

    public List<Kupovina> Load()
    {
        if (!File.Exists(_filePath))
            return new List<Kupovina>();

        string[] lines = File.ReadAllLines(_filePath);

        return _serializer.FromCSV(
            lines.Where(line => !string.IsNullOrWhiteSpace(line))
        );
    }

    public void Save(List<Kupovina> kupovine)
    {
        string csv = _serializer.ToCSV(kupovine);
        File.WriteAllText(_filePath, csv);
    }
}