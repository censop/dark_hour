using Godot;
using System;

public partial class LockedDoor : StaticBody2D
{
	[Export] Area2D _interactionArea;
	[Export] String DoorId = "Door_001";

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
		GD.Print("Cant intract with door");
    }


    private void OnBodyEntered(Node2D body)
    {
        if (body is Player) _isBodyInsideArea = true;
		GD.Print("Can intract with door");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
