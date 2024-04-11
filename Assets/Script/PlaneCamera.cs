using UnityEngine;

public class PlaneCamera : MonoBehaviour
{
    public Transform plane; // Reference to the plane GameObject

    void LateUpdate()
    {
        if (plane != null)
        {
            // Set the camera's rotation to match the plane's rotation
            transform.rotation = plane.rotation;
        }
    }
}
