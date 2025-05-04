namespace Gameplay.MainMenu.SettingsSound.Common
{
    public interface ISettingsStorage
    {
        public float MasterVolume { get; set; }
        public float MusicVolume { get; set; }
        public float EffectVolume { get; set; }
        public bool IsMuted { get; set; }
    }
}