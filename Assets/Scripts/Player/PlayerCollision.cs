using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public event Action<IInteractable> OnInteractableEntered;
    public event Action<IInteractable> OnInteractableExited;

    public event Action<Monster> OnMonsterEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            OnInteractableEntered?.Invoke(interactable);
        }

        Monster monster = collision.GetComponent<Monster>();

        if (monster != null)
        {
            Debug.Log("Touched " + monster);
            OnMonsterEntered?.Invoke(monster);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            OnInteractableExited?.Invoke(interactable);
        }

        //Monster monster = collision.GetComponent<Monster>();

        //if (monster != null)
        //    OnMonsterEntered?.Invoke(monster);
    }
}