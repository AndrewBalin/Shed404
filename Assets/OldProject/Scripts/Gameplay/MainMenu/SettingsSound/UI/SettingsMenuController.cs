using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Gameplay.MainMenu.Common;
using Gameplay.MainMenu.SettingsSound.Common;

namespace Gameplay.MainMenu.SettingsSound.UI
{
    public class SettingsMenuController : MonoBehaviour, ISettingsMenu
    {
        [SerializeField] private GameObject _panelRoot;
        [SerializeField] private Button _closeButton;
        [SerializeField] private AudioSource _clickSfx;
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private Image _muteOnImage;
        [SerializeField] private Image _muteOffImage;

        private IAudioService _audioService;
        private ISettingsStorage _storage;
        private IMenuButtonsProvider _buttonsProvider;
        private IMusicLaunchService _musicLaunchService; 

        [Inject]
        public void Construct(IAudioService audioService, ISettingsStorage storage, IMenuButtonsProvider buttonsProvider, IMusicLaunchService musicLaunchService)
        {
            _audioService = audioService;
            _storage = storage;
            _buttonsProvider = buttonsProvider;
            _musicLaunchService = musicLaunchService;
        }

        private void Awake()
        {
            _panelRoot.SetActive(false);
            
            _masterSlider.value = _storage.MasterVolume;
            _musicSlider.value = _storage.MusicVolume;
            _sfxSlider.value = _storage.EffectVolume;
            
            _muteToggle.isOn = !_storage.IsMuted;
            _muteOnImage.enabled = _muteToggle.isOn;
            _muteOffImage.enabled = !_muteToggle.isOn;
            
            _masterSlider.onValueChanged.AddListener(_audioService.SetMasterVolume);
            _musicSlider.onValueChanged.AddListener(_audioService.SetMusicVolume);
            _sfxSlider.onValueChanged.AddListener(_audioService.SetEffectVolume);
            
            _muteToggle.onValueChanged.AddListener(isOn =>{ _audioService.SetMuted(!isOn); OnMuteIconChange(isOn);});
            _closeButton.onClick.AddListener(() =>{ _musicLaunchService.PlayEffect(_clickSfx);Hide();});
        }

        public void Show()
        {
            _panelRoot.SetActive(true);
            
            foreach (Button button in _buttonsProvider.AllButtons)
                button.interactable = false;
        }

        public void Hide()
        {
            foreach (Button button in _buttonsProvider.AllButtons)
                button.interactable = true;
            
            _panelRoot.SetActive(false);
        }

        private void OnMuteIconChange(bool isOn)
        {
            _muteOnImage.enabled = isOn;
            _muteOffImage.enabled = !isOn;
        }
    }
}