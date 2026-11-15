using System.Text.Json;
using BookFair.Core.Models;

namespace BookFair.Core.DAO;

public class KupovinaDAO
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public KupovinaDAO(string dataDirectory)
    {
        _filePath = dataDirectory
                    + Path.DirectorySeparatorChar
                    + "kupovine.json";
    }

    public List<Kupovina> Load()
    {
        if (!File.Exists(_filePath))
            return new List<Kupovina>();

        string json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<Kupovina>>(json)
               ?? new List<Kupovina>();
    }

    public void Save(List<Kupovina> kupovine)
    {
        string? directory = Path.GetDirectoryName(_filePath);

        if (directory != null)
            Directory.CreateDirectory(directory);

        string json = JsonSerializer.Serialize(kupovine, JsonOptions);

        File.WriteAllText(_filePath, json);
    }
}