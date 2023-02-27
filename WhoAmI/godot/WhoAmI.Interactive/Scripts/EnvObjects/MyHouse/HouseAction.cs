using Godot;
public class HouseAction : Area
{   
    [Export] public string Description { get; set; } = "";
    GameActionSignal GameActionSignal;
    public override void _Ready(){
        GameActionSignal = this.GetGameActionSignal();
    }
    public void OnActionAreaEntered(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        GameActionSignal.EmitSignal(Signals.DialogActionEvent, Description);
    }   
}
