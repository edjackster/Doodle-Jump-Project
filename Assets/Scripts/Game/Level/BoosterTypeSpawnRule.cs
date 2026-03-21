using UnityEngine;

[CreateAssetMenu(menuName = "Settings/BoosterSpawnRule")]
public class BoosterTypeSpawnRule: ScriptableObject
{
    [field: SerializeField] public BoosterType Type { get; private set; }
    [field: SerializeField] public float Chance { get; private set; } // Передаём высоту, возвращаем шанс [0..1]
}