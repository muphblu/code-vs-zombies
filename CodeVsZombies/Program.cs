using System;
using System.Linq;
using System.Numerics;

internal class Program
{
    public static void Main(string[] args)
    {
        IInputProvider inputProvider = args.FirstOrDefault() is var inputPath && inputPath is not null
                                           ? new FileInputProvider(inputPath)
                                           : new ConsoleInputProvider();
                                   
        var inputParser = new InputParser(inputProvider);
        while (true)
        {
            var turnInput = inputParser.ParseTurnInput();

            var prioritizedZombies = turnInput.Zombies.OrderBy(z => turnInput.Humans
                                                    .Select(h => (Human: h,
                                                                     Distance: Vector2.Distance(
                                                                         h.Position,
                                                                         z.Position)))
                                                    .MinBy(x => x.Distance).Distance +
                                           Vector2.Distance(z.Position, turnInput.PlayerPosition));
            
            var priorityTarget = prioritizedZombies.First();
            var targetNextPosition = priorityTarget.NextPosition;

            Console.WriteLine($"{targetNextPosition.X} {targetNextPosition.Y}");
        }
    }
}