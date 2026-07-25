using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Animator[] heartAnimators;

    private int currentHealth;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= PlayerHealth_OnHealthChanged;
    }
    private void Start()
    {
        currentHealth = playerHealth.GetHealth();
    }

    private void PlayerHealth_OnHealthChanged(int newHealth)
    {
        if (newHealth < currentHealth)
        {
            heartAnimators[currentHealth - 1].SetTrigger("LoseHeart");
        }

        currentHealth = newHealth;
    }
}
