using Godot;
public static class SignalObjectExtension
{
    public static GameActionSignal GetGameActionSignal(this Node node){
        return node.GetNode<GameActionSignal>("/root/GameActionSignal");
    }
}