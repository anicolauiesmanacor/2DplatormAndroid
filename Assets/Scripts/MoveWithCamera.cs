using UnityEngine;

public class MoveWithCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 offset;
    [SerializeField] private bool moveInX;
    [SerializeField] private bool moveInY;

    private void Start()
    {
        // Find the main camera in the scene
        Camera mainCamera = Camera.main;
        if (mainCamera != null) {
            cameraTransform = mainCamera.transform;
            // Calculate the initial offset between the camera and the GameObject
            offset = transform.position - cameraTransform.position;
        }
    }

    private void LateUpdate() {
        if (cameraTransform != null) {
            // Update the position of the GameObject to match the camera's position
            if (moveInX) {
                float y = transform.position.y;
                transform.position = cameraTransform.position + offset;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }
            if (moveInY) {
                float x = transform.position.x;
                transform.position = cameraTransform.position + offset;
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }
        }
    }
}