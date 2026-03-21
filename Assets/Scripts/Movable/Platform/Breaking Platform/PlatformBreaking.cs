using UnityEngine;

[RequireComponent(typeof(BreakDetector))]
[RequireComponent(typeof(PlatformBreakingAnimator))]
[RequireComponent(typeof(PlatformBreakingMover))]
[RequireComponent(typeof(PlatformBreakingSoundPlayer))]
public class PlatformBreaking : Platform
{
    private BreakDetector _breakDetector;
    private PlatformBreakingAnimator _animator;
    private PlatformBreakingMover _mover;
    private PlatformBreakingSoundPlayer _soundPlayer;
    
    [field: SerializeField]  public override PlatformType Type { get; protected set; } = PlatformType.Breakable;

    private void Awake()
    {
        _breakDetector = GetComponent<BreakDetector>();
        _animator = GetComponent<PlatformBreakingAnimator>();
        _mover = GetComponent<PlatformBreakingMover>();
        _soundPlayer = GetComponent<PlatformBreakingSoundPlayer>();
    }

    private void OnEnable()
    {
        _breakDetector.OnBreak += Break;
    }

    private void OnDisable()
    {
        _breakDetector.OnBreak -= Break;
    }

    private void Break()
    {
        _animator.Break();
        _mover.Fall();
        _soundPlayer.Play();
    }
}