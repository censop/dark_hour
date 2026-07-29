using Godot;
using System;

public partial class DialogueLabel : Label
{
	[Export] Timer _labelTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnDialogueTriggered += OnDialogueTriggered;
		_labelTimer.Timeout += Hide;
	}

    public override void _ExitTree()
    {
		SignalHub.Instance.OnDialogueTriggered -= OnDialogueTriggered;
    }

    private void OnDialogueTriggered(string dialogue)
    {
		Show();
        Text = dialogue;
		_labelTimer.Start();
    }

}
