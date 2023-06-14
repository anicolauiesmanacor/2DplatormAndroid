using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public Text debugText;

    private PlayerInput playerInput;

    private bool pl_walkingL = false;
    private bool pl_walkingR = false;

    private GameManager gameManager;
    private Rigidbody2D rb;
    private Animator anim;

    public ParticleSystem spawnPS;
    public ParticleSystem explosionPS;

    public Transform floorChecker;
    public Transform respawnPoint;

    public LayerMask groundLayer;
    public LayerMask deathLayer;

    private float horizontal;
    [SerializeField] public float speed;
    [SerializeField] private float jump;
    private bool isFacingRight = true;
    public bool isJumping = false;
    
    public const int ST_IDLE = 0;
    public const int ST_WALK = 1;
    public const int ST_JUMP = 2;
    public const int ST_DIE = 3;
    public int state;
    public int prev_state;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Start() {
        PlayerDeath();
        prev_state = state = ST_IDLE;
        ChangeAnimatorState(0);
    }

    private void Update() {
        if (gameManager.startGame && !gameManager.isGameOver) {
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

            UpdatePlayerAnimationState();
            PlayPlayerFX();
        } else if (gameManager.isGameOver) {
            prev_state = state = ST_IDLE;
            ChangeAnimatorState(0);
            horizontal = 0;
            gameManager.startGame = gameManager.isGameOver = false;
        }
    }

    void PlayPlayerFX() {
        if (prev_state != state) {
            switch (state) {
                case ST_IDLE:
                    SoundManager.Instance.StopFXSound();
                    break;
                case ST_WALK:
                    SoundManager.Instance.PlayWalkingSound();
                    break;
                case ST_JUMP:
                    SoundManager.Instance.PlayJumpSound();
                    break;
                case ST_DIE:
                    SoundManager.Instance.PlayDieSound();
                    break;
            }
        }
    }

    public void PlayerDeath() {
        spawnPS.Play();
        this.gameObject.SetActive(true);
        transform.position = respawnPoint.transform.position;
    }

    private void UpdatePlayerAnimationState() {
        if (prev_state != state)
            prev_state = state;

        if (IsGrounded())
            if (horizontal != 0) {
                state = ST_WALK;
                ChangeAnimatorState(1);
            } else {
                state = ST_IDLE;
                ChangeAnimatorState(0);
            }
        else 
            if (rb.velocity.y > 0f) {
                state = ST_JUMP;
                ChangeAnimatorState(2);
            } else {
                ChangeAnimatorState(3);
            }
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

     public void MovementL (InputAction.CallbackContext context) {
        if (gameManager.startGame) {
            if (context.performed) {
                Debug.Log("MovementL");
                debugText.text = "MovementL";
                pl_walkingL = true;
                horizontal = -1;
            } else if (context.canceled) {
                pl_walkingL = false;
                horizontal = 0;
            }
        }
    }

    public void MovementR (InputAction.CallbackContext context) {
        if (gameManager.startGame) {
            if (context.performed) {
                Debug.Log("MovementR");
                debugText.text = "MovementR";
                pl_walkingR = true;
                horizontal = 1;
            } else if (context.canceled) {
                pl_walkingR = false;
                horizontal = 0;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        if (gameManager.startGame) {
            if (context.performed && IsGrounded()) {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                isJumping = true;
            }

            if (context.canceled && rb.velocity.y > 0f) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                isJumping = false;
            }
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(floorChecker.position, 0.9f, groundLayer);
    }

    private bool IsDead() {
        return Physics2D.OverlapCircle(floorChecker.position, 0.9f, deathLayer);
    }
}
