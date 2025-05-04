using System.Collections.Generic;
using UnityEngine.UI;

namespace Gameplay.MainMenu.Common
{
    public interface IMenuButtonsProvider
    {
        public Button StartButton { get; }
        public Button SettingsButton { get; }
        public Button QuitButton { get; }
        public IEnumerable<Button> AllButtons { get; }
    }
}