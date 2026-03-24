using UnityEngine;
using Zenject;

public class MovableSpawner : MonoBehaviour
{
    private const float StartDistance = 9.5f;
    private const float FinishDistance = 3f;
    
    [SerializeField] private float _xRange = 1f;
    [SerializeField] private Finish _finishPrefab;
    
    private float _distance;
    private int _currentPlatformIndex;
    private int _currentTrapIndex;
    private bool _isFinishSpawned = false;
    private LevelData _movableData;
    
    private PlatformPool _platformPool;
    private TrapPool _trapPool;
    private SignalBus _signalBus;
    private LevelGeneratorService _levelGeneratorService;

    [Inject]
    public void Construct(LevelGeneratorService generatorService, PlatformPool platformPool, TrapPool trapPool,SignalBus signalBus)
    {
        _levelGeneratorService = generatorService;
        _platformPool = platformPool;
        _trapPool = trapPool;
        _signalBus = signalBus;
        RestoreInitialState();
    }

    private void Start()
    {
        DistanceOvercame(0f);
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<MovableDetectedSignal>(Push);
        _signalBus.Subscribe<RestartSignal>(Restart);
        _signalBus.Subscribe<StopSignal>(OnStop);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MovableDetectedSignal>(Push);
        _signalBus.Unsubscribe<RestartSignal>(Restart);
        _signalBus.Unsubscribe<StopSignal>(OnStop);
    }

    private void RestoreInitialState()
    {
        _finishPrefab.gameObject.SetActive(false);
        _signalBus.Fire(new MovableDespawnedSignal(_finishPrefab));
        _movableData = _levelGeneratorService.Generate();
        _currentPlatformIndex = 0;
        _currentTrapIndex = 0;
        _distance = StartDistance;
    }

    private void OnStop(StopSignal signal)
    {
        DistanceOvercame(signal.Distance);
    }

    private void DistanceOvercame(float deltaDistance)
    {
        _distance += deltaDistance;

        if ( _currentPlatformIndex == _movableData.PlatformsData.Count)
        {
            if(_isFinishSpawned == false && _movableData.PlatformsData[_currentPlatformIndex - 1].Y + FinishDistance < _distance)
            {
                _isFinishSpawned = true;
                _finishPrefab.gameObject.SetActive(true);
                _finishPrefab.transform.localPosition = Vector3.zero;
                _signalBus.Fire(new MovableSpawnedSignal(_finishPrefab));
            }
            
            return;
        }
        
        while (_currentPlatformIndex < _movableData.PlatformsData.Count && _movableData.PlatformsData[_currentPlatformIndex].Y < _distance)
        {
            Spawn(_movableData.PlatformsData[_currentPlatformIndex]);
            _currentPlatformIndex++;
        }
        
        while (_currentTrapIndex < _movableData.TrapsData.Count && _movableData.TrapsData[_currentTrapIndex].Y < _distance)
        {
            Spawn(_movableData.TrapsData[_currentTrapIndex]);
            _currentTrapIndex++;
        }
    }

    private void Spawn(PlatformGenerationData platformData)
    {
        float xPos = platformData.X * _xRange;
        float yPos = transform.position.y + platformData.Y - _distance;
        
        Platform platform = _platformPool.Get(platformData.Type);
        platform.transform.parent = transform;
        platform.Reload();
        platform.AddBooster(platformData.BoosterType);
        
        _signalBus.Fire(new MovableSpawnedSignal(platform));

        platform.transform.position = new Vector3(xPos, yPos, 0);
        platform.gameObject.SetActive(true);
    }

    private void Spawn(TrapGenerationData platformData)
    {
        float xPos = platformData.X * _xRange;
        float yPos = transform.position.y + platformData.Y - _distance;
        
        Hole trap = _trapPool.Get();
        trap.transform.parent = transform;
        
        _signalBus.Fire(new MovableSpawnedSignal(trap));

        trap.transform.position = new Vector3(xPos, yPos, 0);
        trap.gameObject.SetActive(true);
    }

    private void Restart(RestartSignal signal)
    {
        foreach (var child in transform.GetComponentsInChildren<Movable>()) {
            if(child.gameObject.activeSelf)
                Despawn(child);
            // child.gameObject.SetActive(false);
        }
        
        _isFinishSpawned = false;
        RestoreInitialState();
        DistanceOvercame(0f);
    }

    private void Push(MovableDetectedSignal signal)
    {
        Despawn(signal.Movable);
    }

    private void Despawn(Movable movable)
    {
        if (movable is Platform platform)
            _platformPool.Return(platform);
        
        if (movable is Hole hole)
            _trapPool.Return(hole);
        
        _signalBus.Fire(new MovableDespawnedSignal(movable));
    }
}