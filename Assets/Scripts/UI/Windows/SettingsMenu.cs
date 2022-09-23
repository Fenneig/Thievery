using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;

    private void Awake()
    {
        _mainMenu = GameObject.FindWithTag("MainMenu");
    }

    public void OnReturn()
    {
        if (_mainMenu != null) _mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
