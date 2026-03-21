using UnityEngine;
using Zenject;

[RequireComponent(typeof(SpringAnimationController))]
public class Spring : Bouncy
{
    private SpringAnimationController _animationController;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void Awake()
    {
        _animationController = GetComponent<SpringAnimationController>();
    }

    public void Release()
    {
        _signalBus.Fire(new SpringReleaseSignal());
        _animationController.Release();
    }

    public void Reload()
    {
        if(_animationController)
            _animationController.Reload();
    }
}
