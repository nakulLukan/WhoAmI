using Godot;
using WhoAmI.Interactive.Scripts.Shared.Extensions;
public class FootballAction : RigidBody
{
    [Export] public float Power { get; set; }
    [Signal] delegate void ExecuteAction();
    UIControlSignal _gameActionSignal = null;
    Spatial _subPlayer = null;
    public override void _Ready()
    {
        this.Connect(nameof(ExecuteAction), this, nameof(OnKick));
        _gameActionSignal = this.GetUIControlSignal();
        _subPlayer = this.GetPlayer().GetSubPlayer();
    }

    public void OnActionAreaEntered(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(UIControlSignal.PlayerFootballEntered), this);
    }

    public void OnActionAreaExit(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(UIControlSignal.ActionAreaExit));
    }

    public void OnKick(int magnitude)
    {
        var normalisedPower = Power * (System.Math.Min(100F, magnitude) / 10000);
        var direction = _subPlayer.GlobalTransform.basis.z.Normalized() * normalisedPower;
        this.ApplyImpulse(Vector3.Zero, direction * Power);
    }
}
