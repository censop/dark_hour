using Godot;
using System;

public partial class SlidingDoor : StaticBody2D
{
	[Export] Area2D _interactionArea;
	[Export] CollisionShape2D _collisionShape;
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
        if (@event.IsActionPressed("interact") && _isBodyInsideArea)
		{
			UnlockDoor();
		}
    }

	private void UnlockDoor()
	{
		_collisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		_sprite.Visible = false;

	}
}