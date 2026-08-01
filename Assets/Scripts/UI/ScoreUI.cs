using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject addedText;
    [SerializeField] private Animator containerAnimator;
    [SerializeField] private Animator addedTextAnimator;

    private void Awake()
    {
        scoreText.text = "0";
    }

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        ScoreManager.Instance.OnMilestoneAchived += ScoreManager_OnMilestoneAchived;
    }

    private void ScoreManager_OnMilestoneAchived()
    {
        containerAnimator.SetTrigger("Milestone");
    }

    private void ScoreManager_OnScoreChanged()
    {
        scoreText.text = ScoreManager.Instance.GetScore().ToString();

        addedText.SetActive(true);

        containerAnimator.SetTrigger("Pop");
        addedTextAnimator.SetTrigger("Float");
    }

    public void Animation_Finished()
    {
        addedText.SetActive(false);
    }
}
