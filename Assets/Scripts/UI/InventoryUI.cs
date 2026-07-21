using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image[] slotImages;
    [SerializeField] private RectTransform selectionArrow;

    private float arrowPositionX = 1.5f;
    private float arrowRotationZ = 90f;

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
            selectionArrow.anchoredPosition = new Vector2(-arrowPositionX, selectionArrow.anchoredPosition.y);
            selectionArrow.localEulerAngles = new Vector3(0f, 0f , arrowRotationZ);
        }
        else
        {
            selectionArrow.anchoredPosition = new Vector2(arrowPositionX, selectionArrow.anchoredPosition.y);
            selectionArrow.localEulerAngles = new Vector3(0f, 0f, -arrowRotationZ);
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
