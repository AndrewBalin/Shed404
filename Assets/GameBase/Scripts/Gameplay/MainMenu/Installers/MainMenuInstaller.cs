using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using Gameplay.MainMenu.Common;
using Gameplay.MainMenu.Controllers;
using Gameplay.MainMenu.SceneFunctions;
using Gameplay.MainMenu.SettingsSound.UI;

namespace Gameplay.MainMenu.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private string _gameSceneName = "GameScene";
        [SerializeField] private AudioMixer _audioMixer;  

        public override void InstallBindings()
        {
            Container.Bind<IMenuButtonsProvider>().To<MenuButtonsProvider>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().WithArguments(_gameSceneName);
            Container.Bind<IGameQuitter>().To<GameQuitter>().AsSingle();
            
            Container.Bind<ISettingsMenu>().To<SettingsMenuController>().FromComponentInHierarchy(includeInactive: true).AsSingle();
            
            Container.Bind<MainMenuController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MenuMusicPlayer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MenuButtonSoundPlayer>().FromComponentInHierarchy().AsSingle();
        }
    }
}