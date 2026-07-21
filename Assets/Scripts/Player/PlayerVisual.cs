using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public static PlayerVisual Instance {  get; private set; }


    private float lastInputX;
    private float lastInputY;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void UpdateVisual(Vector2 moveInput)
    {

        bool isMoving = moveInput.sqrMagnitude > 0;
        animator.SetBool("IsWalking", isMoving);

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        if (isMoving)
        {
            lastInputX = moveInput.x;
            lastInputY = moveInput.y;
        }

        animator.SetFloat("LastInputX", lastInputX);
        animator.SetFloat("LastInputY", lastInputY);

    }
}
