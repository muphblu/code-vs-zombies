﻿using System.Collections.Generic;
using System.Numerics;
using System.Text;

public class InputParser
{
    private readonly IInputProvider _inputProvider;

    public InputParser(IInputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    public TurnInput ParseTurnInput()
    {
        var parts = _inputProvider.ReadLine().Split(' ');
        var playerPosition = new Vector2(int.Parse(parts[0]), int.Parse(parts[1]));
        var humanCount = int.Parse(_inputProvider.ReadLine());
        var humans = new List<Human>();
        for (var i = 0; i < humanCount; i++)
        {
            parts = _inputProvider.ReadLine().Split(' ');
            humans.Add(new Human(int.Parse(parts[0]), new Vector2(int.Parse(parts[1]), int.Parse(parts[2]))));
        }
        
        var zombieCount = int.Parse(_inputProvider.ReadLine());
        var zombies = new List<Zombie>();
        for (var i = 0; i < zombieCount; i++)
        {
            parts = _inputProvider.ReadLine().Split(' ');
            zombies.Add(new Zombie(int.Parse(parts[0]), new Vector2(int.Parse(parts[1]), int.Parse(parts[2])), new Vector2(int.Parse(parts[3]), int.Parse(parts[4]))));
        }
        
        return new TurnInput(playerPosition, humans, zombies);
    }
}

public record Human(int Id, Vector2 Position);
public record Zombie(int Id, Vector2 Position, Vector2 NextPosition);

public record TurnInput(Vector2 PlayerPosition, List<Human> Humans, List<Zombie> Zombies)
{
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{PlayerPosition.X} {PlayerPosition.Y}");
        builder.AppendLine($"{Humans.Count}");
        foreach (var human in Humans)
        {
            builder.AppendLine($"{human.Id} {human.Position.X} {human.Position.Y}");
        }
        builder.AppendLine($"{Zombies.Count}");
        foreach (var zombie in Zombies)
        {
            builder.AppendLine($"{zombie.Id} {zombie.Position.X} {zombie.Position.Y} {zombie.NextPosition.X} {zombie.NextPosition.Y}");
        }
        return builder.ToString();
    }
}