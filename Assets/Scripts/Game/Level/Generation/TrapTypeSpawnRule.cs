using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Trap Spawn Rule")]
public class TrapTypeSpawnRule: ScriptableObject
{
    [SerializeField] private AnimationCurve _chanceByHeight;
    [SerializeField] private float _minSpawnHeight;
    [SerializeField] private float _trapSafeSpawnDistance;
    
    public AnimationCurve ChanceByHeight => _chanceByHeight;
    public float MinSpawnHeight => _minSpawnHeight;
    public float TrapSafeSpawnDistance => _trapSafeSpawnDistance;
}