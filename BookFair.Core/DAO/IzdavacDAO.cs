using BookFair.Core.Models;
using BookFair.Core.Serialization;

namespace BookFair.Core.DAO;

public class IzdavacDAO
{
    private readonly string _filePath;
    private readonly Serializer<Izdavac> _serializer = new();

    public IzdavacDAO(string dataDirectory)
    {
        Directory.CreateDirectory(dataDirectory);
        _filePath = Path.Combine(dataDirectory, "izdavaci.csv");
    }

    public List<Izdavac> Load()
    {
        if (!File.Exists(_filePath))
            return new List<Izdavac>();

        string[] lines = File.ReadAllLines(_filePath);

        return _serializer.FromCSV(
            lines.Where(line => !string.IsNullOrWhiteSpace(line))
        );
    }

    public void Save(List<Izdavac> izdavaci)
    {
        string csv = _serializer.ToCSV(izdavaci);
        File.WriteAllText(_filePath, csv);
    }
}