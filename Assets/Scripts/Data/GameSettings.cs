using Data.Properties;
using UnityEngine;

namespace Data
{

[CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _global;
        [SerializeField] private FloatPersistentProperty _music;
        [SerializeField] private FloatPersistentProperty _sfx;

        public FloatPersistentProperty Global => _global;
        public FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Sfx => _sfx;

        private static GameSettings _instance;
        public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _global = new FloatPersistentProperty(1f, SoundSettings.Global.ToString());
            _music = new FloatPersistentProperty(1f, SoundSettings.Music.ToString());
            _sfx = new FloatPersistentProperty(1f, SoundSettings.Sfx.ToString());
        }

        private void OnValidate()
        {
            _global.Validate();
            _music.Validate();
            _sfx.Validate();
        }
        
        public enum SoundSettings 
        {
            Global,
            Music,
            Sfx
        }
    }
}