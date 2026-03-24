using UnityEngine;

[CreateAssetMenu(menuName = "Settings/BoosterSpawnRule")]
public class BoosterTypeSpawnRule: ScriptableObject
{
    [SerializeField] private BoosterType _type;
    [SerializeField] private float _chance;
    
    public BoosterType Type => _type;
    public float Chance => _chance;
}