using Godot;

public class UIControlSignal : Node
{
    [Signal] public delegate void ActionAreaExit();
    [Signal] public delegate void PlayerDialogEntered();
    [Signal] public delegate void PlayerFootballEntered();
    [Signal] public delegate void SoundTrackEntered();
}
