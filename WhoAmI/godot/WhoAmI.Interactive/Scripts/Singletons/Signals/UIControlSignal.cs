using Godot;

public class UIControlSignal : Node
{
    public const string Signal = "PlayerDialog";
    [Signal] public delegate void PlayerDialog(string dialogText);
}
