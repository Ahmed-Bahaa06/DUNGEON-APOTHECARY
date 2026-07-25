using UnityEngine;
using static UnityEditor.Progress;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance {  get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void TryDeliver(Monster monster)
    {
        ItemSO cure = PlayerInventory.Instance.GetSelectedItem();

        Debug.Log(cure == null ? "Empty" : cure.itemName);

        if (cure == null)
        {
            Debug.Log("No cure");
            Player.Instance.health.TakeDamage();
            return;
        }

        bool healed = monster.ReceiveCure(cure);

        if (healed)
        {
            monster.Heal();
            Debug.Log("Monster healed!");
        }
        else
        {
            Debug.Log("Wrong cure");
            Player.Instance.health.TakeDamage();
        }

        if (cure.type == ItemSO.ItemType.Cure)
        {
            PlayerInventory.Instance.RemoveSelectedItem();
        }
    }
}
