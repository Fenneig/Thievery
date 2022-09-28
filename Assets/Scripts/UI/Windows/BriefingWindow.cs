using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Windows
{
    public class BriefingWindow : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Text _text;
        [SerializeField] private string[] _tutorialText;
        private int _tutorialNum = 0;
        private void Awake()
        {
            OnShowMenu();
        }

        public void OnShowMenu()
        {
            gameObject.SetActive(true);

            _text.text = _tutorialText[_tutorialNum];
            _input.enabled = false;
            Time.timeScale = 0f;
        }

        public void OnCloseMenu()
        {
            _input.enabled = true;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            _tutorialNum++;
        }
    }
}