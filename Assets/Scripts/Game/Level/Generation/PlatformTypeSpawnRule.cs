using UnityEngine;

[CreateAssetMenu(menuName = "Settings/PlatformSpawnRule")]
public class PlatformTypeSpawnRule: ScriptableObject
{
    [SerializeField] private PlatformType _type;
    [SerializeField] private AnimationCurve _chanceByHeight;
    [SerializeField] private float _minSpawnHeight;
    [SerializeField] private bool _canSpawnBooster;
    
    public PlatformType Type => _type;
    public AnimationCurve ChanceByHeight => _chanceByHeight;
    public float MinSpawnHeight => _minSpawnHeight;
    public bool CanSpawnBooster => _canSpawnBooster;
}