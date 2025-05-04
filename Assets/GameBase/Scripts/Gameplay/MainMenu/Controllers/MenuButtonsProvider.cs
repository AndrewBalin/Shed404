using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.MainMenu.Common;

namespace Gameplay.MainMenu.Controllers
{
    public class MenuButtonsProvider : MonoBehaviour, IMenuButtonsProvider
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        public Button StartButton => _startButton;
        public Button SettingsButton => _settingsButton;
        public Button QuitButton => _quitButton;

        public IEnumerable<Button> AllButtons
        {
            get
            {
                yield return _startButton;
                yield return _settingsButton;
                yield return _quitButton;
            }
        }
    }
}