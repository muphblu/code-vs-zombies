using System;
using System.Collections.Generic;
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
        var solver = new SimpleSolver();
        while (true)
        {
            var turnInput = inputParser.ParseTurnInput();
            var turnSolution = solver.SolveTurn(turnInput);
            Console.WriteLine($"{Math.Ceiling(turnSolution.X)} {Math.Ceiling(turnSolution.Y)}");
        }
    }
}