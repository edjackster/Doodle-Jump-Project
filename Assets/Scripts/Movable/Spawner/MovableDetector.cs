using UnityEngine;
using Zenject;

public class MovableDetector : MonoBehaviour
{
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out Movable movable))
        {
            _signalBus.Fire(new MovableDetectedSignal(movable));
        }
    }
}
