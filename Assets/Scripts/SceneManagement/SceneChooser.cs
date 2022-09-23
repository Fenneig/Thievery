using UnityEngine;

namespace SceneManagement
{
    public class SceneChooser : MonoBehaviour
    {
        [SerializeField] private Scene _scene;

        public void LoadScene()
        {
            StartCoroutine(SceneLoader.LoadSceneAsync(_scene));
        }
    }
}