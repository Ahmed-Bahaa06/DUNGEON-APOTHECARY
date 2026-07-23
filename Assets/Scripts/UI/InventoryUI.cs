using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image[] slotImages;
    [SerializeField] private GameObject leftSelectionArrow;
    [SerializeField] private GameObject rightSelectionArrow;


    private void OnEnable()
    {

        PlayerInventory.Instance.OnInventoryChanged += PlayerInventory_OnInventoryChanged;

        UpdateInventory();
    }
    private void OnDisable()
    {
        PlayerInventory.Instance.OnInventoryChanged -= PlayerInventory_OnInventoryChanged;
    }

    private void PlayerInventory_OnInventoryChanged()
    {
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            UpdateCell(i);
        }

        UpdateSelectionArrow();
    }

    private void UpdateSelectionArrow()
    {
        if (PlayerInventory.Instance.GetSelectedSlot() == 0)
        {
            leftSelectionArrow.SetActive(true);
            rightSelectionArrow.SetActive(false);
        }
        else
        {
            rightSelectionArrow.SetActive(true);
            leftSelectionArrow.SetActive(false);
        }
    }

    private void UpdateCell(int idx)
    {
        ItemSO item = PlayerInventory.Instance.GetItem(idx);

        if (item != null)
        {
            slotImages[idx].sprite = item.sprite;
            slotImages[idx].enabled = true;
        }
        else
        {
            slotImages[idx].enabled = false;
        }
    }
}
