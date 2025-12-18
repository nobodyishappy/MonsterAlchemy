using UnityEngine;

public interface IScoreHandler
{
    int PositiveScore { get; }
    int NegativeScore { get; }
    int BonusScore { get; }
    public event System.Action<int> OnScoreChanged;
    void AddScore(int baseAmount, int bonus);
    void ResetScore();
}
