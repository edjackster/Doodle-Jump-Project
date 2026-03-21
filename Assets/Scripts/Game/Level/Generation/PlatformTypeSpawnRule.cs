using UnityEngine;

[CreateAssetMenu(menuName = "Settings/PlatformSpawnRule")]
public class PlatformTypeSpawnRule: ScriptableObject
{
    [field: SerializeField] public PlatformType Type { get; private set; }
    [field: SerializeField] public AnimationCurve ChanceByHeight { get; private set; }
    [field: SerializeField] public float MinSpawnHeight { get; private set; }
    [field: SerializeField] public bool CanSpawnBooster { get; private set; }
}