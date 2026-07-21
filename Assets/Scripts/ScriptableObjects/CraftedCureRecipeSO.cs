using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu()]
public class CraftedCureRecipeSO : ScriptableObject
{

    public List<ItemSO> itemList;

    public string cureName;
    public Sprite sprite;
}
