using UnityEngine;

public class PlayerRespawnController : MonoBehaviour
{
    private Vector3 respawnPosition;

    // Set the player's respawn position
    public void SetRespawnPosition(Vector3 position)
    {
        respawnPosition = position;
    }

    // Respawn the player at the last checkpoint position
    public void Respawn()
    {
        transform.position = respawnPosition;
    }
}
