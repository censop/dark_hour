using Godot;
using System;

public partial class BatterySpawner : Node2D
{

	[Export] Battery _battery;
	[Export] float MinY = -200.0f;
	[Export] float MinX = -50.0f;
	[Export] float MaxY = 0f;
	[Export] float MaxX = 50.0f;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
