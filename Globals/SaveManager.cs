using Godot;

public partial class SaveManager : Node
{
    public static SaveManager Instance {private set; get;}

    public override void _Ready()
    {
        Instance = this;
    }
}
