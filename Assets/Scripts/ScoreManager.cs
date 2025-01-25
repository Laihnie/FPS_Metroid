using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public Text scoreText;
    private int score = 0;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        UpdateScoreUI();
    }

    public void ScoreEnhance()
    {
       
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
       
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}