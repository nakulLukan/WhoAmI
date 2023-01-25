using Godot;
using System;

public static class RigidBodyObjectExtension
{
    public static float CalculateGravity(this CollisionObject rigidBody, float bodyMass, Vector3 gravityPoint, float attractingBodyMass){
        double G = Math.Pow(6.67D, -4D);
        double upper = G * bodyMass * attractingBodyMass;
        float lower = (rigidBody.GlobalTransform.origin - gravityPoint).Length();

        return (float)(upper/ (lower * lower));
    }
}