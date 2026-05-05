using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] private float _walkSpeed = 60.0f;
	[Export] private float _runSpeed = 100.0f;


	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		int xDir = 0;
		int yDir = 0;

		if (Input.IsActionPressed("walk_right")) xDir = 1;
		if (Input.IsActionPressed("walk_left")) xDir = -1;
		if (Input.IsActionPressed("walk_up")) yDir = -1;
		if (Input.IsActionPressed("walk_down")) yDir = 1;

		Vector2 inputDir = new Vector2(xDir*_walkSpeed, yDir * _walkSpeed);

		Velocity = inputDir.Normalized() * _walkSpeed;

        MoveAndSlide();

	}
}
