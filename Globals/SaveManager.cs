using Godot;

public partial class SaveManager : Node
{
    public static SaveManager Instance {private set; get;}

    public Vector2 LastCheckpoint {set; get;} //when character died
    public Vector2 LastPosition {set; get;} //when the player closes game and opens it again

    public override void _Ready()
    {
        Instance = this;
        LastCheckpoint = Vector2.Zero;
        LastPosition = Vector2.Zero;
    }
}
