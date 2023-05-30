using UnityEngine;

public class MoveWithCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 offset;

    private void Start()
    {
        // Find the main camera in the scene
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraTransform = mainCamera.transform;
            // Calculate the initial offset between the camera and the GameObject
            offset = transform.position - cameraTransform.position;
        }
        else
        {
            Debug.LogWarning("Main camera not found in the scene!");
        }
    }

    private void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // Update the position of the GameObject to match the camera's position
            transform.position = cameraTransform.position + offset;
        }
    }
}