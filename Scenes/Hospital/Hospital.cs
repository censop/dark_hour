using Godot;
using System;

public partial class Hospital : Node2D
{

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("menu"))
		{
			SignalHub.EmitOnMenuPressed();
		}
    }
}
