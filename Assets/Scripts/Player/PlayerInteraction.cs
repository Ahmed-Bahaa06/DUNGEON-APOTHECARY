//using System;
//using UnityEngine;


//public class PlayerInteraction : MonoBehaviour
//{
//    [SerializeField] private LayerMask chestLayer;

//    public event Action<IInteractable> OnInteractableEntered;
//    public event Action OnInteractableExited;

//    private IInteractable currentInteractable;

//    private void OnEnable()
//    {
//        PlayerInput.Instance.OnInteractAction += PlayerInput_OnInteractAction;
//    }
//    private void OnDisable()
//    {
//        PlayerInput.Instance.OnInteractAction -= PlayerInput_OnInteractAction;
//    }

//    private void PlayerInput_OnInteractAction()
//    {
//        currentInteractable?.Interact();
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        //if ((chestLayer.value & (1 << collision.gameObject.layer)) == 0)
//        //    return;

//        IInteractable interactable = collision.GetComponent<IInteractable>();

//        if (interactable == null)
//            return;

//        currentInteractable = interactable;
//        OnInteractableEntered?.Invoke(interactable);
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        //if ((chestLayer.value & (1 << collision.gameObject.layer)) == 0)
//        //    return;

//        IInteractable interactable = collision.GetComponent<IInteractable>();

//        if (interactable == null || interactable != currentInteractable)
//            return;

//        currentInteractable = null;
//        OnInteractableExited?.Invoke();
//    }

//}

using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public event Action<IInteractable> OnInteractableEntered;
    public event Action OnInteractableExited;

    private IInteractable currentInteractable;

    private void OnEnable()
    {
        PlayerInput.Instance.OnInteractAction += Interact;
    }

    private void OnDisable()
    {
        PlayerInput.Instance.OnInteractAction -= Interact;
    }

    public void SetInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
        OnInteractableEntered?.Invoke(interactable);
    }

    public void ClearInteractable(IInteractable interactable)
    {
        if (interactable != currentInteractable)
            return;

        currentInteractable = null;
        OnInteractableExited?.Invoke();
    }

    private void Interact()
    {
        currentInteractable?.Interact();
    }
}