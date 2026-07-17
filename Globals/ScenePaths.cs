using Godot;
using System;

public partial class ScenePaths : Node
{
    public static ScenePaths Instance {private set; get;}
    public static String HospitalPath = "res://Scenes/Map/Hospital/Hospital.tscn";
    
    public override void _Ready()
    {
        Instance = this;
    }
}
