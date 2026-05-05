using Godot;
using System;

public partial class PlayerAnimation : Node2D
{
	[Export] AnimationTree _animationTree;
	Player _player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetParent<Player>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_player.Velocity.Length() > 0.1f)
        {
            Vector2 direction = _player.Velocity.Normalized();
            
            //_animationTree.Set("parameters/Idle/blend_position", direction);
            _animationTree.Set("parameters/blend_position", direction);
        }
	}
}
