using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

        Time.timeScale = 1f;
    }

    private void Start()
    {
        highScoreText.text = SaveManager.Instance.GetHighScore().ToString();
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnPlayButtonClicked()
    {
        Loader.Load(Loader.Scene.GameScene);
    }
}