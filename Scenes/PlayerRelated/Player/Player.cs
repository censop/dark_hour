using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] private float _walkSpeed = 60.0f;
	[Export] private float _runSpeed = 100.0f;

	public bool IsInteracting = false;


	public override void _Ready()
    {
        SignalHub.Instance.OnSaveGame += OnSaveGame;
        Position = GlobalVariables.PlayerPos;
    }

    public override void _ExitTree()
    {
        SignalHub.Instance.OnSaveGame -= OnSaveGame;
    }


    private void OnSaveGame()
    {
        SaveManager.Instance.SaveGame(GlobalPosition, GlobalVariables.BatteryTimePercentage, GlobalVariables.CollectedBatteries, GlobalVariables.NotConsumedBatteries, GlobalVariables.DoorKeysCollected, GlobalVariables.TurntOnLights);
    }

    public override void _PhysicsProcess(double delta)
	{
		if (IsInteracting) 
        {
            Velocity = Vector2.Zero;
            MoveAndSlide();
            return;
        }

		Vector2 inputDir = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
        Velocity = inputDir * _walkSpeed;
        MoveAndSlide();

        if (Input.IsActionJustPressed("interact"))
        {
            StartInteraction();
        }
    }

    public void StartInteraction()
    {
        IsInteracting = true;
        GD.Print("Interaction Started");
        
        //PLACEHOLDER FOR NOW
        GetTree().CreateTimer(0.4f).Timeout += () => IsInteracting = false;
    }
	
}
