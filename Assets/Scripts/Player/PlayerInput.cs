using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;



[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public event Action OnInteractAction;
    public event Action OnDropAction;
    public event Action OnSelectLeftItemAction;
    public event Action OnSelectRightItemAction;

    private MyInputActions myInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        myInput = new MyInputActions();
    }

    private void OnEnable()
    {
        myInput.Enable();

        myInput.Player.Interact.performed += Interact_performed;
        myInput.Player.Drop.performed += Drop_performed;
        myInput.Player.SelectLeftItem.performed += SelectLeftItem_performed;
        myInput.Player.SelectRightItem.performed += SelectRightItem_performed;
    }


    private void OnDisable()
    {

        myInput.Player.Interact.performed -= Interact_performed;
        myInput.Player.Drop.performed -= Drop_performed;
        myInput.Player.SelectLeftItem.performed -= SelectLeftItem_performed;
        myInput.Player.SelectRightItem.performed -= SelectRightItem_performed;

        myInput.Disable();
    }

    private void Drop_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            OnDropAction?.Invoke();
        }
    }
    private void Interact_performed(InputAction.CallbackContext obj)
    {

        if (obj.performed)
        {
            OnInteractAction?.Invoke();
        }
    }
    private void SelectLeftItem_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            OnSelectLeftItemAction?.Invoke();
        }

    }

    private void SelectRightItem_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            OnSelectRightItemAction?.Invoke();
        }
    }
    
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = myInput.Player.Move.ReadValue<Vector2>();

        inputVector.Normalize();
        return inputVector;
    }
}
