using System;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;
    [SerializeField] private LayerMask chestLayer;

    private void OnEnable()
    {

        PlayerInput.Instance.OnInteractAction += PlayerInput_OnInteractAction;
    }
    private void OnDisable()
    {
        PlayerInput.Instance.OnInteractAction -= PlayerInput_OnInteractAction;
    }

    private void PlayerInput_OnInteractAction()
    {


        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((chestLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
            } 
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((chestLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && interactable == currentInteractable)
            {
                currentInteractable = null;
            } 
        }
    }

}
