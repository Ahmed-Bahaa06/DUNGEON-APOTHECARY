using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance {  get; private set; }

    public event Action<Monster> OnCorrectDelivery;
    public event Action OnWrongDelivery;
    public event Action OnEmptyDelivery;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void TryDeliver(Monster monster)
    {
        if (!monster.CanReceiveDelivery) return;

        ItemSO cure = PlayerInventory.Instance.GetSelectedItem();

        if (cure == null)
        {
            OnEmptyDelivery?.Invoke();
            return;
        }

        if (monster.Recipe.ReceiveCure(cure))
        {
            OnCorrectDelivery?.Invoke(monster);
        }
        else
        {
            OnWrongDelivery?.Invoke();
        }
    }
}
