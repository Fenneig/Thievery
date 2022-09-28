using Data;
using UnityEngine;

public class SettingsMenuWindow : MonoBehaviour
{
    [SerializeField] private AudioSettingsWidget _music;
    [SerializeField] private AudioSettingsWidget _sfx;

    private void Start()
    {
        _music.SetModel(GameSettings.I.Music);
        _sfx.SetModel(GameSettings.I.Sfx);
    }
}
