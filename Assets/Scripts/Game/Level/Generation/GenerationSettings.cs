using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Generation Settings")]
public class GenerationSettings : ScriptableObject
{
    public List<PlatformTypeSpawnRule> platformSpawnRules;
    public List<BoosterTypeSpawnRule> boosterSpawnRules;
    public TrapTypeSpawnRule trapSpawnRules;
    public float minPlatformStepY = 0.8f;
    public float maxPlatformStepY = 1.5f;
    public float minTrapStepY = 1f;
    public float maxTrapStepY = 1.3f;
    public float maxJumpY = 2.5f;
    public float levelHeight = 200f;
}
