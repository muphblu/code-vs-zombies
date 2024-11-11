using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;

public class SimpleSolver : ISolver
{
    private List<int> _willBeEaten = new List<int>();
    
    public Vector2 SolveTurn(TurnInput turnInput, CancellationToken cancellationToken)
    {
        _willBeEaten = turnInput.Humans.Select(x=>x.Id).Intersect(_willBeEaten).ToList();   
            var prioritizedZombies = 
                turnInput.Zombies
                         .Select(
                    z =>
                    {
                        var closestHuman = turnInput.Humans.Select(h => (Human: h, Distance: Vector2.Distance(h.Position, z.Position))).MinBy(x => x.Distance);
                        if (_willBeEaten.Contains(closestHuman.Human.Id))
                            return (Zombie:z, closestHuman.Human, StepsZombieToHuman: float.MaxValue, StepsPlayerToHuman: float.MaxValue, PlayerIsTarget: false);

                        var distanceZombieToPlayer = Vector2.Distance(z.Position, turnInput.PlayerPosition); 
                        var stepsZombieToHuman = closestHuman.Distance / Constants.ZombieStep;
                        var distancePlayerToHuman = Vector2.Distance(closestHuman.Human.Position, turnInput.PlayerPosition);
                        var stepsPlayerToHuman = Math.Max((distancePlayerToHuman - Constants.ShotRange) / Constants.PlayerStep, 0);
                        //human will be eaten
                        if (stepsZombieToHuman < stepsPlayerToHuman)
                        {
                            _willBeEaten.Add(closestHuman.Human.Id);
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
            var zombieToHumanMidpoint = (priorityTarget.Zombie.NextPosition + priorityTarget.Human.Position) / 2;
            var playerNextPosition = priorityTarget.PlayerIsTarget ? priorityTarget.Zombie.Position : zombieToHumanMidpoint;

            return playerNextPosition;
    }
}