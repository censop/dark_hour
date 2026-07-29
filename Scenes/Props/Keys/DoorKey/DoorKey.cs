using Godot;
using System;

public partial class DoorKey : Area2D
{

	[Export] String KeyID = "Level 1 Clearance";

	private bool _isBodyInside = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GlobalVariables.DoorKeysCollected.Contains(KeyID))
		{
			QueueFree();
			return;
		}

		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;

	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && _isBodyInside)
		{
			OnCollectDoorKey();
            GD.Print($"DoorKey collected: {KeyID}");
		}
    }

    private void OnCollectDoorKey()
    {
        GlobalVariables.DoorKeysCollected.Add(KeyID);
        SignalHub.EmitOnDialogueTriggered($"{KeyID} Key Collected");
        QueueFree();
    }


    private void OnBodyExited(Node2D body)
    {
        if (body is Player) _isBodyInside = false;
    }


    private void OnBodyEntered(Node2D body)
    {
       if (body is Player) _isBodyInside = true;
    }
}
