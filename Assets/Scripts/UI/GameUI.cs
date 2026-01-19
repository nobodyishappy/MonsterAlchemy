using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;

    [Header("Ending Screen")]
    [SerializeField] private Transform endScreen;
    [SerializeField] private Transform scoreList;

    [Header("Pause Screen")]
    [SerializeField] private Transform pauseScreen; 

    private void Awake()
    {
        endScreen.gameObject.SetActive(false);
    }

    private void OnEnable() 
    {
        GameHandler.Instance.ScoreHandler.OnScoreChanged += UpdateScoreUI;
        UpdateScoreUI(0);
    }

    private void OnDisable()
    {
        GameHandler.Instance.ScoreHandler.OnScoreChanged -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateTimerUI(float currentTime)
    {
        if (currentTime < 0)
        {
            currentTime = 0;
        } else if (currentTime > 0)
        {
            currentTime++;
        }

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndGame()
    {
        int basePoints = GameHandler.Instance.ScoreHandler.PositiveScore;
        int bonusPoints = GameHandler.Instance.ScoreHandler.BonusScore;
        int negativePoints = GameHandler.Instance.ScoreHandler.NegativeScore;

        for(int i = 0; i < 4; i++)
        {
            string scoreName = scoreList.GetChild(i).name;
            TMP_Text scoreText = scoreList.GetChild(i).GetComponent<TMP_Text>();

            if (scoreName == "CustomerServed")
            {
                scoreText.text = basePoints.ToString();
            } else if (scoreName == "Tips")
            {
                scoreText.text = bonusPoints.ToString();
            } else if (scoreName == "CustomerMissed")
            {
                scoreText.text = negativePoints.ToString();
            } else if (scoreName == "ScoreTotal")
            {
                int total = basePoints + bonusPoints + negativePoints;
                scoreText.text = total.ToString();
            }
        }

        endScreen.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
