using Movement;
using UnityEngine;

public class RoofOff : MonoBehaviour
{
    public GameObject roof;
    public PlayerController player;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            roof.SetActive(false);
            player.SetActiveCamera("HomeCamera");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roof.SetActive(true);
            player.SetActiveCamera("MainCamera");
        }
    }
}
