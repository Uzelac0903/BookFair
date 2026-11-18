using BookFair.Core.Models;
using BookFair.Core.Serialization;

namespace BookFair.Core.DAO;

public class PosetilacDAO
{
    private readonly string _filePath;
    private readonly Serializer<Posetilac> _serializer = new();

    public PosetilacDAO(string dataDirectory)
    {
        Directory.CreateDirectory(dataDirectory);
        _filePath = Path.Combine(dataDirectory, "posetioci.csv");
    }

    public List<Posetilac> Load()
    {
        if (!File.Exists(_filePath))
            return new List<Posetilac>();

        string[] lines = File.ReadAllLines(_filePath);

        return _serializer.FromCSV(
            lines.Where(line => !string.IsNullOrWhiteSpace(line))
        );
    }

    public void Save(List<Posetilac> posetioci)
    {
        string csv = _serializer.ToCSV(posetioci);
        File.WriteAllText(_filePath, csv);
    }
}