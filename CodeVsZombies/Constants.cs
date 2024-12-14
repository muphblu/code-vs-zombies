using System.Numerics;

internal static class Constants
{
    public const float ZombieStep = 400;
    public const float PlayerStep = 1000;
    public const float ShotRange = 2000;
    public const int Width = 16000;
    public const int Height = 9000;
    public static readonly Vector2 WaitVector2 = new Vector2(-1, -1);
    public static readonly Vector2 MaxVector2 = new Vector2(Width, Height);
    
}