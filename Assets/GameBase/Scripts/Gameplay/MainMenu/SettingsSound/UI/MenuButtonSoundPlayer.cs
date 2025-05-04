using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Gameplay.MainMenu.Common;
using Gameplay.MainMenu.SettingsSound.Common;

namespace Gameplay.MainMenu.SettingsSound.UI
{
    public class MenuButtonSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _clickSfx;

        private IMusicLaunchService _musicLaunchService;
        private IMenuButtonsProvider _buttonsProvider;

        [Inject]
        public void Construct(IMusicLaunchService musicLaunchService, IMenuButtonsProvider buttonsProvider)
        {
            _musicLaunchService = musicLaunchService;
            _buttonsProvider = buttonsProvider;
        }

        private void Awake()
        {
            foreach (Button btn in _buttonsProvider.AllButtons)
            {
                btn.onClick.AddListener(() => { _musicLaunchService.PlayEffect(_clickSfx); });
            }
        }
    }
}