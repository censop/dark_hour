using Godot;
using System;

public partial class Flashlight : Node2D
{
	[Export] Timer _batteryTimer;
	[Export] Timer _flickerTimer;
	[Export] Timer _deathTimer;
	[Export] PointLight2D _pointLight;
	[Export] float _maxLightScale = 1.5f;
	[Export] float _maxBatteryTime = 60;
	private float _lightEnergy;

	private const float FLICKER_TIMELEFT_UPPER = 5;
	private const float FLICKER_TIMELEFT_LOWER = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_flickerTimer.Start(GD.RandRange(FLICKER_TIMELEFT_LOWER, FLICKER_TIMELEFT_UPPER));

		_batteryTimer.Timeout += OnBatteryDead;
		_flickerTimer.Timeout += OnFlickerTimeOut;
		_deathTimer.Timeout += OnDeathTimerOut;
		SignalHub.Instance.OnBatteryUsed += OnBatteryAcquired;
		SignalHub.Instance.OnPlayerExitedLight += OnLightExited;
		SignalHub.Instance.OnPlayerEnterredLight += OnLightEntered;

		float timeRemaining = _maxBatteryTime * GlobalVariables.BatteryTimePercentage;
		if (timeRemaining > 0)
		{
			_batteryTimer.Start(timeRemaining); 
		}
		else
		{
			OnBatteryDead();
   		}	
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnBatteryUsed -= OnBatteryAcquired;
		SignalHub.Instance.OnPlayerExitedLight -= OnLightExited;
		SignalHub.Instance.OnPlayerEnterredLight -= OnLightEntered;
    }

    private void OnBatteryAcquired()
    {
		GD.Print("resetting the light");
        _pointLight.Enabled = true;
		_pointLight.Scale = new Vector2(_maxLightScale, _maxLightScale);

		_batteryTimer.WaitTime = _maxBatteryTime;
		_batteryTimer.Start();
    }

	private void OnLightExited()
    {
		ContinueFlashlight();
        if (GlobalVariables.BatteryTimePercentage == 0)
		{
			_deathTimer.Start();
			GD.Print("Death timer started");
		}
    }

	private void OnLightEntered()
    {
        PauseFlashlight();
    }

    private async void OnBatteryDead()
    {
        SignalHub.EmitOnBatteryDead();
		_deathTimer.Start();
		GD.Print("Death timer started");
		Scale = new Vector2(0, 0);
		GlobalVariables.BatteryTimePercentage = 0;
    }
	
	private void OnDeathTimerOut()
    {
        if (GlobalVariables.IsPlayerInLight == false && GlobalVariables.BatteryTimePercentage == 0) //checking if player is in the safe area and if new battery wasnt acquired since the deathtimer started
		{
			SignalHub.EmitOnPlayerDead(); //for now since the only way the player dies is if battery is dead
			GD.Print("Death timer timeout");
		}
    }

    public override void _Process(double delta)
	{
		if (!_batteryTimer.IsStopped())
        {
            float timeLeftPercent = (float)(_batteryTimer.TimeLeft / _maxBatteryTime);
			GlobalVariables.BatteryTimePercentage = timeLeftPercent;
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

	private void PauseFlashlight() 
	{
		_batteryTimer.Paused = true;
		_flickerTimer.Paused = true;
		_deathTimer.Paused = true;
		_pointLight.Enabled = false;
	}
	private void ContinueFlashlight() 
	{
		_batteryTimer.Paused = false;
		_flickerTimer.Paused = false;
		_deathTimer.Paused = false;
		_pointLight.Enabled = true;
	}

}
