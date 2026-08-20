using Godot;
using System;

public partial class DeathUi : Control
{
	[Export] private TextureButton _respawnButton;
    [Export] private TextureButton _mainMenuButton;

	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnPlayerDead += OnPlayerDead;
		_respawnButton.Pressed += OnRespawnPressed;
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
        SignalHub.Instance.OnPlayerDead -= OnPlayerDead;
    }

    private void OnRespawnPressed()
    {
        AudioManager.Instance.PlayUIClickSound();
		Hide();
		GetTree().Paused = false;
        SignalHub.EmitOnLoadGame();
		SceneManager.Instance.ReloadScene();
    }

    private void OnPlayerDead()
    {
		GetTree().Paused = true;
        Show();
    }

}
