using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{

    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    private bool isFacingRight = true;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f) {
            Flip();
        } else if (isFacingRight && horizontal < 0f) {
            Flip();
        }
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context) {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (context.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }   
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.9f, groundLayer);
    }

    private void OnMove(InputValue inputValue) {
        Debug.Log("Moving");
    }

    private void OnJump(InputValue inputValue) {
        Debug.Log("Jump");
    }
}
