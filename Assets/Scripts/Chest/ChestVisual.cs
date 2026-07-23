using UnityEngine;
using System;

public class ChestVisual : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private Chest chest;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        chest.OnChestOpened += Chest_OnChestOpened;
        chest.OnChestRefilled += Chest_OnChestRefilled;
    }

    private void OnDisable()
    {
        chest.OnChestOpened -= Chest_OnChestOpened;
        chest.OnChestRefilled -= Chest_OnChestRefilled;
    }

    private void Chest_OnChestOpened()
    {
        animator.SetTrigger("OpenChest");
        item.SetActive(false);
    }

    private void Chest_OnChestRefilled()
    {
        animator.SetTrigger("CloseChest");
    }

    public void Animation_CloseFinished()
    {
        item.SetActive(true);

        chest.FinishRefill();

        // Play sound
    }
}
