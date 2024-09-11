using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTarget;    // The object the camera will follow (e.g., player)
    public float followOffsetX;       // Horizontal offset from the target
    public static float followSpeed = 2.0f;  // Initial follow speed (static so it can be accessed by other scripts)
    public float speedIncreaseRate = 0.1f; // Rate at which the follow speed increases over time

    // Follow the target horizontally
    public void FollowCameraTargetHorizontally()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.x = cameraTarget.position.x + followOffsetX;

        // Move camera smoothly towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    void Update()
    {
        FollowCameraTargetHorizontally();

        // Gradually increase follow speed
        followSpeed += speedIncreaseRate * Time.deltaTime;
    }
}
