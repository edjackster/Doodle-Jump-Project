using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GenerationSettings _settings;
    [SerializeField] private PlatformPool _platformPool;
    [SerializeField] private TrapPool _trapPool;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        DeclareSoundSignals();
        DeclareTriggerSignals();
        DeclareGameStateSignals();
        DeclareMovableSignals();
        DeclareUISignals();

        Container.BindInstance(_settings).AsSingle();
        Container.BindInstance(_platformPool).AsSingle();
        Container.BindInstance(_trapPool).AsSingle();

        if (Application.isMobilePlatform)
        {
            Container.BindInterfacesTo<MobileInput>().AsSingle(); // чтобы он ещё ITickable получил
        }
        else
        {
            Container.BindInterfacesTo<DesktopInput>().AsSingle();
        }

        Container.BindInterfacesTo<GameStateService>().AsSingle();
        Container.BindInterfacesTo<ScoreCounterService>().AsSingle();
        Container.BindInterfacesTo<AdService>().AsSingle();
        Container.Bind<LevelGeneratorService>().AsSingle();
    }

    private void DeclareUISignals()
    {
        Container.DeclareSignal<PauseButtonSignal>();
        Container.DeclareSignal<RestartButtonSignal>();
        Container.DeclareSignal<ResumeButtonSignal>();
        Container.DeclareSignal<MenuButtonSignal>();
        Container.DeclareSignal<StartButtonSignal>();
    }

    private void DeclareSoundSignals()
    {
        Container.DeclareSignal<BounceSignal>();
        Container.DeclareSignal<SpringReleaseSignal>();
    }

    private void DeclareMovableSignals()
    {
        Container.DeclareSignal<MovableDetectedSignal>();
        Container.DeclareSignal<MovableSpawnedSignal>();
        Container.DeclareSignal<MovableDespawnedSignal>();
    }

    private void DeclareTriggerSignals()
    {
        Container.DeclareSignal<PickUpSignal>();
        Container.DeclareSignal<FallSignal>();
        Container.DeclareSignal<FinishSignal>();
        Container.DeclareSignal<FallInHallSignal>();
        Container.DeclareSignal<StopSignal>();
        Container.DeclareSignal<ScoreChangedSignal>();
        Container.DeclareSignal<HighScoreChangedSignal>();
        Container.DeclareSignal<ResumeAdWatchedSignal>();
    }

    private void DeclareGameStateSignals()
    {
        Container.DeclareSignal<ResumeSignal>();
        Container.DeclareSignal<RestartSignal>();
        Container.DeclareSignal<LoseSignal>();
        Container.DeclareSignal<WinSignal>();
        Container.DeclareSignal<PauseSignal>();
        Container.DeclareSignal<UnpauseSignal>();
    }
}