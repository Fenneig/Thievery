using System;
using Data;
using Data.Properties;
using UnityEngine;

public class AudioSettingsComponent : MonoBehaviour
{
    [SerializeField] GameSettings.SoundSettings _mode;
    private AudioSource _source;
    private FloatPersistentProperty _model;

    public AudioSource Source => _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _model = FindProperty();
        _model.OnChanged += OnSoundSettingsChange;

        OnSoundSettingsChange(_model.Value, _model.Value);
    }

    private void OnSoundSettingsChange(float newValue, float oldValue)
    {
        _source.volume = newValue;
    }

    private FloatPersistentProperty FindProperty()
    {
        switch (_mode)
        {
            case GameSettings.SoundSettings.Global: return GameSettings.I.Global;
            case GameSettings.SoundSettings.Music: return GameSettings.I.Music;
            case GameSettings.SoundSettings.Sfx: return GameSettings.I.Sfx;
        }
        throw new ArgumentException("Undefiend argument");
    }

    private void OnDestroy()
    {
        _model.OnChanged -= OnSoundSettingsChange;
    }
}
