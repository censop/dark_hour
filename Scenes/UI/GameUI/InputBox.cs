using Godot;
using System;

public partial class InputBox : LineEdit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnInputRequested += OnInputRequested;
		TextSubmitted += OnSubmitted;
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnInputRequested -= OnInputRequested;
    }

	private void OnSubmitted(string code)
    {
        SignalHub.EmitOnInputSubmitted(code);
		Hide();
    }



    private void OnInputRequested()
    {
        Text = "";
		Show();
		GrabFocus();
    }
}
