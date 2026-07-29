using UnityEngine;
using System.Collections;

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

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= GameOver;
    }

    private void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("Death");
    }

    public void OnInvincibilityStarted()
    {
        animator.SetTrigger("Flash");
    }
}
