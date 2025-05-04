using UnityEngine;
using UnityEngine.Audio;
using Gameplay.MainMenu.SettingsSound.Common;

namespace Gameplay.MainMenu.SettingsSound.Services
{
    public class AudioService : IAudioService
    {
        private const float MinDb = -80f;
        private const float MinLinear = 0.0001f;
        private const float DbMultiplier = 20f;
        
        private readonly AudioMixer _audioMixer;
        private readonly ISettingsStorage _storage;
        
        private float _lastMaster;
        
        public AudioService(AudioMixer audioMixer, ISettingsStorage storage)
        {
            _audioMixer = audioMixer;
            _storage    = storage;
            _lastMaster = _storage.MasterVolume;
            InitializeFromStorage();
        }
        
        private void InitializeFromStorage()
        {
            SetMasterVolume(_storage.MasterVolume);
            SetMusicVolume(_storage.MusicVolume);
            SetEffectVolume(_storage.EffectVolume);
            SetMuted(_storage.IsMuted);
        }
        
        public void SetMasterVolume(float volume)
        {
            _lastMaster = Mathf.Clamp01(volume);
            _storage.MasterVolume = _lastMaster;

            float db = Mathf.Log10(Mathf.Max(_lastMaster, MinLinear)) * DbMultiplier;
            _audioMixer.SetFloat(AudioParams.MasterVolume, db);
        }

        public void SetMusicVolume(float volume)
        {
            float v = Mathf.Clamp01(volume);
            _storage.MusicVolume = v;

            float db = Mathf.Log10(Mathf.Max(v, MinLinear)) * DbMultiplier;
            _audioMixer.SetFloat(AudioParams.MusicVolume, db);
        }

        public void SetEffectVolume(float volume)
        {
            float v = Mathf.Clamp01(volume);
            _storage.EffectVolume = v;

            float db = Mathf.Log10(Mathf.Max(v, MinLinear)) * DbMultiplier;
            _audioMixer.SetFloat(AudioParams.EffectVolume, db);
        }

        public void SetMuted(bool isMuted)
        {
            _storage.IsMuted = isMuted;
            float db = isMuted ? MinDb : Mathf.Log10(Mathf.Max(_lastMaster, MinLinear)) * DbMultiplier;
            
            _audioMixer.SetFloat(AudioParams.MasterVolume, db);
        }
    }
}