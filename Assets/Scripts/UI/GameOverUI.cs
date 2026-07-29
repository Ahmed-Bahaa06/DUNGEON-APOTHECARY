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
    [SerializeField] private GameObject panel;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        panel.SetActive(false);
    }
    private void Start()
    {
        GameManager.Instance.OnGameOver += GameOver;
        restartButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene");
        });
    }

    private void GameOver()
    {
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        scoreText.text = ScoreManager.Instance.GetScore().ToString();

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
}
