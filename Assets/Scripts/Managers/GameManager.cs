using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [Header("Game Settings")]
    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 8f;
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float spawnDecrease = 1f;

    [Header("Patience Settings")]
    [SerializeField] private float monsterPatience = 20f;
    [SerializeField] private float minPatience = 6f;
    [SerializeField] private float patienceDecrease = 1f;

    public float SpawnInterval => spawnInterval;
    public float MonsterPatience => monsterPatience;

    public bool IsGameOver { get; private set; }
    public bool IsPaused { get; private set; }

    public event Action OnGameOver;
    public event Action OnGamePaused;
    public event Action OnGameUnpaused;

    private int monstersServed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        Player.Instance.health.OnPlayerDied += PlayerDied;
        Monster.OnMonsterServed += Monster_OnMonsterServed;
        PlayerInput.Instance.OnPauseAction += PlayerInput_OnPauseAction;
    }

    private void PlayerInput_OnPauseAction()
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (IsGameOver) return;

        if (IsPaused)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;

        OnGamePaused?.Invoke();
    }

    private void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;

        OnGameUnpaused?.Invoke();
    }

    private void Monster_OnMonsterServed()
    {
        monstersServed++;

        if (monstersServed % 3 == 0)
        {
            monsterPatience = Mathf.Max(minPatience, monsterPatience - patienceDecrease);
        }

        if (monstersServed % 5 == 0)
        {
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnDecrease);
        }
    }

    private void PlayerDied()
    {
        if (IsGameOver) return;

        IsGameOver = true;
        OnGameOver?.Invoke();
    }

   
}
