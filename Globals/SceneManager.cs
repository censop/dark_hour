using Godot;
using System;

public partial class SceneManager : Node
{
    public static SceneManager Instance {private set; get;}

    public override void _Ready()
    {
        Instance = this;
    }

    public void ReloadScene()
    {
        GetTree().ReloadCurrentScene();
    }

    public void ChangeScene(String scenePath)
    {
        GetTree().ChangeSceneToFile(scenePath);
    }
}
