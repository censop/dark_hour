using Godot;
using System;

public partial class PauseMenu : Control
{

	[Export] private TextureButton _resumeButton;
	[Export] private TextureButton _saveButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnMenuPressed += OnMenuPressed;
		_resumeButton.Pressed += OnResumePressed;
		_saveButton.Pressed += OnSavePressed;
	}

    private void OnSavePressed()
    {
        SignalHub.EmitOnSaveGame();
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
}
