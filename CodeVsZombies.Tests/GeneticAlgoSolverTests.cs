using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using NUnit.Framework;

namespace CodeVsZombies.Tests
{
    public class GeneticAlgoSolverTests
    {
        [Test]
        public void SolveTurn_ReturnsExpectedMove()
        {
            var solver = new GeneticAlgoSolver();
            var turnInput = new TurnInput(
                new Vector2(0, 0),
                new List<Human> { new Human(1, new Vector2(1000, 1000)) },
                new List<Zombie> { new Zombie(1, new Vector2(2000, 2000), new Vector2(2500, 2500)) }
            );
            var cancellationToken = new CancellationToken();

            var result = solver.SolveTurn(turnInput, cancellationToken);

            Assert.AreEqual(new Vector2(1000, 1000), result);
        }

        [Test]
        public void SolveTurn_WithMultipleHumansAndZombies_ReturnsExpectedMove()
        {
            var solver = new GeneticAlgoSolver();
            var turnInput = new TurnInput(
                new Vector2(0, 0),
                new List<Human>
                {
                    new Human(1, new Vector2(1000, 1000)),
                    new Human(2, new Vector2(2000, 2000))
                },
                new List<Zombie>
                {
                    new Zombie(1, new Vector2(3000, 3000), new Vector2(3500, 3500)),
                    new Zombie(2, new Vector2(4000, 4000), new Vector2(4500, 4500))
                }
            );
            var cancellationToken = new CancellationToken();

            var result = solver.SolveTurn(turnInput, cancellationToken);

            Assert.AreEqual(new Vector2(1000, 1000), result);
        }

        [Test]
        public void SolveTurn_WithNoHumans_ReturnsWaitVector()
        {
            var solver = new GeneticAlgoSolver();
            var turnInput = new TurnInput(
                new Vector2(0, 0),
                new List<Human>(),
                new List<Zombie>
                {
                    new Zombie(1, new Vector2(2000, 2000), new Vector2(2500, 2500))
                }
            );
            var cancellationToken = new CancellationToken();

            var result = solver.SolveTurn(turnInput, cancellationToken);

            Assert.AreEqual(Constants.WaitVector2, result);
        }
    }
}
