using UnityEngine;
using Gameplay.MainMenu.Common;

namespace Gameplay.MainMenu.SceneFunctions
{
    public class GameQuitter : IGameQuitter
    {
        public void Quit()
        {
            Application.Quit();
            
            // UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}