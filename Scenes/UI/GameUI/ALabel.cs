using Godot;
using System;

public partial class ALabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnBatteryCollected += OnRemainingBatteryChange;
		SignalHub.Instance.OnBatteryUsed += OnRemainingBatteryChange;
		SignalHub.Instance.OnLoadGame += OnRemainingBatteryChange;
		Text = $"x {GlobalVariables.NotConsumedBatteries}";
	}

	public override void _ExitTree()
	{
		SignalHub.Instance.OnBatteryCollected -= OnRemainingBatteryChange;
		SignalHub.Instance.OnBatteryUsed -= OnRemainingBatteryChange;
		SignalHub.Instance.OnLoadGame -= OnRemainingBatteryChange;
	}

    private void OnRemainingBatteryChange()
    {
        Text = $"x {GlobalVariables.NotConsumedBatteries}";
    }
}
