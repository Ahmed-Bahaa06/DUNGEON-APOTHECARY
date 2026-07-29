using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {  get; private set; }

    public event Action OnScoreChanged;

    private int score;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void OnEnable()
    {
        Monster.OnMonsterServed += Monster_OnAnyMonsterServed;
    }

    private void Monster_OnAnyMonsterServed()
    {
        score++;
        OnScoreChanged?.Invoke();
    }

    public int GetScore()
    {
        return score;
    }

    private void OnDisable()
    {
        Monster.OnMonsterServed += Monster_OnAnyMonsterServed;
    }
}
