using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTouchController : MonoBehaviour {
    InputAction.CallbackContext context;
    private bool pl_walking = false;
    private bool pl_walkingL = false;
    private bool pl_walkingR = false;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate() {
        if (pl_walking) {
            Vector2 inputVector = context.ReadValue<Vector2>();
            rb.AddForce(new Vector2(inputVector.x, 0) * speed, ForceMode2D.Force);
        } else if (pl_walkingL || pl_walkingR) {
            if (pl_walkingL) {
                rb.AddForce(new Vector2(1, 0) * -speed, ForceMode2D.Force);
            } else {
                rb.AddForce(new Vector2(1, 0) * speed, ForceMode2D.Force);
            }
        }
    }

    public void Movement (InputAction.CallbackContext context) {
        Debug.Log("Movement");
        if (context.performed)
            pl_walking = true;
        else if (context.canceled)
            pl_walking = false;

        this.context = context;
    }

    public void MovementL (InputAction.CallbackContext context) {
        Debug.Log("MovementL");
        if (context.performed)
            pl_walkingL = true;
        else if (context.canceled)
            pl_walkingL = false;
        this.context = context;
    }

    public void MovementR (InputAction.CallbackContext context) {
        Debug.Log("MovementR");
        if (context.performed)
            pl_walkingR = true;
        else if (context.canceled)
            pl_walkingR = false;
        this.context = context;
    }

    public void Jump (InputAction.CallbackContext context) {
        Debug.Log("Jump: " + context.phase);
        if (context.performed) {
            rb.AddForce(Vector3.up * jump, ForceMode2D.Impulse);
        }
    }
}