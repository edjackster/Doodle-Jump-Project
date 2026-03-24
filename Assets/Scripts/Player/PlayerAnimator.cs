using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const string FallAnimationName = "Fall in Hole";
    private const string IdleAnimationName = "idle";
    
    [SerializeField] private Sprite _idle;
    [SerializeField] private Sprite _jump;
    [SerializeField] private float _minVelocityToJump;
    
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_rigidbody.velocity.y < _minVelocityToJump)
            _spriteRenderer.sprite = _idle;
        else
            _spriteRenderer.sprite = _jump;
            
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<RestartSignal>(Reset);
        _signalBus.Subscribe<ResumeSignal>(Reset);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<RestartSignal>(Reset);
        _signalBus.Unsubscribe<ResumeSignal>(Reset);
    }

    public void PlayFallAnimation()
    {
        _animator.Play(FallAnimationName);
    }

    private void Reset()
    {
        _animator.Play(IdleAnimationName);
    }
}
