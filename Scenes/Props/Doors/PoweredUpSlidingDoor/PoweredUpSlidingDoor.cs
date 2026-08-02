using Godot;
using System;

public partial class PoweredUpSlidingDoor : StaticBody2D
{
	[Export] Area2D _interactionArea;
	[Export] CollisionShape2D _collisionShape;
	[Export] Node2D _sprite;

	[Export] string GeneratorCode = "gen_001";

	[Export] string DoorID = "powered_up_door_001";

	private bool _isBodyInsideArea = false;

	private bool _isOpen = false;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_interactionArea.BodyEntered += OnBodyEntered;
		_interactionArea.BodyExited += OnBodyExited;

		SignalHub.Instance.OnGeneratorWorking += OnGeneratorWorking;
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnGeneratorWorking -= OnGeneratorWorking;
    }


    private void OnGeneratorWorking(string genCode)
    {
        if (GeneratorCode == genCode)
		{
			_isOpen = true;
		}
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
			if (_isOpen)
			{
				UnlockDoor();
			}
			else
			{
				SignalHub.EmitOnDialogueTriggered("Needs Electricity");
			}
		} 
    }

	private void UnlockDoor()
	{
		_collisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		_sprite.Visible = false;

	}
}
