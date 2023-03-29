using Godot;
public static class SignalObjectExtension
{
    public static UIControlSignal GetUIControlSignal(this Node node) =>
        node.GetNode<UIControlSignal>("/root/UIControlSignal");
        
    public static GameActionSignal GetGameActionSignal(this Node node) =>
        node.GetNode<GameActionSignal>("/root/GameActionSignal");
}