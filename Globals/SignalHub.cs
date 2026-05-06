using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance {get; private set;}


	[Signal] public delegate void OnBatteryUsedEventHandler();
    [Signal] public delegate void OnBatteryDeadEventHandler();

	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnBatterUsed()
	{
		Instance.EmitSignal(SignalName.OnBatteryUsed);
	}

    public static void EmitOnBatteryDead()
	{
		Instance.EmitSignal(SignalName.OnBatteryDead);
	}

}
