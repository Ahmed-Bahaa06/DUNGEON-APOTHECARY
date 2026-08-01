using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event Action OnScoreChanged;
    public event Action OnMilestoneAchived;

    private int score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        Monster.OnMonsterServed += Monster_OnAnyMonsterServed;
    }

    private void Monster_OnAnyMonsterServed()
    {
        score++;

        if (score % 5 == 0)
        {
            OnMilestoneAchived?.Invoke();
        }

        OnScoreChanged?.Invoke();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return SaveManager.Instance.GetHighScore();
    }

    public void SaveHighScore()
    {
        if (score > SaveManager.Instance.GetHighScore())
        {
            SaveManager.Instance.SetHighScore(score);
        }
    }

    private void OnDisable()
    {
        Monster.OnMonsterServed -= Monster_OnAnyMonsterServed;
    }
}