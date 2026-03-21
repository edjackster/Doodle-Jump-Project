using UnityEngine;
using Zenject;

public class Finish : Movable
{
    [SerializeField] private PlayerDetector _playerDetector;
    
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void OnEnable()
    {
        _playerDetector.PlayerDetected += OnFinish;
    }

    private void OnDisable()
    {
        _playerDetector.PlayerDetected -= OnFinish;
    }

    private void OnFinish(Player _)
    {
        _signalBus.Fire(new FinishSignal());
    }
}
