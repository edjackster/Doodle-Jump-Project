using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class JetpackAnimator : MonoBehaviour
{
    private const string StartTriggerName = "Start";

    private Animator _animator;
    private SpriteRenderer _renderer;
    
    private bool _isPlaying = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    public void StartAnimation()
    {
        if (_isPlaying == false)
            _animator.SetTrigger(StartTriggerName);
        
        _isPlaying = true;
        _renderer.enabled = true;
    }

    public void StopAnimation()
    {
        _isPlaying = false;
        _renderer.enabled = false;
    }
}