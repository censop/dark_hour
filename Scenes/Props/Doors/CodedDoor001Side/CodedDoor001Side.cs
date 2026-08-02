using Godot;
using System;

public partial class CodedDoor001Side : StaticBody2D
{
	[Export] Area2D _interactionArea;
	[Export] CollisionShape2D _colShape;
	[Export] String DoorCode = "4482";
	[Export] AnimatedSprite2D _doorLeft;
	[Export] AnimatedSprite2D _doorRight;

	[Export] string DoorID = "coded_door_001";

	private bool _isOpen = false;

	private bool _isBodyInsideArea = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_interactionArea.BodyEntered += OnBodyEntered;
		_interactionArea.BodyExited += OnBodyExited;

		SignalHub.Instance.OnInputSubmitted += OnCodeSubmitted;

	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnInputSubmitted -= OnCodeSubmitted;
    }


    private void OnCodeSubmitted(string code)
    {
        if (code == DoorCode && !_isOpen)
		{
			UnlockDoor();
			_isOpen = true;
		}
		else if (code != DoorCode)
		{
			SignalHub.EmitOnDialogueTriggered("Incorrect Code");
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
			SignalHub.EmitOnInputRequested();
		}
		
    }

    private void UnlockDoor()
	{
		_colShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		if (_doorLeft != null) _doorLeft.Play("openLeftDoor");
		if (_doorRight != null) _doorRight.Play("openRightDoor");
	}

}
