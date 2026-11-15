using System.Text.Json;
using BookFair.Core.Models;

namespace BookFair.Core.DAO;

public class PosetilacDAO
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public PosetilacDAO(string dataDirectory)
    {
        _filePath = dataDirectory
                    + Path.DirectorySeparatorChar
                    + "posetioci.json";
    }

    public List<Posetilac> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Posetilac>();
        }

        string json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<Posetilac>>(json)
               ?? new List<Posetilac>();
    }

    public void Save(List<Posetilac> posetioci)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(posetioci, JsonOptions);

        File.WriteAllText(_filePath, json);
    }
}