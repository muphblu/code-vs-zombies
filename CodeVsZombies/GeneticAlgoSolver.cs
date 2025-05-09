﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;

public class GeneticAlgoSolver: ISolver
{
    private readonly Random _random = new();
    private readonly int _populationSize = 100;
    private readonly int _solutionSize = 20;
    private readonly int _generations = 50;
    private const int BestCount = 2;
    private const int ParentsCount = 2;
    private const double MutationRate = 0.1;
    private Vector2[]? _bestSolution;
    private int _moveCounter;


    public Vector2 SolveTurn(TurnInput turnInput, CancellationToken cancellationToken)
    {
        if (_bestSolution != null)
        {
            if (turnInput.PlayerPosition == _bestSolution[_moveCounter]) 
                _moveCounter++;


            return _moveCounter < _bestSolution.Length
                       ? _bestSolution[_moveCounter]
                       : Constants.WaitVector2;
        }

        
        var population = InitialPopulation(turnInput);
        var generationsCounter = _generations;
        for (var generation = 0; generation < _generations; generation++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                generationsCounter = generation;
                break;
            }
            
            var newPopulation = new List<Vector2[]>(_populationSize);
            newPopulation.AddRange(population.Take(BestCount));

            var selected = Select(population, turnInput, ParentsCount);

            while (newPopulation.Count < _populationSize)
            {
                for (var i = 0; i < selected.Length - 1; i++)
                {
                    var (offspringA, offspringB) = Crossover(selected[i], selected[i + 1]);
                    Mutate(offspringA);
                    Mutate(offspringB);
                    newPopulation.Add(offspringA);
                    newPopulation.Add(offspringB);
                }
            }

            population = newPopulation.Count == _populationSize
                             ? newPopulation.ToArray()
                             : newPopulation.Take(_populationSize).ToArray();
        }

        _bestSolution = GetBestSolution(population, turnInput);
        Console.Error.WriteLine($"Generations: {generationsCounter}");
        return _bestSolution[_moveCounter];
    }

    private Vector2[] GetBestSolution(Vector2[][] population, TurnInput turnInput)
    {
        return population.Select(x => (Solution: x, Score: EvaluateSolution(x, turnInput)))
                         .OrderByDescending(x => x.Score)
                         .First().Solution;

    }

    private void Mutate(Vector2[] offspring)
    {
        for (var i = 0; i < offspring.Length * MutationRate; i++)
        {
            offspring[_random.Next(offspring.Length)] =
                new Vector2(_random.Next(Constants.Width), _random.Next(Constants.Height));
        }
    }

    private (Vector2[], Vector2[]) Crossover(Vector2[] solutionA, Vector2[] solutionB)
    {
        var divideIndex = _random.Next(_solutionSize);
        return (solutionA.Take(divideIndex).Concat(solutionB.Skip(divideIndex)).ToArray(),
                solutionB.Take(divideIndex).Concat(solutionA.Skip(divideIndex)).ToArray());
    }

    private Vector2[][] Select(Vector2[][] population, TurnInput turnInput, int neededCount)
    {
        return population.Select(s => (Solution: s, Scope: EvaluateSolution(s, turnInput)))
                         .Where(x => x.Scope >= 0)
                         .OrderByDescending(x => x.Scope)
                         .Take(neededCount)
                         .Select(x => x.Solution)
                         .ToArray();
    }

    private Vector2[][] InitialPopulation(TurnInput turnInput)
    {
        var humansByDistance = turnInput.Humans
                                        .Select(h => (Human: h.Position, Distance: Vector2.Distance(h.Position, turnInput.PlayerPosition)))
                                        .OrderBy(x => x.Distance)
                                        .ToArray();
        
        var population = new Vector2[_populationSize][];

        for (var i = 0; i < 2; i++)
        {
            var solution = new Vector2[_solutionSize];
            for (var j = 0; j < _solutionSize; j++)
            {
                var index = (int) Math.Truncate(j % humansByDistance.Length * _random.NextSingle());
                solution[j] = humansByDistance[index].Human;
            }
            population[i] = solution;
        }
        
        for (var i = 2; i < _populationSize; i++)
        {
            var solution = new Vector2[_solutionSize];
            for (var j = 0; j < _solutionSize; j++)
            {
                solution[j] = new Vector2(_random.Next(Constants.Width), _random.Next(Constants.Height));
            }
            population[i] = solution;
        }

        return population;
    }

    private int EvaluateSolution(Vector2[] solution, TurnInput turnInput)
    {
        var simulator = new Simulator();
        var pointsEvaluator = new PointsEvaluator();

        var score = 0;
        var currentState = turnInput;

        foreach (var move in solution)
        {
            while (currentState.PlayerPosition != move && currentState.Zombies.Count > 0 && currentState.Humans.Count > 0)
            {
                var previousState = currentState;
                currentState = simulator.SimulateTurnByTargetPosition(currentState, move);
                score += pointsEvaluator.EvaluateTurn(previousState, currentState);
            }
        }

        return score;
    }
}