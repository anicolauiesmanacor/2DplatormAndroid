using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{

    private Rigidbody2D rb;
    private Animator anim;

    public ParticleSystem spawnPS;
    public ParticleSystem explosionPS;

    public Transform floorChecker;
    public Transform respawnPoint;

    public LayerMask groundLayer;
    public LayerMask deathLayer;

    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    private bool isFacingRight = true;
    public bool isJumping = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start() {
        ChangeAnimatorState(0);
    }

    private void Update() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f) {
            Flip();
        } else if (isFacingRight && horizontal < 0f) {
            Flip();
        }

        if (IsDead()) {
            explosionPS.transform.position = transform.position;
            this.gameObject.SetActive(false);
            explosionPS.Play();
            Invoke("PlayerDeath", 3);
        }

        UpdateAnimations();
    }

    private void PlayerDeath() {
        spawnPS.Play();
        this.gameObject.SetActive(true);
        transform.position = respawnPoint.transform.position;
    }

    private void UpdateAnimations() {
        if (IsGrounded())
            if (horizontal != 0) {
                SoundManager.Instance.PlayWalkingSound();
                ChangeAnimatorState(1);
                
            } else
                ChangeAnimatorState(0);
        else 
            if (rb.velocity.y > 0f) {
                SoundManager.Instance.PlayJumpSound();
                ChangeAnimatorState(2);
            } else
                ChangeAnimatorState(3);
    }

    private void ChangeAnimatorState(int i) {
        anim.SetBool("idle", false);
        anim.SetBool("walk", false);
        anim.SetBool("jump", false);
        anim.SetBool("fall", false);
        switch (i) {
            case 0:
                anim.SetBool("idle", true);
                break;
            case 1:
                anim.SetBool("walk", true);
                break;
            case 2: 
                anim.SetBool("jump", true);
                break;
            case 3:
                anim.SetBool("fall", true);
                break;
            default:
                break;
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
        //SoundManager.Instance.PlayWalkingSound();
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && IsGrounded()) {
            //SoundManager.Instance.PlayJumpSound();
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJumping = true;
        }

        if (context.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            isJumping = false;
        }   
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(floorChecker.position, 0.9f, groundLayer);
    }

    private bool IsDead() {
        //SoundManager.Instance.PlayDieSound();
        return Physics2D.OverlapCircle(floorChecker.position, 0.9f, deathLayer);
    }
}
