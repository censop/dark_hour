using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance {get; private set;}


	[Signal] public delegate void OnBatteryUsedEventHandler();

	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnBatterUsed()
	{
		Instance.EmitSignal(SignalName.OnBatteryUsed);
	}

}
