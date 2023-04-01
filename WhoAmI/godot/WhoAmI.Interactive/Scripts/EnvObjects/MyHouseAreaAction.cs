using Godot;

public class MyHouseAreaAction : Node
{
    [Export] public string Description { get; set; } = "HouseIntro";
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
