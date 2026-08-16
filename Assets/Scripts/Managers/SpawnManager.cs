using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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

    private bool canSpawn;

    private const int MaxMonstersPerSide = 3;
    private const int MaxChasingMonsters = 2;

    private List<Monster>[] waitingQueues =
    {
        new List<Monster>(),
        new List<Monster>()
    };

    private int chasingMonsters;

    private float spawnTimer;

    private Dictionary<Monster, ObjectPool<Monster>> monsterPools = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        CreatePools();
    }

    private void Start()
    {
        spawnTimer = 5f;
    }

    private void Update()
    {
        if (!canSpawn) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            TrySpawn();
            spawnTimer = GameManager.Instance.SpawnInterval;
        }
    }

    private void CreatePools()
    {
        foreach (Monster monsterPrefab in monsters)
        {
            ObjectPool<Monster> pool = new ObjectPool<Monster>(
                () => CreateMonster(monsterPrefab),

                monster =>
                {
                    monster.gameObject.SetActive(true);
                },

                monster =>
                {
                    monster.ResetMonster();
                    monster.gameObject.SetActive(false);
                },

                monster =>
                {
                    Destroy(monster.gameObject);
                },

                true,
                8,
                15
            );

            monsterPools.Add(monsterPrefab, pool);
        }
    }

    private Monster CreateMonster(Monster monsterPrefab)
    {
        Monster monster = Instantiate(monsterPrefab);

        monster.SetPrefab(monsterPrefab);

        monster.gameObject.SetActive(false);

        return monster;
    }

    public void Resume()
    {
        canSpawn = true;
        spawnTimer = 0.5f * GameManager.Instance.SpawnInterval;
    }

    public void Stop()
    {
        canSpawn = false;
    }

    private void TrySpawn()
    {
        bool leftHasSpace = waitingQueues[0].Count < MaxMonstersPerSide;
        bool rightHasSpace = waitingQueues[1].Count < MaxMonstersPerSide;

        if (!leftHasSpace && !rightHasSpace)
            return;

        if (leftHasSpace && rightHasSpace)
        {
            Spawn(Random.Range(0, 2));
            return;
        }

        if (leftHasSpace)
            Spawn(0);
        else
            Spawn(1);
    }

    private void Spawn(int side)
    {
        int idx = Random.Range(0, monsters.Length);

        float sign = side == 0 ? -1f : 1f;

        Vector3 spawnPos = new Vector3(
            sign * spawnPosX,
            0f,
            0f
        );

        Monster monster = monsterPools[monsters[idx]].Get();

        monster.transform.SetPositionAndRotation(
            spawnPos,
            Quaternion.identity
        );

        waitingQueues[side].Add(monster);

        monster.Initialize(
            GetWaitingPosition(side, waitingQueues[side].Count - 1),
            entrancePoints[side].position,
            side
        );

        monster.ChangeState(monster.WaitingState);
    }
    public void ReleaseMonster(Monster monster)
    {
        if (!monsterPools.TryGetValue(monster.Prefab, out ObjectPool<Monster> pool))
            return;

        pool.Release(monster);
    }

    public void MonsterLeftWaiting(Monster monster)
    {
        int side = monster.Side;

        if (!waitingQueues[side].Remove(monster))
            return;

        StartCoroutine(UpdateQueue(side));
    }

    private IEnumerator UpdateQueue(int side)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < waitingQueues[side].Count; i++)
        {
            waitingQueues[side][i].SetWaitingPoint(
                GetWaitingPosition(side, i)
            );
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