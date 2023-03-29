using Godot;
public class DialogAreaAction : Area
{   
    [Export] public string Description { get; set; } = "";
    [Signal] public delegate void ExecuteAction();
    UIControlSignal _uiControlSignal;
    public override void _Ready()
    {
        _uiControlSignal = this.GetUIControlSignal();
        this.Connect(nameof(ExecuteAction), this, nameof(OnDialogActionRecieved));
    }

    void OnDialogActionRecieved()
    {
        this.GetGameActionSignal().EmitSignal(nameof(GameActionSignal.SubTitleDialog), Description);
    }

    public void OnActionAreaEntered(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _uiControlSignal.EmitSignal(nameof(UIControlSignal.PlayerDialogEntered), this);
    }   

    public void OnActionAreaExit(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _uiControlSignal.EmitSignal(nameof(UIControlSignal.ActionAreaExit));
    }   
}
