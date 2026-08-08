using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraEffects : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource damageImpulse;

    private void Start()
    {
        Player.Instance.health.OnHealthChanged += Health_OnPlayerDamaged;
    }

    private void Health_OnPlayerDamaged(int health)
    {
        damageImpulse.GenerateImpulse();
    }

    private void OnDestroy()
    {
        if (Player.Instance != null)
            Player.Instance.health.OnHealthChanged -= Health_OnPlayerDamaged;
    }
}