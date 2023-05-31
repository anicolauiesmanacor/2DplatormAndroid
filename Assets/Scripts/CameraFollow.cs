using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;                    // The player's transform
    [SerializeField] private float smoothSpeed; // The smoothing factor for camera movement
    public Vector3 offset;                      // The offset between the camera and the player
    [SerializeField] private float followDelay; // The delay in seconds before the camera starts following
    
    [SerializeField] private float widthScreenLimit;
    [SerializeField] private float heightScreenLimit;

    private Vector3 desiredPosition;        // The desired position of the camera
    private bool shouldFollow = false;      // Flag to indicate if the camera should start following

    private void LateUpdate() {
        if (target == null)
            return;

        // Calculate the desired position of the camera
        desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;

        // Convert the desired position to screen space
        Vector3 desiredScreenPosition = Camera.main.WorldToScreenPoint(desiredPosition);

        // Calculate the screen width and height at XX%
        float screenWidthThreshold = Screen.width * widthScreenLimit;
        float screenHeightThreshold = Screen.height * heightScreenLimit;

        // Check if the player is within the 75% threshold on both axes
        if (Mathf.Abs(desiredScreenPosition.x - Screen.width / 2) > screenWidthThreshold / 2 ||
            Mathf.Abs(desiredScreenPosition.y - Screen.height / 2) > screenHeightThreshold / 2) {
            // Start following after the specified delay
            if (!shouldFollow)
                Invoke("StartFollowing", followDelay);
        }

        // Follow the player's position with some delay
        if (shouldFollow && target.transform.position.y > 0) {
            // Add some delay by smoothly moving the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }

    private void StartFollowing() {
        shouldFollow = true;
    }
}