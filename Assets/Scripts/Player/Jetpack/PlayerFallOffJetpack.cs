using UnityEngine;

public class PlayerFallOffJetpack: MonoBehaviour
{
    private const string PickUpTriggerName = "Fall";
    
    [SerializeField]private Animator _animator;

    public void FallOff(Transform target)
    {
        transform.position = target.position;
        transform.localScale = target.lossyScale;
        
        _animator.SetTrigger(PickUpTriggerName);
    }
}