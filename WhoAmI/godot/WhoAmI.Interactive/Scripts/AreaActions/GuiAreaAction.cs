using Godot;
public class GuiAreaAction : Node
{
    UIControlSignal _uiControlSignal;

    /// <summary>
    /// The parent node will be used by default if not assigned.
    /// </summary>
    [Export] public Godot.NodePath _parentNode { get; set; }
    Node _parent { get; set; }
    public override void _Ready()
    {
        _uiControlSignal = this.GetUIControlSignal();
        _parent = _parentNode != null ? GetNode(_parentNode) : this.GetParent();
        GetParent().Connect(Signals.AreaBodyEntered, this, nameof(OnActionAreaEntered));
        GetParent().Connect(Signals.AreaBodyExited, this, nameof(OnActionAreaExit));
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
