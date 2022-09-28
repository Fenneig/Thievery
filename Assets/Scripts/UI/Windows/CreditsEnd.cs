using SceneManagement;
using UnityEngine;

namespace UI.Windows
{
    public class CreditsEnd : MonoBehaviour
    {
        [SerializeField] private SceneChooser _sceneChooser;
        public void OnCreditsEnd()
        {
            _sceneChooser.LoadScene();
        }
    }
}
