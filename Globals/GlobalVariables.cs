using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalVariables : Node
{
    public static GlobalVariables Instance {private set; get;}
    public static bool IsPlayerInLight = false;
    public static float BatteryTimePercentage = 1;
    public static List<String> CollectedBatteries = new List<string>();
    public static int NotConsumedBatteries = 0;
    public static List<String> DoorKeysCollected = new List<string>();
    public static List<String> GeneratorsWorking = new List<string>();
    public static List<String> DoorsUnlocked = new List<string>();
    public static List<String> TurntOnLights = new List<string>();
    public static Vector2 PlayerPos = Vector2.Zero;

    public override void _Ready()
    {
        Instance = this;
    }

    public void ResetData()
    {
        IsPlayerInLight = false;
        BatteryTimePercentage = 1;
        CollectedBatteries = new List<string>();
        NotConsumedBatteries = 0;
        DoorKeysCollected = new List<string>();
        GeneratorsWorking = new List<string>();
        DoorsUnlocked = new List<string>();
        TurntOnLights = new List<string>();
        PlayerPos = Vector2.Zero;
    }

}
