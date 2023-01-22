using Godot;
using System;

public static class SpatialObjectExtension
{
    public static void SafeLookAt(this Spatial spatial, Vector3 target, Vector3 upAxis)
    {
        var origin = spatial.GlobalTransform.origin;
        var direction = (target - origin).Normalized();

        if (target == origin)
        {
            return;
        }
        if (direction.Length() == 0)
            return;
        if (direction.x == 0 && direction.z == 0)
        {
            direction.x += 0.001f;
        }

        spatial.LookAt(direction, upAxis);
    }
}
