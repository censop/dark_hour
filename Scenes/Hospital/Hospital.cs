using Godot;
using System;

public partial class Hospital : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnMenuPressed += OnMenuPressed;
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("menu"))
		{
			SignalHub.EmitOnMenuPressed();
		}
    }


    private void OnMenuPressed()
    {
        GD.Print("Menu pressed");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
