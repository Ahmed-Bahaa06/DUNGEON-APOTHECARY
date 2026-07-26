using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invincibleTime = 1.5f;

    public event Action<int> OnHealthChanged;
    public event Action OnPlayerDied;

    public event Action OnInvincibilityStarted;

    private int currentHealth;
    private float invincibleTimer;
    private bool isInvincible;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isInvincible)
            return;

        invincibleTimer -= Time.deltaTime;

        if (invincibleTimer <= 0f)
        {
            isInvincible = false;
        }
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentHealth--;

        if (currentHealth <= 0)
        {
            Debug.Log("Player Died");
            OnPlayerDied?.Invoke();
            return;
        }

        isInvincible = true;
        invincibleTimer = invincibleTime;

        OnHealthChanged?.Invoke(currentHealth);
        OnInvincibilityStarted?.Invoke();
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
