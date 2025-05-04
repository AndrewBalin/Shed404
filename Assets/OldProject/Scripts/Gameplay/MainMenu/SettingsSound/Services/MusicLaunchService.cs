using UnityEngine;
using Gameplay.MainMenu.SettingsSound.Common;

namespace Gameplay.MainMenu.SettingsSound.Services
{
    public class MusicLaunchService : IMusicLaunchService
    {
        private AudioSource _currentMusicSource;

        public void PlayMusic(AudioSource source, bool loop = true)
        {
            if (_currentMusicSource != null && _currentMusicSource.isPlaying)
                _currentMusicSource.Stop();

            _currentMusicSource = source;
            _currentMusicSource.loop = loop;
            
            _currentMusicSource.Play();
        }

        public void StopMusic()
        {
            if (_currentMusicSource != null)
                _currentMusicSource.Stop();
        }

        public void PlayEffect(AudioSource source)
        {
            source.PlayOneShot(source.clip);
        }
    }
}