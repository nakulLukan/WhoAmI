using Godot;
using System;

public class CarMovement : VehicleBody
{
    public float SteeringSpeed { get; set; } = 7;
    public float Accelaration { get; set; } = 1000;
    public float MaxRpm { get; set; } = 50;
    public float MaxTorque { get; set; } = 10;

    private VehicleWheel rwr;
    private VehicleWheel rwl;
    private VehicleWheel fwr;
    private VehicleWheel fwl;

    private float accelaration;
    private float steering;
    public override void _Ready()
    {
        rwr = GetNode("rwr") as VehicleWheel;
        rwl = GetNode("rwl") as VehicleWheel;
        fwr = GetNode("fwr") as VehicleWheel;
        fwl = GetNode("fwl") as VehicleWheel;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        accelaration = Input.GetAxis("ui_down", "ui_up");
        steering = Input.GetAxis("ui_right", "ui_left");
    }

    public override void _PhysicsProcess(float delta)
    {
        var rpm = Math.Abs(rwr.GetRpm());
        // rwr.EngineForce = -1 * accelaration * MaxTorque * (1 - rpm/MaxRpm) * Accelaration;
        // rpm = Math.Abs(rwl.GetRpm());
        // rwl.EngineForce = -1 *accelaration * MaxTorque * (1 - rpm/MaxRpm) * Accelaration;
        EngineForce = accelaration * 100;
        Steering = Mathf.Lerp(Steering, steering * 0.5F, delta * SteeringSpeed);
    }
}
