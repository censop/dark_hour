using Godot;
using System;

public partial class Generator : StaticBody2D
{
	[Export] ColorRect _disabledColor;
	[Export] ColorRect _enabledColor;
	[Export] Area2D _interactableArea;
	[Export] string GeneratorCode = "gen_001";
	bool _isBodyInside = false;
	bool _isWorking = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_interactableArea.BodyEntered += OnBodyEntered;
		_interactableArea.BodyExited += OnBodyExited;

		if (GlobalVariables.GeneratorsWorking.Contains(GeneratorCode))
		{
			_isWorking = true;
			SpriteEnabled();
			return;
		}

		SpriteDisabled();

	}

    private void OnBodyEntered(Node2D body)
    {
        _isBodyInside = true;
    }

    private void OnBodyExited(Node2D body)
    {
        _isBodyInside = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && _isBodyInside && !_isWorking)
		{
			SignalHub.EmitOnGeneratorWorking(GeneratorCode);
			SpriteEnabled();

		}
    }

	private void SpriteEnabled()
	{
		_enabledColor.Show();
		_disabledColor.Hide();
	}

	private void SpriteDisabled()
	{
		_enabledColor.Hide();
		_disabledColor.Show();
	}

}
