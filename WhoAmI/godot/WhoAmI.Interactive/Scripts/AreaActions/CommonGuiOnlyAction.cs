using Godot;
public class CommonGuiOnlyAction : Node
{
    [Export(hintString: "Dialog Key")] public string Description { get; set; } = null;
    [Signal] public delegate void ExecuteAction();
    public override void _Ready()
    {
        this.Connect(nameof(ExecuteAction), this, nameof(ExecuteActionRecieved));
    }

    void ExecuteActionRecieved(int magnitude)
    {
        this.GetGameActionSignal().EmitSignal(nameof(GameActionSignal.SubTitleDialog), Description);
    }
}
