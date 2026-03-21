using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private const float ZeroGravity = 0;

    [SerializeField] private PlayerJetpack _jetpack;
    [SerializeField] private AudioSource _holeSound;
    
    private PlayerMover _playerMover;
    private PlayerAnimator _playerAnimator;
    private Rigidbody2D _rigidbody;
    private SignalBus _signalBus;
    private float _gravityScale;
    
    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _gravityScale = _rigidbody.gravityScale;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<WinSignal>(FreezePlayer);
        _signalBus.Subscribe<LoseSignal>(TurnOffMovement);
        _signalBus.Subscribe<RestartSignal>(UnfreezePlayer);
        _signalBus.Subscribe<ResumeSignal>(UnfreezePlayer);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<WinSignal>(FreezePlayer);
        _signalBus.Unsubscribe<LoseSignal>(TurnOffMovement);
        _signalBus.Unsubscribe<RestartSignal>(UnfreezePlayer);
        _signalBus.Unsubscribe<ResumeSignal>(UnfreezePlayer);
    }

    public void FallInHall()
    {
        _playerAnimator.PlayFallAnimation();
        _jetpack.StopJetpack();
        _holeSound.Play();
        FreezePlayer();
    }

    public void EndFallInHall()
    {
        _signalBus.Fire<FallInHallSignal>();
    }

    private void FreezePlayer()
    {
        TurnOffMovement();
        FreezeFalling();
    }

    private void UnfreezePlayer()
    {
        TurnOnMovement();
        UnFreezeFalling();
    }

    private void TurnOffMovement()
    {
        _playerMover.enabled = false;
    }

    private void TurnOnMovement()
    {
        _playerMover.enabled = true;
    }

    private void FreezeFalling()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.gravityScale = ZeroGravity;
        _rigidbody.velocity = Vector2.zero;
    }

    private void UnFreezeFalling()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.gravityScale = _gravityScale;
    }
}
