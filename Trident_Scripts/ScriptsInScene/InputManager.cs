using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private bool submitPressed = false;
    private bool interactPressed = false;
    private bool uiPressed = false;

    private static InputManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Input Manager in scene.");
        }
        instance = this;
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public void InteractionButtonPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            interactPressed = true;
        }

        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

     public void SubmitButtonPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            submitPressed = true;
        }

        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public void UIButtonPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            uiPressed = true;
        }

        else if (context.canceled)
        {
            uiPressed = false;
        }
    }


     public bool GetInteractPressed() 
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }
    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public bool GetUIButtonPressed() 
    {
        bool result = uiPressed;
        uiPressed = false;
        return result;
    }

    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }
}
