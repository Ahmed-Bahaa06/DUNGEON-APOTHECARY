using System;
using System.Collections;
using System.Collections.Generic;
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

    public event Action OnGameOver;
    private int monstersServed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        Player.Instance.health.OnPlayerDied += PlayerDied;
        Monster.OnMonsterServed += Monster_OnMonsterServed;
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
