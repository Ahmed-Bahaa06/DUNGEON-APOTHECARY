using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }


    [SerializeField] private float speed = 8f;

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerVisual visual;
    [SerializeField] private PlayerCollision collision;
    [SerializeField] private PlayerInteraction interaction;
    [SerializeField] public  PlayerHealth health;

    private Vector2 moveInput;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnEnable()
    {
        collision.OnInteractableEntered += interaction.SetInteractable;
        collision.OnInteractableExited += interaction.ClearInteractable;
        collision.OnMonsterEntered += DeliveryManager.Instance.TryDeliver;

        DeliveryManager.Instance.OnCorrectDelivery += DeliveryManager_OnCorrectDelivery;
        DeliveryManager.Instance.OnWrongDelivery += DeliveryManager_OnWrongDelivery;
        DeliveryManager.Instance.OnEmptyDelivery += DeliveryManager_OnEmptyDelivery;

        health.OnInvincibilityStarted += visual.OnInvincibilityStarted;
    }

    private void DeliveryManager_OnWrongDelivery()
    {
        health.TakeDamage();

        ConsumeSelectedCure();
    }

    private void DeliveryManager_OnEmptyDelivery()
    {
        health.TakeDamage();
    }

    private void DeliveryManager_OnCorrectDelivery(Monster monster)
    {
        ConsumeSelectedCure();
    }

    private void ConsumeSelectedCure()
    {
        ItemSO item = PlayerInventory.Instance.GetSelectedItem();

        if (item == null)
            return;

        if (item.type == ItemSO.ItemType.Cure)
        {
            PlayerInventory.Instance.RemoveSelectedItem();
        }
    }

    private void Update()
    {
        if (!movement.CanMove)
        {
            visual.UpdateVisual(Vector2.zero);
            return;
        }

        moveInput = PlayerInput.Instance.GetMovementVectorNormalized();
        visual.UpdateVisual(moveInput);
    }

    private void FixedUpdate()
    {
        movement.Move(moveInput, speed);
    }

    private void OnDisable()
    {
        collision.OnInteractableEntered -= interaction.SetInteractable;
        collision.OnInteractableExited -= interaction.ClearInteractable;
        collision.OnMonsterEntered -= DeliveryManager.Instance.TryDeliver;

        DeliveryManager.Instance.OnCorrectDelivery -= DeliveryManager_OnCorrectDelivery;
        DeliveryManager.Instance.OnWrongDelivery -= DeliveryManager_OnWrongDelivery;
        DeliveryManager.Instance.OnEmptyDelivery -= DeliveryManager_OnEmptyDelivery;

        health.OnInvincibilityStarted -= visual.OnInvincibilityStarted;

    }
}