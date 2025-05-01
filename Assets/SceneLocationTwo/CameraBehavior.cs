using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject Player;
    //public Transform player;
    //public float smoothSpeed = 5f;
    private Vector3 offset = new Vector3(-5f, 5f, -5f);

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z), 1)  + offset;
    }
}