using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private float yOffset = 1.0f;
    [SerializeField] private float xOffset = 1.0f;


    void Update()
    {
        transform.position = new Vector3(player.position.x + xOffset, player.position.y + yOffset, transform.position.z);
    }
}