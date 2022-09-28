using System;
using Creatures.Hero;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class ZoneSuspiciousLevel : MonoBehaviour
    {
        [SerializeField] private Text _suspiciousLevelText;
        [SerializeField] private string _tag;
        [SerializeField] private string _suspiciousString;
        [SerializeField] private Hero _hero;

        private bool _changedOutfit;
        private SuspiciousLevel _currentSuspiciousLevel = SuspiciousLevel.NoThreat;

        public SuspiciousLevel CurrentSuspiciousLevel => _currentSuspiciousLevel;

        private void Awake()
        {
            ChangeSuspiciousLevelToNoThreat();
            _suspiciousLevelText.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(_tag))
            {
                if (_hero.CurrentZone == null || _hero.CurrentZone != this)
                {
                    _hero.CurrentZone = this;
                    ChangeSuspiciousLevelToNoThreat();
                }

                _suspiciousLevelText.enabled = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(_tag)) _suspiciousLevelText.enabled = false;
        }

        public void ChangeSuspiciousLevelToNoThreat()
        {
            _hero.CanBeHidden = true;
            _hero.ChangeToHiddenState();
            _currentSuspiciousLevel = SuspiciousLevel.NoThreat;
            _suspiciousLevelText.text = _suspiciousString + "безопасно";
            _suspiciousLevelText.color = Color.green;
        }
        public void ChangeSuspiciousLevelToThreat()
        {
            _hero.CanBeHidden = false;
            _hero.ChangeToNotHiddenState();
            _currentSuspiciousLevel = SuspiciousLevel.Threat;
            _suspiciousLevelText.text = _suspiciousString + "опасность";
            _suspiciousLevelText.color = Color.red;
        }
        public void ChangeSuspiciousLevelToSuspicious()
        {
            _hero.ChangeToNotHiddenState();
            _currentSuspiciousLevel = SuspiciousLevel.Suspicious;
            _suspiciousLevelText.text = _suspiciousString + "под подозрением";
            _suspiciousLevelText.color = Color.yellow;
        }

        public void ChangeOutfitInZone()
        {
            if (_currentSuspiciousLevel == SuspiciousLevel.Suspicious && !_changedOutfit)
            {
                ChangeSuspiciousLevelToNoThreat();
                _changedOutfit = true;
            }
        }
        
        [Serializable]
        public enum SuspiciousLevel
        {
            NoThreat,
            Suspicious,
            Threat
        }
    }
}
