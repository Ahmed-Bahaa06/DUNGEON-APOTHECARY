using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private float lastInputX;
    private float lastInputY;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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

    public void OnInvincibilityStarted()
    {
        animator.SetTrigger("Flash");
    }
}
