using Godot;
using System;

public partial class Label : Godot.Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnBatteryCollected += OnRemainingBatteryChange;
		SignalHub.Instance.OnBatteryUsed += OnRemainingBatteryChange;
	}

    private void OnRemainingBatteryChange()
    {
        Text = $"x {GlobalVariables.NotConsumedBatteries}";
    }
}
