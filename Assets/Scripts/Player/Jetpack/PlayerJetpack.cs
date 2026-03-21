using UnityEngine;
using Zenject;

[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(JetpackAnimator))]
[RequireComponent(typeof(JetpackAudio))]
public class PlayerJetpack: MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _duration; 
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private AnimationClip _jetpackAnimation;
    [SerializeField] private PlayerFallOffJetpack _fallOffJetpack;
    
    private SignalBus _signalBus;
    private JetpackAnimator _jetpackAnimator;
    private JetpackAudio _audio;
    private Timer _timer;
    private bool _isFlying = false;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _jetpackAnimator  = GetComponent<JetpackAnimator>();
        _timer = gameObject.GetComponent<Timer>();
        _audio = GetComponent<JetpackAudio>();
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<PickUpSignal>(OnPickUp);
        _signalBus.Subscribe<WinSignal>(StopJetpack);
        _signalBus.Subscribe<RestartSignal>(StopJetpack);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PickUpSignal>(OnPickUp);
        _signalBus.Unsubscribe<WinSignal>(StopJetpack);
        _signalBus.Unsubscribe<RestartSignal>(StopJetpack);
    }

    private void FixedUpdate()
    {
        if (_isFlying)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _force);
        }
    }

    public void StopJetpack()
    {
        _timer.Stop();
        _isFlying = false;
        _audio.StopSound();
        _jetpackAnimator.StopAnimation();
    }

    private void OnPickUp(PickUpSignal signal)
    {
        if(signal.PickUp is Jetpack == false)
            return;
        
        _jetpackAnimator.StartAnimation();
        _timer.TimesUp += OnFlightEnd;
        _timer.Stop();
        _timer.SetTime(_duration);
        _isFlying = true;
        
        _audio.PlaySound();
    }

    private void OnFlightEnd()
    {
        _timer.TimesUp -= OnFlightEnd;
        _isFlying = false;
        
        _fallOffJetpack.FallOff(transform);
        _jetpackAnimator.StopAnimation();
        _audio.StopSound();
    }
}