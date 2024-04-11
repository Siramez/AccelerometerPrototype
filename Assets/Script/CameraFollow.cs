using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public Vector3 offsetThirdPerson = new Vector3(0f, 5f, -10f); // Offset of the camera from the player in third-person view

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate desired position relative to the player
            Vector3 desiredPosition = player.position + offsetThirdPerson;

            // Update camera position
            transform.position = desiredPosition;

            // Track player's direction
            transform.LookAt(player.position);
        }
    }
}
