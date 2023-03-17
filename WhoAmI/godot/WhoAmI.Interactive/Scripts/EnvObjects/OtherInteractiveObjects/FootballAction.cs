using Godot;

public class FootballAction : RigidBody
{
    [Export] public float Power { get; set; }

    GameActionSignal _gameActionSignal = null;
    public override void _Ready()
    {
        this.GetUIControlSignal().Connect(nameof(UIControlSignal.PlayerFootballKicked), this, nameof(OnKick));
        _gameActionSignal = this.GetGameActionSignal();
    }

    public void OnActionAreaEntered(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(GameActionSignal.FootballAction), -GlobalTransform.basis.y, Power);
    }

    public void OnActionAreaExit(Node body)
    {
        if (body.Name != NodeName.Player)
        {
            return;
        }

        _gameActionSignal.EmitSignal(nameof(GameActionSignal.ActionAreaExit));
    }

    public void OnKick(Vector3 direction, float power)
    {
        Utility.Print($"power recieved: {power}");
        this.ApplyImpulse(Vector3.Zero, direction * power);
    }
}
