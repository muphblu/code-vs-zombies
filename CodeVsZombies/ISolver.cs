using System.Numerics;

public interface ISolver
{
    Vector2 SolveTurn(TurnInput turnInput);
}