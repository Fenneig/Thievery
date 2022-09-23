using SceneManagement;
using UnityEngine;
using Utils;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    
    public void OnStartGame()
    {
        StartCoroutine(SceneLoader.LoadSceneAsync(Scene.Level_00));
    }

    public void OnShowSettings()
    {
        _settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnShowCredits()
    {
        SceneLoader.LoadScene(Scene.Credits);
    }

    public void OnExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}