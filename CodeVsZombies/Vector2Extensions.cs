using System;
using System.Numerics;

public static class Vector2Extensions
{
    public static Vector2 MoveTowardsTarget(this Vector2 currentPosition, Vector2 target, float maxDistance)
    {
        var distanceToTarget = Vector2.Distance(currentPosition, target);
        var moveTowardsTarget = currentPosition + (target - currentPosition) * Math.Min(maxDistance / distanceToTarget, 1);
        return moveTowardsTarget with{ X = (float) Math.Truncate(moveTowardsTarget.X), Y = (float) Math.Truncate(moveTowardsTarget.Y)};
    }
}