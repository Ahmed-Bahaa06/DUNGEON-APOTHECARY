using UnityEngine;
using System;

public class CraftingTableVisual : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private SpriteRenderer itemSpriteRenderer;

    [SerializeField] private CraftingTable craftingTable;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        itemSpriteRenderer = item.GetComponentInChildren<SpriteRenderer>();
        item.SetActive(false);
    }


    private void OnEnable()
    {
        craftingTable.OnCraftStarted += CraftingTable_OnCraftStarted;
        craftingTable.OnCraftFinished += CraftingTable_OnCraftFinished;
        craftingTable.OnItemTaken += CraftingTable_OnItemTaken;
    }

    private void OnDisable()
    {
        craftingTable.OnCraftStarted -= CraftingTable_OnCraftStarted;
        craftingTable.OnCraftFinished -= CraftingTable_OnCraftFinished;
        craftingTable.OnItemTaken -= CraftingTable_OnItemTaken;
    }

    private void CraftingTable_OnCraftStarted()
    {
        animator.SetTrigger("Craft");
        item.SetActive(false);
    }

    private void CraftingTable_OnCraftFinished(ItemSO cure)
    {
        itemSpriteRenderer.sprite = cure.sprite;
        animator.SetTrigger("Crafted");
        item.SetActive(true);
    }

    private void CraftingTable_OnItemTaken()
    {
        animator.SetTrigger("Empty");
        item.SetActive(false);
    }

    public void Animation_CraftFinished()
    {

        // Play sound
    }
}
