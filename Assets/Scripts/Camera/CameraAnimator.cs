using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class CameraAnimator : MonoBehaviour
{
    private const string DefaultState = "Default State";
    private const string LoseState = "Lose Screan";
    private const string WinState = "Win Screan";
    
    private SignalBus _signalBus;
    private Animator _animator;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<LoseSignal>(OnLose);
        _signalBus.Subscribe<WinSignal>(OnWin);
        _signalBus.Subscribe<RestartSignal>(ResetCamera);
        _signalBus.Subscribe<ResumeSignal>(ResetCamera);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<LoseSignal>(OnLose);
        _signalBus.Unsubscribe<WinSignal>(OnWin);
        _signalBus.Unsubscribe<RestartSignal>(ResetCamera);
        _signalBus.Unsubscribe<ResumeSignal>(ResetCamera);
    }

    private void OnLose(LoseSignal signal)
    {
        _animator.Play(LoseState);
    }

    private void OnWin(WinSignal signal)
    {
        _animator.Play(WinState);
    }

    private void ResetCamera()
    {
        _animator.Play(DefaultState);
    }
}
