using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlatformBreakingAnimator : MonoBehaviour
{
    private const string BreakTriggerName = "break";
    private const string ReloadTriggerName = "reload";
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Break()
    {
        _animator.SetTrigger(BreakTriggerName);
    }

    public void Reload()
    {
        _animator.SetTrigger(ReloadTriggerName);
    }
}
