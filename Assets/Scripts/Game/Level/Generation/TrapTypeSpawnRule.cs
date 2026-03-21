using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Trap Spawn Rule")]
public class TrapTypeSpawnRule: ScriptableObject
{
    [field: SerializeField] public AnimationCurve ChanceByHeight { get; private set; }
    [field: SerializeField] public float MinSpawnHeight { get; private set; }
    [field: SerializeField] public float TrapSafeSpawnDistance { get; private set; }
}