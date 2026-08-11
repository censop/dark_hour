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

	public override void _ExitTree()
    {
        SignalHub.Instance.OnMenuPressed -= OnMenuPressed;
    }

    private void OnSavePressed()
    {
		AudioManager.Instance.PlayUIClickSound();
        SignalHub.EmitOnSaveGame();
    }

    private void OnMenuPressed()
    {
		AudioManager.Instance.PlayUIClickSound();
        GetTree().Paused = true;
		Show();
    }

	private void OnResumePressed()
	{
		AudioManager.Instance.PlayUIClickSound();
		Hide();
		GetTree().Paused = false;
	}
}
