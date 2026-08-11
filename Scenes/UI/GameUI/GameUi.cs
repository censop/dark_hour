using Godot;
using System;

public partial class GameUi : Control
{
	[Export] private ProgressBar _progressBar;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		_progressBar.Value = GlobalVariables.BatteryTimePercentage * 100;
	}
}
