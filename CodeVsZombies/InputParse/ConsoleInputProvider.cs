using System;

public class ConsoleInputProvider : IInputProvider
{
    public string? ReadLine() => Console.ReadLine();
}