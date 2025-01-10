using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public Text scoreText;
    private int score = 0;

    private void Awake()
    {
        // Assurez-vous qu'il n'y a qu'une seule instance de ScoreManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionnel : pour garder le ScoreManager entre les scènes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialiser le score à 0 au début du jeu
        UpdateScoreUI();
    }

    public void ScoreEnhance()
    {
        // Incrémenter le score
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        // Mettre à jour le texte du score
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}