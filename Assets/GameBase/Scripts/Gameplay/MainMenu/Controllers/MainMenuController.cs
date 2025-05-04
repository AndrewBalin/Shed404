using UnityEngine;
using Zenject;
using Gameplay.MainMenu.Common;

namespace Gameplay.MainMenu.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        private ISettingsMenu _settingsMenu;
        private IGameQuitter _gameQuitter;
        private IMenuButtonsProvider _buttonsProvider;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, ISettingsMenu settingsMenu, IGameQuitter gameQuitter, IMenuButtonsProvider buttonsProvider)
        {
            _sceneLoader = sceneLoader;
            _settingsMenu = settingsMenu;
            _gameQuitter = gameQuitter;
            _buttonsProvider = buttonsProvider;
        }

        private void Awake()
        {
            _settingsMenu.Hide();

            _buttonsProvider.StartButton.onClick.AddListener(_sceneLoader.LoadGameScene);
            _buttonsProvider.SettingsButton.onClick.AddListener(_settingsMenu.Show);
            _buttonsProvider.QuitButton.onClick.AddListener(_gameQuitter.Quit);
        }
    }
}