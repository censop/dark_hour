using Godot;
using System;

public partial class SwitchLight : LightSource
{
	[Export] string SwitchLightID = "SwitchLight_001";
	[Export] Area2D _interactionShape;

	private bool _isLightOn;
	private bool _canInteract = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		_interactionShape.BodyEntered += OnInteractionEntered;
		_interactionShape.BodyExited += OnInteractionExited;

		_isLightOn = GlobalVariables.TurntOnLights.Contains(SwitchLightID);

		SetLightState(_isLightOn);
	}

    private void OnInteractionExited(Node2D body)
    {
        if (body is Player) _canInteract = false;
    }


    private void OnInteractionEntered(Node2D body)
    {
        if (body is Player) _canInteract = true;
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && _canInteract)
		{
			_isLightOn = !_isLightOn;
			GD.Print($"Light state: {_isLightOn}");

            if (_isLightOn)
                GlobalVariables.TurntOnLights.Add(SwitchLightID);
            else
                GlobalVariables.TurntOnLights.Remove(SwitchLightID);

            SetLightState(_isLightOn);
		}
    }

	private void SetLightState(bool turnOn)
    {
        _pointLight.Enabled = turnOn;

        _flickerTimer.Paused = !turnOn;

        _lightArea2D.SetDeferred(Area2D.PropertyName.Monitoring, turnOn);

        if (!turnOn && GlobalVariables.IsPlayerInLight)
        {
            GlobalVariables.IsPlayerInLight = false;
            SignalHub.EmitOnPlayerExitedLight();
        }
    }
}
