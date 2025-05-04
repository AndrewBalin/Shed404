using Zenject;
using Gameplay.AI.Common;

namespace Gameplay.AI
{
    public class SceneAiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IWaypointProvider>().To<WaypointProvider>().FromComponentSibling().AsTransient();
            Container.Bind<INpcAnimation>().To<NpcAnimation>().FromComponentSibling().AsTransient();
            Container.Bind<IPatrolBehaviour>().To<NpcPatrolBehaviour>().FromComponentSibling().AsTransient();
            Container.Bind<IWaypointSelector>().To<RandomWaypointSelector>().AsSingle();
        }
    }
}