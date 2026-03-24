using UnityEngine;
using Zenject;

public abstract class Platform: Movable
{
    [SerializeField] protected PlatformType _type;
    [SerializeField] protected Spring _spring;
    [SerializeField] protected Jetpack _jetpack;

    public PlatformType Type => _type;

    public void AddSignalBus(SignalBus signalBus)
    {
        _spring.Construct(signalBus);
    }

    public void AddBooster(BoosterType boosterType)
    {
        switch (boosterType)
        {
            case BoosterType.Spring:
                _spring.gameObject.SetActive(true);
                break;
            case BoosterType.Jetpack:
                _jetpack.gameObject.SetActive(true);
                break;
        }
    }

    public virtual void Reload()
    {
        _spring.Reload();
        _spring.gameObject.SetActive(false);
        _jetpack.gameObject.SetActive(false);
    }
}