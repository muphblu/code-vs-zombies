using System;

public class PointsEvaluator
{
    private readonly int[] _fibonacciSequence = {0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377};
    public int EvaluateTurn(TurnInput previousState, TurnInput currentState)
    {
        if (currentState.Humans.Count == 0)
            return int.MinValue;

        var sumPoints = 0;
        var zombiesKilled = previousState.Zombies.Count - currentState.Zombies.Count;
        var pointsForOneZombie = (int) Math.Pow(currentState.Humans.Count, 2) * 10;
        for (var i = 1; i <= zombiesKilled; i++)
        {
            sumPoints += _fibonacciSequence[i + 1] * pointsForOneZombie;
        }

        return sumPoints;
    }
}