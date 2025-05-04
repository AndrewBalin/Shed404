using Gameplay.MainMenu.Common;
using UnityEngine.SceneManagement;

namespace Gameplay.MainMenu.SceneFunctions
{
    public class SceneLoader : ISceneLoader
    {
        private readonly string _gameSceneName;

        public SceneLoader(string gameSceneName)
        {
            _gameSceneName = gameSceneName;
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(_gameSceneName);
        }
    }
}