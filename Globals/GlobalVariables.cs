using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalVariables : Node
{
    public static GlobalVariables Instance {private set; get;}
    public static bool IsPlayerInLight = false;
    public static float BatteryTimePercentage = 1;
    public static List<String> ConsumedBatteries = new List<string>();
    public static List<String> DoorKeysCollected = new List<string>();
    public static List<String> TurntOnLights = new List<string>();

    public override void _Ready()
    {
        Instance = this;
    }

}
