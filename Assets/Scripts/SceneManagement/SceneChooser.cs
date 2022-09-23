using UnityEngine;

namespace SceneManagement
{
    public class SceneChooser : MonoBehaviour
    {
        [SerializeField] private Scene _scene;

        public void LoadScene()
        {
            SceneLoader.LoadScene(_scene);
        }
        
        public void LoadSceneAsync()
        {
            StartCoroutine(SceneLoader.LoadSceneAsync(_scene));
        }
    }
}