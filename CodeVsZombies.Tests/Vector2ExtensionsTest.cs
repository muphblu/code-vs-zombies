using System.Numerics;
using NUnit.Framework;

namespace CodeVsZombies.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void MoveTowardsTarget_MoveByLine()
    {
        var current = new Vector2(0, 0);
        var target = new Vector2(2000, 0);
        var maxDistance = 1000;
        
        var result = current.MoveTowardsTarget(target, maxDistance);
        
        var expected = new Vector2(1000, 0);
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void MoveTowardsTarget_TargetIsClose()
    {
        var current = new Vector2(0, 0);
        var target = new Vector2(500, 0);
        var maxDistance = 1000;
        
        var result = current.MoveTowardsTarget(target, maxDistance);
        
        var expected = new Vector2(500, 0);
        Assert.AreEqual(expected, result);
    }
    
    [Test]
    public void MoveTowardsTarget_MoveDiagonal()
    {
        var current = new Vector2(0, 0);
        var target = new Vector2(2000, 2000);
        var maxDistance = 1000;
        
        var result = current.MoveTowardsTarget(target, maxDistance);
        
        var expected = new Vector2(707, 707);
        Assert.AreEqual(expected, result);
    }
}