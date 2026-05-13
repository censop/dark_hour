using Godot;
using System;

public partial class DeathUi : Control
{
	[Export] private TextureButton _respawnButton;
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnPlayerDead += OnPlayerDead;
		_respawnButton.Pressed += OnRespawnPressed;
	}

    private void OnRespawnPressed()
    {
		Hide();
        GetTree().Paused = false;
    }


    public override void _ExitTree()
    {
        SignalHub.Instance.OnPlayerDead -= OnPlayerDead;
    }


    private void OnPlayerDead()
    {
		GetTree().Paused = true;
        Show();
    }

    public override void _Process(double delta)
	{
	}
}
