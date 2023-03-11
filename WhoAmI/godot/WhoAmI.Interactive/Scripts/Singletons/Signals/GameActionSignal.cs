using Godot;

public class GameActionSignal : Node
{
    [Signal] public delegate void DialogAction(string description);
    [Signal] public delegate void DialogActionAreaExit();
}
