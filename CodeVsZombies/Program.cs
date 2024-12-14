using System;
using System.Linq;
using System.Threading;

internal class Program
{
    public static void Main(string[] args)
    {
        IInputProvider inputProvider = args.FirstOrDefault() is var inputPath && inputPath is not null
                                           ? new FileInputProvider(inputPath)
                                           : new ConsoleInputProvider();
        var inputParser = new InputParser(inputProvider);
        var solver = new GeneticAlgoSolver();
        while (true)
        {
            var tokenSource = new CancellationTokenSource(TimeSpan.FromMilliseconds(950));

            var turnInput = inputParser.ParseTurnInput();
            // Console.Error.WriteLine(turnInput.ToString());
            var turnSolution = solver.SolveTurn(turnInput, tokenSource.Token);
            Console.WriteLine(turnSolution == Constants.WaitVector2
                                  ? "WAIT"
                                  : $"{Math.Ceiling(turnSolution.X)} {Math.Ceiling(turnSolution.Y)}");
        }
    }
}