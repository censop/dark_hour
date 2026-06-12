using Godot;
using System;

public partial class GlobalVariables : Node
{
    public static GlobalVariables Instance {private set; get;}
    public static bool IsPlayerInLight = false;

    public override void _Ready()
    {
        Instance = this;
    }

}
