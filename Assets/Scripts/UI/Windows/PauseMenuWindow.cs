using SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuWindow : MonoBehaviour
{
    private PlayerInput _input;
    private void Awake()
    {
        if (_input == null) _input = FindObjectOfType<PlayerInput>();
        _input.enabled = false;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        _input.enabled = true;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void OnGameExit()
    {
        SceneLoader.LoadScene(Scene.MainMenu);
    }

}
