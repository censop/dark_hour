using Godot;
using System;

public partial class MoveableFurnitureBase : CharacterBody2D
{
	[Export] Area2D _interactionArea;
	public override void _Ready()
	{
		_interactionArea.BodyEntered += OnBodyEntered;
		
		_interactionArea.BodyExited += OnBodyExited;
	}

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player) player.GrabbedObject = this; 
    }


    private void OnBodyExited(Node2D body)
    {
        if (body is Player player)
		{
			if (player.GrabbedObject == this) player.GrabbedObject = null; 
		} 
    }
}
