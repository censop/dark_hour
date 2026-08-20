using Godot;
using System;

public partial class ScenePaths : Node
{
    public static ScenePaths Instance {private set; get;}
    public static String HospitalPath = "res://Scenes/Map/Hospital/Hospital.tscn";
    public static String MainMenuPath = "res://Scenes/UI/StartScreen/StartScreen.tscn";
    
    public override void _Ready()
    {
        Instance = this;
    }
}
