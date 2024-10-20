using System.Linq;
using System.Numerics;

public class Simulator
{
    public TurnInput SimulateTurn(TurnInput currentState, Vector2 commandToPlayer)
    {
        //move zombies
        var newZombies = currentState.Zombies.Select(zombie => zombie with { Position = zombie.NextPosition }).ToList();

        //move player
        var newPlayerPosition = currentState.PlayerPosition.MoveTowardsTarget(commandToPlayer, Constants.PlayerStep);
        
        //kill zombies
        newZombies = newZombies.Where(x => Vector2.Distance(x.Position, newPlayerPosition) > Constants.ShotRange).ToList();
        
        //kill humans
        var newHumans = currentState.Humans.ExceptBy(newZombies.Select(x => x.Position), x => x.Position).ToList();
        
        //update zombie target
        newZombies = newZombies.Select(zombie =>
        {
            var closestHuman = newHumans.Select(h => (IsPlayer: false, h.Position))
                                        .Append((IsPlayer: true, Position: newPlayerPosition))
                                        .Select(x => (x.IsPlayer, x.Position, Distance: Vector2.Distance(x.Position, zombie.Position)))
                                        .MinBy(x => x.Distance);
            var nextZombiePosition = zombie.Position.MoveTowardsTarget(closestHuman.Position, Constants.PlayerStep);

            return zombie with { NextPosition = nextZombiePosition };
        }).ToList();

        return new TurnInput(newPlayerPosition, newHumans, newZombies);
    }
}