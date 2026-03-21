using System;
using Zenject;

public class ScoreCounterService: IDisposable
{
    private SignalBus _signalBus;
    private float _score = 0f;
    private float _highScore = 0f;
    
    public ScoreCounterService(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<StopSignal>(OnStop);
        _signalBus.Subscribe<RestartSignal>(OnRestart);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<StopSignal>(OnStop);
        _signalBus.Unsubscribe<RestartSignal>(OnRestart);
    }

    private void OnStop(StopSignal signal)
    {
        _score += signal.Distance;
        
        if (_score > _highScore)
        {
            _highScore = _score;
        }
        
        UpdateScore();
        UpdateHighScore();
    }

    private void OnRestart(RestartSignal signal)
    {
        _score = 0f;
        UpdateScore();
    }

    private void UpdateScore()
    {
        _signalBus.Fire(new ScoreChangedSignal(Convert.ToInt32(_score)));
    }

    private void UpdateHighScore()
    {
        _signalBus.Fire(new HighScoreChangedSignal(Convert.ToInt32(_highScore)));
    }
}
