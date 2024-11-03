using System.Linq;
using System.Numerics;

public class Simulator
{
    public TurnInput SimulateTurn(TurnInput currentState, (bool IsHuman, int Id) target)
    {
        //move zombies
        var newZombies = currentState.Zombies.Select(zombie => zombie with { Position = zombie.NextPosition }).ToList();

        //move player
        var targetPosition = target.IsHuman
                                 ? currentState.Humans.FirstOrDefault(x => x.Id == target.Id)?.Position ?? currentState.PlayerPosition
                                 : newZombies.FirstOrDefault(x => x.Id == target.Id)?.Position ?? currentState.PlayerPosition;
        var newPlayerPosition = currentState.PlayerPosition.MoveTowardsTarget(targetPosition, Constants.PlayerStep);
        
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
            var nextZombiePosition = zombie.Position.MoveTowardsTarget(closestHuman.Position, Constants.ZombieStep);

            return zombie with { NextPosition = nextZombiePosition };
        }).ToList();

        return new TurnInput(newPlayerPosition, newHumans, newZombies);
    }
    
    public TurnInput SimulateTurnByTargetPosition(TurnInput currentState, Vector2 targetPosition)
    {
        //move zombies
        var newZombies = currentState.Zombies.Select(zombie => zombie with { Position = zombie.NextPosition }).ToList();

        //move player
        var newPlayerPosition = currentState.PlayerPosition.MoveTowardsTarget(targetPosition, Constants.PlayerStep);
        
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
            var nextZombiePosition = zombie.Position.MoveTowardsTarget(closestHuman.Position, Constants.ZombieStep);

            return zombie with { NextPosition = nextZombiePosition };
        }).ToList();

        return new TurnInput(newPlayerPosition, newHumans, newZombies);
    }
}