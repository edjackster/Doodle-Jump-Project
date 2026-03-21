using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class SpringAudioSource : MonoBehaviour
{
    private AudioSource _audioSource;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<SpringReleaseSignal>(Play);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<SpringReleaseSignal>(Play);
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        _audioSource.Play();
    }
}
