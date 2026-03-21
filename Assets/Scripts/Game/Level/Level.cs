using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<RestartSignal>(ReturnPlayer);
        _signalBus.Subscribe<ResumeSignal>(ReturnPlayer);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<RestartSignal>(ReturnPlayer);
        _signalBus.Unsubscribe<ResumeSignal>(ReturnPlayer);
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            _signalBus.Fire(new RestartSignal());
        
        if(Input.GetKeyDown(KeyCode.T))
            _signalBus.Fire(new PickUpSignal(new Jetpack()));
    }

    private void ReturnPlayer()
    {
        _player.transform.position = _spawnPoint.position;
    }
}
