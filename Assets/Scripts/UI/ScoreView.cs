using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreView : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;
    private SignalBus _signalBus;
    private string _text;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        _text = _scoreText.text;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<ScoreChangedSignal>(OnScoreChanged);
    }

    private void OnScoreChanged(ScoreChangedSignal signal)
    {
        _scoreText.text = _text + signal.Score;
    }
}
