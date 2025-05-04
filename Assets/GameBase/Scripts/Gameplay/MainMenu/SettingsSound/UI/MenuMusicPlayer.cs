using UnityEngine;
using Zenject;
using Gameplay.MainMenu.SettingsSound.Common;

namespace Gameplay.MainMenu.SettingsSound.UI
{
    public class MenuMusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _menuMusic;
        [SerializeField] private bool _loop = true;

        private IMusicLaunchService _musicLaunchService;
        private IAudioService _audioService;
        private ISettingsStorage _storage;

        [Inject]
        public void Construct(IMusicLaunchService musicLaunchService, IAudioService audioService, ISettingsStorage settingsStorage)
        {
            _musicLaunchService = musicLaunchService;
            _audioService = audioService;
            _storage = settingsStorage;
        }

        private void Start()
        {
            _audioService.SetMasterVolume(_storage.MasterVolume);
            _audioService.SetMusicVolume(_storage.MusicVolume);
            _audioService.SetEffectVolume(_storage.EffectVolume);
            _audioService.SetMuted(_storage.IsMuted);
            
            _musicLaunchService.PlayMusic(_menuMusic, _loop);
        }
    }
}