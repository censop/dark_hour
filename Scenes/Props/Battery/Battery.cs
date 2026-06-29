using Godot;
using System;
using System.Collections;

public partial class Battery : Area2D
{

	private bool _isBodyInside = false;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

    private void OnBodyExited(Node2D body)
    {
        if (body is Player) _isBodyInside = false;
    }


    private void OnBodyEntered(Node2D body)
    {
       if (body is Player) _isBodyInside = true;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && _isBodyInside)
		{
			OnBatteryUsed();
		}
    }

	private void OnBatteryUsed()
	{
		//FOR NOW
		GD.Print("Interacted");
		SignalHub.EmitOnBatteryUsed();
		QueueFree();
	}

}
