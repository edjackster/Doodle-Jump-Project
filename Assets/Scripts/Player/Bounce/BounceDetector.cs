using UnityEngine;
using Zenject;

public class BounceDetector : MonoBehaviour
{
    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Bouncy bouncy) == false)
            return;
        
        foreach (ContactPoint2D point in other.contacts)
        {
            if (point.normal.y > 0.5f)
            {
                JumpSourceType type;
                
                switch (bouncy)
                {
                    case BouncyPlatform :
                        type = JumpSourceType.Platform;
                        break;
                    
                    case Spring spring:
                        type = JumpSourceType.Spring;
                        spring.Release();
                        break;
                    
                    default:
                        type = JumpSourceType.Platform;
                        break;
                }
                
                _signalBus.Fire(new BounceSignal(type));
                return; 
            }
        }
    }
}
