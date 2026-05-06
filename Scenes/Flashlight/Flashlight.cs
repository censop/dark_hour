using Godot;
using System;

public partial class Flashlight : Node2D
{
	[Export] Timer _batteryTimer;
	[Export] PointLight2D _pointLight;
	[Export] float _maxLightScale = 1.5f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_batteryTimer.Timeout += OnBatteryDead;
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
    }
	
    public override void _Process(double delta)
	{
		if (!_batteryTimer.IsStopped())
        {
            float timeLeftPercent = (float)(_batteryTimer.TimeLeft / _batteryTimer.WaitTime);
            Scale = new Vector2(timeLeftPercent, timeLeftPercent);
        }
	}
}
