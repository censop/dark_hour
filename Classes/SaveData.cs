using Godot;
using System;
using System.Collections.Generic;

public class SaveData
{
    public float PlayerPosX {set; get;}
    public float PlayerPosY {set; get;}
    public float BatteryPercentage {set; get;}
    public List<String> AllBatteries {get; set;} = new List<string>();
    public List<String> CurrentBatteries {get; set;} = new List<string>();
    public List<String> DoorKeyInventory {get; set;} = new List<string>();
    public List<String> LightSwitchesOn {get; set;} = new List<string>();
}