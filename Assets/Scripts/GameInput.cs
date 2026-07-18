using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;



[DefaultExecutionOrder(-1)]
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event Action OnInteractAction;
    public event Action OnDropAction;

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
    }


    private void OnDisable()
    {

        myInput.Player.Interact.performed -= Interact_performed;
        myInput.Player.Interact.performed -= Drop_performed;

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

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = myInput.Player.Move.ReadValue<Vector2>();

        inputVector.Normalize();
        return inputVector;
    }
}
