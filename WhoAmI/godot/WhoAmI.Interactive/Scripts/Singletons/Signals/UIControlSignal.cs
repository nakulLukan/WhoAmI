using Godot;

public class UIControlSignal : Node
{
    [Signal] public delegate void ActionAreaExit();
    [Signal] public delegate void ActionAreaEntered();
}
