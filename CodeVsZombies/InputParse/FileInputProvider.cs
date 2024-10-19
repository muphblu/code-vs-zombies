using System;
using System.IO;

public class FileInputProvider : IInputProvider, IDisposable
{
    private readonly StreamReader _reader;

    public FileInputProvider(string path)
    {
        _reader = new StreamReader(path);
    }

    public string? ReadLine() => _reader.ReadLine();

    public void Dispose()
    {
        _reader.Dispose();
    }
}