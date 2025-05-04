using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Gameplay.QuestSystem.Common.Interfaces;
using Gameplay.QuestSystem.Common.SO;
using Gameplay.QuestSystem.Runtime;
using Gameplay.QuestSystem.Triggers;

namespace Gameplay.QuestSystem.Installers
{
    public class QuestsInstaller : MonoInstaller
    {
        [SerializeField] private List<QuestDefinition> _questDefinitions;

        public override void InstallBindings()
        {
            Container.Bind<IEnumerable<QuestDefinition>>().FromInstance(_questDefinitions).AsSingle();
            Container.Bind<IQuestService>().To<QuestService>().AsSingle();
            Container.Bind<IQuestTrigger>().To<QuestAreaTrigger>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesTo<QuestTriggerBinder>().AsSingle();
        }
    }
}