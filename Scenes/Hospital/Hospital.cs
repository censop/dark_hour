using Godot;
using System;

public partial class Hospital : Node2D
{
    [Export] Player _player;

    SaveData _data;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnMenuPressed += OnMenuPressed;


        if (_data != null)
        {
            _player.GlobalPosition = new Vector2(_data.PlayerPosX, _data.PlayerPosY);
        }
	}

    public override void _EnterTree()
    {
        //FOR NOW IT AUTOAMTICALLY LOADS THE GAME
        _data = SaveManager.Instance.LoadGame();

        if (_data != null)
        {
            GlobalVariables.BatteryTimePercentage = _data.BatteryPercentage;
            GlobalVariables.ConsumedBatteries = _data.DestroyedBatteries;
            GlobalVariables.DoorKeysCollected = _data.DoorKeyInventory;
        }

    }


    public override void _ExitTree()
    {
        SignalHub.Instance.OnMenuPressed -= OnMenuPressed;
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("menu"))
		{
			SignalHub.EmitOnMenuPressed();
		}
    }


    private void OnMenuPressed()
    {
        GD.Print("Menu pressed");
    }
}
