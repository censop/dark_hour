using Godot;
using System;

public partial class GameUi : Control
{
	[Export] private TextureButton _menuButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_menuButton.Pressed += OnMenuPressed;
	}

    private void OnMenuPressed()
    {
        GD.Print("Menu button pressed");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
