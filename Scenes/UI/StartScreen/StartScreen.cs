using Godot;
using System;

public partial class StartScreen : Control
{
	[Export] TextureButton _loadButton;
	[Export] TextureButton _restartButton;

	private string _hospitalPath = "";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_loadButton.Pressed += OnLoadPressed;
		_restartButton.Pressed += OnRestartPressed;
        SignalHub.Instance.OnLoadGame += OnLoadPressed;
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnLoadGame -= OnLoadPressed;
    }

    private void OnLoadPressed()
    {
        SaveData data = SaveManager.Instance.LoadGame();

        if (data != null)
        {
            GlobalVariables.BatteryTimePercentage = data.BatteryPercentage;
            GlobalVariables.CollectedBatteries = data.AllBatteries;
            GlobalVariables.DoorKeysCollected = data.DoorKeyInventory;
            GlobalVariables.TurntOnLights = data.LightSwitchesOn;
            GlobalVariables.DoorsUnlocked = data.OpenedDoors;
			GlobalVariables.PlayerPos = new Vector2(data.PlayerPosX, data.PlayerPosY);
            GlobalVariables.NotConsumedBatteries = data.CurrentBatteries;
            GlobalVariables.GeneratorsWorking = data.WorkingGenerators;
        }
		SceneManager.Instance.ChangeScene(ScenePaths.HospitalPath);
    }


    private void OnRestartPressed()
    {
        SceneManager.Instance.ChangeScene(ScenePaths.HospitalPath);
    }

}
