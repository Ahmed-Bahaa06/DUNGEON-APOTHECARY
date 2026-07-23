using UnityEngine;
using System.Collections.Generic;
using System;

public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField] private RecipeListSO cureRecipes;

    [SerializeField] private GameObject interactionPoint;

    [SerializeField] private string interactionText;
    [SerializeField] private string loadingText;

    [SerializeField] private float craftingTimerMax = 3f;

    private bool canCraft = true;
    private bool isCrafting = false;
    private bool hasCraftedItem = false;    

    private CraftedCureRecipeSO currentRecipe;

    public event Action OnCraftStarted;
    public event Action<ItemSO> OnCraftFinished;
    public event Action OnItemTaken;
    public event Action OnStateChanged;

    private float craftingTimer;



    private void Awake()
    {
        craftingTimer = craftingTimerMax;
    }


    private void Update()
    {
        if (!isCrafting)
            return;

        craftingTimer -= Time.deltaTime;

        if (craftingTimer <= 0f)
        {
            isCrafting = false;
            craftingTimer = craftingTimerMax;

            hasCraftedItem = true;

            OnCraftFinished?.Invoke(currentRecipe.craftedCure);
            OnStateChanged?.Invoke();
        }
    }

    public void Interact()
    {
        if (hasCraftedItem)
        {
            if (!PlayerInventory.Instance.AddItem(currentRecipe.craftedCure))
                return;

            OnItemTaken?.Invoke();

            FinishCrafting();
            return;
        }

        if (!canCraft)
            return;

        currentRecipe = GetRecipe();

        if (currentRecipe == null)
            return;

        PlayerInventory.Instance.RemoveItems(currentRecipe.ingredients);

        canCraft = false;
        isCrafting = true;
        craftingTimer = craftingTimerMax;

        OnCraftStarted?.Invoke();
        OnStateChanged?.Invoke();
    }

    public void FinishCrafting()
    {
        canCraft = true;
        hasCraftedItem = false;
        currentRecipe = null;

        OnStateChanged?.Invoke();
    }

    public Vector3 GetInteractionPoint()
    {
        return interactionPoint.transform.position;
    }

    public string GetCurrentInteractionText()
    {
        if (hasCraftedItem)
            return "Take";

        if (canCraft)
            return interactionText;

        return loadingText;
    }

    public bool ShowInteractionKey()
    {
        return canCraft || hasCraftedItem;
    }

    private CraftedCureRecipeSO GetRecipe()
    {
        foreach (CraftedCureRecipeSO recipe in cureRecipes.recipeListSO)
        {
            if (MatchesRecipe(recipe))
                return recipe;
        }

        return null;
    }

    private bool MatchesRecipe(CraftedCureRecipeSO recipe)
    {
        foreach (ItemSO ingredient in recipe.ingredients)
        {
            bool found = false;

            for (int i = 0; i < PlayerInventory.Instance.GetSlotCount(); i++)
            {
                ItemSO item = PlayerInventory.Instance.GetItem(i);

                if (item == ingredient)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                return false;
        }

        return true;
    }
}
