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
        if (isInvincible || currentHealth <= 0) return;

        currentHealth--;
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            OnPlayerDied?.Invoke();
            return;
        }

        isInvincible = true;
        invincibleTimer = invincibleTime;

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
