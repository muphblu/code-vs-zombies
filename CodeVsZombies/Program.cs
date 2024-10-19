using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

internal class Program
{
    public static void Main(string[] args)
    {
        const float zombieStep = 400;
        const float playerStep = 1000;
        const float shotRange = 2000;
        
        IInputProvider inputProvider = args.FirstOrDefault() is var inputPath && inputPath is not null
                                           ? new FileInputProvider(inputPath)
                                           : new ConsoleInputProvider();
        var willBeEaten = new List<int>();
        var inputParser = new InputParser(inputProvider);
        while (true)
        {
            var turnInput = inputParser.ParseTurnInput();
            willBeEaten = turnInput.Humans.Select(x=>x.Id).Intersect(willBeEaten).ToList();   
            var prioritizedZombies = 
                turnInput.Zombies
                         .Select(
                    z =>
                    {
                        var closestHuman = turnInput.Humans.Select(h => (Human: h, Distance: Vector2.Distance(h.Position, z.Position))).MinBy(x => x.Distance);
                        if (willBeEaten.Contains(closestHuman.Human.Id))
                            return (Zombie:z, closestHuman.Human, StepsZombieToHuman: float.MaxValue, StepsPlayerToHuman: float.MaxValue, PlayerIsTarget: false);

                        var distanceZombieToPlayer = Vector2.Distance(z.Position, turnInput.PlayerPosition); 
                        var stepsZombieToHuman = closestHuman.Distance / zombieStep;
                        var distancePlayerToHuman = Vector2.Distance(closestHuman.Human.Position, turnInput.PlayerPosition);
                        var stepsPlayerToHuman = Math.Max((distancePlayerToHuman - shotRange) / playerStep, 0);
                        //human will be eaten
                        if (stepsZombieToHuman < stepsPlayerToHuman)
                        {
                            willBeEaten.Add(closestHuman.Human.Id);
                            return (Zombie: z, closestHuman.Human, StepsZombieToHuman: float.MaxValue, StepsPlayerToHuman: float.MaxValue, PlayerIsTarget: false);
                        }

                        var playerIsTarget = distanceZombieToPlayer < closestHuman.Distance;
                        
                        return
                            (Zombie: z, closestHuman.Human, StepsZombieToHuman: stepsZombieToHuman,
                                StepsPlayerToHuman: stepsPlayerToHuman, 
                                PlayerIsTarget: playerIsTarget);
                    })
                         .OrderBy(x=> x.StepsPlayerToHuman + x.StepsZombieToHuman);
            
            var priorityTarget = prioritizedZombies.First();
            var targetNextPosition = priorityTarget.Zombie.NextPosition;
            var zombieToHumanMidpoint = (priorityTarget.Zombie.NextPosition + priorityTarget.Human.Position) / 2;
            var playerNextPosition = priorityTarget.PlayerIsTarget ? priorityTarget.Zombie.Position : zombieToHumanMidpoint;

            Console.WriteLine($"{Math.Ceiling(playerNextPosition.X)} {Math.Ceiling(playerNextPosition.Y)}");
        }
    }
}