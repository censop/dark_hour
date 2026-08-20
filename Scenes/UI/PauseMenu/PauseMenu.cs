using Godot;
using System;

public partial class PauseMenu : Control
{

	[Export] private TextureButton _resumeButton;


    [Export] private TextureButton _mainMenuButton;
 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnMenuPressed += OnMenuPressed;
		_resumeButton.Pressed += OnResumePressed;
		_mainMenuButton.Pressed += OnMainMenuPressed;
	}

    private void OnMainMenuPressed()
    {
        AudioManager.Instance.PlayUIClickSound();
		Hide();
		GetTree().Paused = false;
        SceneManager.Instance.ChangeScene(ScenePaths.MainMenuPath);
    }

    public override void _ExitTree()
    {
        SignalHub.Instance.OnMenuPressed -= OnMenuPressed;
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
