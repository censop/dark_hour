using Godot;
using System;

public partial class LockedDoor : StaticBody2D
{
	[Export] Area2D _interactionArea;
	[Export] CollisionShape2D _interactionShape;
	[Export] String DoorId = "Door_001";
	[Export] Node2D _sprite;

	private bool _isBodyInsideArea = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_interactionArea.BodyEntered += OnBodyEntered;
		_interactionArea.BodyExited += OnBodyExited;
	}

    private void OnBodyExited(Node2D body)
    {
        if (body is Player) _isBodyInsideArea = false;
    }


    private void OnBodyEntered(Node2D body)
    {
        if (body is Player) _isBodyInsideArea = true;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && _isBodyInsideArea && PlayerHasKey())
		{
			UnlockDoor();
		}
    }

	private void UnlockDoor()
	{
		_interactionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		_sprite.Visible = false;

    	GD.Print($"Door {DoorId} Unlocked!");
	}

	private bool PlayerHasKey()
	{
		for (int i = 0; i < GlobalVariables.DoorKeysCollected.Count; i++)
		{
			if (DoorId[^3..] == GlobalVariables.DoorKeysCollected[i][^3..])
			{
				GD.Print($"Key present");
				return true;
			}
		}

		return false;
	}

}
