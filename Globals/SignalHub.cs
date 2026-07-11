using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance {get; private set;}


	[Signal] public delegate void OnBatteryUsedEventHandler();
    [Signal] public delegate void OnBatteryDeadEventHandler();
	[Signal] public delegate void OnMenuPressedEventHandler();
	[Signal] public delegate void OnPlayerDeadEventHandler();
	[Signal] public delegate void OnPlayerEnterredLightEventHandler();
	[Signal] public delegate void OnPlayerExitedLightEventHandler();
	[Signal] public delegate void OnSaveGameEventHandler();
	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnBatteryUsed()
	{
		Instance.EmitSignal(SignalName.OnBatteryUsed);
	}

	public static void EmitOnPlayerDead()

	{
		Instance.EmitSignal(SignalName.OnPlayerDead);
	}


    public static void EmitOnBatteryDead()
	{
		Instance.EmitSignal(SignalName.OnBatteryDead);
	}

	public static void EmitOnMenuPressed()
	{
		Instance.EmitSignal(SignalName.OnMenuPressed);
	}

	public static void EmitOnPlayerExitedLight()
	{
		Instance.EmitSignal(SignalName.OnPlayerExitedLight);
	}
	public static void EmitOnPlayerEnterredLight()
	{
		Instance.EmitSignal(SignalName.OnPlayerEnterredLight);
	}

	public static void EmitOnSaveGame()
	{
		Instance.EmitSignal(SignalName.OnSaveGame);
	}


}
