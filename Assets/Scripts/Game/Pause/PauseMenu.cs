using UnityEngine;
using Zenject;

public class PauseMenu: MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<PauseSignal>(Pause);
        _signalBus.Subscribe<UnpauseSignal>(Resume);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<PauseSignal>(Pause);
        _signalBus.Unsubscribe<UnpauseSignal>(Resume);
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        _pausePanel.gameObject.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        _pausePanel.gameObject.SetActive(false);
    }
}