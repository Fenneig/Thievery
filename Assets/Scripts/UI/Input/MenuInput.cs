using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private Button _mainMenuExitButton;
    [SerializeField] private Button _settingsMenuReturnButton;

    public void OnCloseMenu(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (_mainMenuExitButton.IsActive()) _mainMenuExitButton.onClick.Invoke();
        if (_settingsMenuReturnButton.IsActive()) _settingsMenuReturnButton.onClick.Invoke();
    }
}