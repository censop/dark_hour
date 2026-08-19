using Godot;
using System;

public partial class Note : Area2D
{
	[Export] public string NoteId = "note_001";

	private bool _isBodyInside = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

    private void OnBodyExited(Node2D body)
    {
        _isBodyInside = false;
    }


    private void OnBodyEntered(Node2D body)
    {
        _isBodyInside = true;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && _isBodyInside)
		{
			SignalHub.EmitOnNoteInteracted(NoteId);
            GD.Print($"Interacted with note {NoteId}");
		}
    }

}
