using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu()]
public class CraftedCureRecipeSO : ScriptableObject
{
    public List<ItemSO> ingredients;
    public ItemSO craftedCure;
}
