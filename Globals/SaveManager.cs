using System;
using System.Collections.Generic;
using System.Text.Json;
using Godot;

public partial class SaveManager : Node
{
    public static SaveManager Instance {private set; get;}
    private String _savePath = "user://savegame.json";

    public override void _EnterTree()
    {
        Instance = this;
    }

    public void SaveGame(Vector2 playerPos, float batteryPercent, List<String> collectedBatteries, List<String> notUsedBatteries, List<String> collectedDoorKeys, List<String> lightsOn)
    {
        SaveData data = new SaveData
        {
            PlayerPosX = playerPos.X,
            PlayerPosY = playerPos.Y,
            BatteryPercentage = batteryPercent,
            AllBatteries = collectedBatteries,
            CurrentBatteries = notUsedBatteries,
            DoorKeyInventory = collectedDoorKeys,
            LightSwitchesOn = lightsOn,
        };

        string jsonString = JsonSerializer.Serialize(data);
        try
        {
            using var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Write); //using keyword automatically closes the file after its done with it
            file.StoreString(jsonString);
            GD.Print($"Save info:{playerPos}, {batteryPercent}, {notUsedBatteries}, Game Saved to: " + ProjectSettings.GlobalizePath(_savePath));
        }catch
        {
            GD.Print("Game couldn't be saved");
        }
    }

    public SaveData LoadGame()
    {
        if (!FileAccess.FileExists(_savePath))
        {
            GD.Print("Save file doesn't exist");
            return null;
        }

        try
        {
            using var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Read);
            string jsonString = file.GetAsText();
            SaveData data = JsonSerializer.Deserialize<SaveData>(jsonString);
            GD.Print("Game loaded");
            return data;
        } catch
        {
            GD.Print("Game couldn't be loaded");
            return null;
        }

    }
}
