using Godot;
using System;

public class GravitationalForce : Node
{
    [Export] public float BodyMass { get; set; }
    RigidBody parent;
    Spatial gravity;
    public override void _Ready()
    {
        parent = GetParent<RigidBody>();
        gravity = this.GetGravityPoint();
    }
    
    public override void _PhysicsProcess(float delta){
        var gravityForce = parent.CalculateGravity(BodyMass, gravity.GlobalTransform.origin, GameConstant.EarthMass);
        parent.AddCentralForce(-parent.GlobalTransform.origin.Normalized() * gravityForce);
    }
}
