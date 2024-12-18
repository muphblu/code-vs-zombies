using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;

namespace CodeVsZombies.Tests
{
    public class SimulatorTests
    {
        [Test]
        public void SimulateTurn_PlayerMovesTowardsHuman()
        {
            var simulator = new Simulator();
            var initialState = new TurnInput(
                new Vector2(0, 0),
                new List<Human> { new Human(1, new Vector2(1000, 1000)) },
                new List<Zombie> { new Zombie(1, new Vector2(5000, 5000), new Vector2(5400, 5000)) }
            );

            var target = (IsHuman: true, Id: 1);
            var result = simulator.SimulateTurn(initialState, target);

            Assert.AreEqual(new Vector2(707, 707), result.PlayerPosition);
        }

        [Test]
        public void SimulateTurn_PlayerMovesTowardsZombie()
        {
            var simulator = new Simulator();
            var initialState = new TurnInput(
                new Vector2(0, 0),
                new List<Human> { new Human(1, new Vector2(1000, 1000)) },
                new List<Zombie> { new Zombie(1, new Vector2(5000, 5000), new Vector2(5400, 5000)) }
            );

            var target = (IsHuman: false, Id: 1);
            var result = simulator.SimulateTurn(initialState, target);

            Assert.AreEqual(new Vector2(1000, 0), result.PlayerPosition);
        }

        [Test]
        public void SimulateTurnByTargetPosition_PlayerMovesTowardsTarget()
        {
            var simulator = new Simulator();
            var initialState = new TurnInput(
                new Vector2(0, 0),
                new List<Human> { new Human(1, new Vector2(1000, 1000)) },
                new List<Zombie> { new Zombie(1, new Vector2(5000, 5000), new Vector2(5400, 5000)) }
            );

            var targetPosition = new Vector2(2000, 2000);
            var result = simulator.SimulateTurnByTargetPosition(initialState, targetPosition);

            Assert.AreEqual(new Vector2(1414, 1414), result.PlayerPosition);
        }

        [Test]
        public void SimulateTurnByTargetPosition_PlayerKillsZombie()
        {
            var simulator = new Simulator();
            var initialState = new TurnInput(
                new Vector2(0, 0),
                new List<Human> { new Human(1, new Vector2(1000, 1000)) },
                new List<Zombie> { new Zombie(1, new Vector2(1500, 0), new Vector2(1900, 0)) }
            );

            var targetPosition = new Vector2(2000, 0);
            var result = simulator.SimulateTurnByTargetPosition(initialState, targetPosition);

            Assert.AreEqual(new Vector2(1000, 0), result.PlayerPosition);
            Assert.AreEqual(0, result.Zombies.Count);
        }
    }
}
