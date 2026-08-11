using Godot;
using System;

public partial class AudioManager : Node
{
    public static AudioManager Instance {private set; get;}

    [Export] AudioStreamPlayer _uiClickSound;

    public override void _Ready()
    {
        Instance = this;
    }

    public void PlayUIClickSound()
    {
        _uiClickSound.Play();
    }
}
