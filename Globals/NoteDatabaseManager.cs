using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class NoteDatabaseManager : Node
{
    public static NoteDatabaseManager Instance {private set; get;}
    public Dictionary<string, NoteEntry> Notes { get; private set; } = new Dictionary<string, NoteEntry>();

    public override void _Ready()
    {
        Instance = this;
        LoadDatabase();
    }

    private void LoadDatabase()
    {
        using var file = FileAccess.Open("res://JsonFiles/NoteDatabase.json", FileAccess.ModeFlags.Read);
        
        if (file == null)
        {
            GD.PrintErr("Failed to load lore database!");
            return;
        }

        // 2. Read the text
        string jsonText = file.GetAsText();

        // 3. Convert the JSON text into our C# Dictionary
        Notes = JsonSerializer.Deserialize<Dictionary<string, NoteEntry>>(jsonText);
        
        GD.Print($"Successfully loaded {Notes.Count} lore notes.");
    }

}
