using System;
using System.Linq;
using System.Numerics;
using System.Threading;


public class OneTargetSimulationSolver : ISolver
{
    private readonly Simulator _simulator = new();
    private readonly PointsEvaluator _pointsEvaluator = new();
    public Vector2 SolveTurn(TurnInput turnInput, CancellationToken cancellationToken)
    {
        var possibleMoves = turnInput.Zombies.Select(z => (IsHuman: false, z.Id))
                                     .Concat(turnInput.Humans.Select(h => (IsHuman: true, h.Id)))
                                     .ToList();
        const int simulationsCount = 30;

        Console.Error.WriteLine(turnInput.ToString());
        var winnerMove = possibleMoves.First();
        var maxScore = int.MinValue;
        foreach (var possibleMove in possibleMoves)
        {
            var score = 0;
            var currentState = turnInput;
            for (var i = 0; i < simulationsCount; i++)
            {
                if(currentState.Zombies.Count == 0 || currentState.Humans.Count == 0)
                    break;
                var previousState = currentState;
                currentState = _simulator.SimulateTurn(currentState, possibleMove);
                score += _pointsEvaluator.EvaluateTurn(previousState, currentState);
            }
            if(maxScore < score)
            {
                maxScore = score;
                winnerMove = possibleMove;
            }
        }
        
        return winnerMove.IsHuman
                   ? turnInput.Humans.First(x => x.Id == winnerMove.Id).Position
                   : turnInput.Zombies.First(x => x.Id == winnerMove.Id).Position;
    }
}