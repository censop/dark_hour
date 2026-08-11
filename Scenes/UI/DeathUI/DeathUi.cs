using Godot;
using System;

public partial class DeathUi : Control
{
	[Export] private TextureButton _respawnButton;

    [Export] AudioStreamPlayer2D _clickSound;
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnPlayerDead += OnPlayerDead;
        _respawnButton.Pressed += PressSound;
		_respawnButton.Pressed += OnRespawnPressed;
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnPlayerDead -= OnPlayerDead;
    }


    private void OnRespawnPressed()
    {
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

    private void PressSound()
    {
        _clickSound.Play();
    }

}
