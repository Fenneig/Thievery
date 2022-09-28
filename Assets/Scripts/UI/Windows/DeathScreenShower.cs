using UnityEngine;

namespace UI.Windows
{
    public class DeathScreenShower : MonoBehaviour
    {
        [SerializeField] private GameObject _deathScreen;

        public void ShowDeathScreen()
        {
            _deathScreen.SetActive(true);
        }
    }
}
