using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;

namespace CodeVsZombies.Tests;


public class PointEvaluatorTests
{

    [Test]
    public void EvaluateTurn_NoHumans_ReturnsMinValue()
    {
        var evaluator = new PointsEvaluator();
        var previousState = new TurnInput(new Vector2(0, 0),
                                          new List<Human>{new(1, new Vector2(5000, 5000))},
                                          new List<Zombie>
                                              { new(1, new Vector2(5000, 5000), new Vector2(5400, 5000)) });
        var currentState = new TurnInput(new Vector2(1000, 0),
                                         new List<Human>(),
                                         new List<Zombie>
                                             { new(1, new Vector2(5400, 5000), new Vector2(5800, 5000)) });

        var result = evaluator.EvaluateTurn(previousState, currentState);

        Assert.AreEqual(int.MinValue, result);
    }
    
    [Test]
    public void EvaluateTurn_OneHumanOneZombieKilled()
    {
        var evaluator = new PointsEvaluator();
        var previousState = new TurnInput(new Vector2(8000, 5000),
                                          new List<Human>{new(1, new Vector2(1000, 1000))},
                                          new List<Zombie>
                                              { new(1, new Vector2(5000, 5000), new Vector2(5400, 5000)) });
        var currentState = new TurnInput(new Vector2(7000, 5000),
                                         new List<Human>{new(1, new Vector2(1000, 1000))},
                                         new List<Zombie>());

        var result = evaluator.EvaluateTurn(previousState, currentState);

        Assert.AreEqual(10, result);
    }
    
    [Test]
    public void EvaluateTurn_SixHumanTwoZombiesKilled()
    {
        var evaluator = new PointsEvaluator();
        var previousState = new TurnInput(new Vector2(8000, 5000),
                                          new List<Human>
                                          {
                                              new(1, new Vector2(1000, 1000)),
                                              new(2, new Vector2(1001, 1000)),
                                              new(3, new Vector2(1002, 1000)),
                                              new(4, new Vector2(1003, 1000)),
                                              new(5, new Vector2(1004, 1000)),
                                              new(6, new Vector2(1005, 1000)),
                                          },
                                          new List<Zombie>
                                          {
                                              new(1, new Vector2(5000, 5000), new Vector2(5400, 5000)),
                                              new(1, new Vector2(5001, 5000), new Vector2(5401, 5000))
                                          });
        var currentState = new TurnInput(new Vector2(7000, 5000),
                                         new List<Human>
                                         {
                                             new(1, new Vector2(1000, 1000)),
                                             new(2, new Vector2(1001, 1000)),
                                             new(3, new Vector2(1002, 1000)),
                                             new(4, new Vector2(1003, 1000)),
                                             new(5, new Vector2(1004, 1000)),
                                             new(6, new Vector2(1005, 1000)),
                                         },
                                         new List<Zombie>());

        var result = evaluator.EvaluateTurn(previousState, currentState);

        Assert.AreEqual(360+720, result);
    }
}