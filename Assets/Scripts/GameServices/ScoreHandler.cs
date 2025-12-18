public class ScoreHandler : IScoreHandler
{
    public int PositiveScore { get; private set;}
    public int NegativeScore { get; private set;}
    public int BonusScore { get; private set;}
    public event System.Action<int> OnScoreChanged;

    public void AddScore(int baseAmount, int bonus)
    {
        if (baseAmount < 0)
        {
            NegativeScore += baseAmount;
        } else
        {
            PositiveScore += baseAmount;
        }
        BonusScore += bonus;

        OnScoreChanged?.Invoke(PositiveScore + NegativeScore + BonusScore);
    }

    public void ResetScore()
    {
        PositiveScore = 0;
        NegativeScore = 0;
        BonusScore = 0;
    }
}
