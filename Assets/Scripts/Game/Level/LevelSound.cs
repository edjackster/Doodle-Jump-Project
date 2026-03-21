using UnityEngine;
using Zenject;

public class LevelSound : MonoBehaviour
{
    [SerializeField] private AudioSource _fallSound;
    [SerializeField] private AudioSource _winSound;
    
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<FallSignal>(OnFall);
        _signalBus.Subscribe<WinSignal>(OnWin);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<FallSignal>(OnFall);
        _signalBus.Unsubscribe<WinSignal>(OnWin);
    }

    private void OnFall(FallSignal signal)
    {
        _fallSound.Play();
    }

    private void OnWin(WinSignal signal)
    {
        _winSound.Play();
    }
}
