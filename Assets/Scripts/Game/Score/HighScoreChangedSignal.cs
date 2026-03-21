public class HighScoreChangedSignal
{
    public int Score { get; }
    
    public HighScoreChangedSignal(int score)
    {
        Score = score;
    }
}