using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressBar : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Update()
    {
        _image.fillAmount = SceneLoader.GetLoadingProgress();
    }
}
