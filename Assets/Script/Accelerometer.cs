using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    public float forwardSpeed = 1.0f; // Forward movement speed multiplier
    public float backwardSpeed = 0.5f; // Backward movement speed multiplier
    public float jumpForce = 5.0f; // Force applied when jumping
    public float jumpSpeed = 2.0f; // Speed of the jump
    public float shakeThreshold = 2.0f; // Minimum acceleration magnitude to detect a shake
    public AudioClip JumpSound = null;
    public AudioClip CoinSound = null;

    private Rigidbody rigid;
    private AudioSource mAudioSource = null;
    private bool isJumping = false;
    private bool canJump = true; // Flag to track if the object can jump
    private bool deviceTilted = false; // Flag to track if the device has been tilted

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        mAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!deviceTilted) // Check if the device has been tilted
            return; // If not, exit the Update method without applying any movement

        Vector3 rawTilt = Input.acceleration;

        // Reverse the Z component for movement along the forward/backward axis
        float zTilt = -rawTilt.z;

        // Combine X, Y, and Z components for movement
        Vector3 tilt = new Vector3(rawTilt.x, rawTilt.y, zTilt);

        // Scale the tilt vector by the movement speeds
        tilt.x *= forwardSpeed; // Tilt left/right to move sideways
        tilt.y *= forwardSpeed; // Tilt up/down to move sideways
        tilt.z *= zTilt > 0 ? forwardSpeed : backwardSpeed; // Tilt forward/backward to move forward/backward

        // Apply the force to the rigidbody
        rigid.AddForce(tilt);

        // Check for shake gesture if not already jumping and can jump
        if (!isJumping && canJump && IsShaking())
        {
            Jump();
            if (mAudioSource != null && JumpSound != null)
            {
                mAudioSource.PlayOneShot(JumpSound);
            }
        }
    }

    private bool IsShaking()
    {
        // Calculate acceleration magnitude
        float accelerationMagnitude = Input.acceleration.magnitude;

        // If acceleration magnitude exceeds the threshold, consider it a shake
        return accelerationMagnitude > shakeThreshold;
    }

    private void Jump()
    {
        // Set rigidbody's velocity to achieve a jump with controlled speed
        Vector3 currentVelocity = rigid.velocity;
        currentVelocity.y = jumpSpeed;
        rigid.velocity = currentVelocity;

        // Set jumping flag to true
        isJumping = true;
        // Set canJump flag to false to prevent further jumping until landing
        canJump = false;

        // Invoke method to reset jumping flag after a short delay (adjust as needed)
        Invoke("ResetJumpFlag", 0.5f);
    }

    private void ResetJumpFlag()
    {
        isJumping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin"))
        {
            if (mAudioSource != null && CoinSound != null)
            {
                mAudioSource.PlayOneShot(CoinSound);
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object has collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Set canJump flag to true to allow jumping again
            canJump = true;
        }
    }

    // Add this method to detect when the device has been tilted
    private void FixedUpdate()
    {
        if (!deviceTilted && Input.acceleration.magnitude > 0.1f) // Check if the device has been tilted
        {
            deviceTilted = true; // Set the flag to indicate that the device has been tilted
        }
    }
}
