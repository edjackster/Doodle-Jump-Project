using System;
using Zenject;

public class GameStateService: IInitializable, IDisposable
{
    private SignalBus _signalBus;
    private GameState _state = GameState.Running;

    public GameStateService(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<FinishSignal>(Win);
        _signalBus.Subscribe<FallSignal>(Lose);
        _signalBus.Subscribe<FallInHallSignal>(Lose);
        _signalBus.Subscribe<RestartButtonSignal>(OnRestartPress);
        _signalBus.Subscribe<PauseButtonSignal>(SwitchPause);
        _signalBus.Subscribe<ResumeAdWatchedSignal>(Resume);
        _signalBus.Subscribe<MenuButtonSignal>(TurnPauseOff);
    }
    
    public void Dispose()
    {
        _signalBus.Unsubscribe<FinishSignal>(Win);
        _signalBus.Unsubscribe<FallSignal>(Lose);
        _signalBus.Unsubscribe<FallInHallSignal>(Lose);
        _signalBus.Unsubscribe<RestartButtonSignal>(OnRestartPress);
        _signalBus.Unsubscribe<PauseButtonSignal>(SwitchPause);
        _signalBus.Unsubscribe<ResumeAdWatchedSignal>(Resume);
        _signalBus.Unsubscribe<MenuButtonSignal>(TurnPauseOff);
    }

    private void OnRestartPress()
    {
        _state = GameState.Running;
        _signalBus.Fire(new RestartSignal());
    }

    private void SwitchPause()
    {
        switch (_state)
        {
            case GameState.Running:
                TurnPauseOn();
                break;
            
            case GameState.Pause:
                TurnPauseOff();
                break;
        }
    }

    private void TurnPauseOn()
    {
        _state = GameState.Pause;
        _signalBus.Fire(new PauseSignal());
    }

    private void TurnPauseOff()
    {
        _state = GameState.Running;
        _signalBus.Fire(new UnpauseSignal());
    }

    private void Win()
    {
        if(_state != GameState.Running)
            return;
        
        _signalBus.Fire(new WinSignal());
        _state = GameState.Win;
    }

    private void Lose()
    {
        if(_state != GameState.Running)
            return;
        
        _signalBus.Fire(new LoseSignal());
        _state = GameState.GameOver;
    }

    private void Resume()
    {
        if(_state != GameState.GameOver)
            return;
        
        _signalBus.Fire(new ResumeSignal());
        _state = GameState.Running;
    }
}
