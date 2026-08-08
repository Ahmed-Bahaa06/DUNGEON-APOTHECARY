using System;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-1)]
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private const int SlotCount = 2;
    private ItemSO[] slots = new ItemSO[SlotCount];

    public event Action OnInventoryChanged;
    public event Action OnItemAdded;
    private int selectedSlot = 0;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnEnable()
    {
        PlayerInput.Instance.OnSelectLeftItemAction += PlayerInput_OnSelectLeftItemAction;
        PlayerInput.Instance.OnSelectRightItemAction += PlayerInput_OnSelectRightItemAction;
        PlayerInput.Instance.OnDropAction += PlayerInput_OnDropAction;
        PlayerInput.Instance.OnToggleSlotAction += PlayerInput_OnToggleSlotAction;
    }

    private void PlayerInput_OnToggleSlotAction()
    {
        SelectSlot(1 - selectedSlot);
    }

    private void PlayerInput_OnDropAction()
    {
        RemoveSelectedItem();
    }

    private void OnDisable()
    {
        PlayerInput.Instance.OnSelectLeftItemAction -= PlayerInput_OnSelectLeftItemAction;
        PlayerInput.Instance.OnSelectRightItemAction -= PlayerInput_OnSelectRightItemAction;
        PlayerInput.Instance.OnDropAction -= PlayerInput_OnDropAction;
        PlayerInput.Instance.OnToggleSlotAction -= PlayerInput_OnToggleSlotAction;
    }

    private void PlayerInput_OnSelectRightItemAction()
    {
        SelectSlot(1);
    }

    private void PlayerInput_OnSelectLeftItemAction()
    {
        SelectSlot(0);
    }

    public bool AddItem(ItemSO item)
    {

        for (int i = 0; i < slots.Length; i++)
        {

            if (slots[i] == null)
            {
                slots[i] = item;
                SelectSlot(i);

                OnItemAdded?.Invoke();
                OnInventoryChanged?.Invoke();


                return true;
            }
        }
        return false;
    }

    public void RemoveSelectedItem()
    {
        if (slots[selectedSlot] == null) return;

        slots[selectedSlot] = null;

        int otherSlot = 1 - selectedSlot;

        if (slots[otherSlot] != null)
        {
            selectedSlot = otherSlot;
        }

        OnInventoryChanged?.Invoke();

    }

    public void RemoveItems(List<ItemSO> ingredients)
    {

        foreach (ItemSO ingredient in ingredients)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == ingredient)
                {
                    slots[i] = null;
                    break;
                }
            }
        }

        OnInventoryChanged?.Invoke();
    }

    public ItemSO GetSelectedItem()
    {
        return slots[selectedSlot];
    }

    public ItemSO GetItem(int slot)
    {
        if (slot < 0 || slot >= slots.Length)
            return null;

        return slots[slot];
    }

    public void SelectSlot(int slot)
    {
        if (slot < 0 || slot >= slots.Length)
            return;

        if (selectedSlot == slot)
            return;

        selectedSlot = slot;
        OnInventoryChanged?.Invoke();
    }

    public int GetSelectedSlot()
    {
        return selectedSlot;
    }

    public bool IsSlotEmpty(int slot)
    {
        if (slot < 0 || slot >= slots.Length)
            return true;

        return slots[slot] == null;
    }

    public bool IsFull()
    {
        return slots[0] != null && slots[1] != null;
    }

    public bool HasSelectedItem()
    {
        return slots[selectedSlot] != null;
    }

    public int GetSlotCount()
    {
        return SlotCount;
    }
}
