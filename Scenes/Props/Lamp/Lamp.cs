using Godot;
using System;
using System.Drawing;

public partial class Lamp : Area2D
{
	[Export] Timer _flickerTimer;
	[Export] PointLight2D _pointLight;

	private const float FLICKER_TIMELEFT_UPPER = 5;
	private const float FLICKER_TIMELEFT_LOWER = 3;
	public override void _Ready()
	{
		_flickerTimer.Timeout += OnFlickerTimeOut;
	}

    private async void OnFlickerTimeOut()
    {
        float originalEnergy = _pointLight.Energy;
		_pointLight.Energy = (float)GD.RandRange(0.2f, 0.5f);

		await ToSignal(GetTree().CreateTimer(0.1f), SceneTreeTimer.SignalName.Timeout);

		_pointLight.Energy = originalEnergy;
		_flickerTimer.Start(GD.RandRange(FLICKER_TIMELEFT_LOWER, FLICKER_TIMELEFT_UPPER));
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
