using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public static PlayerVisual Instance {  get; private set; }

    //private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void UpdateVisual(Vector2 moveInput)
    {
        //animator.SetFloat("MoveX", moveInput.x);
        //animator.SetFloat("MoveY", moveInput.y);

        //bool isMoving = moveInput.sqrMagnitude > 0;
        //animator.SetBool("IsMoving", isMoving);

        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;
    }
}
