using UnityEngine;
using Zenject;

public class FallDetector : MonoBehaviour
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
        _playerDetector.PlayerDetected += OnFall;
    }

    private void OnDisable()
    {
        _playerDetector.PlayerDetected -= OnFall;
    }

    private void OnFall(Player _)
    {
        _signalBus.Fire(new FallSignal());
    }
}
