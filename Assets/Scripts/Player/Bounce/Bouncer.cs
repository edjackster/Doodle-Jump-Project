using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Bouncer : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    [SerializeField] private float bounceSpringForce;

    private Rigidbody2D _rigidbody;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Bounce(bounceForce);
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<BounceSignal>(OnBounceSignal);
        _signalBus.Subscribe<RestartSignal>(StandardBounce);
        _signalBus.Subscribe<ResumeSignal>(StandardBounce);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<BounceSignal>(OnBounceSignal);
        _signalBus.Unsubscribe<RestartSignal>(StandardBounce);
        _signalBus.Unsubscribe<ResumeSignal>(StandardBounce);
    }

    private void StandardBounce()
    {
        Bounce(bounceForce);
    }

    private void OnBounceSignal(BounceSignal signal)
    {
        switch (signal.Type)
        {
            case JumpSourceType.Platform:
                Bounce(bounceForce);
                break;
            case JumpSourceType.Spring:
                Bounce(bounceSpringForce);
                break;
        }
    }

    private void Bounce(float force)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, force);
    }
}