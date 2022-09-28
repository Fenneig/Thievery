using UnityEngine;

namespace UI.Windows
{
    public class VictoryWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _victoryScreen;

        public void Show()
        {
            _victoryScreen.SetActive(true);
        }
    }
}