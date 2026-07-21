using UnityEngine;


[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        Ingredient,
        Cure,
    }

    public string itemName;
    public Sprite sprite;
    public ItemType type;

}
