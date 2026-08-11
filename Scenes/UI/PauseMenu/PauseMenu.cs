using Godot;
using System;

public partial class PauseMenu : Control
{

	[Export] private TextureButton _resumeButton;
	[Export] private TextureButton _saveButton;

	[Export] AudioStreamPlayer2D _clickSound;
 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnMenuPressed += OnMenuPressed;
		SignalHub.Instance.OnMenuPressed += PressSound;
		_resumeButton.Pressed += PressSound;
		_resumeButton.Pressed += OnResumePressed;
		_saveButton.Pressed += PressSound;
		_saveButton.Pressed += OnSavePressed;
	}

	public override void _ExitTree()
    {
        SignalHub.Instance.OnMenuPressed -= OnMenuPressed;
		SignalHub.Instance.OnMenuPressed -= PressSound;
    }

    private void OnSavePressed()
    {
        SignalHub.EmitOnSaveGame();
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

	private void PressSound()
	{
		_clickSound.Play();
	}
}
