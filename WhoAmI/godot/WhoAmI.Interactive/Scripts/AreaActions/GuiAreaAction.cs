using Godot;
public class GuiAreaAction : Node
{   
    UIControlSignal _uiControlSignal;
    Node _parent;
    public override void _Ready()
    {
        _uiControlSignal = this.GetUIControlSignal();
        _parent = (Node)this.GetParent();
        _parent.Connect(Signals.AreaBodyEntered, this, nameof(OnActionAreaEntered));
        _parent.Connect(Signals.AreaBodyExited, this, nameof(OnActionAreaExit));
    }

    public void OnActionAreaEntered(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _uiControlSignal.EmitSignal(nameof(UIControlSignal.ActionAreaEntered), _parent);
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
