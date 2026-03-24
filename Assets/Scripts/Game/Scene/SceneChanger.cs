using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneChanger : MonoBehaviour
{
    private const string MenuSceneName = "Menu";
    private const string LevelSceneName = "Level";
    
    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<MenuButtonSignal>(OnMenuPress);
        _signalBus.Subscribe<StartButtonSignal>(OnStartPress);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MenuButtonSignal>(OnMenuPress);
        _signalBus.Unsubscribe<StartButtonSignal>(OnStartPress);
    }

    private void OnMenuPress(MenuButtonSignal signal)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MenuSceneName);
    }

    private void OnStartPress(StartButtonSignal signal)
    {
        SceneManager.LoadScene(LevelSceneName);
    }
}
