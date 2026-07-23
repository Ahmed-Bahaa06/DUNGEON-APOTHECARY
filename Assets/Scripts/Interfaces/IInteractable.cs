using UnityEngine;
using System;

public interface IInteractable
{
    public event Action OnStateChanged;

    public void Interact();
    public Vector3 GetInteractionPoint();
    public string GetCurrentInteractionText();
    public bool ShowInteractionKey();
}
