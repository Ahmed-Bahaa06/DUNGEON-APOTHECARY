using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private GameObject panel;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        panel.SetActive(false);
    }
    private void Start()
    {
        GameManager.Instance.OnGameOver += GameOver;

        restartButton.onClick.AddListener(OnRestartButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void GameOver()
    {
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        int score = ScoreManager.Instance.GetScore();
        int highScore = SaveManager.Instance.GetHighScore();

        if (score > highScore)
        {
            SaveManager.Instance.SetHighScore(score);
            highScore = score;
        }

        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();

        panel.SetActive(true);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;

            canvasGroup.alpha = timer / fadeDuration;

            yield return null;
        }

        canvasGroup.alpha = 1f;

        Time.timeScale = 0f;
    }


    private void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        Loader.Load(Loader.Scene.GameScene);
    }

    private void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
