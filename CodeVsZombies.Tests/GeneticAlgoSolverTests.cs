using System;
using System.Numerics;
using NUnit.Framework;

namespace CodeVsZombies.Tests;

public class GeneticAlgoSolverTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    // [Test]
    // public void MutateVector2Length_ShouldMutate()
    // {
    //     var vector = new Vector2(1000, 1000);
    //     var seed = 100;
    //     var maxVectorLengthMutationRatio = 0.2f;
    //     var solver = new GeneticAlgoSolver(seed, 90f, maxVectorLengthMutationRatio);
    //     var random = new Random(seed);
    //     var ratio = 1 + maxVectorLengthMutationRatio * (2 * random.NextSingle() - 1);
    //     var result = solver.MutateVector2Length(vector);
    //     
    //     var expected = new Vector2(vector.X * ratio, vector.Y * ratio);
    //     
    //     Assert.AreEqual(expected, result);
    //     
    // }
    //
    // [Test]
    // public void MutateVector2Direction_ShouldMutate()
    // {
    //     var vector = new Vector2(1000, 1000);
    //     var seed = 100;
    //     var maxVectorDirectionMutationDegrees = 90f;
    //     var solver = new GeneticAlgoSolver(seed, maxVectorDirectionMutationDegrees);
    //     var random = new Random(seed);
    //     var ratio = 1 + maxVectorLengthMutationRatio * (2 * random.NextSingle() - 1);
    //     var result = solver.MutateVector2Length(vector);
    //     
    //     var expected = new Vector2(vector.X * ratio, vector.Y * ratio);
    //     
    //     Assert.AreEqual(expected, result);
    //     
    // }
}