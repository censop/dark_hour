using Godot;
using System;
using System.Drawing;

public partial class Lamp : StaticBody2D
{
	[Export] Timer _flickerTimer;
	[Export] PointLight2D _pointLight;
	[Export] Area2D _lightArea2D;

	private const float FLICKER_TIMELEFT_UPPER = 5;
	private const float FLICKER_TIMELEFT_LOWER = 3;
	public override void _Ready()
	{
		_flickerTimer.Timeout += OnFlickerTimeOut;
		_lightArea2D.BodyEntered += OnLightBodyEntered;
		_lightArea2D.BodyExited += OnLightBodyExited;
	}

    private void OnLightBodyExited(Node2D body)
    {
		if (body is Player)
		{
			GlobalVariables.IsPlayerInLight = false;
			GD.Print("body exitedddd");
			SignalHub.EmitOnPlayerExitedLight();
		}
    }

    private void OnLightBodyEntered(Node2D body)
    {
		if (body is Player)
		{
			GlobalVariables.IsPlayerInLight = true;
			GD.Print("body enterrred");	
		}
    }


    private async void OnFlickerTimeOut()
    {
        float originalEnergy = _pointLight.Energy;
		_pointLight.Energy = (float)GD.RandRange(0.2f, 0.5f);

		await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);

		_pointLight.Energy = originalEnergy;
		_flickerTimer.Start(GD.RandRange(FLICKER_TIMELEFT_LOWER, FLICKER_TIMELEFT_UPPER));
    }

}
