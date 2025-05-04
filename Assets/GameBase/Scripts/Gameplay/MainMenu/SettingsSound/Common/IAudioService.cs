namespace Gameplay.MainMenu.SettingsSound.Common
{
    public interface IAudioService
    {
        public void SetMasterVolume(float volume);
        
        public void SetMusicVolume(float volume);
        
        public void SetEffectVolume(float volume);
        
        public void SetMuted(bool isMuted);
    }
}