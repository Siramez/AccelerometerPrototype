using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private Gamepad controller = null;
    private Rigidbody rb;

    // Define movement speed
    public float movementSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Enable gyroscope
        Input.gyro.enabled = true;
    }

    void Update()
    {
        if (controller == null)
        {
            try
            {
                controller = DS4.getConroller(); // Use your DS4 controller retrieval method
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }
        else
        {
            // Get gyroscope rotation
            Quaternion gyroRotation = DS4.getRotation();

            // Map gyroscope rotation to movement directions
            Vector3 moveDirection = Vector3.zero;
            Vector3 forwardDirection = gyroRotation * Vector3.forward;
            moveDirection += forwardDirection * Mathf.Clamp(-forwardDirection.z, 0f, 1f);
            moveDirection += Vector3.Cross(forwardDirection, Vector3.up) * Mathf.Clamp(-forwardDirection.x, 0f, 1f);

            // Apply movement
            rb.MovePosition(rb.position + moveDirection * movementSpeed * Time.deltaTime);
        }
    }
}