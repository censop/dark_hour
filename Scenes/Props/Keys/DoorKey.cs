using Godot;
using System;

public partial class DoorKey : Area2D
{

	[Export] String KeyID = "DoorKey_001";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;

	}

    private void OnBodyExited(Node2D body)
    {
        throw new NotImplementedException();
    }


    private void OnBodyEntered(Node2D body)
    {
        throw new NotImplementedException();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
