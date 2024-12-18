using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;

namespace CodeVsZombies.Tests
{
    public class SimpleSolverTests
    {
        [Test]
        public void SolveTurn_PlayerMovesTowardsZombie()
        {
            var solver = new SimpleSolver();
            var turnInput = new TurnInput(
                new Vector2(0, 0),
                new List<Human> { new Human(1, new Vector2(1000, 1000)) },
                new List<Zombie> { new Zombie(1, new Vector2(5000, 5000), new Vector2(5400, 5000)) }
            );

            var result = solver.SolveTurn(turnInput, default);

            Assert.AreEqual(new Vector2(5200, 3000), result);
        }

        [Test]
        public void SolveTurn_PlayerMovesTowardsHuman()
        {
            var solver = new SimpleSolver();
            var turnInput = new TurnInput(
                new Vector2(0, 0),
                new List<Human> { new Human(1, new Vector2(1000, 1000)) },
                new List<Zombie> { new Zombie(1, new Vector2(500, 500), new Vector2(900, 500)) }
            );

            var result = solver.SolveTurn(turnInput, default);

            Assert.AreEqual(new Vector2(707, 707), result);
        }

        [Test]
        public void SolveTurn_PlayerMovesTowardsMidpoint()
        {
            var solver = new SimpleSolver();
            var turnInput = new TurnInput(
                new Vector2(0, 0),
                new List<Human> { new Human(1, new Vector2(1000, 1000)) },
                new List<Zombie> { new Zombie(1, new Vector2(2000, 2000), new Vector2(2400, 2000)) }
            );

            var result = solver.SolveTurn(turnInput, default);

            Assert.AreEqual(new Vector2(1700, 1700), result);
        }
    }
}
