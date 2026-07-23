using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject promptRoot;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private GameObject keyImage;

    private IInteractable currentInteractable;

    private void Awake()
    {
        promptRoot.SetActive(false);
    }
    private void OnEnable()
    {
        playerInteraction.OnInteractableEntered += PlayerInteraction_OnInteractableEntered;
        playerInteraction.OnInteractableExited += PlayerInteraction_OnInteractableExited;
    }

    private void OnDisable()
    {
        if (playerInteraction == null)
            return;

        playerInteraction.OnInteractableEntered -= PlayerInteraction_OnInteractableEntered;
        playerInteraction.OnInteractableExited -= PlayerInteraction_OnInteractableExited;
    }

    private void PlayerInteraction_OnInteractableEntered(IInteractable interactable)
    {
        if (currentInteractable != null)
            currentInteractable.OnStateChanged -= Refresh;

        currentInteractable = interactable;

        if (currentInteractable != null)
            currentInteractable.OnStateChanged += Refresh;

        Refresh();
    }

    private void PlayerInteraction_OnInteractableExited()
    {
        if (currentInteractable != null)
            currentInteractable.OnStateChanged -= Refresh;

        currentInteractable = null;
        promptRoot.SetActive(false);
    }

    private void Refresh()
    {
        if (currentInteractable == null)
        {
            promptRoot.SetActive(false);
            return;
        }

        promptRoot.SetActive(true);
        promptRoot.transform.position = currentInteractable.GetInteractionPoint();

        interactionText.text = currentInteractable.GetCurrentInteractionText();

        RectTransform textRect = interactionText.rectTransform;

        if (currentInteractable.ShowInteractionKey())
        {
            textRect.anchoredPosition = new Vector2(0.5f, textRect.anchoredPosition.y);
        }
        else
        {
            textRect.anchoredPosition = new Vector2(0f, textRect.anchoredPosition.y);
        }

        keyImage.SetActive(currentInteractable.ShowInteractionKey());
    }

}
