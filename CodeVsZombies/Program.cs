using System;
using System.Linq;

internal class Program
{
    public static void Main(string[] args)
    {
        IInputProvider inputProvider = args.FirstOrDefault() is var inputPath && inputPath is not null
                                           ? new FileInputProvider(inputPath)
                                           : new ConsoleInputProvider();
        var inputParser = new InputParser(inputProvider);
        var solver = new StrandingSimulationSolver();
        while (true)
        {
            var turnInput = inputParser.ParseTurnInput();
            var turnSolution = solver.SolveTurn(turnInput);
            Console.WriteLine($"{Math.Ceiling(turnSolution.X)} {Math.Ceiling(turnSolution.Y)}");
        }
    }
}