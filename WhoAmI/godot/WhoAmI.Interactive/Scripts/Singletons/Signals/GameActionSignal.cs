using Godot;

public class GameActionSignal : Node
{
    [Signal] public delegate void SubTitleDialog();
    [Signal] public delegate void PlayerFootballKicked();
}
