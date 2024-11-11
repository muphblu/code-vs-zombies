using System.Linq;
using System.Numerics;
using System.Threading;

public class StrandingSimulationSolver : ISolver
{
    private readonly Simulator _simulator = new();
    private readonly PointsEvaluator _pointsEvaluator = new();

    private readonly Vector2[] _possibleMoves = {
        new(0, 0),
        new(0, Constants.Height/2),
        new(0, Constants.Height),
        new(Constants.Width/2, 0),
        new(Constants.Width, 0),
        new(Constants.Width/2, Constants.Height),
        new(Constants.Width, Constants.Height/2),
        new(Constants.Width, Constants.Height),
    };

    public Vector2 SolveTurn(TurnInput turnInput, CancellationToken cancellationToken)
    {
        const int turnsToSimulate = 6;

        var winnerMove = _possibleMoves.First();
        var maxScore = int.MinValue;
        
        foreach (var possibleMove in _possibleMoves.Append(turnInput.PlayerPosition))
        {
            var moveScore = 0;
            var newState = _simulator.SimulateTurnByTargetPosition(turnInput, possibleMove);
            if(newState.Zombies.Count == 0 || newState.Humans.Count == 0)
                continue;
            
            moveScore += _pointsEvaluator.EvaluateTurn(turnInput, newState);
            moveScore = RecursiveWalking(newState, turnsToSimulate, moveScore);

            if (maxScore < moveScore)
            {
                maxScore = moveScore;
                winnerMove = possibleMove;
            }
        }

        return winnerMove;
    }

    private int RecursiveWalking(TurnInput turnInput, int turnsLeft, int score)
    {
        if(turnsLeft <= 0)
            return score;

        turnsLeft -= 1;
        var maxScore = score;
        foreach (var possibleMove in _possibleMoves.Append(turnInput.PlayerPosition))
        {
            var moveScore = score;
            var newState = _simulator.SimulateTurnByTargetPosition(turnInput, possibleMove);
            if(newState.Zombies.Count == 0 || newState.Humans.Count == 0)
                continue;
            
            moveScore += _pointsEvaluator.EvaluateTurn(turnInput, newState);
            moveScore = RecursiveWalking(newState, turnsLeft, moveScore);

            if (maxScore < moveScore) 
                maxScore = moveScore;
        }
        
        return maxScore;
    }
}