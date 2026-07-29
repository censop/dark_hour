using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class LockedDoor001 : StaticBody2D
{
	[Export] Area2D _interactionArea;
	[Export] CollisionShape2D _interactionShape;
	[Export] String DoorId = "Level 1 Clearance";
	[Export] AnimatedSprite2D _doorLeft;
	[Export] AnimatedSprite2D _doorRight;

	[Export] bool _isOpen = false;

	private string _clearanceType;

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
        if (@event.IsActionPressed("interact") && _isBodyInsideArea)
		{
			if (PlayerHasKey())
			{
				_isOpen = !_isOpen;
				if (_isOpen)
				{
					UnlockDoor();
				}
				else
				{
					LockDoor();
				}
			}
			else
			{
				SignalHub.EmitOnDialogueTriggered($"Need {DoorId} Key");
			}
			
		}
		
    }

    private void LockDoor()
    {
        _interactionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, false);
		if (_doorLeft != null) _doorLeft.Play("closeLeftDoor");
		if (_doorRight != null) _doorRight.Play("closeRightDoor");

    	GD.Print($"Door {DoorId} Locked!");
    }

    private void UnlockDoor()
	{
		_interactionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		if (_doorLeft != null) _doorLeft.Play("openLeftDoor");
		if (_doorRight != null) _doorRight.Play("openRightDoor");


    	GD.Print($"Door {DoorId} Unlocked!");
	}

	private bool PlayerHasKey()
	{
		for (int i = 0; i < GlobalVariables.DoorKeysCollected.Count; i++)
		{
			if (DoorId == GlobalVariables.DoorKeysCollected[i])
			{
				GD.Print($"Key present");
				return true;
			}
		}

		return false;
	}

}