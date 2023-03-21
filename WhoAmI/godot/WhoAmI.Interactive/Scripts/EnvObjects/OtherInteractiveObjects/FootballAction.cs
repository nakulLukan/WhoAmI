using Godot;
using WhoAmI.Interactive.Scripts.Shared.Extensions;
public class FootballAction : RigidBody
{
    [Export] public float Power { get; set; }

    GameActionSignal _gameActionSignal = null;
    Spatial _subPlayer = null;
    public override void _Ready()
    {
        this.GetUIControlSignal().Connect(nameof(UIControlSignal.PlayerFootballKicked), this, nameof(OnKick));
        _gameActionSignal = this.GetGameActionSignal();
        _subPlayer = this.GetPlayer().GetSubPlayer();
    }

    public void OnActionAreaEntered(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(GameActionSignal.FootballAction));
    }

    public void OnActionAreaExit(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(GameActionSignal.ActionAreaExit));
    }

    public void OnKick()
    {
        var direction = _subPlayer.GlobalTransform.basis.z.Normalized();
        this.ApplyImpulse(Vector3.Zero, direction * Power);
    }
}
