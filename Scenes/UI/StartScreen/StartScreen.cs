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
	}

    private void OnLoadPressed()
    {
        SaveData data = SaveManager.Instance.LoadGame();

        if (data != null)
        {
            GlobalVariables.BatteryTimePercentage = data.BatteryPercentage;
            GlobalVariables.ConsumedBatteries = data.DestroyedBatteries;
            GlobalVariables.DoorKeysCollected = data.DoorKeyInventory;
            GlobalVariables.TurntOnLights = data.LightSwitchesOn;
			GlobalVariables.PlayerPos = new Vector2(data.PlayerPosX, data.PlayerPosY);
        }
		SceneManager.Instance.ChangeScene(ScenePaths.HospitalPath);
    }


    private void OnRestartPressed()
    {
        SceneManager.Instance.ChangeScene(ScenePaths.HospitalPath);
    }

}
