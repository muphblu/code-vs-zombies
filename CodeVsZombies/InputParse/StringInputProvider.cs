public class StringInputProvider : IInputProvider
{
    private readonly string[] _inputs;
    private int _index = 0;

    public StringInputProvider(string input)
    {
        this._inputs = input.Split("\n");
    }

    public string? ReadLine()
    {
        if (_inputs.Length == 0 || _index >= _inputs.Length)
            return null;

        return _inputs[_index++];
    }
}