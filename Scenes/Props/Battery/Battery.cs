using Godot;
using System;
using System.Collections;

public partial class Battery : Area2D
{

	[Export] String UniqueID = "Battery_001"; //need to change this for each battery

	private bool _isBodyInside = false;
	public override void _Ready()
	{
		GD.Print("Battery ID: " + UniqueID);

		if (GlobalVariables.CollectedBatteries.Contains(UniqueID))
		{
			QueueFree();
			return;
		}

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
			OnBatteryCollected();
		}
		
    }

	private void OnBatteryCollected()
	{
		//FOR NOW
		GD.Print("Interacted");
		GlobalVariables.CollectedBatteries.Add(UniqueID);
		GlobalVariables.NotConsumedBatteries++;
		SignalHub.EmitOnBatteryCollected();
		GD.Print("NotConsumedBatteries: " + GlobalVariables.NotConsumedBatteries);
		QueueFree();
	}

}
