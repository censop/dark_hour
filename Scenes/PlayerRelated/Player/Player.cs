using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] private float _walkSpeed = 60.0f;
	[Export] private float _runSpeed = 100.0f;

    public CharacterBody2D GrabbedObject = null;
    private bool _isGrabbing = false;
    [Export] private float _pushPullSpeed = 40.0f;


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
        if (Input.IsActionPressed("interact") && GrabbedObject != null)
        {
            _isGrabbing = true;

        }
        else
        {
            _isGrabbing = false;
        }

        Vector2 inputDir = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");

        if (_isGrabbing && inputDir != Vector2.Zero)
        {
            Vector2 step = inputDir * _pushPullSpeed * (float)delta;

            AddCollisionExceptionWith(GrabbedObject);
            GrabbedObject.AddCollisionExceptionWith(this);

            KinematicCollision2D playerCol = MoveAndCollide(step, testOnly: true);
            KinematicCollision2D boxCol = GrabbedObject.MoveAndCollide(step, testOnly: true);

            if (playerCol == null && boxCol == null)
            {
                MoveAndCollide(step);
                GrabbedObject.MoveAndCollide(step);
            }

            RemoveCollisionExceptionWith(GrabbedObject);
            GrabbedObject.RemoveCollisionExceptionWith(this);
        }
        else
        {
            if (IsInteracting) 
            {
                Velocity = Vector2.Zero;
                MoveAndSlide();
                return;
            }

            Velocity = inputDir * _walkSpeed;
            MoveAndSlide();

            if (Input.IsActionJustPressed("interact"))
            {
                StartInteraction();
            }
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
