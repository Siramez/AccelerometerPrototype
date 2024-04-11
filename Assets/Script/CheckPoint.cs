using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Camera mainCamera;
    public Camera topDownCamera;

    private bool isTopDownView = false; // Flag to track if the camera is in top-down view

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Toggle cameras
            ToggleCameras();
        }
    }

    private void ToggleCameras()
    {
        if (isTopDownView)
        {
            // Switch to main camera
            mainCamera.gameObject.SetActive(true);
            topDownCamera.gameObject.SetActive(false);
        }
        else
        {
            // Switch to top-down camera
            mainCamera.gameObject.SetActive(false);
            topDownCamera.gameObject.SetActive(true);
        }

        // Update camera view flag
        isTopDownView = !isTopDownView;
    }
}
