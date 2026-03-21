using UnityEngine;
using Zenject;

public class PlayerStopper : MonoBehaviour
{
    [SerializeField] private float _stopHeight = 0f;
    
    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    void Update()
    {
        if (transform.position.y > _stopHeight)
        {
            var dif = transform.position.y;
            transform.position = new Vector3(transform.position.x, _stopHeight, transform.position.z);
            _signalBus.Fire(new StopSignal(dif));
        }
    }
}
