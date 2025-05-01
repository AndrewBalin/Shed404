using UnityEngine;
using UnityEngine.AI;

namespace Interactive
{
    public class InteractiveUseCase : MonoBehaviour
    {
        // Класс от которого будет наследоваться любая
        // интерактивность в игре с управлением Point&Click
        public void Interact(Interactable interactable, NavMeshAgent agent, string action)
        /*
         * :interactable: объект с которым взаимодействуем
         * :agent: игрок
         */
        {
            // Здесь вы можете добавить логику взаимодействия с объектом
            // игроком, или чем-то другим
        }
    }
}