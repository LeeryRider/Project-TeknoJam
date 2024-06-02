using UnityEngine;

public class MirrorTeleport : MonoBehaviour
{
    public Transform otherMirrorTransform;
    private bool playerInTrigger = false;
    public KeyCode interactKey = KeyCode.E;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; 
        }
    }


    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(interactKey))
        {
            TeleportPlayerToOtherMirror();
        }
    }

    private void TeleportPlayerToOtherMirror()
    {

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform.position = otherMirrorTransform.position;
    }
}
