using System.Numerics;
using System.Threading;

public interface ISolver
{
    Vector2 SolveTurn(TurnInput turnInput, CancellationToken cancellationToken);
}