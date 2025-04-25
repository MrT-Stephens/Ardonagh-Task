using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Ardonagh.Interfaces;
using Avalonia;

namespace Ardonagh.Services;

public class SaveLoadService<T> : ISaveLoadService<T>
{
    private readonly string _FilePath;

    public SaveLoadService(string fileName = "data.json")
    {
        var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Application.Current?.Name ?? "Ardonagh");
        
        _FilePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
    }

    public void Save(List<T> items)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(items, options);
        File.WriteAllText(_FilePath, json);
    }

    public List<T> Load()
    {
        if (!File.Exists(_FilePath))
            return [];

        var json = File.ReadAllText(_FilePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}