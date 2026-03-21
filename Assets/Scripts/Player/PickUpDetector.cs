using UnityEngine;
using Zenject;

public class PickUpDetector : MonoBehaviour
{
    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out PickUp pickUp) == false)
            return;
        
        _signalBus.Fire(new PickUpSignal(pickUp));
    }
}