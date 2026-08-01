using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance {get; private set;}

	//battery related
	[Signal] public delegate void OnBatteryUsedEventHandler();
	[Signal] public delegate void OnBatteryCollectedEventHandler();
    [Signal] public delegate void OnBatteryDeadEventHandler();

	//UI related
	[Signal] public delegate void OnMenuPressedEventHandler();
	[Signal] public delegate void OnDialogueTriggeredEventHandler(string dialogue);
	[Signal] public delegate void OnInputRequestedEventHandler();
	[Signal] public delegate void OnInputSubmittedEventHandler(string code);

	//player related
	[Signal] public delegate void OnPlayerDeadEventHandler();
	[Signal] public delegate void OnPlayerEnterredLightEventHandler();
	[Signal] public delegate void OnPlayerExitedLightEventHandler();

	//game related
	[Signal] public delegate void OnSaveGameEventHandler();
	[Signal] public delegate void OnLoadGameEventHandler();


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

	public static void EmitOnLoadGame()
	{
		Instance.EmitSignal(SignalName.OnLoadGame);
	}

	public static void EmitOnBatteryCollected()
	{
		Instance.EmitSignal(SignalName.OnBatteryCollected);
	}

	public static void EmitOnDialogueTriggered(string dialogue)
	{
		Instance.EmitSignal(SignalName.OnDialogueTriggered, dialogue);
	}

	public static void EmitOnInputSubmitted(string code)
	{
		Instance.EmitSignal(SignalName.OnInputSubmitted, code);
	}

	public static void EmitOnInputRequested()
	{
		Instance.EmitSignal(SignalName.OnInputRequested);
	}



}
