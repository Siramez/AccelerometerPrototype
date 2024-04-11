using UnityEngine;

public class FlyingPlaneController : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Speed of rotating the plane
    public float maxRollAngle = 45.0f; // Maximum roll angle of the plane
    public float maxPitchAngle = 45.0f; // Maximum pitch angle of the plane
    public float maxYawAngle = 45.0f; // Maximum yaw angle of the plane
    public float forwardSpeed = 5.0f; // Forward movement speed
    public float acceleration = 10.0f; // Acceleration multiplier for changing speed
    public float maxSpeed = 10.0f; // Maximum speed of the plane

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get the input from accelerometer
        float horizontalInput = Input.acceleration.x;
        float verticalInput = Input.acceleration.y;

        // Calculate rotation around the Z-axis (roll) and X-axis (pitch)
        float rollRotation = horizontalInput * rotationSpeed * Time.deltaTime;
        float pitchRotation = verticalInput * rotationSpeed * Time.deltaTime;

        // Apply rotation to the plane
        transform.Rotate(Vector3.forward, -rollRotation);
        transform.Rotate(Vector3.right, pitchRotation);

        // Get touch input for yaw control
        float yawInput = 0f;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                // Left half of the screen for left yaw
                yawInput = -1f;
            }
            else
            {
                // Right half of the screen for right yaw
                yawInput = 1f;
            }
        }

        // Calculate rotation around the Y-axis (yaw)
        float yawRotation = yawInput * rotationSpeed * Time.deltaTime;

        // Apply rotation to the plane
        transform.Rotate(Vector3.up, yawRotation);

        // Clamp the roll, pitch, and yaw angles to the specified range
        float currentRollAngle = transform.localRotation.eulerAngles.z;
        float currentPitchAngle = transform.localRotation.eulerAngles.x;
        float currentYawAngle = transform.localRotation.eulerAngles.y;

        currentRollAngle = Mathf.Clamp(currentRollAngle > 180 ? currentRollAngle - 360 : currentRollAngle, -maxRollAngle, maxRollAngle);
        currentPitchAngle = Mathf.Clamp(currentPitchAngle > 180 ? currentPitchAngle - 360 : currentPitchAngle, -maxPitchAngle, maxPitchAngle);
        currentYawAngle = Mathf.Clamp(currentYawAngle > 180 ? currentYawAngle - 360 : currentYawAngle, -maxYawAngle, maxYawAngle);

        // Apply the clamped rotation angles
        transform.localRotation = Quaternion.Euler(currentPitchAngle, currentYawAngle, currentRollAngle);

        // Calculate forward acceleration based on touch input
        float forwardAcceleration = acceleration; // Always apply forward acceleration

        // Apply force to move the plane forward
        rb.AddForce(transform.forward * forwardSpeed * forwardAcceleration, ForceMode.Acceleration);

        // Limit the maximum speed of the plane
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
