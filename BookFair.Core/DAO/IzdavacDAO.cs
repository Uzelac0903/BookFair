using System.Text.Json;
using BookFair.Core.Models;

namespace BookFair.Core.DAO;

public class IzdavacDAO
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public IzdavacDAO(string dataDirectory)
    {
        _filePath = dataDirectory
                    + Path.DirectorySeparatorChar
                    + "izdavaci.json";
    }

    public List<Izdavac> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Izdavac>();
        }

        string json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<Izdavac>>(json)
               ?? new List<Izdavac>();
    }

    public void Save(List<Izdavac> izdavaci)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(izdavaci, JsonOptions);

        File.WriteAllText(_filePath, json);
    }
}