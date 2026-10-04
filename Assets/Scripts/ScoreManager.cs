using UnityEngine;
using TMPro;
public class ScoreManager : MonoSingleton<ScoreManager>
{
    private int score;
    private int topScore;
    private int maxScore;
    [SerializeField] private Color currentColor;
    [SerializeField] private TMP_Text  scoreText;
    [SerializeField] private TMP_Text  topScoreText;
    private void OnEnable()
    {
        StateMachine.Instance.OnStateChanged += HandleStateChanged;
    }
    private void OnDisable()
    {
        StateMachine.Instance.OnStateChanged -= HandleStateChanged;
    
        
    }
    private void Start()
    {
        score = 0;
        topScore = 0;
        maxScore = 100;
        UpdateScoreText();
        scoreText.color = currentColor;
        topScoreText.color = Color.gold;
    }
    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
        topScoreText.text = $"Best Score: {topScore}";
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
        if (score >= maxScore)
        {
            MemoryGameMachine.Instance.SetWinningStatus(true);
            MemoryGameMachine.Instance.EndGame();
        }
    }

    private void HandleStateChanged(GameState state)
    {
        if (state == GameState.Preview)
        {
            score = 0;
            UpdateScoreText();
        }
        if (state == GameState.Results)
        {
            if (score > topScore)
            {
                topScore = score;
            }
            UpdateScoreText();
        }
    }
    public void SetMaxScore(int maxScore)
    {
        this.maxScore = maxScore;
    }
}