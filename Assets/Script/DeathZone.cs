using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Respawn the player
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        // Get the PlayerRespawnController component attached to the player GameObject
        PlayerRespawnController respawnController = player.GetComponent<PlayerRespawnController>();

        if (respawnController != null)
        {
            // Respawn the player at the last checkpoint position
            respawnController.Respawn();
        }
        else
        {
            Debug.LogWarning("PlayerRespawnController script not found on player GameObject. Cannot respawn player.");
        }
    }
}
