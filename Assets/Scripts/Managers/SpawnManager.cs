using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private Monster[] monsters;

    [Header("Spawn")]
    [SerializeField] private float spawnPosX = 12f;

    [Header("Waiting")]
    [SerializeField] private Transform[] waitingPoints;     // 0 = Left, 1 = Right

    [Header("Entrance")]
    [SerializeField] private Transform[] entrancePoints;    // 0 = Left, 1 = Right

    [SerializeField] private float waitingOffset = 1f;

    private const int MaxMonstersPerSide = 3;
    private const int MaxChasingMonsters = 2;

    private List<Monster>[] waitingQueues =
    {
        new List<Monster>(),
        new List<Monster>()
    };
    private int chasingMonsters;

    private float spawnTimer;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        spawnTimer = 5f; // First Spawn
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            TrySpawn();

            spawnTimer = GameManager.Instance.SpawnInterval;
        }
    }

    private void TrySpawn()
    {
        bool leftHasSpace = waitingQueues[0].Count < MaxMonstersPerSide;
        bool rightHasSpace = waitingQueues[1].Count < MaxMonstersPerSide;

        if (!leftHasSpace && !rightHasSpace) return;

        if (leftHasSpace && rightHasSpace)
        {
            Spawn(Random.Range(0, 2));
            return;
        }

        if (leftHasSpace) Spawn(0);

        else Spawn(1);
    }

    private void Spawn(int side)
    {
        int idx = Random.Range(0, monsters.Length);

        float sign = side == 0 ? -1f : 1f;

        Vector3 spawnPos = new Vector3(sign * spawnPosX, 0f, 0f);

        Monster monster = Instantiate(monsters[idx], spawnPos, Quaternion.identity);

        waitingQueues[side].Add(monster);

        monster.Initialize(
            GetWaitingPosition(side, waitingQueues[side].Count - 1),
            entrancePoints[side].position,
            side);
    }

    public void MonsterLeftWaiting(Monster monster)
    {
        int side = monster.Side;

        if (!waitingQueues[side].Remove(monster)) return;

        StartCoroutine(UpdateQueue(side));
    }

    private IEnumerator UpdateQueue(int side)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < waitingQueues[side].Count; i++)
        {
            waitingQueues[side][i].SetWaitingPoint(GetWaitingPosition(side, i));
        }
    }

    public bool CanStartChasing()
    {
        if (chasingMonsters >= MaxChasingMonsters)
            return false;

        chasingMonsters++;
        return true;
    }

    public void MonsterStoppedChasing()
    {
        chasingMonsters--;

        if (chasingMonsters < 0)
            chasingMonsters = 0;
    }

    private Vector3 GetWaitingPosition(int side, int index)
    {
        float sign = side == 0 ? -1f : 1f;

        Vector3 pos = waitingPoints[side].position;
        pos.x += sign * waitingOffset * index;

        return pos;
    }
}