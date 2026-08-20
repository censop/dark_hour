using Godot;
using System;

public partial class EndOfDemoUi : CanvasLayer
{ 
	[Export] private ColorRect _fadeRect;
    [Export] private Label _thankYouLabel;
	
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnDemoEndTriggered += OnDemoEndTriggered;
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnDemoEndTriggered -= OnDemoEndTriggered;
    }


    private void OnDemoEndTriggered()
    {
		_fadeRect.Modulate = new Color(_fadeRect.Modulate, 0f);
        _thankYouLabel.Modulate = new Color(_thankYouLabel.Modulate, 0f);
		
		Show();

        Tween tween = CreateTween();

        tween.TweenProperty(_fadeRect, "modulate:a", 1.0f, 2);

        tween.TweenProperty(_thankYouLabel, "modulate:a", 1.0f, 1);
    }

}
