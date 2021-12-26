using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    
    #region Properties

    public EventSystem eventSystem;

    private InputAction lastInput;

    #endregion

    #region Methods
    void Awake()
    {
        instance = this;
    }

    public void OnClickA(InputAction.CallbackContext context)
    {
        if (context.action.WasReleasedThisFrame())
        {
            AudioManager.instance.ClickItemAudio();
            InventoryManager.instance.MoveItemEvent();
        }
    }

    public void OnClickY(InputAction.CallbackContext context)
    {
        if (context.action.WasReleasedThisFrame())
        {
            AudioManager.instance.ClickItemAudio();
            InventoryManager.instance.ItemManagement();
        }
    }

    public void OnNavigation(InputAction.CallbackContext context)
    {
        if (context.action.WasReleasedThisFrame())
        {
            if (InputManager.instance.eventSystem.currentSelectedGameObject != null)
                AudioManager.instance.NavigationAudio();
        }
    }

    public void OnScreenResize(InputAction.CallbackContext context)
    {
        if (context.action.WasReleasedThisFrame())
        {
            switch (context.action.activeControl.displayName)
            {
                case "Right Bumper":
                    ScreenManager.instance.ScreenResize(true); // Increase Size if True
                    break;
                case "Left Bumper":
                    ScreenManager.instance.ScreenResize(); // Descrease Size if False
                    break;
            }
        }
    }

    #endregion
}
