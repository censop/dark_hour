using Godot;
using System;
using System.Text.Json.Serialization;

public class NoteEntry
{
    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Body")]
    public string Body { get; set; }
}
