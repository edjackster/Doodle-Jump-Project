using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JetpackAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    public void StopSound()
    {
        _audioSource.Stop();
    }
}