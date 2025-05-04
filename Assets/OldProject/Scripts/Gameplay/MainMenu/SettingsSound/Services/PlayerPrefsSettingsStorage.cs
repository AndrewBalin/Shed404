using UnityEngine;
using Gameplay.MainMenu.SettingsSound.Common;

namespace Gameplay.MainMenu.SettingsSound.Services
{
    public class PlayerPrefsSettingsStorage : ISettingsStorage
    {
        private const string KeyMaster = "MasterVolume";
        private const string KeyMusic  = "MusicVolume";
        private const string KeyEffect = "EffectVolume";
        private const string KeyMuted  = "IsMuted";
        private const float DefaultVol = 1f;

        public float MasterVolume
        {
            get => PlayerPrefs.GetFloat(KeyMaster, DefaultVol);
            set
            {
                PlayerPrefs.SetFloat(KeyMaster, value);
                PlayerPrefs.Save();
            }
        }

        public float MusicVolume
        {
            get => PlayerPrefs.GetFloat(KeyMusic, DefaultVol);
            set
            {
                PlayerPrefs.SetFloat(KeyMusic, value);
                PlayerPrefs.Save();
            }
        }

        public float EffectVolume
        {
            get => PlayerPrefs.GetFloat(KeyEffect, DefaultVol);
            set
            {
                PlayerPrefs.SetFloat(KeyEffect, value);
                PlayerPrefs.Save();
            }
        }

        public bool IsMuted
        {
            get => PlayerPrefs.GetInt(KeyMuted, 0) == 1;
            set
            {
                PlayerPrefs.SetInt(KeyMuted, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
    }
}