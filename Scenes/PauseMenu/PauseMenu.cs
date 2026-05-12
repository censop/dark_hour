using Godot;
using System;

public partial class PauseMenu : Control
{

	[Export] private TextureButton _resumeButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnMenuPressed += OnMenuPressed;
		_resumeButton.Pressed += OnResumePressed;
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnMenuPressed -= OnMenuPressed;
    }



    private void OnMenuPressed()
    {
        GetTree().Paused = true;
		Show();
    }

	private void OnResumePressed()
	{
		Hide();
		GetTree().Paused = false;
	}



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
