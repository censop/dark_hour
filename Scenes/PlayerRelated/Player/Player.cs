using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] private float _walkSpeed = 60.0f;
	[Export] private float _runSpeed = 100.0f;

    public CharacterBody2D GrabbedObject = null;
    private bool _isGrabbing = false;
    [Export] private float _pushPullSpeed = 20.0f;

    [Export] private AudioStreamPlayer2D _footstepAudio;
    private ulong _lastStepTime = 0;


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
        SaveManager.Instance.SaveGame(
            GlobalPosition, 
            GlobalVariables.BatteryTimePercentage, 
            GlobalVariables.CollectedBatteries, 
            GlobalVariables.NotConsumedBatteries, 
            GlobalVariables.DoorKeysCollected, 
            GlobalVariables.TurntOnLights, 
            GlobalVariables.DoorsUnlocked,
            GlobalVariables.GeneratorsWorking
        );
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
            Velocity = inputDir * _pushPullSpeed;

            AddCollisionExceptionWith(GrabbedObject);
            GrabbedObject.AddCollisionExceptionWith(this);

            KinematicCollision2D playerCol = MoveAndCollide(step, testOnly: true);
            KinematicCollision2D boxCol = GrabbedObject.MoveAndCollide(step, testOnly: true);

            if (playerCol == null && boxCol == null)
            {
                MoveAndCollide(step);
                GrabbedObject.MoveAndCollide(step);
            }
            else 
            {       
                // for animation
                Velocity = Vector2.Zero; 
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

    public void PlayFootstep()
    {
        ulong currentTime = Time.GetTicksMsec();
    
        // If less than 100 milliseconds have passed since the last step, ignore this call!
        // This blocks the "double step" glitch when walking diagonally.
        if (currentTime - _lastStepTime < 100) 
        {
            return; 
        }
        
        // Save this time for the next step
        _lastStepTime = currentTime;
        _footstepAudio.PitchScale = (float)GD.RandRange(0.8, 1.2);
        _footstepAudio.Play();
    }
	
}
