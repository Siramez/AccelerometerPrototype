// LeftAileronController.cs
using UnityEngine;

public class LeftAileronController : MonoBehaviour
{
    public float aileronSpeed = 5.0f; // Speed of aileron movement

    private void FixedUpdate()
    {
        // Get input (e.g., accelerometer) to control left aileron
        float input = Input.acceleration.x; // Adjust as needed

        // Rotate left aileron based on input
        Quaternion targetRotation = Quaternion.Euler(input * 45f, 0f, 0f); // Example rotation
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, aileronSpeed * Time.deltaTime);
    }
}
