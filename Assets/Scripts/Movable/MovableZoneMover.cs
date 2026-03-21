using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MovableZoneMover : MonoBehaviour
{
    [SerializeField] private PlayerStopper _playerStopper;
    
    private SignalBus _signalBus;
    private List<Movable> _platforms = new List<Movable>();

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void Awake()
    {
        _platforms.AddRange(GetComponentsInChildren<Platform>());
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<MovableSpawnedSignal>(OnSpawned);
        _signalBus.Subscribe<MovableDespawnedSignal>(OnDespawned);
        _signalBus.Subscribe<StopSignal>(OnStop);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MovableSpawnedSignal>(OnSpawned);
        _signalBus.Unsubscribe<MovableDespawnedSignal>(OnDespawned);
        _signalBus.Unsubscribe<StopSignal>(OnStop);
    }

    private void OnSpawned(MovableSpawnedSignal signal)
    {
        _platforms.Add(signal.Movable);
    }

    private void OnDespawned(MovableDespawnedSignal signal)
    {
        _platforms.Remove(signal.Movable);
    }

    private void OnStop(StopSignal signal)
    {
        MovePlatforms(signal.Distance);
    }

    private void MovePlatforms(float delta)
    {
        foreach (var platform in _platforms)
            platform.transform.Translate(Vector3.down * delta,  Space.World);
    }
}
