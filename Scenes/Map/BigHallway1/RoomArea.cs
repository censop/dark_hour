using Godot;
using System;

public partial class RoomArea : Area2D
{
	[Export] ColorRect _blackoutSquare;
	[Export] float _fadeDuration = 0.5f;

    private Tween _fadeTween; 

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
        
        Color startColor = _blackoutSquare.Modulate;
        startColor.A = 1.0f;
        _blackoutSquare.Modulate = startColor;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player) 
        {
            FadeRoom(0.0f); 
        }
    }

    private void OnBodyExited(Node2D body)
    {
        if (body is Player) 
        {
            FadeRoom(1.0f); 
        }
    }

    private void FadeRoom(float targetAlpha)
    {

        if (_fadeTween != null && _fadeTween.IsValid())
        {
            _fadeTween.Kill();
        }

        _fadeTween = CreateTween();
        
        _fadeTween.TweenProperty(_blackoutSquare, "modulate:a", targetAlpha, _fadeDuration);
    }
}
