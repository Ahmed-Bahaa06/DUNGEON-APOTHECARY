using UnityEngine;
using UnityEngine.Events;

public class UnityEventBridge : MonoBehaviour
{
    public UnityEvent myAnimationEvent;

    // Call this via the Animation Event dropdown
    public void InvokeEvent()
    {
        myAnimationEvent?.Invoke();
    }
}
