using System;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSO item;
    [SerializeField] private GameObject interactionPoint;
    [SerializeField] private string interactionText;
    [SerializeField] private string loadingText;
    [SerializeField] private float refillTimerMax = 5f;

    private bool canOpen = true;
    private bool isRefilling = false;

    public event Action OnChestOpened;
    public event Action OnChestRefilled;
    public event Action OnStateChanged;

    private float refillTimer;

    private void Awake()
    {
        refillTimer = refillTimerMax;
    }

    private void Update()
    {
        if (!isRefilling)
            return;

        refillTimer -= Time.deltaTime;

        if (refillTimer <= 0f)
        {
            isRefilling = false;

            OnChestRefilled?.Invoke();
        }
    }
    public void FinishRefill()
    {
        canOpen = true;
        OnStateChanged?.Invoke();
    }

    public void Interact()
    {
        if (!canOpen)
            return;

        if (!PlayerInventory.Instance.AddItem(item))
            return;

        canOpen = false;
        isRefilling = true;
        refillTimer = refillTimerMax;

        OnChestOpened?.Invoke();
        OnStateChanged?.Invoke();
    }

    public Vector3 GetInteractionPoint()
    {
        return interactionPoint.transform.position;
    }

    public string GetCurrentInteractionText()
    {
        if (canOpen)
            return interactionText;

        return loadingText;
    }

    public bool ShowInteractionKey()
    {
        return canOpen;
    }
}
