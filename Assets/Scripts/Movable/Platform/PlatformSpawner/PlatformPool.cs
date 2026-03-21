using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlatformPool: MonoBehaviour
{
    [SerializeField] private List<Platform> _prefabs;
    
    private Dictionary<PlatformType, Queue<Platform>> _pools = new();
    private Dictionary<PlatformType, Platform> _prefabDictionary = new();
    private SignalBus _signalBus;

#if UNITY_EDITOR
    private void OnValidate()
    {
        var types = new HashSet<PlatformType>();
        foreach (var entry in _prefabs)
        {
            if (entry == null)
            {
                Debug.LogError($"Null prefab", this);
                return;
            }
            if (!types.Add(entry.Type))
                Debug.LogError($"Duplicate PlatformType in prefab list: {entry.Type}", this);
        }
    }
#endif

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        foreach (Platform prefab in _prefabs)
        {
            _prefabDictionary.Add(prefab.Type, prefab);
            _pools[prefab.Type] = new Queue<Platform>();
        }
    }

    public Platform Get(PlatformType type)
    {
        var queue = _pools[type];
        
        Platform platform;
        
        if (queue.Count > 0)
        {
            platform = queue.Dequeue();
            platform.gameObject.SetActive(true);
            return platform;
        }
        else
        {
            Platform prefab = _prefabDictionary[type];
            platform = Instantiate(prefab);
            platform.AddSignalBus(_signalBus);
            platform.gameObject.SetActive(true);
            return platform;
        }
    }

    public void Return(Platform platform)
    {
        platform.Reload();
        platform.gameObject.SetActive(false);
        _pools[platform.Type].Enqueue(platform);
    }
}