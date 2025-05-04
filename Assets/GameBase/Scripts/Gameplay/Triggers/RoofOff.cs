using UnityEngine;

public class RoofOff : MonoBehaviour
{
    public GameObject roof;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            roof.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roof.SetActive(true);
        }
    }
}
