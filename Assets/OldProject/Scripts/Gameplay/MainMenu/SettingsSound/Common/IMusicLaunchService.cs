using UnityEngine;

namespace Gameplay.MainMenu.SettingsSound.Common
{
    public interface IMusicLaunchService
    {
        public void PlayMusic(AudioSource clip, bool loop = true);
        
        public void StopMusic();
        
        public void PlayEffect(AudioSource clip);
    }
}