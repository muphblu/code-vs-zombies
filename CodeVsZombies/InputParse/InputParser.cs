using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class InputParser
{
    private readonly IInputProvider _inputProvider;

    public InputParser(IInputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    public TurnInput ParseMonthInput()
    {
        return new TurnInput();
    }
}

public record TurnInput
{
}