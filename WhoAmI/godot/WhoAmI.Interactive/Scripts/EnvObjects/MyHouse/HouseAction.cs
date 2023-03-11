using Godot;
public class HouseAction : Area
{   
    [Export] public string Description { get; set; } = "";
    GameActionSignal _gameActionSignal;
    public override void _Ready(){
        _gameActionSignal = this.GetGameActionSignal();
    }

    public void OnActionAreaEntered(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(GameActionSignal.DialogAction), Description);
    }   

    public void OnActionAreaExit(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(GameActionSignal.DialogActionAreaExit));
    }   
}
