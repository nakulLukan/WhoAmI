using Godot;
public class FootballAction : RigidBody
{
    [Export] public float Power { get; set; }
    [Export] public AudioStreamMP3 KickSound { get; set; } = null;

    [Signal] delegate void ExecuteAction(int magnitude);
    Spatial _subPlayer = null;
    public override void _Ready()
    {
        this.Connect(nameof(ExecuteAction), this, nameof(OnKick));
        _subPlayer = this.GetPlayer().GetSubPlayer();
    }

    public void OnKick(int magnitude)
    {
        var normalisedPower = Power * (System.Math.Min(100F, magnitude) / 10000);
        var direction = _subPlayer.GlobalTransform.basis.z.Normalized() * normalisedPower;
        this.ApplyImpulse(Vector3.Zero, direction * Power);
        this.GetSoundEffectStreamProvider()
            .EmitSignal(nameof(SoundEffectStreamProvider.PlayFromAudioStream), KickSound);
    }
}
