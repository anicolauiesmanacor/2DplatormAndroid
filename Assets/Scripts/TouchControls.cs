using UnityEngine;
using UnityEngine.InputSystem;

public class TouchControls : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isMoving;
    private PlayerController pController;

    void Start() {
        pController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnTouchStart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pressed");
            touchStartPos = context.ReadValue<Vector2>();
            isMoving = true;
        }
    }

    public void OnTouchMove(InputAction.CallbackContext context)
    {
        if (isMoving && context.performed)
        {
            Debug.Log("drag");
            Vector2 touchCurrentPos = context.ReadValue<Vector2>();
            Vector2 touchDelta = touchCurrentPos - touchStartPos;

            Vector3 movement = new Vector3(touchDelta.x, 0, touchDelta.y) * pController.speed;
            transform.position += movement;
        }
    }

    public void OnTouchEnd(InputAction.CallbackContext context)
    {
        Debug.Log("release");
        if (context.performed)
        {
            isMoving = false;
        }
    }
}