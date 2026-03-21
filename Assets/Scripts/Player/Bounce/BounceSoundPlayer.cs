using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class BounceSoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<BounceSignal>(PlayBounceSound);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<BounceSignal>(PlayBounceSound);
    }

    private void PlayBounceSound(BounceSignal signal)
    {
        if (signal.Type == JumpSourceType.Platform) 
            _audioSource.Play();
    }
}