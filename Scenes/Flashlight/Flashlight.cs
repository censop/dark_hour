using Godot;
using System;

public partial class Flashlight : Node2D
{
	[Export] Timer _batteryTimer;
	[Export] Timer _flickerTimer;
	[Export] PointLight2D _pointLight;
	[Export] float _maxLightScale = 1.5f;

	private const float FLICKER_TIMELEFT_UPPER = 5;
	private const float FLICKER_TIMELEFT_LOWER = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_flickerTimer.Start(GD.RandRange(FLICKER_TIMELEFT_LOWER, FLICKER_TIMELEFT_UPPER));

		_batteryTimer.Timeout += OnBatteryDead;
		_flickerTimer.Timeout += OnFlickerTimeOut;
		SignalHub.Instance.OnBatteryUsed += OnBatteryAcquired;
		_batteryTimer.Start();
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnBatteryUsed -= OnBatteryAcquired;
    }

    private void OnBatteryAcquired()
    {
		GD.Print("resetting the light");
        _pointLight.Enabled = true;
		_pointLight.Scale = new Vector2(_maxLightScale, _maxLightScale);
		_batteryTimer.Start();
    }

    private void OnBatteryDead()
    {
        SignalHub.EmitOnBatteryDead();
		//PLACEHOLDER FOR NOW
		GD.Print("battery died");

		Scale = new Vector2(0, 0);
		QueueFree();
    }
	
    public override void _Process(double delta)
	{
		if (!_batteryTimer.IsStopped())
        {
            float timeLeftPercent = (float)(_batteryTimer.TimeLeft / _batteryTimer.WaitTime);
            Scale = new Vector2(timeLeftPercent, timeLeftPercent);
        }
	}

	private async void OnFlickerTimeOut()
	{
		if (_batteryTimer.IsStopped()) return;

		float originalEnergy = _pointLight.Energy;
		_pointLight.Energy = (float)GD.RandRange(0.5f, 0.7f);

		await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);

		_pointLight.Energy = originalEnergy;
		_flickerTimer.Start(GD.RandRange(FLICKER_TIMELEFT_LOWER, FLICKER_TIMELEFT_UPPER));

	}
}
