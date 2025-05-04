using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using Gameplay.MainMenu.SettingsSound.Common;
using Gameplay.MainMenu.SettingsSound.Services;

namespace Gameplay.MainMenu.Installers
{
    [CreateAssetMenu(fileName = "ProjectContextInstaller", menuName = "Zenject/Installers/ProjectContextInstaller")]
    public class ProjectContextInstaller : ScriptableObjectInstaller<ProjectContextInstaller>
    {
        [SerializeField] private AudioMixer _audioMixer;

        public override void InstallBindings()
        {
            Container.Bind<ISettingsStorage>().To<PlayerPrefsSettingsStorage>().AsSingle();
            Container.Bind<IMusicLaunchService>().To<MusicLaunchService>().AsSingle();
            Container.Bind<IAudioService>().To<AudioService>().AsSingle().WithArguments(_audioMixer);
        }
    }
}