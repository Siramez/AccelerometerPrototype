using UnityEngine;

public class RespawnCheckpoint : MonoBehaviour
{
    // Optional variable to specify whether this checkpoint is the initial spawn point
    public bool isInitialSpawn = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Update the player's respawn position to this checkpoint's location
            UpdatePlayerRespawnPosition(other.gameObject);
        }
    }

    private void UpdatePlayerRespawnPosition(GameObject player)
    {
        // Set the player's respawn position to this checkpoint's location
        PlayerRespawnController respawnController = player.GetComponent<PlayerRespawnController>();
        if (respawnController != null)
        {
            respawnController.SetRespawnPosition(transform.position);
        }
        else
        {
            Debug.LogWarning("PlayerRespawnController script not found on player GameObject. Respawn position not updated.");
        }
    }
}
