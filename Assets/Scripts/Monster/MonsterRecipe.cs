using UnityEngine;

public class MonsterRecipe : MonoBehaviour
{
    [SerializeField] private CraftedCureRecipeSO recipeSO;

    public ItemSO RequiredCure => recipeSO.craftedCure;
    public ItemSO[] RequiredIngredients => recipeSO.ingredients.ToArray();

    public bool ReceiveCure(ItemSO cure)
    {
        return cure == RequiredCure;
    }
}
