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

        health.OnInvincibilityStarted += visual.OnInvincibilityStarted;
    }

    private void OnDisable()
    {
        collision.OnInteractableEntered -= interaction.SetInteractable;
        collision.OnInteractableExited -= interaction.ClearInteractable;

        collision.OnMonsterEntered -= DeliveryManager.Instance.TryDeliver;

        health.OnInvincibilityStarted -= visual.OnInvincibilityStarted;

    }

    private void Update()
    {
        moveInput = PlayerInput.Instance.GetMovementVectorNormalized();
        visual.UpdateVisual(moveInput);
    }

    private void FixedUpdate()
    {
        movement.Move(moveInput, speed);
    }
}