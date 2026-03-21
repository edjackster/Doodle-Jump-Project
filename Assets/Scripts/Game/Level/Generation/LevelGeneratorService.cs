using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGeneratorService
{
    private const float MaxX = 1;
    private readonly GenerationSettings _settings;

    private List<PlatformType> _breakables = new()
    {
        PlatformType.Breakable,
        PlatformType.MovingBreakable
    };

    public LevelGeneratorService(GenerationSettings settings)
    {
        _settings = settings;
    }

    public LevelData Generate()
    {
        var platformsData = GeneratePlatforms();
        var trapsData = GenerateTraps(platformsData);

        return new LevelData(platformsData, trapsData);
    }

    private List<PlatformGenerationData> GeneratePlatforms()
    {
        List<PlatformGenerationData> platformsData = new List<PlatformGenerationData>();
        platformsData.Add(new PlatformGenerationData(0, 0, PlatformType.Normal));

        float lastY = 0f;
        float lastStable = 0f;
        float minStep = _settings.minPlatformStepY;

        while (lastY < _settings.levelHeight)
        {
            var maxStep = _settings.maxPlatformStepY;
            var previous = platformsData.LastOrDefault();

            if (_breakables.Contains(previous.Type) == false)
                maxStep = _settings.maxJumpY;

            float y = lastY + Random.Range(minStep, maxStep);

            var available = _settings.platformSpawnRules.Where(rule => y >= rule.MinSpawnHeight).ToList();

            if (y >= _settings.maxJumpY + lastStable - minStep)
            {
                available.RemoveAll(rule => _breakables.Contains(rule.Type));
                y = _settings.maxJumpY + lastStable;
            }

            var platformRule = WeightedRandomPick(available, y);
            var boosterType = platformRule.CanSpawnBooster
                ? PickRandomBoosterType(_settings.boosterSpawnRules)
                : BoosterType.None;
            float randomX = Random.Range(-MaxX, MaxX);

            platformsData.Add(new PlatformGenerationData(y, randomX, platformRule.Type, boosterType));

            if (_breakables.Contains(platformRule.Type) == false)
                lastStable = y;

            lastY = y;
        }

        return platformsData;
    }

    private List<TrapGenerationData> GenerateTraps(List<PlatformGenerationData> platformsData)
    {
        List<TrapGenerationData> trapsData = new List<TrapGenerationData>();

        for (int i = 0; i < platformsData.Count - 1; i++)
        {
            if (_settings.trapSpawnRules.MinSpawnHeight > platformsData[i].Y)
                continue;

            var current = platformsData[i];
            var next = platformsData[i + 1];

            if (next.Y - current.Y < _settings.trapSpawnRules.MinSpawnHeight)
                continue;

            var y = Random.Range(current.Y, next.Y);
            float chance = Random.value;

            if (chance > _settings.trapSpawnRules.ChanceByHeight.Evaluate(y))
                continue;

            if (TryGetRandomTrapX(y, platformsData, out var x) == false)
                continue; // не нашли безопасное место — пропускаем

            trapsData.Add(new TrapGenerationData(y, x));
        }

        return trapsData;
    }

    private PlatformTypeSpawnRule WeightedRandomPick(List<PlatformTypeSpawnRule> rules, float y)
    {
        // Весовое случайное распределение
        float sum = rules.Sum(r => r.ChanceByHeight.Evaluate(y));
        float val = Random.value * sum;
        float cumulative = 0f;

        foreach (var rule in rules)
        {
            cumulative += rule.ChanceByHeight.Evaluate(y);
            if (val <= cumulative)
                return rule;
        }

        return rules[0];
    }

    private BoosterType PickRandomBoosterType(List<BoosterTypeSpawnRule> rules)
    {
        float randomValue = Random.value;
        float cumulative = 0f;

        foreach (var entry in rules)
        {
            cumulative += entry.Chance;
            if (randomValue < cumulative)
                return entry.Type;
        }

        return BoosterType.None;
    }

    private bool TryGetRandomTrapX(float trapY, List<PlatformGenerationData> platformsData, out float x,
        int attemptsCount = 10)
    {
        for (int i = 0; i < attemptsCount; i++)
        {
            x = Random.Range(-MaxX, MaxX);

            if (IsTooCloseToAnyPlatform(x, trapY, platformsData))
                return true;
        }

        x = 0;

        return false;
    }

    private bool IsTooCloseToAnyPlatform(float trapX, float trapY, List<PlatformGenerationData> platforms)
    {
        foreach (var platform in platforms)
        {
            var isClose = platforms.All(platformData =>
            {
                var vector = new Vector2(platform.X - trapX, platform.Y - trapY);
                // Debug.Log(vector.magnitude);
                return vector.magnitude > _settings.trapSpawnRules.TrapSafeSpawnDistance;
            });

            if (isClose)
            {
                return true;
            }
        }

        return false;
    }
}